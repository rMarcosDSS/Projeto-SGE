using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ComponentFactory.Krypton.Toolkit;
using MySql.Data.MySqlClient;

namespace Projeto_SGE_Testes
{
    public partial class AltPagamentos : KryptonForm
    {
        public MySqlConnection conn;
        public string merro;
        public string idpag, tipo, idfunc, idserv;

        private void AltPagamentos_Load(object sender, EventArgs e)
        {
            bdcomboboxfunc();
            bdcomboboxserv();
             conn = ConectarBanco();

            string sql = "select * from tbpagamento where (IdPagamento ='" + idpag + "')";
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
                        txtId.Text = idpag;
                        txtDesc.Text = Convert.ToString(resul["descricao"]);
                        dtVencimento.Value = Convert.ToDateTime(resul["vencimento"]);
                        txtValor.Text = Convert.ToString(resul["valor"]);
                        txtStatus.Text = Convert.ToString(resul["status"]);
                        tipo = Convert.ToString(resul["tipo"]);
                        cbTipo.Text = tipo;
                        if(tipo == "Custo")
                        {
                            rbtnFunc.Checked = true;
                            rbtnServ.Checked = false;
                            cbIdF.Visible = true;
                            cbIdS.Visible = false;
                            cbIdF.Text = Convert.ToString(resul["IdFunc"]);
                        }
                        else if (tipo == "Receita")
                        {
                            rbtnFunc.Checked = false;
                            rbtnServ.Checked = true;
                            cbIdF.Visible = false;
                            cbIdS.Visible = true;
                            cbIdS.Text = Convert.ToString(resul["IdServ"]);
                        }
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
        public AltPagamentos(string IdPagamento)
        {
            InitializeComponent();

            idpag = IdPagamento;

            rbtnFunc.Checked = false;
            rbtnServ.Checked = false;
            cbIdF.Visible = false;
            cbIdS.Visible = false;
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
            cbIdS.DisplayMember = "IdLstServico";
            cbIdS.DataSource = dt;
        }

        private void rbtnFunc_CheckedChanged(object sender, EventArgs e)
        {
            rbtnServ.Checked = false;
            cbIdF.Visible = true;
            cbIdS.Visible = false;
        }

        private void btnAlterar_Click(object sender, EventArgs e)
        {
            conn = ConectarBanco();
            string d1, m1, a1;
            d1 = dtVencimento.Value.Day.ToString();
            m1 = dtVencimento.Value.Month.ToString();
            a1 = dtVencimento.Value.Year.ToString();
            string DataVencimento = a1 + "-" + m1 + "-" + d1;
            if (String.IsNullOrEmpty(txtDesc.Text) || String.IsNullOrEmpty(txtStatus.Text) || String.IsNullOrEmpty(txtValor.Text) || String.IsNullOrEmpty(dtVencimento.Text))
            {
                MessageBox.Show("Campos Vazio");
                verificacampos();
                //Limpar_Campos();
            }
            else
            {
                if(rbtnFunc.Checked)
                {
                    string sql = "update tbpagamento set IdFunc='"+cbIdF.Text+"', tipo='"+cbTipo.Text+"', descricao='"+txtDesc.Text+"', vencimento='"+DataVencimento+"', valor='"+txtValor.Text+"', status='"+txtStatus.Text+"' where IdPagamento='"+txtId.Text+"'";

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
                else if (rbtnServ.Checked)
                {
                    string sql = "update tbpagamento set IdServ='" + cbIdS.Text + "', tipo='" + cbTipo.Text + "', descricao='" + txtDesc.Text + "', vencimento='" + DataVencimento + "', valor='" + txtValor.Text + "', status='" + txtStatus.Text + "' where IdPagamento='" + txtId.Text + "'";

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
        }

        private void btnExcluir_Click(object sender, EventArgs e)
        {
            conn = ConectarBanco();
            string status = "Oculto";
            if (MessageBox.Show("Deseja mesmo deletar este produto?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                string sql = "update tbpagamento set status='" + status + "' where IdPagamento='" + txtId.Text + "'";

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
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbtnServ_CheckedChanged(object sender, EventArgs e)
        {
            rbtnFunc.Checked = false;
            cbIdF.Visible = false;
            cbIdS.Visible = true;
        }

        private void bdcomboboxfunc()
        {

            conn = ConectarBanco();

            string sql = "select * from tbfuncionario where (statusfunc = 'Ativo')";

            MySqlCommand comd = new MySqlCommand(sql, conn);
            comd.CommandText = "select * from tbfuncionario where (statusfunc = 'Ativo')";
            MySqlDataReader dr = comd.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            cbIdF.DisplayMember = "IdFunc";
            cbIdF.DataSource = dt;
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
            if (txtStatus.Text == "Status Atual")
            {
                lblast4.Visible = true;
            }
            if (cbTipo.Text == "")
            {
                lblast5.Visible = true;
            }
        }

        private void txtValor_TextChanged(object sender, EventArgs e)
        {
            lblast2.Visible = false;
        }

        private void cbTipo_TextChanged(object sender, EventArgs e)
        {
            lblast5.Visible = false;
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
    }
}
