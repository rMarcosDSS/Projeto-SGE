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

namespace Projeto_SGE_Testes
{
    public partial class ConsFuncionario : Form
    {
        public string strConn;
        public MySqlConnection conn;
        public string merro;
        public string idfunc;

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

        public ConsFuncionario()
        {
            InitializeComponent();
            
        }

        private void bdcombobox()
        {

            conn = ConectarBanco();

            string sql = "select * from tbfuncionario where (statusfunc = 'Ativo')";

            MySqlCommand comd = new MySqlCommand(sql, conn);
            comd.CommandText = "select * from tbfuncionario where (statusfunc = 'Ativo')";
            MySqlDataReader dr = comd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            cbNome.DisplayMember = "nomefunc";
            cbNome.DataSource = dt;
        }


        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            CadFuncionario cadFuncionario = new CadFuncionario();
            cadFuncionario.ShowDialog();
            bdcombobox();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            AltFuncionario altFuncionario = new AltFuncionario(idfunc);
            altFuncionario.ShowDialog();
            bdcombobox();
        }

        private void cbCargo_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void ConsFuncionario_Load(object sender, EventArgs e)
        {
            bdcombobox();
        }

        private void cbNome_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn = ConectarBanco();

            string sql = "select * from tbfuncionario where (nomefunc ='" + cbNome.Text + "') and (statusfunc = 'Ativo')";
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
                        idfunc = Convert.ToString(resul["IdFunc"]);
                        txtSobreN.Text = Convert.ToString(resul["sobrenomefunc"]);
                        txtRG.Text = Convert.ToString(resul["rg"]);
                        txtCont.Text = Convert.ToString(resul["contato"]);
                        txtEmail.Text = Convert.ToString(resul["email"]);
                        txtCPF.Text = Convert.ToString(resul["cpf"]);
                        txtDataN.Text = Convert.ToString(resul["datanascimento"]);
                        txtDataA.Text = Convert.ToString(resul["dataadmissao"]);
                        txtCargo.Text = Convert.ToString(resul["cargo"]);
                        txtSal.Text = Convert.ToString(resul["salario"]);
                        txtSalP.Text = Convert.ToString(resul["prazosalario"]);
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

        private void txtSalP_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
