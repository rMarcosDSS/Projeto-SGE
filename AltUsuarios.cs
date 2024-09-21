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
    public partial class AltUsuarios : KryptonForm
    {
        public MySqlConnection conn;
        public string merro;
        public string iduser, nome, login, senha, nivel;
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

        public AltUsuarios(string id)
        {
            InitializeComponent();
            iduser = id;
        }

        private void cbNome_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void CadUsuarios_Load(object sender, EventArgs e)
        {
            conn = ConectarBanco();

            string sql = "select * from tbuser where (IdUser='" + iduser + "')";
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
                        txtNome.Text = Convert.ToString(resul["nomeuser"]);
                        txtSenha.Text = Convert.ToString(resul["senha"]);
                        txtUser.Text = Convert.ToString(resul["login"]);
                        cbNivel.Text = Convert.ToString(resul["nivel"]);
                        
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

        

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtNome.Text) || String.IsNullOrEmpty(txtSenha.Text) || String.IsNullOrEmpty(txtUser.Text) || String.IsNullOrEmpty(cbNivel.Text))
            {
                MessageBox.Show("Campos Vazio");
                verificarcampos();
                //Limpar_Campos();
            }
            else
            {
                conn = ConectarBanco();
                string sql = "update tbuser set nomeuser='" + txtNome.Text + "', login='" + txtUser.Text + "', senha='" + txtSenha.Text + "', nivel='" + cbNivel.Text + "' where IdUser='" + iduser+ "'";
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

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja mesmo deletar este funcionário?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                conn = ConectarBanco();
                string sql = "delete from tbuser where IdUser='"+iduser+"'";
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

        private void kryptonPanel3_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
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

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]

        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]

        private extern static void SendMessage(System.IntPtr hWmd, int wMsg, int wParam, int Iparam);

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void verificarcampos()
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
    }
}
