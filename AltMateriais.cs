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

namespace Projeto_SGE_Testes
{
    public partial class AltMateriais : KryptonForm
    {
        public MySqlConnection conn;
        public string merro;
        public string Id;
        public bool bms;

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
        public AltMateriais(string Idms, bool ms)
        {
            InitializeComponent();

            Id = Idms;
            bms = ms;
        }

        private void AltMateriais_Load(object sender, EventArgs e)
        {
            conn = ConectarBanco();

            if(bms == true)
            {
                string sql = "select * from tbmateriais where (IdMateriais='"+ Id +"')";
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
                            txtNome.Text = Convert.ToString(resul["nomematerial"]);
                            txtPreco.Text = Convert.ToString(resul["precomaterial"]);
                            txtQtd.Value = Convert.ToInt32(Convert.ToString(resul["quant"]));
                            rbtnMaterial.Checked = true;
                            rbtnServico.Checked = false;
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
            else if (bms == false)
            {
                string sql = "select * from tblstservico where (IdLstServico='" + Id + "')";
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
                            txtNome.Text = Convert.ToString(resul["nomeservico"]);
                            txtPreco.Text = Convert.ToString(resul["precoservico"]);
                            txtQtd.Value = 1;
                            rbtnServico.Checked = true;
                            rbtnMaterial.Checked = false;

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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            int quant = Convert.ToInt32(txtQtd.Value);
            conn = ConectarBanco();
            if (String.IsNullOrEmpty(txtNome.Text) || String.IsNullOrEmpty(txtPreco.Text) && (quant > 0))
            {
                MessageBox.Show("Campos Vazio");
                verificarcampos();
                //Limpar_Campos();
            }
            else
            { 
                if((bms==true)&&(rbtnMaterial.Checked))
                {
                    string sql = "update tbmateriais set nomematerial='"+ txtNome.Text +"', precomaterial='"+ txtPreco.Text +"', quant='"+ quant +"' where IdMateriais='"+ Id +"'";
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
                else if ((bms == false) && (rbtnServico.Checked))
                {
                    string sql = "update tblstservico set nomeservico='"+ txtNome.Text +"', precoservico='"+txtPreco.Text +"' where IdLstServico='"+ Id +"'";
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
                else
                {
                    MessageBox.Show("Campos desselecionado");

                    //Limpar_Campos();
                }
            }
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            conn = ConectarBanco();
            string status = "Desativado";
            if (MessageBox.Show("Deseja mesmo deletar este produto?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                conn = ConectarBanco();
                if (String.IsNullOrEmpty(txtNome.Text) || String.IsNullOrEmpty(txtPreco.Text))
                {
                    MessageBox.Show("Campos Vazio");

                    //Limpar_Campos();
                }
                else
                {
                    if ((bms == true) && (rbtnMaterial.Checked))
                    {
                        string sql = "update tbmateriais set status='"+ status +"' where IdMateriais='"+ Id +"'";
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
                    else if ((bms == false) && (rbtnServico.Checked))
                    {
                        string sql = "update tblstservico set status='"+ status +"' where IdLstServico='"+ Id +"'";
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
                        MessageBox.Show("Campos desselecionado");

                        //Limpar_Campos();
                    }
                }
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

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void verificarcampos()
        {

            if (String.IsNullOrEmpty(txtNome.Text))
            {
                lblast1.Visible = true;
            }

            if (String.IsNullOrEmpty(txtPreco.Text))
            {
                lblast2.Visible = true;
            }

        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {
            lblast1.Visible = false;
        }

        private void txtPreco_TextChanged(object sender, EventArgs e)
        {
            lblast2.Visible = false;
        }
    }
}
