using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using iTextSharp.text.pdf;
using iTextSharp.text;
using MySql.Data.MySqlClient;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Projeto_SGE_Testes
{
    public partial class ConsPagamentos : Form
    {

        static BaseFont fonteBase = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);

        public string strConn;
        public MySqlConnection conn;
        public string merro;
        public string IdPagamento;

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

        public ConsPagamentos()
        {
            InitializeComponent();

        }

        public void dtPagamentoFunc()
        {
            conn = ConectarBanco();

            string sql = "select IdPagamento, IdFunc, tipo, descricao, vencimento, valor, status from tbpagamento where tipo='Custo' and status <> 'Oculto'";

            MySqlDataAdapter comd = new MySqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            comd.Fill(dt);
            dtPagaFunc.DataSource = dt;


            string sql1 = "select IdPagamento, IdServ, tipo, descricao, vencimento, valor, status from tbpagamento where tipo='Receita' and status <> 'Oculto'";

            MySqlDataAdapter comd1 = new MySqlDataAdapter(sql1, conn);
            DataTable dt1 = new DataTable();
            comd1.Fill(dt1);
            dtPagServ.DataSource = dt1;
        }


        private void ConsPagamentos_Load(object sender, EventArgs e)
        {
            dtPagamentoFunc();
            gbfunc.Visible = false;
            dtPagaFunc.Visible = false;
            gbserv.Visible = false;
            dtPagServ.Visible = false;
            
        }

        private void cbNome_SelectedIndexChanged_1(object sender, EventArgs e)
        {
           
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
                AltPagamentos altPagamentos = new AltPagamentos(IdPagamento);
                altPagamentos.ShowDialog();
            dtPagamentoFunc();
            gbfunc.Visible = false;
            dtPagaFunc.Visible = false;
            gbserv.Visible = false;
            dtPagServ.Visible = false;

        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            CadPagamentos cadPagamentos = new CadPagamentos();
            cadPagamentos.ShowDialog();
            dtPagamentoFunc();
            gbfunc.Visible = false;
            dtPagaFunc.Visible = false;
            gbserv.Visible = false;
            dtPagServ.Visible = false;
        }

        private void rbtnFunc_CheckedChanged(object sender, EventArgs e)
        {
            gbfunc.Visible = true;
            dtPagaFunc.Visible = true;
            gbserv.Visible = false;
            dtPagServ.Visible = false;
            ButtonFunc.Visible = true;
            ButtonServ.Visible = false;
        }

        private void rbtnServico_CheckedChanged(object sender, EventArgs e)
        {
            gbfunc.Visible = false;
            dtPagaFunc.Visible = false;
            gbserv.Visible = true;
            dtPagServ.Visible = true;
            ButtonFunc.Visible = false;
            ButtonServ.Visible = true;
        }

        private void SelecioneFuncionario(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                pesqminidfunc();
            }
            else
            {
                IdPagamento = Convert.ToString(dtPagaFunc.Rows[e.RowIndex].Cells[0].Value);
            }
        }

        private void SelecioneServico(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                pesqminidserv();
            }
            else
            {
                IdPagamento = Convert.ToString(dtPagServ.Rows[e.RowIndex].Cells[0].Value);
            }
            
        }

        public void pesqminidfunc()
        {
            conn = ConectarBanco();

            string sql = "select min(IdPagamento) from tborcamento where tipo='Custo'";

            MySqlCommand comd2 = new MySqlCommand(sql, conn);
            MySqlDataReader resul = comd2.ExecuteReader();
            if (resul.HasRows)
            {
                while (resul.Read())
                {
                    IdPagamento = Convert.ToString(resul["min(IdPagamento)"]);
                }
                comd2.Connection.Close();
            }
        }

        public void pesqminidserv()
        {
            conn = ConectarBanco();

            string sql = "select min(IdPagamento) from tborcamento where tipo='Receita'";

            MySqlCommand comd2 = new MySqlCommand(sql, conn);
            MySqlDataReader resul = comd2.ExecuteReader();
            if (resul.HasRows)
            {
                while (resul.Read())
                {
                    IdPagamento = Convert.ToString(resul["min(IdPagamento)"]);
                }
                comd2.Connection.Close();
            }
        }

        private void kryptonButton2_Click_1(object sender, EventArgs e)
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

        private void btnQuitar_Click(object sender, EventArgs e)
        {
            conn = ConectarBanco();

            string status = "Quitado";

            if (MessageBox.Show("Deseja mesmo quitar esse pagamento?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                string sql = "update tbpagamento set status='"+ status +"' where IdPagamento='"+ IdPagamento +"'";

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
                    MessageBox.Show("Quitado com Sucesso");
                    // Limpar_Campos();
                }
            }
        }

        private void ButtonFunc_Click(object sender, EventArgs e)
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

                    var tit = new Paragraph("Relatório de Faturamento de Custo", ti);
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


                    PdfPTable tabela = new PdfPTable(7);
                    float[] larguras = { 1.3f, 1.3f, 1.5f, 1.5f, 1.5f, 1.5f , 1.5f};
                    tabela.SetWidths(larguras);
                    tabela.DefaultCell.BorderWidth = 0;
                    tabela.WidthPercentage = 100;
                    tabela.DefaultCell.FixedHeight = 30;


                    CriarCelulaTexto(tabela,"Id_Pagamento", PdfPCell.ALIGN_CENTER, true);
                    CriarCelulaTexto(tabela,"Id_Funcionário", PdfPCell.ALIGN_CENTER, true);
                    CriarCelulaTexto(tabela,"Tipo", PdfPCell.ALIGN_CENTER, true);
                    CriarCelulaTexto(tabela,"Descrição", PdfPCell.ALIGN_CENTER, true);
                    CriarCelulaTexto(tabela,"Vencimento", PdfPCell.ALIGN_CENTER, true);
                    CriarCelulaTexto(tabela,"Valor", PdfPCell.ALIGN_CENTER, true);
                    CriarCelulaTexto(tabela,"Status", PdfPCell.ALIGN_CENTER, true);

                    conn = ConectarBanco();

                    string sql = "select IdPagamento, IdFunc, tipo, descricao, vencimento, valor, status from tbpagamento where tipo='Custo' and status <> 'Oculto'";



                    MySqlDataAdapter comd = new MySqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    comd.Fill(dt);


                    for (int i = 0; i < dtPagaFunc.Rows.Count; i++)
                    {
                        CriarCelulaTexto(tabela, dt.Rows[i].Field<Int32>("IdPagamento").ToString(), PdfPCell.ALIGN_CENTER);
                        CriarCelulaTexto(tabela, dt.Rows[i].Field<Int32?>("IdFunc").ToString(), PdfPCell.ALIGN_CENTER);
                        CriarCelulaTexto(tabela, dt.Rows[i].Field<string>("tipo"), PdfPCell.ALIGN_CENTER);
                        CriarCelulaTexto(tabela, dt.Rows[i].Field<string>("descricao"), PdfPCell.ALIGN_CENTER);
                        CriarCelulaTexto(tabela, dt.Rows[i].Field<DateTime>("vencimento").ToString(), PdfPCell.ALIGN_CENTER);
                        CriarCelulaTexto(tabela, dt.Rows[i].Field<string>("valor"), PdfPCell.ALIGN_CENTER);
                        CriarCelulaTexto(tabela, dt.Rows[i].Field<string>("status"), PdfPCell.ALIGN_CENTER);
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

        private void ButtonServ_Click(object sender, EventArgs e)
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

                    var tit = new Paragraph("Relatório de Faturamento de Receita", ti);
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


                    PdfPTable tabela = new PdfPTable(7);
                    float[] larguras = { 1.3f, 1.3f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f };
                    tabela.SetWidths(larguras);
                    tabela.DefaultCell.BorderWidth = 0;
                    tabela.WidthPercentage = 100;
                    tabela.DefaultCell.FixedHeight = 30;



                    CriarCelulaTexto(tabela, "Id_Pagamento", PdfPCell.ALIGN_CENTER, true);
                    CriarCelulaTexto(tabela, "Id_Serviço", PdfPCell.ALIGN_CENTER, true);
                    CriarCelulaTexto(tabela, "Tipo", PdfPCell.ALIGN_CENTER, true);
                    CriarCelulaTexto(tabela, "Descrição", PdfPCell.ALIGN_CENTER, true);
                    CriarCelulaTexto(tabela, "Vencimento", PdfPCell.ALIGN_CENTER, true);
                    CriarCelulaTexto(tabela, "Valor", PdfPCell.ALIGN_CENTER, true);
                    CriarCelulaTexto(tabela, "Status", PdfPCell.ALIGN_CENTER, true);

                    conn = ConectarBanco();

                    string sql = "select IdPagamento, IdServ, tipo, descricao, vencimento, valor, status from tbpagamento where tipo='Receita' and status <> 'Oculto'";



                    MySqlDataAdapter comd = new MySqlDataAdapter(sql, conn);
                    DataTable dt = new DataTable();
                    comd.Fill(dt);


                    for (int i = 0; i < dtPagServ.Rows.Count; i++)
                    {
                        CriarCelulaTexto(tabela, dt.Rows[i].Field<Int32>("IdPagamento").ToString(), PdfPCell.ALIGN_CENTER);
                        CriarCelulaTexto(tabela, dt.Rows[i].Field<Int32?>("IdServ").ToString(), PdfPCell.ALIGN_CENTER);
                        CriarCelulaTexto(tabela, dt.Rows[i].Field<string>("tipo"), PdfPCell.ALIGN_CENTER);
                        CriarCelulaTexto(tabela, dt.Rows[i].Field<string>("descricao"), PdfPCell.ALIGN_CENTER);
                        CriarCelulaTexto(tabela, dt.Rows[i].Field<DateTime>("vencimento").ToString(), PdfPCell.ALIGN_CENTER);
                        CriarCelulaTexto(tabela, dt.Rows[i].Field<string>("valor"), PdfPCell.ALIGN_CENTER);
                        CriarCelulaTexto(tabela, dt.Rows[i].Field<string>("status"), PdfPCell.ALIGN_CENTER);
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
    }
}
