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
    public partial class AltObras : KryptonForm
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
        public AltObras(string idobras)
        {
            InitializeComponent();
            Id = idobras;
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AltObras_Load(object sender, EventArgs e)
        {
            conn = ConectarBanco();

            string sql = "select * from tbobras where (IdObra ='" + Id + "')";
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
                        txtNome.Text = Convert.ToString(resul["nomeobra"]);
                        txtCliente.Text = Convert.ToString(resul["cliente"]);
                        txtNomeResp.Text = Convert.ToString(resul["nomeresponsavel"]);
                        txtContato.Text = Convert.ToString(resul["contato"]);
                        txtStatus.Text = Convert.ToString(resul["statusobra"]);
                        txtCep.Text = Convert.ToString(resul["cep"]);
                        txtCid.Text = Convert.ToString(resul["cidade"]);
                        txtBai.Text = Convert.ToString(resul["bairro"]);
                        txtEst.Text = Convert.ToString(resul["estado"]);
                        txtLogr.Text = Convert.ToString(resul["logradouro"]);
                        txtNum.Text = Convert.ToString(resul["numero"]);

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

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja mesmo deletar esta obra?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                string Status = "Desativado";
                conn = ConectarBanco();
                string sql = "update tbobras set status='" + Status + "' where IdObra='" + Id + "'";
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

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            if(String.IsNullOrEmpty(txtNome.Text) || String.IsNullOrEmpty(txtCliente.Text) || String.IsNullOrEmpty(txtNomeResp.Text) || String.IsNullOrEmpty(txtContato.Text) || String.IsNullOrEmpty(txtCep.Text) || String.IsNullOrEmpty(txtEst.Text) || String.IsNullOrEmpty(txtCid.Text) || String.IsNullOrEmpty(txtLogr.Text) || String.IsNullOrEmpty(txtBai.Text) || String.IsNullOrEmpty(txtNum.Text))
            {
                MessageBox.Show("Campos Vazio");
                verificacampos();
                //Limpar_Campos();
            }
            else
            {
                conn = ConectarBanco();
                string sql = "update tbobras set nomeobra='" + txtNome.Text + "', cliente='" + txtCliente.Text + "', nomeresponsavel='" + txtNomeResp.Text + "', contato='" + txtContato.Text + "', statusobra='" + txtStatus.Text + "', cep='" + txtCep.Text + "', cidade='" + txtCid.Text + "', bairro='" + txtBai.Text+ "', estado='" + txtEst.Text + "', logradouro='" + txtLogr.Text + "', numero='" + txtNum.Text + "', status='Ativo' where IdObra='" + Id + "'";
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

        private void kryptonPanel2_MouseDown(object sender, MouseEventArgs e)
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

        private void txtCep_TextChanged(object sender, EventArgs e)
        {
            lblast5.Visible = false;
            if (txtCep.Text.Length == 9)
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
    }
}
