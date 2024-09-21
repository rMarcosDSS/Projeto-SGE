using MySql.Data.MySqlClient;
using MySql.Data.MySqlClient.Memcached;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using System.IO;
using iTextSharp.text.pdf;
using iTextSharp.text;

namespace Projeto_SGE_Testes
{
    public partial class ConsClientes : Form
    {

        public string strConn;
        public MySqlConnection conn;
        public string merro;
        public string idcliente;

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

        public ConsClientes()
        {
            InitializeComponent();
           
        }

        private void ConsClientes_Load(object sender, EventArgs e)
        {
            bdcombobox();
        }

        private void bdcombobox()
        {

            conn = ConectarBanco();

            string sql = "select * from tbcliente where (statuscliente = 'Ativo')";

            MySqlCommand comd = new MySqlCommand(sql, conn);
            comd.CommandText = "select * from tbcliente where (statuscliente = 'Ativo')";
            MySqlDataReader dr = comd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            cbNome.DisplayMember = "nomecliente";
            cbNome.DataSource = dt;
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            CadClientes cadClientes = new CadClientes();
            cadClientes.ShowDialog();
            bdcombobox();
        }

        private void cbNome_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn = ConectarBanco();

            string sql = "select * from tbcliente where (nomecliente ='" + cbNome.Text + "') and (statuscliente = 'Ativo')";
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
                        txtCnpj.Text = Convert.ToString(resul["cnpj"]);
                        txtbCPF.Text = Convert.ToString(resul["cpf"]);
                        idcliente = Convert.ToString(resul["IdCliente"]);
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
            AltClientes altClientes = new AltClientes(idcliente);
            altClientes.ShowDialog();
            bdcombobox();

        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            
        }

        private void cbNome_Enter(object sender, EventArgs e)
        {
            bdcombobox();
        }

        private void kryptonPanel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
    }
}
