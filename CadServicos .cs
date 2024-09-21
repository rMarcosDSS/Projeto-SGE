using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using MySql.Data.MySqlClient;
using System.Runtime.InteropServices;

namespace Projeto_SGE_Testes
{
    public partial class CadServicos : KryptonForm
    {
        public string strConn;
        public MySqlConnection conn;
        public string merro;
        public string IdObra, IdServ;
        public string IdCliente, IdLstServ, IdMat;
        public int vmmat, totalf;
        public string premv, Idlse, totalfms;

        public MySqlConnection ConectarBanco()
        {
            merro = "falso";

            MySqlConnection conexao1 = new MySqlConnection(Conexao.strConn);

            try
            {
                conexao1.Open();
                return conexao1;
            }
            catch
            {
                MessageBox.Show("Impossível estabelecer a conexão");
                merro = "true";
                return conexao1;
            }
        }

        public CadServicos()
        {
            InitializeComponent();
        }

        private void bdpesquisaobra()
        {

            conn = ConectarBanco();

            string sql = "select * from tbobras where (status = 'Ativo')";

            MySqlCommand comd = new MySqlCommand(sql, conn);
            comd.CommandText = "select * from tbobras where (status = 'Ativo')";
            MySqlDataReader dr = comd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            cbObra.DisplayMember = "nomeobra";
            cbObra.DataSource = dt;
        }

        private void CadServicos_Load(object sender, EventArgs e)
        {
            bdpesquisaobra();
            descobriridserv();
            dtMSServico(IdServ);
        }

        public void dtMSServico(string idmsserv)
        {
            conn = ConectarBanco();
            string sql = "select * from tbmsservico where IdServico='" + idmsserv + "'";

            MySqlDataAdapter comd = new MySqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            comd.Fill(dt);
            dtgmsServ.DataSource = dt;
        }

        public void descobriridserv()
        {
            conn = ConectarBanco();

            string sql2 = "select max(IdServico) from tbservico";

            MySqlCommand comd2 = new MySqlCommand(sql2, conn);
            MySqlDataReader resul = comd2.ExecuteReader();
            if (resul.HasRows)
            {
                while (resul.Read())
                {
                    IdServ = Convert.ToString(resul["max(IdServico)"]);
                    if (String.IsNullOrEmpty(IdServ))
                    {
                        IdServ = "1";
                    }
                    else
                    {
                        IdServ = Convert.ToString(Convert.ToInt16(IdServ) + 1);
                    }
                }
                comd2.Connection.Close();
            }
        }

        private void bdpesquisamaterial()
        {

            conn = ConectarBanco();

            string sql = "select * from tbmateriais";

            MySqlCommand comd = new MySqlCommand(sql, conn);
            comd.CommandText = "select * from tbmateriais";
            MySqlDataReader dr = comd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            txtMat.DisplayMember = "nomematerial";
            txtMat.DataSource = dt;
        }

        private void bdpesquisaservico()
        {

            conn = ConectarBanco();

            string sql = "select * from tblstservico";

            MySqlCommand comd = new MySqlCommand(sql, conn);
            comd.CommandText = "select * from tblstservico";
            MySqlDataReader dr = comd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            txtServ.DisplayMember = "nomeservico";
            txtServ.DataSource = dt;
        }

        private void txtMat_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn = ConectarBanco();

            if (rbtnMaterial.Checked)
            {
                string sql = "select * from tbmateriais where (nomematerial ='" + txtMat.Text + "')";
                MySqlCommand comd = new MySqlCommand(sql, conn);

                if (merro == "true")
                {
                    MessageBox.Show("Erro na conexão com o banco de dados");
                    Application.Exit();
                }
                else
                {
                    MySqlDataReader resul = comd.ExecuteReader();
                    if (resul.HasRows)
                    {
                        while (resul.Read())
                        {
                            IdMat = Convert.ToString(resul["IdMateriais"]);
                            txtPreco.Text = Convert.ToString(resul["precomaterial"]);
                            vmmat = Convert.ToInt32(resul["quant"]);
                        }
                        comd.Connection.Close();

                    }
                    else
                    {
                        MessageBox.Show("Produto não localizado");
                        //Limpar_Campos();
                    }
                }
            }
        }

