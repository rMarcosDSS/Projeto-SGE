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

namespace Projeto_SGE_Testes
{
    public partial class ConsUsuarios : KryptonForm
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

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            CadUsuarios cadusuarios = new CadUsuarios();
            cadusuarios.ShowDialog();
            bdpesquser();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            AltUsuarios altUsuarios = new AltUsuarios(iduser);
            altUsuarios.ShowDialog();
            bdpesquser();
        }

        public ConsUsuarios()
        {
            InitializeComponent();
        }

        private void cbNome_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn = ConectarBanco();

            string sql = "select * from tbuser where (nomeuser ='"+ cbNome.Text + "')";
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
                        iduser = Convert.ToString(resul["IdUser"]);
                        txtUsuario.Text = Convert.ToString(resul["login"]);
                        txtSenha.Text = Convert.ToString(resul["senha"]);
                        txtCargo.Text = Convert.ToString(resul["nivel"]);

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

        private void ConsUsuarios_Load(object sender, EventArgs e)
        {
            bdpesquser();
        }

        public void bdpesquser()
        {
            conn = ConectarBanco();

            string sql = "select * from tbuser";

            MySqlCommand comd = new MySqlCommand(sql, conn);
            comd.CommandText = "select * from tbuser";
            MySqlDataReader dr = comd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            cbNome.DisplayMember = "nomeuser";
            cbNome.DataSource = dt;
        }
    }
}
