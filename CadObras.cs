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
using Correios;

namespace Projeto_SGE_Testes
{
    public partial class CadObras : KryptonForm
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
        public CadObras()
        {
            InitializeComponent();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtNome.Text) || String.IsNullOrEmpty(txtCliente.Text) || String.IsNullOrEmpty(txtCep.Text) ||
                 String.IsNullOrEmpty(txtCid.Text) || String.IsNullOrEmpty(txtBai.Text) || String.IsNullOrEmpty(txtEst.Text) ||
                  String.IsNullOrEmpty(txtLogr.Text) || String.IsNullOrEmpty(txtContato.Text) || String.IsNullOrEmpty(txtNomeResp.Text))
            {
                MessageBox.Show("Campos Vazio");

                //Limpar_Campos();
                verificacampos();
            }
            else
            {
                string status = "Em progresso";
                string statusf = "Ativo";
                conn = ConectarBanco();
                string sql = "insert into tbobras (nomeobra, cliente, nomeresponsavel, contato, statusobra, cep, cidade, bairro, estado, logradouro, numero, status) values ('" + txtNome.Text + "', '" + txtCliente.Text + "', '"+ txtNomeResp.Text + "', '"+ txtContato.Text + "', '"+ status + "', '" + txtCep.Text + "' , '" + txtCid.Text + "' , '" + txtBai.Text + "' , '" + txtEst.Text + "' , '" + txtLogr.Text + "' , '" + txtNum.Text + "' , '" + statusf + "' )";

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

        private void txtCep_TextChanged(object sender, EventArgs e)
        {
            lblast5.Visible = false;
            if (txtCep.Text.Length == 8)
            {
                try
                {
                    CorreiosApi correiosApi = new CorreiosApi();
                    var retorno = correiosApi.consultaCEP(txtCep.Text);

                    txtCid.Text = retorno.cidade;
                    txtBai.Text = retorno.bairro;
                    txtEst.Text = retorno.uf;
                    txtLogr.Text = retorno.end;
                }
                catch { }
            }
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void CadObras_Load(object sender, EventArgs e)
        {

        }

        public void verificacampos()
        {
            if (String.IsNullOrEmpty(txtNome.Text))
            {
                lblast1.Visible = true;
            }
            if (String.IsNullOrEmpty(txtCliente.Text))
            {
                lblast2.Visible = true;
            }
            if (String.IsNullOrEmpty(txtNomeResp.Text))
            {
                lblast3.Visible = true;
            }
            if (String.IsNullOrEmpty(txtContato.Text))
            {
                lblast4.Visible = true;
            }
            if (String.IsNullOrEmpty(txtCep.Text))
            {
                lblast5.Visible = true;
            }
            if (String.IsNullOrEmpty(txtNum.Text))
            {
                lblast6.Visible = true;
            }
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

        private void txtCliente_TextChanged(object sender, EventArgs e)
        {
            lblast2.Visible = false;
        }

        private void txtNomeResp_TextChanged(object sender, EventArgs e)
        {
            lblast3.Visible = false;
        }

        private void txtContato_TextChanged(object sender, EventArgs e)
        {
            lblast4.Visible = false;
        }

        private void txtNum_TextChanged(object sender, EventArgs e)
        {
            lblast6.Visible = false;
        }
    }
}
