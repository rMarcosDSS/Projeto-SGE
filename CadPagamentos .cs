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
using ComponentFactory.Krypton.Toolkit;
using Microsoft.Win32.SafeHandles;
using MySql.Data.MySqlClient;
using MySql.Data.MySqlClient.Memcached;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using System.Runtime.InteropServices;

namespace Projeto_SGE_Testes
{
    public partial class CadPagamentos : KryptonForm
    {
        public MySqlConnection conn;
        public string merro;
        public string IdFunc, IdServ;
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
        public CadPagamentos()
        {
            InitializeComponent();
        }

        private void bdpesquisafuncionario()
        {

            conn = ConectarBanco();

            string sql = "select * from tbfuncionario";

            MySqlCommand comd = new MySqlCommand(sql, conn);
            comd.CommandText = "select * from tbfuncionario";
            MySqlDataReader dr = comd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            cbFouV.DisplayMember = "nomefunc";
            cbFouV.DataSource = dt;
        }

        private void bdpesquisaservico()
        {

            conn = ConectarBanco();

            string sql = "select * from tbservico";

            MySqlCommand comd = new MySqlCommand(sql, conn);
            comd.CommandText = "select * from tbservico";
            MySqlDataReader dr = comd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            cbFouV.DisplayMember = "IdServico";
            cbFouV.DataSource = dt;
        }

        private void CadPagamentos_Load(object sender, EventArgs e)
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


        private void rbtnFunc_CheckedChanged(object sender, EventArgs e)
        {
            bdpesquisafuncionario();
        }

        public void verificacampos()
        {
            if (String.IsNullOrEmpty(txtDesc.Text))
            {
                lblast1.Visible = true;
            }
            if (String.IsNullOrEmpty(txtValor.Text))
            {
                lblast2.Visible = true;
            }
            if ((rbtnFunc.Checked == false) && (rbtnServ.Checked == false))
            {
                    lblast3.Visible = true;                
            }
            if(txtStatus.Text == "Status Atual")
            { 
                lblast4.Visible = true;
            }

        }

        private void cbFouV_SelectedIndexChanged(object sender, EventArgs e)
        {
            conn = ConectarBanco();

            if (rbtnFunc.Checked)
            {
                string sql = "select * from tbfuncionario where (nomefunc ='" + cbFouV.Text + "')";
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
                            IdFunc = Convert.ToString(resul["IdFunc"]);
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
            else if(rbtnServ.Checked)
            {
                string sql = "select * from tbservico where (IdServico ='" + cbFouV.Text + "')";
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
                            IdServ = Convert.ToString(resul["IdServico"]);
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

        private void rbtnServ_CheckedChanged(object sender, EventArgs e)
        {
            bdpesquisaservico();
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtValor_TextChanged(object sender, EventArgs e)
        {
            lblast2.Visible = false;
        }

        private void txtDesc_TextChanged(object sender, EventArgs e)
        {
            lblast1.Visible = false;
        }

        private void cbFouV_TextChanged(object sender, EventArgs e)
        {
            lblast3.Visible = false;
        }

        private void txtStatus_TextChanged(object sender, EventArgs e)
        {
            lblast4.Visible = false;
        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(txtDesc.Text) || String.IsNullOrEmpty(txtValor.Text) || String.IsNullOrEmpty(dtVencimento.Text) || String.IsNullOrEmpty(cbFouV.Text))
            {
                MessageBox.Show("Campos Vazio");
                verificacampos();
                //Limpar_Campos();
            }
            else
            {
                string d1, m1, a1;
                d1 = dtVencimento.Value.Day.ToString();
                m1 = dtVencimento.Value.Month.ToString();
                a1 = dtVencimento.Value.Year.ToString();
                string DataVencimento = a1 + "-" + m1 + "-" + d1;

                conn = ConectarBanco();

                if (rbtnFunc.Checked)
                {
                    string tipo = "Custo";
                    string sql = "insert into tbpagamento (IdFunc, tipo, descricao, vencimento, valor, status) " +
                    "values ('" + IdFunc + "','" + tipo + "','" + txtDesc.Text + "','" + DataVencimento + "','" + txtValor.Text + "','" + txtStatus.Text + "')";

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
                else if(rbtnServ.Checked)
                {
                    string tipo = "Receita";
                    string sql = "insert into tbpagamento (IdServ, tipo, descricao, vencimento, valor, status) " +
                    "values ('" + IdServ + "','" + tipo + "','" + txtDesc.Text + "','" + DataVencimento + "','" + txtValor.Text + "','" + txtStatus.Text + "')";

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

                    MessageBox.Show("Cadastrado com Sucesso");
                    // Limpar_Campos();
                }


            }
        }

       
    }
}
