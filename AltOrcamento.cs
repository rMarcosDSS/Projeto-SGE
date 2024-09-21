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
using System.Runtime.CompilerServices;
using System.Net.NetworkInformation;
using System.Security.Cryptography.X509Certificates;

namespace Projeto_SGE_Testes
{
    public partial class AltOrcamento : KryptonForm
    {
        public MySqlConnection conn;
        public string merro;
        public int totalf = 0;
        public string IdCliente, IdServ, IdMat;
        public string IdOrca = "";
        public string IdMSO, precototal;
        public string nomecliente, cpf, cnpj;

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

        public AltOrcamento(String IdOrcamento)
        {
            InitializeComponent();

            IdOrca = IdOrcamento;
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

        private void bdpesquisacliente()
        {

            conn = ConectarBanco();

            string sql = "select * from tbcliente";

            MySqlCommand comd = new MySqlCommand(sql, conn);
            comd.CommandText = "select * from tbcliente";
            MySqlDataReader dr = comd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            txtNome.DisplayMember = "nomecliente";
            txtNome.DataSource = dt;
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtNome_SelectedIndexChanged(object sender, EventArgs e)
        {
            string cpf, cnpj;
            conn = ConectarBanco();

            string sql = "select * from tbcliente where (nomecliente ='" + txtNome.Text + "')";
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
                        IdCliente = Convert.ToString(resul["IdCliente"]);
                        cpf = Convert.ToString(resul["cpf"]);
                        cnpj = Convert.ToString(resul["cnpj"]);

                        if(String.IsNullOrEmpty(cpf))
                        {
                            rbtnCnpj.Enabled = true;
                            rbtnCpf.Enabled = false;
                            txtCnpj.Enabled = false;
                            txtCpf.Enabled = false;
                            txtCnpj.Visible = true;
                            txtCpf.Visible = false;
                            rbtnCnpj.Checked = true;
                            txtCnpj.Text = cnpj;
                        }
                        else if(String.IsNullOrEmpty(cnpj))
                        {
                            rbtnCnpj.Enabled = false;
                            rbtnCpf.Enabled = true;
                            txtCnpj.Enabled = false;
                            txtCpf.Enabled = false;
                            txtCnpj.Visible = false;
                            txtCpf.Visible = true;
                            rbtnCpf.Checked = true;
                            txtCpf.Text = cpf;
                        }
                        else
                        {
                            MessageBox.Show("Cliente não possuí cadastro");
                            rbtnNao.Checked = true;
                        }
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

        private void CadOrcamentos_Load(object sender, EventArgs e)
        {
            dtMSOrcamento(IdOrca);
            rbtnCnpj.Enabled = false;
            rbtnCpf.Enabled = false;
            txtCnpj.Enabled = false;
            txtCpf.Enabled = false;
            txtCnpj.Visible = false;
            txtCpf.Visible = false;
            pesqorca();
        }

        public void dtMSOrcamento(string idmsorca)
        {
            conn = ConectarBanco();
            string sql = "select * from tbmsorcamento where IdOrcamento='" + idmsorca + "'";

            MySqlDataAdapter comd = new MySqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            comd.Fill(dt);
            dtgOrca.DataSource = dt;
        }

        public void pesqorca()
        {
            conn = ConectarBanco();

            string sql = "select * from tborcamento where IdOrcamento='"+ IdOrca +"'";

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
                        IdCliente = Convert.ToString(resul["IdCliente"]);
                        nomecliente = Convert.ToString(resul["nomecliente"]);
                        cpf = Convert.ToString(resul["cpf"]);
                        cnpj = Convert.ToString(resul["cnpj"]);
                        totalf = Convert.ToInt32(Convert.ToString(resul["total"]));

                        txtNome.Text = nomecliente;
                        txtTotal.Text = Convert.ToString(totalf);

                        if(String.IsNullOrEmpty(cpf) && String.IsNullOrEmpty(cnpj))
                        {
                            rbtnNao.Checked = true;
                            rbtnSim.Checked = false;
                            rbtnCnpj.Enabled = false;
                            rbtnCpf.Enabled = false;
                            txtCnpj.Enabled = false;
                            txtCpf.Enabled = false;
                            txtCnpj.Visible = false;
                            txtCpf.Visible = false;
                        }
                        else
                        {
                            rbtnSim.Checked = true;
                            rbtnCnpj.Enabled = true;
                            rbtnCpf.Enabled = true;

                            if (String.IsNullOrEmpty(cpf))
                            {
                                rbtnCnpj.Checked = true;
                                rbtnCpf.Checked = false;
                                txtCnpj.Enabled = true;
                                txtCpf.Enabled = false;
                                txtCnpj.Visible = true;
                                txtCpf.Visible = false;
                                txtCnpj.Text = cnpj;
                            }
                            else if(String.IsNullOrEmpty(cnpj))
                            {
                                rbtnCnpj.Checked = false;
                                rbtnCpf.Checked = true;
                                txtCnpj.Enabled = false;
                                txtCpf.Enabled = true;
                                txtCnpj.Visible = false;
                                txtCpf.Visible = true;
                                txtCpf.Text = cpf;
                            }
                        }
                    }
                    comd.Connection.Close();
                }
            }
        }

