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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Runtime.InteropServices;

namespace Projeto_SGE_Testes
{
    public partial class CadMateriais : KryptonForm
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
        public CadMateriais()
        {
            InitializeComponent();
        }

        public void verificacampos()
        {
            if (string.IsNullOrEmpty(txtNome.Text))
            {
                lblast1.Visible = true;
            }
            if (string.IsNullOrEmpty(txtPreco.Text))
            {
                lblast2.Visible = true;
            }
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            int quant = Convert.ToInt32(txtQtd.Value);
            string status = "Ativo";
            if (String.IsNullOrEmpty(txtNome.Text) || String.IsNullOrEmpty(txtPreco.Text) && (quant > 0))
            {
                MessageBox.Show("Campos Vazio");
                verificacampos();

                //Limpar_Campos();
            }
            else
            {
                if(rbtnMaterial.Checked)
                {
                    conn = ConectarBanco();

                    string sql = "insert into tbmateriais (nomematerial, precomaterial, quant, status) values ('"+ txtNome.Text +"' , '"+ txtPreco.Text +"' , '"+ quant +"' , '"+ status +"' )";

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
                else if (rbtnServico.Checked)
                {
                    conn = ConectarBanco();

                    string sql = "insert into tblstservico (nomeservico, precoservico, status) values ('"+ txtNome.Text +"' , '"+ txtPreco.Text +"' , '"+ status+ "' )";

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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CadMateriais_Load(object sender, EventArgs e)
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

        private void txtPreco_TextChanged(object sender, EventArgs e)
        {
            lblast2.Visible = false;
        }
    }
}
