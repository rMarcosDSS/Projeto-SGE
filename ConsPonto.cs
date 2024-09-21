using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text;
using MySql.Data.MySqlClient;
using iTextSharp.text.pdf.parser;

namespace Projeto_SGE_Testes
{
    public partial class ConsPonto : Form
    {

        static BaseFont fonteBase = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);


        public string strConn;
        public MySqlConnection conn;
        public string merro, IdPonto;

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
        public ConsPonto()
        {
            InitializeComponent();

        }

        public void pesquisaponto()
        {
            conn = ConectarBanco();

            string sql = "select * from tbponto where status='Ativo'";

            MySqlDataAdapter comd = new MySqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            comd.Fill(dt);
            dtgPonto.DataSource = dt;
        }

        public void pesquisapontofunc(string IdFunc)
        {
            conn = ConectarBanco();

            string sql = "select * from tbponto where status='Ativo' and IdFunc = '"+ IdFunc + "'";

            MySqlDataAdapter comd = new MySqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            comd.Fill(dt);
            dtgPonto.DataSource = dt;
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


        private void ConsPonto_Load(object sender, EventArgs e)
        {
            bdcombobox();
            rbtnTb.Checked = true;
            rbtnFunc.Checked = false;
            cbNome.Visible = false;

        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            CadPonto cadponto = new CadPonto();
            cadponto.ShowDialog();
            rbtnTb.Checked = true;
            pesquisapontofunc(idfunc);
            pesquisaponto();
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            AltPonto altponto = new AltPonto(IdPonto);
            altponto.ShowDialog();
            rbtnTb.Checked = true;
            pesquisapontofunc(idfunc);
            pesquisaponto();
        }

        private void dtgPonto_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void rbtnTb_CheckedChanged(object sender, EventArgs e)
        {
            pesquisaponto();
            cbNome.Visible = false;
            
        }

        private void rbtnFunc_CheckedChanged(object sender, EventArgs e)
        {
            cbNome.Visible = true;
           
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
                        pesquisapontofunc(idfunc);           
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

        private void kryptonButton3_Click(object sender, EventArgs e)
        {
            using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "PDF File|*.pdf", ValidateNames = true })
            {
                if (sfd.ShowDialog() == DialogResult.OK)
                {

                    var pxPorMm = 72 / 25.2F;

                    var fonteParagrafo = new iTextSharp.text.Font(fonteBase, 11, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                    var fonteParagrafo2 = new iTextSharp.text.Font(fonteBase, 11, iTextSharp.text.Font.NORMAL, BaseColor.LIGHT_GRAY);
                    var fonteParagrafo3 = new iTextSharp.text.Font(fonteBase, 13, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);



                    var ti = new iTextSharp.text.Font(fonteBase, 20, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

                    var tit = new Paragraph("Relatório Ponto",ti);
                    tit.Alignment = Element.ALIGN_CENTER;
                    tit.SpacingBefore = 30;
                    tit.SpacingAfter = 30;


                    var assin = new Paragraph("EPS Instalações\r\n18.464.179/0001-01", fonteParagrafo);
                    assin.Alignment = Element.ALIGN_RIGHT;
                    

                    Paragraph p = new Paragraph(new Chunk(new iTextSharp.text.pdf.draw.LineSeparator(0.0F, 100.0F, BaseColor.BLACK, Element.ALIGN_LEFT, 1)));
                    p.SpacingBefore = 250;


                    var sp = new Paragraph("São Paulo, de janeiro de 2020", fonteParagrafo3);
                    sp.Alignment = Element.ALIGN_RIGHT;




                    var titulo = new Paragraph("Endereço: Rua Thyrso Burgos Lopes, 356, Itaim Paulista, São Paulo(SP).\r\nCEP: 08130 - 050", fonteParagrafo2);
                    titulo.Alignment = Element.ALIGN_CENTER;
                    titulo.SpacingBefore = 30;



                    PdfPTable tabela = new PdfPTable(5);
                    float[] larguras = { 1.3f, 1.3f, 1.5f, 1.5f, 1.5f};
                    tabela.SetWidths(larguras);
                    tabela.DefaultCell.BorderWidth = 0;
                    tabela.WidthPercentage = 100;
                    tabela.DefaultCell.FixedHeight = 30;
                    tabela.DefaultCell.FixedHeight = 30;
   
                    CriarCelulaTexto(tabela,"Id_Ponto", PdfPCell.ALIGN_CENTER, true);
                    CriarCelulaTexto(tabela,"Id_Funcionário", PdfPCell.ALIGN_CENTER, true);
                    CriarCelulaTexto(tabela,"Id_Servico", PdfPCell.ALIGN_CENTER, true);
                    CriarCelulaTexto(tabela,"Dataponto", PdfPCell.ALIGN_CENTER, true);
                    CriarCelulaTexto(tabela,"Status", PdfPCell.ALIGN_CENTER, true);

                    conn = ConectarBanco();

                    string sql = "select * from tbponto where status='Ativo'";



                    MySqlDataAdapter comd = new MySqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    comd.Fill(dt);


                    for (int i = 0; i < dtgPonto.Rows.Count; i++)
                    {
                        CriarCelulaTexto(tabela,dt.Rows[i].Field<Int32>("IdPonto").ToString(), PdfPCell.ALIGN_CENTER);
                        CriarCelulaTexto(tabela,dt.Rows[i].Field<Int32>("IdFunc").ToString(), PdfPCell.ALIGN_CENTER);
                        CriarCelulaTexto(tabela,dt.Rows[i].Field<Int32>("IdServico").ToString(), PdfPCell.ALIGN_CENTER);
                        CriarCelulaTexto(tabela,dt.Rows[i].Field<DateTime>("dataponto").ToString(), PdfPCell.ALIGN_CENTER);
                        CriarCelulaTexto(tabela,dt.Rows[i].Field<string>("status"), PdfPCell.ALIGN_CENTER);
                    }


     

                    iTextSharp.text.Document doc = new iTextSharp.text.Document(PageSize.A4, 15 * pxPorMm, 15 * pxPorMm, 15 * pxPorMm, 20 * pxPorMm);


                    iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance("img\\Empresa.png");
                    logo.Alignment = Element.ALIGN_CENTER;

                    try
                    {
                        PdfWriter.GetInstance(doc, new FileStream(sfd.FileName, FileMode.Create));
                        doc.Open();
                        doc.Add(logo);
                        doc.Add(tit);
                        doc.Add(tabela);
                        doc.Add(p);
                        doc.Add(assin);
                        doc.Add(sp);

                        doc.Add(titulo);

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    finally
                    {
                        doc.Close();
                    }
                }

            }

            
        }

        static void CriarCelulaTexto(PdfPTable tabela, string texto,
            int alinhamento = PdfPCell.ALIGN_LEFT,
            bool negrito = false, bool italico = false,
            int tamanhoFonte = 12, int alturaCelula = 25)
        {
            int estilo = iTextSharp.text.Font.NORMAL;
            if (negrito && italico)
            {
                estilo = iTextSharp.text.Font.BOLDITALIC;
            }
            else if (negrito)
            {
                estilo = iTextSharp.text.Font.BOLD;
            }
            else if (italico)
            {
                estilo = iTextSharp.text.Font.ITALIC;
            }

            BaseFont fonteBase = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);
            iTextSharp.text.Font fonte = new iTextSharp.text.Font(fonteBase, tamanhoFonte,
                estilo, iTextSharp.text.BaseColor.BLACK);

            //cor de fundo diferente para linhas pares e ímpares
            var bgColor = iTextSharp.text.BaseColor.WHITE;
            if (tabela.Rows.Count % 2 == 1)
                bgColor = new BaseColor(0.95f, 0.95f, 0.95f);

            PdfPCell celula1 = new PdfPCell(new Phrase(texto, fonte));
            celula1.HorizontalAlignment = alinhamento;
            celula1.VerticalAlignment = PdfPCell.ALIGN_MIDDLE;
            celula1.Border = 0;
            celula1.BorderWidthBottom = 1;
            celula1.PaddingBottom = 5; //pra alinhar melhor verticalmente
            celula1.FixedHeight = alturaCelula;
            celula1.BackgroundColor = bgColor;
            tabela?.AddCell(celula1);
        }
        private void dtgPonto_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                pesqminid();
            }
            else
            {
                IdPonto = Convert.ToString(dtgPonto.Rows[e.RowIndex].Cells[0].Value);
            }
         
        }

        public void pesqminid()
        {
            conn = ConectarBanco();

            string sql = "select min(IdPonto) from tbponto";

            MySqlCommand comd2 = new MySqlCommand(sql, conn);
            MySqlDataReader resul = comd2.ExecuteReader();
            if (resul.HasRows)
            {
                while (resul.Read())
                {
                    IdPonto = Convert.ToString(resul["min(IdPonto)"]);
                }
                comd2.Connection.Close();
            }
        }
    }
}
