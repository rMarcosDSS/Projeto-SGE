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
using ComponentFactory.Krypton.Toolkit;
using MySql.Data.MySqlClient;

namespace Projeto_SGE_Testes
{
    public partial class Login : KryptonForm
    {
        public MySqlConnection conn;
        public string merro;

        public Login()
        {
            InitializeComponent();
        }
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


        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void kryptonPanel1_MouseDown(object sender, MouseEventArgs e)
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
            if(string.IsNullOrEmpty(txtLogin.Text))
            {
                lblast1.Visible = true;
            }
            if (string.IsNullOrEmpty(txtSenha.Text))
            {
                lblast2.Visible = true;
            }
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtLogin.Text) || String.IsNullOrEmpty(txtSenha.Text))
            {
                MessageBox.Show("Campos Vazios!");
                verificacampos();

            }
            else
            {


                conn = ConectarBanco();

                string sql = "Select * From tbuser where (login='" + txtLogin.Text + "' and senha='" + txtSenha.Text + "')";

                MySqlCommand comd = new MySqlCommand(sql, conn);

                if (merro == "true")
                {

                    MessageBox.Show("Desculpe entre em contato com o administrador para poder entrar no sistema");
                    //Close();

                    Application.Exit();
                }
                else
                {
                    MySqlDataReader resul = comd.ExecuteReader();

                    if (resul.HasRows)
                    {

                        puxanivel();
                        MessageBox.Show("Você está liberado para entra no sistema de controle");

                        Menu formmenu = new Menu();
                        formmenu.Show();
                        this.Hide();
                    }
                    else
                    {
                        MessageBox.Show("Login ou Senha errado, liberação bloqueada");
                    }
                }
            }// */
            //Conexao.niveluser = "usuario";
            //Conexao.niveluser = "admin";
            /*Menu formmenu = new Menu();
            formmenu.Show();
            this.Hide();*/
        }

        public void puxanivel()
        {
            conn = ConectarBanco();

            string sql = "Select * From tbuser where (login='" + txtLogin.Text + "' and senha='" + txtSenha.Text + "')";
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
                        Conexao.niveluser = Convert.ToString(resul["nivel"]);

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

        private void txtLogin_TextChanged(object sender, EventArgs e)
        {
            lblast1.Visible = false;
        }

        private void txtSenha_TextChanged(object sender, EventArgs e)
        {
            lblast2.Visible = false;
        }

        private void kryptonPanel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