        private void txtServ_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn = ConectarBanco();
            if (rbtnServico.Checked)
            {
                string sql = "select * from tblstservico where (nomeservico ='" + txtServ.Text + "')";
                MySqlCommand comd = new MySqlCommand(sql, conn);

                if (merro == "true")
                {
                    MessageBox.Show("Erro na conexão com o banco de dados");
                    Application.Exit();
                }
                else
                {
                    MySqlDataReader resul = comd.ExecuteReader();
                    if (resul.HasRows)
                    {
                        while (resul.Read())
                        {
                            IdLstServ = Convert.ToString(resul["IdLstServico"]);
                            txtPreco.Text = Convert.ToString(resul["precoservico"]);
                        }
                        comd.Connection.Close();

                    }
                    else
                    {
                        MessageBox.Show("Produto não localizado");
                        //Limpar_Campos();
                    }
                }
            }
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]

        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]

        private extern static void SendMessage(System.IntPtr hWmd, int wMsg, int wParam, int Iparam);

        private void cbObra_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn = ConectarBanco();

            string sql = "select * from tbobras where (nomeobra ='" + cbObra.Text + "')";
            MySqlCommand comd = new MySqlCommand(sql, conn);

            if (merro == "true")
            {
                MessageBox.Show("Erro na conexão com o banco de dados");
                Application.Exit();
            }
            else
            {
                MySqlDataReader resul = comd.ExecuteReader();
                if (resul.HasRows)
                {
                    while (resul.Read())
                    {
                        IdObra = Convert.ToString(resul["IdObra"]);
                    }
                    comd.Connection.Close();

                }
                else
                {
                    MessageBox.Show("Produto não localizado");
                    //Limpar_Campos();
                }
            }
        }

        public void attvalor()
        {
            conn = ConectarBanco();

            string sql2 = "select sum(precototal) from tbmsservico where IdServico='"+IdServ+"'";

            MySqlCommand comd2 = new MySqlCommand(sql2, conn);
            MySqlDataReader resul = comd2.ExecuteReader();
            if (resul.HasRows)
            {
                while (resul.Read())
                {
                    totalfms = Convert.ToString(resul["sum(precototal)"]);
                    if (String.IsNullOrEmpty(IdServ))
                    {
                        totalfms = "00";
                        txtTotalprod.Text = Convert.ToString(totalfms);
                    }
                    else
                    {
                        txtTotalprod.Text = Convert.ToString(totalfms);
                    }
                }
                comd2.Connection.Close();
            }
        }

        private void txtValorServ_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void txtValorServ_TextChanged(object sender, EventArgs e)
        {
            lblast4.Visible = false;
            if (String.IsNullOrEmpty(txtTotalprod.Text))
            {
                txtTotalfinal.Text = Convert.ToString(0 + Convert.ToInt32(txtValorServ.Text));
            }
            else
            {
                txtTotalfinal.Text = Convert.ToString(Convert.ToInt32(txtTotalprod.Text) + Convert.ToInt32(txtValorServ.Text));
            }
            
        }

        private void rbtnMaterial_CheckedChanged(object sender, EventArgs e)
        {
            bdpesquisamaterial();
            txtMat.Visible = true;
            txtServ.Visible = false;
        }

        private void rbtnServico_CheckedChanged(object sender, EventArgs e)
        {
            bdpesquisaservico();
            txtMat.Visible = false;
            txtServ.Visible = true;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            conn = ConectarBanco();


            if (txtQtd.Value >= 0)
            {
                if (rbtnMaterial.Checked)
                {
                    insertms(IdServ, IdMat, txtMat.Text, Convert.ToString(txtQtd.Value), txtPreco.Text);
                    dtMSServico(IdServ);
                    attvalor();
                }
                else if (rbtnServico.Checked)
                {
                    insertms(IdServ, IdLstServ, txtMat.Text, Convert.ToString(txtQtd.Value), txtPreco.Text);
                    dtMSServico(IdServ);
                    attvalor();
                }

            }

            conn.Close();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtTotalprod_TextChanged(object sender, EventArgs e)
        {
            
        }

        private void dtgmsServ_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Idlse = Convert.ToString(dtgmsServ.Rows[e.RowIndex].Cells[0].Value);

            premv = Convert.ToString(dtgmsServ.Rows[e.RowIndex].Cells[7].Value);
        }

        public void verificacampos()
        {
            if (String.IsNullOrEmpty(cbTurno.Text))
            {
                lblast1.Visible = true;
            }
            if (String.IsNullOrEmpty(cbStatus.Text))
            {
                lblast2.Visible = true;
            }
            if (String.IsNullOrEmpty(txtDetalhes.Text))
            {
                lblast3.Visible = true;
            }
            if (String.IsNullOrEmpty(txtValorServ.Text))
            {
                lblast4.Visible = true;
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(Idlse))
            {
                MessageBox.Show("Campo não selecionado!");
            }
            else
            {
                conn = ConectarBanco();

                if (MessageBox.Show("Deseja mesmo remover este produto?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    string sql = "delete from tbmsservico where IdMSServico='" + Idlse + "'";

                    MySqlCommand comd = new MySqlCommand(sql, conn);

                    if (merro == "true")
                    {
                        MessageBox.Show("Erro na conexão com o banco de dados");
                        Application.Exit();
                    }
                    else
                    {
                        comd.ExecuteNonQuery();
                        comd.Connection.Close();

                        MessageBox.Show("Deletado com Sucesso");
                        conn = ConectarBanco();
                        string sql1 = "update tbmateriais set quant='" + vmmat + "' where IdMateriais='" + IdMat + "'";
                        MySqlCommand comd1 = new MySqlCommand(sql1, conn);

                        if (merro == "true")
                        {
                            MessageBox.Show("Erro na conexão com o banco de dados");
                            Application.Exit();
                        }
                        else
                        {
                            comd1.ExecuteNonQuery();
                            MessageBox.Show("Alterado com Sucesso");
                            comd1.Connection.Close();
                            dtMSServico(IdServ);
                            totalf = totalf + Convert.ToInt32(premv);
                            //txtTotalprod.Text = Convert.ToString(totalf);
                            attvalor();

                        }
                        // Limpar_Campos();
                    }

                }
            }
        }

        private void cbTurno_TextChanged(object sender, EventArgs e)
        {
            lblast1.Visible = false;
        }

        private void cbStatus_TextChanged(object sender, EventArgs e)
        {
            lblast2.Visible = false;
        }

        private void txtDetalhes_TextChanged(object sender, EventArgs e)
        {
            lblast3.Visible = false;
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(cbObra.Text) || String.IsNullOrEmpty(cbTurno.Text))
            {
                MessageBox.Show("Campos Vazio");

                verificacampos();
                //Limpar_Campos();
            }
            else
            {
                string d1, m1, a1;
                d1 = dtDataInicio.Value.Day.ToString();
                m1 = dtDataInicio.Value.Month.ToString();
                a1 = dtDataInicio.Value.Year.ToString();
                string DataInicio = a1 + "-" + m1 + "-" + d1;

                conn = ConectarBanco();
                
                    string sql = "insert into tbservico (IdObra, turno, detalhes, datainicio, precoproduto, precoservico, precototal, status) " +
                    "values ('"+ IdObra +"','"+ cbTurno.Text +"','"+ txtDetalhes.Text +"','"+ DataInicio +"','"+ txtTotalprod.Text +"','"+ txtValorServ.Text +"','"+ txtTotalfinal.Text +"','"+ cbStatus.Text +"')";

                    MySqlCommand comd = new MySqlCommand(sql, conn);

                    if (merro == "true")
                    {
                        MessageBox.Show("Erro na conexão com o banco de dados");
                        Application.Exit();
                    }
                    else
                    {
                        comd.ExecuteNonQuery();
                        comd.Connection.Close();
                        MessageBox.Show("Cadastrado com Sucesso");
                        criarpag();
                        this.Close();
                        // Limpar_Campos();
                }
            }
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void kryptonPanel2_MouseDown(object sender, MouseEventArgs e)
        {
            
                ReleaseCapture();
                SendMessage(this.Handle, 0x112, 0xf012, 0);
            
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(Idlse))
            {
                MessageBox.Show("Campo não selecionado!");
            }
            else
            {
                conn = ConectarBanco();

                if (MessageBox.Show("Deseja mesmo remover este produto?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    string sql = "delete from tbmsservico where IdMSServico='"+ Idlse +"'";

                    MySqlCommand comd = new MySqlCommand(sql, conn);

                    if (merro == "true")
                    {
                        MessageBox.Show("Erro na conexão com o banco de dados");
                        Application.Exit();
                    }
                    else
                    {
                        comd.ExecuteNonQuery();
                        MessageBox.Show("Deletado com Sucesso");
                        int nquant = Convert.ToInt32(vmmat);
                        string sql1 = "update tbmateriais set quant='" + nquant + "' where IdMateriais='" + IdMat + "'";
                        MySqlCommand comd1 = new MySqlCommand(sql1, conn);

                        if (merro == "true")
                        {
                            MessageBox.Show("Erro na conexão com o banco de dados");
                            Application.Exit();
                        }
                        else
                        {
                            comd1.ExecuteNonQuery();
                            MessageBox.Show("Alterado com Sucesso");
                            comd.Connection.Close();
                            dtMSServico(IdServ);
                            totalf = totalf + Convert.ToInt32(premv);
                            //txtTotalprod.Text = Convert.ToString(totalf);
                            attvalor();

                        }
                        // Limpar_Campos();
                    }
                   
                }
            }
        }

        public void insertms(string idserv, string idms, string nprod, string quant, string punit)
        {
            conn = ConectarBanco();

            string ptotal;
            ptotal = Convert.ToString(Convert.ToInt32(quant) * Convert.ToInt32(punit));
            /*totalfms = Convert.ToInt32(ptotal) + totalfms;
            txtTotalprod.Text = Convert.ToString(totalfms);*/

            if(rbtnMaterial.Checked)
            {
                string sql = "insert into tbmsservico (IdServico, IdMaterial, nomeproduto, quantidade, precounitario, precototal) values ('" + idserv + "','" + idms + "','" + nprod + "','" + quant + "','" + punit + "','" + ptotal + "')";

                MySqlCommand comd = new MySqlCommand(sql, conn);

                if (merro == "true")
                {
                    MessageBox.Show("Erro na conexão com o banco de dados");
                    Application.Exit();
                }
                else
                {
                    comd.ExecuteNonQuery();
                    comd.Connection.Close();
                    MessageBox.Show("Cadastrado com Sucesso");
                    attvalor();
                    if (rbtnMaterial.Checked)
                    {
                        conn = ConectarBanco();
                        int nquant = Convert.ToInt32(vmmat) - Convert.ToInt32(quant); 
                        string sql1 = "update tbmateriais set quant='" + nquant + "' where IdMateriais='" + IdMat + "'";
                        MySqlCommand comd1 = new MySqlCommand(sql1, conn);

                        if (merro == "true")
                        {
                            MessageBox.Show("Erro na conexão com o banco de dados");
                            Application.Exit();
                        }
                        else
                        {
                            comd1.ExecuteNonQuery();
                            comd1.Connection.Close();
                            MessageBox.Show("Alterado com Sucesso");
                        }
                    }
                }
                
            }
            else if(rbtnServico.Checked)
            {
                conn = ConectarBanco();
                string sql = "insert into tbmsservico (IdServico, IdLstServico, nomeproduto, quantidade, precounitario, precototal) values ('" + idserv + "','" + idms + "','" + nprod + "','" + quant + "','" + punit + "','" + ptotal + "')";

                MySqlCommand comd = new MySqlCommand(sql, conn);

                if (merro == "true")
                {
                    MessageBox.Show("Erro na conexão com o banco de dados");
                    Application.Exit();
                }
                else
                {
                    comd.ExecuteNonQuery();
                    comd.Connection.Close();
                    MessageBox.Show("Cadastrado com Sucesso");
                    attvalor();
                }
            }
            
        }

        public void criarpag()
        {
            conn = ConectarBanco();
            string tipo = "Receita";
            string descricao = txtDetalhes.Text;
            string valor = txtTotalfinal.Text;
            string status = "Pendente";

            string d1, m1, a1, m2;
            d1 = dtDataInicio.Value.Day.ToString();
            m1 = dtDataInicio.Value.Month.ToString();
            a1 = dtDataInicio.Value.Year.ToString();
            m2 = Convert.ToString(Convert.ToInt16(m1) + 1);
            string vencimento = a1 + "-" + m2 + "-" + d1;
            if (Convert.ToInt16(m1)==13)
            {
                m2 = "01";
                a1 = Convert.ToString(Convert.ToInt16(a1) + 1);
                vencimento = a1 + "-" + m2 + "-" + d1;
            }

            string sql1 = "insert into tbpagamento (IdServ, tipo, descricao, vencimento, valor, status) " +
                    "values ('" + IdServ + "','" + tipo + "','" + descricao + "','" + vencimento + "','" + valor + "','" + status + "')";

            MySqlCommand comd1 = new MySqlCommand(sql1, conn);

            if (merro == "true")
            {
                MessageBox.Show("Erro na conexão com o banco de dados");
                Application.Exit();
            }
            else
            {
                comd1.ExecuteNonQuery();
                comd1.Connection.Close();
                MessageBox.Show("Cadastrado com Sucesso");
                // Limpar_Campos();
            }
        }
    }
}
