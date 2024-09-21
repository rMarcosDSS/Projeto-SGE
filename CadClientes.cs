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
using System.Runtime.InteropServices;
using MySql.Data.MySqlClient;

namespace Projeto_SGE_Testes
{
    public partial class CadClientes : KryptonForm
    {
        public MySqlConnection conn;
        public string merro;

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
        public CadClientes()
        {
            InitializeComponent();
            txtCnpj.Visible = false;
            txtCpf.Visible = false;

        }

        private void rbtnCpf_CheckedChanged(object sender, EventArgs e)
        {
            txtCnpj.Visible = false;
            txtCpf.Visible = true;
            lblast2.Visible = false;
        }

        private void rbtnCnpj_CheckedChanged(object sender, EventArgs e)
        {
            txtCnpj.Visible = true;
            txtCpf.Visible = false;
            lblast2.Visible = false;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public static class ValidaCPF
        {
            public static bool IsCpf(string cpf)
            {

                int[] multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                int[] multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };
                string tempCpf;
                string digito;
                int soma;
                int resto;
                cpf = cpf.Trim();
                cpf = cpf.Replace(".", "").Replace("-", "");
                if (cpf.Length != 11)
                    return false;
                tempCpf = cpf.Substring(0, 9);
                soma = 0;

                for (int i = 0; i < 9; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];
                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = resto.ToString();
                tempCpf = tempCpf + digito;
                soma = 0;
                for (int i = 0; i < 10; i++)
                    soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];
                resto = soma % 11;
                if (resto < 2)
                    resto = 0;
                else
                    resto = 11 - resto;
                digito = digito + resto.ToString();
                return cpf.EndsWith(digito);


            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
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
            else if (String.IsNullOrEmpty(txtCnpj.Text) && (rbtnCnpj.Checked))
            {
                MessageBox.Show("Campos Vazio");
                verificacampos();
            }
            else
            {
                
                
                if (rbtnCpf.Checked)
                {
                    if (CadFuncionario.ValidaCPF.IsCpf(txtCpf.Text))
                    {
                        if (
                            txtCpf.Text == "00000000000" || txtCpf.Text == "11111111111" || txtCpf.Text == "22222222222" ||
                            txtCpf.Text == "33333333333" || txtCpf.Text == "44444444444" || txtCpf.Text == "55555555555" ||
                            txtCpf.Text == "66666666666" || txtCpf.Text == "77777777777" || txtCpf.Text == "88888888888" ||
                            txtCpf.Text == "99999999999"
                            )
                        {
                            MessageBox.Show("O número é um CPF Inválido !");
                        }
                        else
                        {
                            string status = "Ativo";
                            conn = ConectarBanco();
                            string sql = "insert into tbcliente (nomecliente, cpf ,statuscliente) values ('" + txtNome.Text + "' , '" + txtCpf.Text + "' , '" + status + "' )";
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
                                this.Close();
                                //Limpar_Campos();

                            }
                        
                        }

                    }
                    else
                    {
                        MessageBox.Show("O número é um CPF Inválido !");
                    }
                }
                else if (rbtnCnpj.Checked)
                {
                    string status = "Ativo";
                    conn = ConectarBanco();
                    string sql = "insert into tbcliente (nomecliente, cnpj, statuscliente) values ('" + txtNome.Text + "' , '" + txtCnpj.Text + "' , '" + status + "' )";
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

        public void verificacampos()
        {
            if (String.IsNullOrEmpty(txtNome.Text))
            {
                lblast1.Visible = true;
            }
            if(rbtnCnpj.Checked)
            {
                if(String.IsNullOrEmpty(txtCnpj.Text))
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

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CadClientes_Load(object sender, EventArgs e)
        {

        }

        private void kryptonPanel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }


        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]

        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]

        private extern static void SendMessage(System.IntPtr hWmd, int wMsg, int wParam, int Iparam);

        private void txtNome_TextChanged(object sender, EventArgs e)
        {
            lblast1.Visible = false;
        }

        private void txtCnpj_TextChanged(object sender, EventArgs e)
        {
            lblast2.Visible = false;
        }

        private void txtCpf_TextChanged(object sender, EventArgs e)
        {
            lblast2.Visible = false;
        }

        private void txtCnpj_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}
