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

namespace Projeto_SGE_Testes
{
    public partial class ConsObras : KryptonForm
    {
        public string strConn;
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
        public ConsObras()
        {
            InitializeComponent();

        }

        private void bdpesquisaobra()
        {

            conn = ConectarBanco();

            string sql = "select * from tbobras where (status = 'Ativo')";

            MySqlCommand comd = new MySqlCommand(sql, conn);
            comd.CommandText = "select * from tbobras where (status = 'Ativo')";
            MySqlDataReader dr = comd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            cbNome.DisplayMember = "nomeobra";
            cbNome.DataSource = dt;
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            CadObras cadObras = new CadObras();
            cadObras.ShowDialog();
            bdpesquisaobra();
        }

        private void cbNome_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn = ConectarBanco();

            string sql = "select * from tbobras where (nomeobra ='" + cbNome.Text + "')";
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
                        Id = Convert.ToString(resul["IdObra"]);
                        txtNomeResp.Text = Convert.ToString(resul["nomeresponsavel"]);
                        txtContato.Text = Convert.ToString(resul["contato"]);
                        txtCliente.Text = Convert.ToString(resul["cliente"]);
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

        private void ConsObras_Load(object sender, EventArgs e)
        {
            bdpesquisaobra();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            AltObras altobras = new AltObras(Id);
            altobras.ShowDialog();
            bdpesquisaobra();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
                    }

        private void txtCep_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
