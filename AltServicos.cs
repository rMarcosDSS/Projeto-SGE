using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using ComponentFactory.Krypton.Toolkit;
using MySql.Data.MySqlClient;
using MySql.Data.MySqlClient.Memcached;

namespace Projeto_SGE_Testes
{
    public partial class AltServicos : KryptonForm
    {
        public MySqlConnection conn;
        public string merro;
        public string idserv, idobra;
        public string IdCliente, IdLstServ, IdMat;
        public int vmmat, qtdexc, totalf;
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

        public AltServicos(string IdServ)
        {
            InitializeComponent();
            idserv = IdServ;
        }

        private void bdpesqserv()
        {

            conn = ConectarBanco();

            string sql = "select * from tbservico where IdServico='" + idserv + "'";

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
                        cbObra.Text = Convert.ToString(resul["IdObra"]);
                        cbTurno.Text = Convert.ToString(resul["turno"]);
                        txtDetalhes.Text = Convert.ToString(resul["detalhes"]);
                        cbStatus.Text = Convert.ToString(resul["status"]);
                        dtDataInicio.Value = Convert.ToDateTime(resul["datainicio"]);
                        txtTotalfinal.Text = Convert.ToString(resul["precototal"]);
                        txtTotalprod.Text = Convert.ToString(resul["precoproduto"]);
                        txtValorServ.Text = Convert.ToString(resul["precoservico"]);
                        if (cbStatus.Text == "Finalizado")
                        {
                            rbtnSim.Checked = true;
                            dtDataFinal.Enabled = true;
                            dtDataFinal.Value = Convert.ToDateTime(resul["datafinal"]);
                        }
                        else
                        {
                            rbtnNao.Checked = true;
                            dtDataFinal.Enabled = false;
                        }
                    }
                    comd.Connection.Close();
                }
                else
                {
                    MessageBox.Show("Serviço não localizado");
                    //Limpar_Campos();
                }
            }
        }

        private void AltServicos_Load(object sender, EventArgs e)
        {
            bdpesqserv();
            bdpesquisaobra();
            dtMSServico(idserv);
            attvalor();
            
            if (cbStatus.Text == "Finalizado")
            {
                rbtnSim.Checked = true;
                rbtnNao.Checked = false;
                dtDataFinal.Enabled = true;
            }
            else
            {
                dtDataFinal.Enabled = false;
            }
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

        private void bdpesquisaobra()
        {

            conn = ConectarBanco();

            string sql = "select * from tbobras";

            MySqlCommand comd = new MySqlCommand(sql, conn);
            comd.CommandText = "select * from tbobras";
            MySqlDataReader dr = comd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            cbObra.DisplayMember = "nomeobra";
            cbObra.DataSource = dt;
        }

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
                        idobra = Convert.ToString(resul["IdObra"]);
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

        private void rbtnNao_CheckedChanged(object sender, EventArgs e)
        {
            dtDataFinal.Enabled = false;
        }

        private void rbtnSim_CheckedChanged(object sender, EventArgs e)
        {
            dtDataFinal.Enabled = true;
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            conn = ConectarBanco();

            attvalor();

            string d1, m1, a1;
            d1 = dtDataInicio.Value.Day.ToString();
            m1 = dtDataInicio.Value.Month.ToString();
            a1 = dtDataInicio.Value.Year.ToString();
            string DataInicio = a1 + "-" + m1 + "-" + d1;
            string DataFinal;

            if(String.IsNullOrEmpty(cbObra.Text) || String.IsNullOrEmpty(cbTurno.Text) || String.IsNullOrEmpty(cbStatus.Text) || String.IsNullOrEmpty(txtDetalhes.Text))
            {
                MessageBox.Show("Campos Vazio");

                //Limpar_Campos();
            }
            else
            {
                if(rbtnNao.Checked)
                {
                    conn = ConectarBanco();
                    string sql = "update tbservico set IdObra='" + idobra + "', turno='" + cbTurno.Text + "', datainicio='" + DataInicio + "', precoproduto='" + txtTotalprod.Text + "', precoservico='"+ txtValorServ.Text + "', precototal='"+ txtTotalfinal.Text +"', status='" + cbStatus.Text+"' where IdServico='" + idserv+"'";

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
                        MessageBox.Show("Alterado com Sucesso");
                        conn = ConectarBanco();
                        string sql2 = "update tbpagamento set valor='" + txtTotalfinal.Text + "' where IdServ='" + idserv + "'";
                        MySqlCommand comd2 = new MySqlCommand(sql2, conn);

                        if (merro == "true")
                        {
                            MessageBox.Show("Erro na conexão com o banco de dados");
                            Application.Exit();
                        }
                        else
                        {
                            comd2.ExecuteNonQuery();
                            comd2.Connection.Close();
                            MessageBox.Show("Alterado com Sucesso");
                            this.Close();
                        }
                        // Limpar_Campos();
                    }
                }
                else if(rbtnSim.Checked)
                {
                    conn = ConectarBanco();
                    string d2, m2, a2;
                    d2 = dtDataFinal.Value.Day.ToString();
                    m2 = dtDataFinal.Value.Month.ToString();
                    a2 = dtDataFinal.Value.Year.ToString();
                    DataFinal = a2 + "-" + m2 + "-" + d2;
                    string sql1 = "update tbservico set IdObra='"+ idobra +"', turno='"+ cbTurno.Text +"', datainicio='"+ DataInicio +"', datafinal='"+DataFinal+ "', precoproduto='" + txtTotalprod.Text + "', precoservico='" + txtValorServ.Text + "', precototal='" + txtTotalfinal.Text + "', status='" + cbStatus.Text + "' where IdServico='" + idserv + "'";

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
                        conn = ConectarBanco();
                        string sql2 = "update tbpagamento set valor='"+txtTotalfinal.Text+"' where IdServ='"+idserv+"'";
                        MySqlCommand comd2 = new MySqlCommand(sql2, conn);

                        if (merro == "true")
                        {
                            MessageBox.Show("Erro na conexão com o banco de dados");
                            Application.Exit();
                        }
                        else
                        {
                            comd2.ExecuteNonQuery();
                            comd2.Connection.Close();
                            MessageBox.Show("Alterado com Sucesso");
                            this.Close();
                        }
                        // Limpar_Campos();
                    }
                }

            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            conn = ConectarBanco();
            string status = "Oculto";
            if (MessageBox.Show("Deseja mesmo deletar este produto?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                string sql = "update tbservico set status='" + status + "' where IdServico='"+ idserv +"'";

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
                    this.Close();
                    // Limpar_Campos();
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

        private void btnAdd_Click(object sender, EventArgs e)
        {
            conn = ConectarBanco();


            if (txtQtd.Value >= 0)
            {
                if (rbtnMaterial.Checked)
                {
                    insertms(idserv, IdMat, txtMat.Text, Convert.ToString(txtQtd.Value), txtPreco.Text);
                    dtMSServico(idserv);
                    attvalor();
                }
                else if (rbtnServico.Checked)
                {
                    insertms(idserv, IdLstServ, txtMat.Text, Convert.ToString(txtQtd.Value), txtPreco.Text);
                    dtMSServico(idserv);
                    attvalor();
                }

            }

            conn.Close();
        }

        public void insertms(string idserv, string idms, string nprod, string quant, string punit)
        {
            conn = ConectarBanco();

            string ptotal;
            ptotal = Convert.ToString(Convert.ToInt32(quant) * Convert.ToInt32(punit));
            /*totalfms = Convert.ToInt32(ptotal) + totalfms;
            txtTotalprod.Text = Convert.ToString(totalfms);*/

            if (rbtnMaterial.Checked)
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
            else if (rbtnServico.Checked)
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

        public void attvalor()
        {
            conn = ConectarBanco();

            string sql2 = "select sum(precototal) from tbmsservico where IdServico='" + idserv + "'";

            MySqlCommand comd2 = new MySqlCommand(sql2, conn);
            MySqlDataReader resul = comd2.ExecuteReader();
            if (resul.HasRows)
            {
                while (resul.Read())
                {
                    totalfms = Convert.ToString(resul["sum(precototal)"]);
                    if (String.IsNullOrEmpty(totalfms))
                    {
                        totalfms = "00";
                        txtTotalprod.Text = Convert.ToString(totalfms);

                    }
                    else
                    {
                        txtTotalprod.Text = Convert.ToString(totalfms);
                        if (String.IsNullOrEmpty(txtValorServ.Text))
                        {
                            txtTotalfinal.Text = txtTotalprod.Text;
                        }
                        else
                        {
                            int total = Convert.ToInt32(txtTotalprod.Text) + Convert.ToInt32(txtValorServ.Text);
                            txtTotalfinal.Text = Convert.ToString(total);
                        }
                    }
                }
                comd2.Connection.Close();
            }
        }

        private void btnRemover_Click(object sender, EventArgs e)
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
                        string sql1 = "update tbmateriais set quant='"+ vmmat +"' where IdMateriais='"+ IdMat +"'";
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
                            dtMSServico(idserv);
                            totalf = totalf + Convert.ToInt32(premv);
                            //txtTotalprod.Text = Convert.ToString(totalf);
                            attvalor();

                        }
                        // Limpar_Campos();
                    }

                }
            }
        }

        private void dtgmsServ_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            Idlse = Convert.ToString(dtgmsServ.Rows[e.RowIndex].Cells[0].Value);

            qtdexc = Convert.ToInt32(dtgmsServ.Rows[e.RowIndex].Cells[5].Value); ;

            premv = Convert.ToString(dtgmsServ.Rows[e.RowIndex].Cells[7].Value);
        }

        private void rbtnMaterial_CheckedChanged_1(object sender, EventArgs e)
        {
            bdpesquisamaterial();
            txtMat.Visible = true;
            txtServ.Visible = false;
        }

        private void rbtnServico_CheckedChanged_1(object sender, EventArgs e)
        {
            bdpesquisaservico();
            txtMat.Visible = false;
            txtServ.Visible = true;
        }

        private void txtTotalprod_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            attvalor();
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

        private void SelectStatus(object sender, EventArgs e)
        {
            if(cbStatus.Text == "Finalizado")
            {
                rbtnSim.Checked = true;
                rbtnNao.Checked = false;
                dtDataFinal.Enabled = true;
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

        private void txtQtd_ValueChanged(object sender, EventArgs e)
        {

        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]

        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]

        private extern static void SendMessage(System.IntPtr hWmd, int wMsg, int wParam, int Iparam);

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
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
    }
}
