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
    public partial class AltClientes : KryptonForm
    {
        public MySqlConnection conn;
        public string merro;
        public string Id;

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
        public AltClientes(string idcliente)
        {
            InitializeComponent();
            txtCnpj.Visible = false;
            txtCpf.Visible = false;

            Id = idcliente;
        }

        private void rbtnCpf_CheckedChanged(object sender, EventArgs e)
        {
            txtCnpj.Visible = false;
            txtCpf.Visible = true;
        }

        private void rbtnCnpj_CheckedChanged(object sender, EventArgs e)
        {
            txtCnpj.Visible = true;
            txtCpf.Visible = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AltClientes_Load(object sender, EventArgs e)
        {
            conn = ConectarBanco();

            string sql = "select * from tbcliente where (IdCliente ='" + Id + "')";
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
                        txtId.Text = Id;
                        txtNome.Text = Convert.ToString(resul["nomecliente"]);
                        string cnpj = Convert.ToString(resul["cnpj"]);
                        string cpf = Convert.ToString(resul["cpf"]);
                        txtStatus.Text = Convert.ToString(resul["statuscliente"]);
                        if(String.IsNullOrEmpty(cnpj))
                        {
                            txtCpf.Text = cpf;
                            rbtnCpf.Checked = true;
                            txtCpf.Visible = true;
                        }
                        else if (String.IsNullOrEmpty(cpf))
                        {
                            txtCnpj.Text = cnpj;
                            rbtnCnpj.Checked = true;
                            txtCnpj.Visible = true;
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

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtNome.Text))
            {
                MessageBox.Show("Campos Vazio");
                verificacampos();

                //Limpar_Campos();
            }
            else if (String.IsNullOrEmpty(txtCpf.Text) && (rbtnCpf.Checked))
            {
                MessageBox.Show("Campos Vazio");
                verificacampos();
            }
            else if (String.IsNullOrEmpty(txtCpf.Text) && (rbtnCnpj.Checked))
            {
                MessageBox.Show("Campos Vazio");
                verificacampos();
            }
            else
            {
                if (rbtnCpf.Checked)
                {
                    conn = ConectarBanco();
                    string sql = "update tbcliente set nomecliente='" + txtNome.Text + "', cpf='" + txtCpf.Text + "' where IdCliente='"+Id+"'";
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
                    conn = ConectarBanco();
                    string sql = "update tbcliente set nomecliente='" + txtNome.Text + "', cnpj='" + txtCnpj.Text + "' where IdCliente='" + Id + "'";
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
                else
                {
                    MessageBox.Show("Campos Vazio");

                    //Limpar_Campos();
                }
            }

        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja mesmo deletar este funcionário?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                if (String.IsNullOrEmpty(txtNome.Text))
                {
                    MessageBox.Show("Campos Vazio");
                    verificacampos();

                    //Limpar_Campos();
                }
                else if (String.IsNullOrEmpty(txtCpf.Text) && (rbtnCpf.Checked))
                {
                    MessageBox.Show("Campos Vazio");
                    verificacampos();
                }
                else if (String.IsNullOrEmpty(txtCpf.Text) && (rbtnCnpj.Checked))
                {
                    MessageBox.Show("Campos Vazio");
                    verificacampos();
                }
                else
                {
                    if (rbtnCpf.Checked || rbtnCnpj.Checked)
                    {
                        string Status = "Desativado";
                        conn = ConectarBanco();
                        string sql = "update tbcliente set statuscliente='" + Status+ "' where IdCliente='" + Id + "'";
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
                    else
                    {
                        MessageBox.Show("Não excluido!");
                    }
                }
            }
            else
            {
                MessageBox.Show("Não excluido!");
            }
        }

        private void kryptonPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            this.Close();
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

        public void verificacampos()
        {

            if (String.IsNullOrEmpty(txtNome.Text))
            {
                lblast2.Visible = true;
            }

            if (rbtnCnpj.Checked)
            {
                if (String.IsNullOrEmpty(txtCnpj.Text))
                {
                    lblast2.Visible = true;
                }
            }
            if (rbtnCpf.Checked)
            {
                if (String.IsNullOrEmpty(txtCpf.Text))
                {
                    lblast2.Visible = true;
                }
            }
        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {
            lblast2.Visible = false;
        }

        private void txtCnpj_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            lblast3.Visible = false;
        }

        private void txtStatus_TextChanged(object sender, EventArgs e)
        {
            lblast4.Visible = false;
        }

        private void txtCpf_TextChanged(object sender, EventArgs e)
        {
            lblast3.Visible = false;
        }
    }
}
