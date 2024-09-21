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

namespace Projeto_SGE_Testes
{
    public partial class ConsMateriais : Form
    {
        public string strConn;
        public MySqlConnection conn;
        public string merro;
        public string Idms;
        public bool ms;


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

        private void bdcombobox()
        {

            conn = ConectarBanco();

            string sql = "select * from tbmateriais where status='Ativo'";

            MySqlCommand comd = new MySqlCommand(sql, conn);
            comd.CommandText = "select * from tbmateriais where status='Ativo'";
            MySqlDataReader dr = comd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            cbNome.DisplayMember = "nomematerial";
            cbNome.DataSource = dt;
        }

        private void bdcomboboxserv()
        {

            conn = ConectarBanco();

            string sql = "select * from tblstservico where status='Ativo'";

            MySqlCommand comd = new MySqlCommand(sql, conn);
            comd.CommandText = "select * from tblstservico where status='Ativo'";
            MySqlDataReader dr = comd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            cbNomeServ.DisplayMember = "nomeservico";
            cbNomeServ.DataSource = dt;
        }

        public ConsMateriais()
        {
            InitializeComponent();
          


        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            AltMateriais altmateriais = new AltMateriais(Idms, ms);
            altmateriais.ShowDialog();
            bdcombobox();
            bdcomboboxserv();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            CadMateriais altMateriais = new CadMateriais();
            altMateriais.ShowDialog();
            bdcombobox();
            bdcomboboxserv();
        }

        private void cbNome_SelectedIndexChanged(object sender, EventArgs e)
        {
           

            conn = ConectarBanco();

            string sql = "select * from tbmateriais where (nomematerial ='" + cbNome.Text + "' and status='Ativo')";
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
                        Idms = Convert.ToString(resul["IdMateriais"]);
                        txtPreco.Text = Convert.ToString(resul["precomaterial"]);
                        txtQtd.Value = Convert.ToInt32(Convert.ToString(resul["quant"]));
                        ms = true;
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

        private void ConsMateriais_Load(object sender, EventArgs e)
        {
            lblNome.Visible = false;
            lblPreco.Visible = false;
            txtPreco.Visible = false;
            cbNome.Visible = false;
            precoServ.Visible = false;
            cbNomeServ.Visible = false;

            txtQtd.Visible = false;
            label12.Visible = false;
            txtrs.Visible = false;

        }

        private void rbtnMaterial_CheckedChanged(object sender, EventArgs e)
        {
            bdcombobox();

            lblNome.Visible = false;
            lblPreco.Visible = false;
            precoServ.Visible = false;
            cbNomeServ.Visible = false;

            lblNome.Visible = true;
            lblPreco.Visible = true;
            txtPreco.Visible = true;
            cbNome.Visible = true;

            txtQtd.Visible = true;
            label12.Visible = true;
            txtrs.Visible = true;







        }

        private void rbtnServico_CheckedChanged(object sender, EventArgs e)
        {

            bdcomboboxserv();

            lblNome.Visible = false;
            lblPreco.Visible = false;
            txtPreco.Visible = false;
            cbNome.Visible = false;

            lblNome.Visible = true;
            lblPreco.Visible = true;
            precoServ.Visible = true;
            cbNomeServ.Visible = true;

            txtQtd.Visible = true;
            label12.Visible = true;
            txtrs.Visible = true;



        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {

           
            
        }

        private void cbNomeServ_SelectedIndexChanged(object sender, EventArgs e)
        {


            conn = ConectarBanco();

            string sql = "select * from tblstservico where (nomeservico ='" + cbNomeServ.Text + "' and status='Ativo')";
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
                        Idms = Convert.ToString(resul["IdLstServico"]);
                        precoServ.Text = Convert.ToString(resul["precoservico"]);
                        txtQtd.Value = 1;
                        ms = false;
                    }
                    comd.Connection.Close();

                }

            }


        }

        private void txtPreco_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }

        private void label12_Click(object sender, EventArgs e)
        {

        }

        private void txtQtd_ValueChanged(object sender, EventArgs e)
        {

        }
    }
}