        private void rbtnSim_CheckedChanged(object sender, EventArgs e)
        {
            bdpesquisacliente();
            rbtnCnpj.Enabled = true;
            rbtnCpf.Enabled = true;
            
        }

        public void insertms(string idorcam, string nprod, string quant, string punit, string idms)
        {
            conn = ConectarBanco();

            string ptotal;
            ptotal = Convert.ToString(Convert.ToInt32(quant) * Convert.ToInt32(punit));
            totalf = Convert.ToInt32(ptotal) + totalf;
            txtTotal.Text = Convert.ToString(totalf);
            string punitario = "R$ "+punit;
            string ptotalf = "R$ "+ptotal;

            if (rbtnMaterial.Checked)
            {
                string sql = "insert into tbmsorcamento (IdOrcamento, IdMaterial, nomeproduto, quantidade, precounitario, precototal, status) values ('"+ idorcam +"','"+ idms +"','"+ nprod +"','"+ quant +"','"+ punitario +"','"+ ptotalf +"','Ativo')";

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
                }
            }
            else if (rbtnServico.Checked)
            {
                string sql = "insert into tbmsorcamento (IdOrcamento, IdServico, nomeproduto, quantidade, precounitario, precototal, status) values ('" + idorcam +"','"+ idms +"','"+ nprod +"','"+ quant +"','"+ punitario +"','"+ ptotalf +"','Ativo')";

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
                }
            }

            txtQtd.Value = 1;
            txtPreco.Clear();
            txtMat.ResetText();
            txtServ.ResetText();
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            conn = ConectarBanco();
           
            
            if (txtQtd.Value >= 0)
            {
                if(rbtnMaterial.Checked)
                {
                    insertms(IdOrca,txtMat.Text, Convert.ToString(txtQtd.Value), txtPreco.Text, IdMat);
                    dtMSOrcamento(IdOrca);

                }
                else if(rbtnServico.Checked)
                {
                    insertms(IdOrca, txtServ.Text, Convert.ToString(txtQtd.Value), txtPreco.Text, IdServ);
                    dtMSOrcamento(IdOrca);
                }
                
            }

