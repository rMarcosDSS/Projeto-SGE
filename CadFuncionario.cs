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
    public partial class CadFuncionario : KryptonForm
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
        public CadFuncionario()
        {
            InitializeComponent();
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
            if (String.IsNullOrEmpty(txtNome.Text) || String.IsNullOrEmpty(txtCpf.Text) || String.IsNullOrEmpty(txtSobrenome.Text) || String.IsNullOrEmpty(txtEmail.Text) || String.IsNullOrEmpty(txtContato.Text) || String.IsNullOrEmpty(txtRg.Text) || String.IsNullOrEmpty(txtSalario.Text))
            {
                MessageBox.Show("Campos Vazio");
                verificacampos();

                //Limpar_Campos();
            }
            else
            {
                string d1, m1, a1;
                d1 = dtDataNasc.Value.Day.ToString();
                m1 = dtDataNasc.Value.Month.ToString();
                a1 = dtDataNasc.Value.Year.ToString();
                string DataNascf = a1+"-"+m1+"-"+d1;
                string d2, m2, a2;
                d2 = dtDataAdmis.Value.Day.ToString();
                m2 = dtDataAdmis.Value.Month.ToString();
                a2 = dtDataAdmis.Value.Year.ToString();
                string DataAdmisf = a2 + "-" + m2 + "-" + d2;
                string status = "Ativo";


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
                        conn = ConectarBanco();
                        string sql = "insert into tbfuncionario (nomefunc, sobrenomefunc, rg, cpf, contato, email, datanascimento, dataadmissao, cargo, salario, prazosalario, statusfunc) " +
                            "values ('" + txtNome.Text + "','" + txtSobrenome.Text + "','" + txtRg.Text + "','" + txtCpf.Text + "','" + txtContato.Text + "','" + txtEmail.Text
                            + "','" + DataNascf + "','" + DataAdmisf + "','" + cbCargo.Text + "','" + txtSalario.Text + "','" + cbPrazoSal.Text + "','" + status + "')";

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
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CadFuncionario_Load(object sender, EventArgs e)
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

        public void verificacampos()
        {
            if (String.IsNullOrEmpty(txtNome.Text))
            {
                lblast1.Visible = true;
            }
            if (String.IsNullOrEmpty(txtSobrenome.Text))
            {
                lblast2.Visible = true;
            }
            if (String.IsNullOrEmpty(txtRg.Text))
            {
                lblast3.Visible = true;
            }
            if (String.IsNullOrEmpty(txtContato.Text))
            {
                lblast4.Visible = true;
            }
            if (String.IsNullOrEmpty(txtEmail.Text))
            {
                lblast5.Visible = true;
            }
            if (String.IsNullOrEmpty(txtCpf.Text))
            {
                lblast6.Visible = true;
            }
            if (String.IsNullOrEmpty(txtSalario.Text))
            {
                lblast7.Visible = true;
            }
            if (String.IsNullOrEmpty(cbCargo.Text))
            {
                lblast8.Visible = true;
            }
            if (String.IsNullOrEmpty(cbPrazoSal.Text))
            {
                lblast9.Visible = true;
            }
        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {
            lblast1.Visible = false;
        }

        private void txtSobrenome_TextChanged(object sender, EventArgs e)
        {
            lblast2.Visible = false;
        }

        private void txtRg_TextChanged(object sender, EventArgs e)
        {
            lblast3.Visible = false;
        }

        private void txtContato_TextChanged(object sender, EventArgs e)
        {
            lblast4.Visible = false;
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            lblast5.Visible = false;
        }

        private void txtCpf_TextChanged(object sender, EventArgs e)
        {
            lblast6.Visible = false;
        }

        private void txtSalario_TextChanged(object sender, EventArgs e)
        {
            lblast7.Visible = false;
        }

        private void cbCargo_TextChanged(object sender, EventArgs e)
        {
            lblast8.Visible = false;
        }

        private void cbPrazoSal_TextChanged(object sender, EventArgs e)
        {
            lblast9.Visible = false;
        }
    }

}