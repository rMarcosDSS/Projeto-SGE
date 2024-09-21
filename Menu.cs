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

namespace Projeto_SGE_Testes
{
    public partial class Menu : KryptonForm
    {
        bool siderbarexpand;
        public Menu()
        {
            InitializeComponent();
        }

        public void loadform(object Form)
        {
            pictureBox1.Visible = false;
            if (this.MainPanel.Controls.Count > 0)
                this.MainPanel.Controls.RemoveAt(0);
            Form f = Form as Form;
            f.TopLevel = false;
            f.Dock = DockStyle.Fill;
            this.MainPanel.Controls.Add(f);
            this.MainPanel.Tag = f;
            f.Show();
        }

        public void verificauser()
        {
            if (Conexao.niveluser == "Usuario")
            {
                button1.Enabled = false;
                panelbutton.Size = panelbutton.MinimumSize;
                panelfohat.Size = panelfohat.MaximumSize;
            }
            else if (Conexao.niveluser == "Admin")
            {
                button1.Enabled = true;
                panelbutton.Size = panelbutton.MaximumSize;
                panelfohat.Size = panelfohat.MinimumSize;
            }
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void kryptonPanel1_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]

        private extern static void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]

        private extern static void SendMessage(System.IntPtr hWmd, int wMsg, int wParam, int Iparam);

        private void sidebartimer1_Tick(object sender, EventArgs e)
        {
            if (siderbarexpand)
            {
                SidebarMenu.Width -= 10;

                if (SidebarMenu.Width == SidebarMenu.MinimumSize.Width)
                {
                    siderbarexpand = false;
                    sidebartimer1.Stop();
                    lblfohat.Visible = false;
                }
            }
            else
            {
                SidebarMenu.Width += 10;

                if (SidebarMenu.Width == SidebarMenu.MaximumSize.Width)
                {
                    siderbarexpand = true;
                    sidebartimer1.Stop();
                    lblfohat.Visible = true;
                }


            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            sidebartimer1.Start();
        }

        private void btnCliente_Click(object sender, EventArgs e)
        {
            loadform(new ConsClientes());
        }

        private void btnObras_Click(object sender, EventArgs e)
        {
            loadform(new ConsObras());
        }

        private void btnFuncionario_Click(object sender, EventArgs e)
        {
            loadform(new ConsFuncionario());
        }

        private void btnMateriais_Click(object sender, EventArgs e)
        {
            loadform(new ConsMateriais());
        }

        private void btnOrcamentos_Click(object sender, EventArgs e)
        {
            loadform(new ConsOrcamentos());
        }

        private void btnServicos_Click(object sender, EventArgs e)
        {
            loadform(new ConsServicos());
        }

        private void btnPagamentos_Click(object sender, EventArgs e)
        {
            loadform(new ConsPagamentos());
        }

        private void btnPonto_Click(object sender, EventArgs e)
        {
            loadform(new ConsPonto());
        }

        private void kryptonPanel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadform(new ConsUsuarios());
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void Menu_Load(object sender, EventArgs e)
        {
            verificauser();
        }
    }
}
