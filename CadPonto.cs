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
    public partial class CadPonto : Form
    {
        public MySqlConnection conn;
        public string merro;
        public string idfunc, idserv, idpag, salario, vpagat;

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
        public CadPonto()
        {
            InitializeComponent();
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

        private void CadPonto_Load(object sender, EventArgs e)
        {
            bdpesquisafunc();
            bdpesquisaserv();
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
                        salario = Convert.ToString(resul["salario"]);
                    }
                    comd.Connection.Close();
                    verifpag();

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

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            string d1, m1, a1;
            d1 = dtData.Value.Day.ToString();
            m1 = dtData.Value.Month.ToString();
            a1 = dtData.Value.Year.ToString();
            string DataPonto = a1 + "-" + m1 + "-" + d1;
            conn = ConectarBanco();

            string sql = "insert into tbponto (IdFunc, IdServico, dataponto, status) values ('" + idfunc + "','" + idserv + "','" + DataPonto + "' , 'Ativo')";

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
                criarpag();
                this.Close();
            }

        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            this.Close();
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

        public void verifpag()
        {
            conn = ConectarBanco();

            string sql = "select * from tbpagamento where (status='Pendente' and IdFunc='"+idfunc+"')";

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
                        idpag = Convert.ToString(resul["IdPagamento"]);
                        vpagat = Convert.ToString(resul["valor"]);

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

        public void criarpag()
        {
            conn=ConectarBanco();

            string d1, m1, a1;
            d1 = dtData.Value.Day.ToString();
            m1 = dtData.Value.Month.ToString();
            a1 = dtData.Value.Year.ToString();
            string DataPonto = a1 + "-" + m1 + "-" + d1;


            if (String.IsNullOrEmpty(idpag) || String.IsNullOrEmpty(vpagat))
            {
                string valorfinal = salario;

                string sql = "insert into tbpagamento (IdFunc, tipo, descricao, vencimento, valor, status) values ('" + idfunc + "','Custo','Pagamento','" + DataPonto + "','" + valorfinal + "','Pendente')";

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
                    // Limpar_Campos();
                }
            }
            else
            {
                string valorfinal = Convert.ToString(Convert.ToInt16(vpagat)+ Convert.ToInt16(salario));
                string sql = "update tbpagamento set valor='"+valorfinal+"', vencimento='"+DataPonto+"' where IdFunc='"+idfunc+"'";

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
                }
            }
        }
    }
}
