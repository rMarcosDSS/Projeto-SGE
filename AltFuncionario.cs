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
    public partial class AltFuncionario : KryptonForm
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

        public AltFuncionario(string idfunc)
        {
            InitializeComponent();
            Id = idfunc;
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AltFuncionario_Load(object sender, EventArgs e)
        {
            conn = ConectarBanco();

            string sql = "select * from tbfuncionario where (IdFunc ='" + Id + "')";
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
                        txtNome.Text = Convert.ToString(resul["nomefunc"]);
                        txtSobrenome.Text = Convert.ToString(resul["sobrenomefunc"]);
                        txtRg.Text = Convert.ToString(resul["rg"]);
                        txtCpf.Text = Convert.ToString(resul["cpf"]);
                        txtContato.Text = Convert.ToString(resul["contato"]);
                        txtEmail.Text = Convert.ToString(resul["email"]);
                        cbCargo.Text = Convert.ToString(resul["cargo"]);
                        txtSalario.Text = Convert.ToString(resul["salario"]);
                        cbPrazoSal.Text = Convert.ToString(resul["prazosalario"]);
                        txtStatus.Text = Convert.ToString(resul["statusfunc"]);
                        dtDataAdmis.Value = Convert.ToDateTime(resul["dataadmissao"]);
                        dtDataNasc.Value = Convert.ToDateTime(resul["datanascimento"]);

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
            if(MessageBox.Show("Deseja mesmo deletar este funcionário?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation,MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                string Status = "Desativado";
                conn = ConectarBanco();
                string sql = "update tbfuncionario set statusfunc='" + Status + "' where IdFunc='" + Id + "'";
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
            if(String.IsNullOrEmpty(txtId.Text) || String.IsNullOrEmpty(txtStatus.Text) || String.IsNullOrEmpty(txtNome.Text) || String.IsNullOrEmpty(txtSobrenome.Text) || String.IsNullOrEmpty(txtRg.Text) || String.IsNullOrEmpty(txtContato.Text) || String.IsNullOrEmpty(txtEmail.Text) || String.IsNullOrEmpty(txtCpf.Text) || String.IsNullOrEmpty(txtSalario.Text))
            {
                MessageBox.Show("Campos Vazio");
                verificarcampos();
                //Limpar_Campos();
            }
            else
            {
                string d1, m1, a1;
                d1 = dtDataNasc.Value.Day.ToString();
                m1 = dtDataNasc.Value.Month.ToString();
                a1 = dtDataNasc.Value.Year.ToString();
                string DataNascf = a1 + "-" + m1 + "-" + d1;
                string d2, m2, a2;
                d2 = dtDataAdmis.Value.Day.ToString();
                m2 = dtDataAdmis.Value.Month.ToString();
                a2 = dtDataAdmis.Value.Year.ToString();
                string DataAdmisf = a2 + "-" + m2 + "-" + d2;

                conn = ConectarBanco();
                string sql = "update tbfuncionario set nomefunc='" + txtNome.Text + "', sobrenomefunc='" + txtSobrenome.Text + "', rg='"+ txtRg.Text + "', contato='"+ txtContato.Text +  "', email='" + txtEmail.Text + "', cpf='" + txtCpf.Text + "', datanascimento='" + DataNascf + "', dataadmissao='" +DataAdmisf + "', cargo='" + cbCargo.Text + "', salario='" + txtSalario.Text + "', prazosalario='" + cbPrazoSal.Text + "', statusfunc='Ativo ' where IdFunc='" + Id + "'";
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

        private void kryptonPanel2_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]

        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]

        private extern static void SendMessage(System.IntPtr hWmd, int wMsg, int wParam, int Iparam);

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void verificarcampos()
        {
            if (String.IsNullOrEmpty(txtStatus.Text))
            {
                lblast1.Visible = true;
            }

            if (String.IsNullOrEmpty(txtNome.Text))
            {
                lblast2.Visible = true;
            }

            if (String.IsNullOrEmpty(txtSobrenome.Text))
            {
                lblast3.Visible = true;
            }

            if (String.IsNullOrEmpty(txtRg.Text))
            {
                lblast4.Visible = true;
            }

            if (String.IsNullOrEmpty(txtContato.Text))
            {
                lblast5.Visible = true;
            }

            if (String.IsNullOrEmpty(txtEmail.Text))
            {
                lblast6.Visible = true;
            }

            if (String.IsNullOrEmpty(txtCpf.Text))
            {
                lblast7.Visible = true;
            }

            if (String.IsNullOrEmpty(txtSalario.Text))
            {
                lblast8.Visible = true;
            }

            if (String.IsNullOrEmpty(cbCargo.Text))
            {
                lblast9.Visible = true;
            }

            if (String.IsNullOrEmpty(cbPrazoSal.Text))
            {
                lblast10.Visible = true;
            }
        }

        private void txtStatus_TextChanged(object sender, EventArgs e)
        {
            lblast1.Visible = false;
        }

        private void txtNome_TextChanged(object sender, EventArgs e)
        {
            lblast2.Visible = false;
        }

        private void txtSobrenome_TextChanged(object sender, EventArgs e)
        {
            lblast3.Visible = false;
        }

        private void txtRg_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            lblast4.Visible = false;
        }

        private void txtContato_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            lblast5.Visible = false;
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            lblast6.Visible = false;
        }

        private void txtCpf_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            lblast7.Visible = false;
        }

        private void txtSalario_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {
            lblast8.Visible = false;
        }

        private void cbCargo_TextChanged(object sender, EventArgs e)
        {
            lblast9.Visible = false;
        }

        private void cbPrazoSal_TextChanged(object sender, EventArgs e)
        {
            lblast10.Visible = false;
        }
    }
}
