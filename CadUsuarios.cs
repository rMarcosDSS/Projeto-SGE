using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using ComponentFactory.Krypton.Toolkit;
using System.Runtime.InteropServices;

namespace Projeto_SGE_Testes
{
    public partial class CadUsuarios : KryptonForm
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

        public CadUsuarios()
        {
            InitializeComponent();
        }

        private void cbNome_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CadUsuarios_Load(object sender, EventArgs e)
        {

        }

        public void verificacampos()
        {
            if (String.IsNullOrEmpty(txtNome.Text))
            {
                lblast1.Visible = true;
            }
            if (String.IsNullOrEmpty(txtUser.Text))
            {
                lblast2.Visible = true;
            }
            if (String.IsNullOrEmpty(txtSenha.Text))
            {
                lblast3.Visible = true;
            }
            if (cbNivel.Text == "Selecione o Nivel")
            {
                lblast4.Visible = true;
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(txtNome.Text)|| String.IsNullOrEmpty(txtSenha.Text) || String.IsNullOrEmpty(txtUser.Text) || String.IsNullOrEmpty(cbNivel.Text))
            {
                MessageBox.Show("Campos Vazio");
                verificacampos();
                //Limpar_Campos();
            }
            else
            {
                conn = ConectarBanco();
                string sql = "insert into tbuser (nomeuser, login, senha, nivel) values ('"+ txtNome.Text +"','"+ txtUser.Text +"','"+ txtSenha.Text +"','"+ cbNivel.Text +"')";

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

        private void btnCancelar_Click(object sender, EventArgs e)
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

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {
            lblast1.Visible = false;
        }

        private void txtUser_TextChanged(object sender, EventArgs e)
        {
            lblast2.Visible = false;
        }

        private void txtSenha_TextChanged(object sender, EventArgs e)
        {
            lblast3.Visible = false;
        }

        private void cbNivel_TextChanged(object sender, EventArgs e)
        {
            lblast4.Visible = false;
        }
    }
}