            conn.Close();
            
        }

        private void kryptonPanel3_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }


        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]

        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]

        private extern static void SendMessage(System.IntPtr hWmd, int wMsg, int wParam, int Iparam);

        private void rbtnMaterial_CheckedChanged(object sender, EventArgs e)
        {
            bdpesquisamaterial();
            txtMat.Visible = true;
            txtServ.Visible = false;
        }

        private void rbtnNao_CheckedChanged(object sender, EventArgs e)
        {
            rbtnCnpj.Enabled = false;
            rbtnCpf.Enabled = false;
            txtCnpj.Enabled = false;
            txtCpf.Enabled = false;
            txtCnpj.Visible = false;
            txtCpf.Visible = false;
        }

        private void rbtnCpf_CheckedChanged(object sender, EventArgs e)
        {
            txtCpf.Visible = true;
            txtCnpj.Enabled = false;
            txtCnpj.Visible = false;
        }

        private void rbtnCnpj_CheckedChanged(object sender, EventArgs e)
        {
            txtCnpj.Visible = true;
            txtCpf.Enabled = false;
            txtCpf.Visible = false;
        }

        private void rbtnServico_CheckedChanged(object sender, EventArgs e)
        {
            bdpesquisaservico();
            txtMat.Visible = false;
            txtServ.Visible = true;
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
                            IdServ = Convert.ToString(resul["IdLstServico"]);
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

        private void txtCnpj_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            conn = ConectarBanco();

            if (MessageBox.Show("Deseja mesmo excluir esse orçamento?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                string sql = "delete from tborcamento where IdOrcamento='" + IdOrca + "'";
                string sql1 = "delete from tbmsorcamento where IdOrcamento='" + IdOrca + "'";

                MySqlCommand comd = new MySqlCommand(sql, conn);
                MySqlCommand comd1 = new MySqlCommand(sql1, conn);

                if (merro == "true")
                {
                    MessageBox.Show("Erro na conexão com o banco de dados");
                    Application.Exit();
                }
                else
                {
                    comd.ExecuteNonQuery();
                    comd1.ExecuteNonQuery();
                    comd.Connection.Close();
                    comd1.Connection.Close();
                    MessageBox.Show("Orçamento excluído");
                    this.Close();
                    // Limpar_Campos();
                }
            }
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void dtgOrca_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            IdMSO = Convert.ToString(dtgOrca.Rows[e.RowIndex].Cells[0].Value);

            precototal = Convert.ToString(dtgOrca.Rows[e.RowIndex].Cells[7].Value);

            conn = ConectarBanco();


        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            conn = ConectarBanco();

            if(String.IsNullOrEmpty(txtNome.Text)||String.IsNullOrEmpty(txtTotal.Text))
            {
                MessageBox.Show("Campos Vazio");
            }
            else
            {
                if(rbtnNao.Checked)
                {
                    string sql = "update tborcamento set nomecliente='"+ txtNome.Text +"', total='"+ txtTotal.Text +"' where IdOrcamento='"+ IdOrca +"'";

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
                        this.Close();
                        // Limpar_Campos();
                    }
                }
                if(rbtnSim.Checked)
                {
                    if(rbtnCpf.Checked)
                    {
                        string sql = "update tborcamento set IdCliente='"+IdCliente+"', nomecliente='"+ txtNome.Text +"', cpf='"+ txtCpf.Text+"', total='" + txtTotal.Text + "' where IdOrcamento='" + IdOrca + "'";

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
                            this.Close();
                            // Limpar_Campos();
                        }
                    }
                    else if (rbtnCnpj.Checked)
                    {
                        string sql = "update tborcamento set IdCliente='"+ IdCliente +"', nomecliente='"+ txtNome.Text +"', cnpj='"+ txtCnpj.Text +"', total='"+ txtTotal.Text + "' where IdOrcamento='"+ IdOrca + "'";

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
                            this.Close();
                            // Limpar_Campos();
                        }
                    }
                }
            }
        }

        private void btnRemover_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(IdMSO))
            {
                MessageBox.Show("Campo não selecionado!");
            }
            else
            {
                conn = ConectarBanco();
                if (MessageBox.Show("Deseja mesmo deletar este produto?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                {
                    string sql = "delete from tbmsorcamento where IdMSOrcamento='"+ IdMSO +"'";

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
                        // Limpar_Campos();
                    }
                }
                dtMSOrcamento(IdOrca);
                string[] precototalstring = precototal.Split(' ');
                int redtotal = Convert.ToInt32(precototalstring[1]);
                totalf = totalf - redtotal;
                txtTotal.Text = Convert.ToString(totalf);
            }
        }

        public void limpartudo()
        {
            txtNome.Text = "";
            txtCnpj.Text = "";
            txtCpf.Text = "";
            txtMat.Text = "";
            txtServ.Text = "";
            txtPreco.Text = "";
            txtQtd.Value = 1;
            txtTotal.Text = "";
            rbtnCnpj.Enabled = false;
            rbtnCpf.Enabled = false;
            txtCnpj.Enabled = false;
            txtCpf.Enabled = false;
            txtCnpj.Visible = false;
            txtCpf.Visible = false;
            rbtnNao.Checked = true;
            rbtnSim.Checked = false;
            rbtnCnpj.Checked = false;
            rbtnCpf.Checked = false;
            rbtnMaterial.Checked = false;
            rbtnServico.Checked = false;

        }

        
    }
}
