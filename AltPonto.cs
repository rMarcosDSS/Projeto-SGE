using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace Projeto_SGE_Testes
{
    public partial class AltPonto : Form
    {
        public MySqlConnection conn;
        public string merro;
        public string idponto,idfunc, idserv;

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
        public AltPonto(string IDP)
        {
            InitializeComponent();

            idponto = IDP;
        }

        private void bdpesquisafunc()
        {

            conn = ConectarBanco();

            string sql = "select * from tbfuncionario where (statusfunc = 'Ativo')";

            MySqlCommand comd = new MySqlCommand(sql, conn);
            comd.CommandText = "select * from tbfuncionario where (statusfunc = 'Ativo')";
            MySqlDataReader dr = comd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            cbNomeFunc.DisplayMember = "nomefunc";
            cbNomeFunc.DataSource = dt;
        }
        private void bdpesquisaserv()
        {

            conn = ConectarBanco();

            string sql = "select * from tbservico where (status = 'Iniciado')";
            MySqlCommand comd = new MySqlCommand(sql, conn);
            comd.CommandText = "select * from tbservico where (status = 'Iniciado')";
            MySqlDataReader dr = comd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            cbServ.DisplayMember = "IdServico";
            cbServ.DataSource = dt;
        }

        public void pesquisaponto()
        {
            conn = ConectarBanco();

            string sql = "select * from tbponto where IdPonto='"+ idponto +"'";

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

                    while(resul.Read())
                    {
                        idfunc = Convert.ToString(resul["IdFunc"]);
                        cbNomeFunc.Text = idfunc;
                        idserv = Convert.ToString(resul["IdServico"]);
                        cbServ.Text = idserv;
                        dtData.Value = Convert.ToDateTime(resul["dataponto"]);
                    }
                    comd.Connection.Close();   
                }
            }
        }

        private void CadPonto_Load(object sender, EventArgs e)
        {
            
            bdpesquisafunc();
            bdpesquisaserv();
            pesquisaponto();
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

        private void cbNomeFunc_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn = ConectarBanco();

            string sql = "select * from tbfuncionario where (nomefunc ='"+ cbNomeFunc.Text +"') and (statusfunc = 'Ativo')";
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


        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            conn = ConectarBanco();
            string status = "Oculto";
            if (MessageBox.Show("Deseja mesmo deletar este ponto?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                string sql = "update tbponto set status='" + status + "' where IdPonto='"+ idponto +"'";

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
                    // Limpar_Campos();
                    this.Close();
                }
            }
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            conn = ConectarBanco();

            string d1, m1, a1;
            d1 = dtData.Value.Day.ToString();
            m1 = dtData.Value.Month.ToString();
            a1 = dtData.Value.Year.ToString();
            string Dataponto = a1 + "-" + m1 + "-" + d1;

            if (String.IsNullOrEmpty(cbNomeFunc.Text) || String.IsNullOrEmpty(cbServ.Text))
            {
                MessageBox.Show("Campos Vazio");

                //Limpar_Campos();
            }
            else
            {
                string sql = "update tbponto set IdFunc='"+ idfunc +"', IdServico='"+ idserv +"', dataponto='" + Dataponto + "' where IdPonto='"+ idponto +"'";

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
                    // Limpar_Campos();
                    this.Close();
                }
            }
        }

        private void cbServ_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn = ConectarBanco();

            string sql = "select * from tbservico where (IdServico ='"+ cbServ.Text +"' and status = 'Iniciado')";
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
                        idserv = Convert.ToString(resul["IdServico"]);
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
    }
}
