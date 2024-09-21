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
using Org.BouncyCastle.Crypto;

namespace Projeto_SGE_Testes
{
    public partial class ConsServicos : Form
    {
        int cell = 0;
        static BaseFont fonteBase = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);

        public string strConn;
        public MySqlConnection conn;
        public string merro;
        public string IdServ;

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

        private void bdpesquisaserv()
        {

            conn = ConectarBanco();

            string sql = "select * from tbservico where status <> 'Oculto'";

            MySqlDataAdapter comd = new MySqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            comd.Fill(dt);
            dtServ.DataSource = dt;
        }

        private void bdpesquisamsserv(string IDS)
        {
            conn = ConectarBanco();

            string sql = "select * from tbmsservico where IdServico='"+IDS+"'";

            MySqlDataAdapter comd = new MySqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            comd.Fill(dt);
            dtmsservico.DataSource = dt;
        }

        public ConsServicos()
        {
            InitializeComponent();

        }

        private void kryptonButton1_Click(object sender, EventArgs e)
        {
            CadServicos cadservicos = new CadServicos();
            cadservicos.ShowDialog();
            bdpesquisaserv();
        }

        private void kryptonButton2_Click(object sender, EventArgs e)
        {
            AltServicos altServicos = new AltServicos(IdServ);
            altServicos.ShowDialog();
            bdpesquisaserv();
        }

        private void ConsServicos_Load(object sender, EventArgs e)
        {
            bdpesquisaserv();
        }

        public void pesqminid()
        {
            conn = ConectarBanco();

            string sql = "select min(IdServico) from tbservico";

            MySqlCommand comd2 = new MySqlCommand(sql, conn);
            MySqlDataReader resul = comd2.ExecuteReader();
            if (resul.HasRows)
            {
                while (resul.Read())
                {
                    IdServ = Convert.ToString(resul["min(IdServico)"]);
                }
                comd2.Connection.Close();
            }
        }

        private void selectserv(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex == -1)
            {
                pesqminid();
            }
            else
            {
                IdServ = Convert.ToString(dtServ.Rows[e.RowIndex].Cells[0].Value);
                bdpesquisamsserv(IdServ);
            }
            cell = 1;
        }

        private void kryptonButton3_Click(object sender, EventArgs e)
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

        private void btnFinalizar_Click(object sender, EventArgs e)
        {
            conn = ConectarBanco();

            string d1, m1, a1;
            d1 = dtfinal.Value.Day.ToString();
            m1 = dtfinal.Value.Month.ToString();
            a1 = dtfinal.Value.Year.ToString();
            string DataFinal = a1 + "-" + m1 + "-" + d1;

            string status = "Finalizado";

            if (MessageBox.Show("Deseja mesmo finalizar esse serviço?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                string sql = "update tbservico set datafinal='"+DataFinal+"', status='"+ status +"' where IdServico='"+ IdServ +"'";

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
                    MessageBox.Show("Finalizado com Sucesso");
                    // Limpar_Campos();
                }
            }
        }

        private void ConsServicos_Activated(object sender, EventArgs e)
        {
            bdpesquisaserv();
        }

        private void ConsServicos_Enter(object sender, EventArgs e)
        {
            bdpesquisaserv();
        }

        private void kryptonButton1_Click_1(object sender, EventArgs e)
        {
            if (cell == 0)
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

                        var tit = new Paragraph("Relatório Serviços", ti);
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



                        CriarCelulaTexto(tabela, "Id_Servico", PdfPCell.ALIGN_CENTER, true);
                        CriarCelulaTexto(tabela, "Id_Obra", PdfPCell.ALIGN_CENTER, true);
                        CriarCelulaTexto(tabela, "Turno", PdfPCell.ALIGN_CENTER, true);
                        CriarCelulaTexto(tabela, "Detalhes", PdfPCell.ALIGN_CENTER, true);
                        CriarCelulaTexto(tabela, "Datainicio", PdfPCell.ALIGN_CENTER, true);
                        CriarCelulaTexto(tabela, "Datafinal", PdfPCell.ALIGN_CENTER, true);
                        CriarCelulaTexto(tabela, "Precoproduto", PdfPCell.ALIGN_CENTER, true);

                        conn = ConectarBanco();

                        string sql = "select * from tbservico where status <> 'Oculto'";



                        MySqlDataAdapter comd = new MySqlDataAdapter(sql, conn);
                        DataTable dt = new DataTable();
                        comd.Fill(dt);


                        for (int i = 0; i < dtServ.Rows.Count; i++)
                        {
                            CriarCelulaTexto(tabela, dt.Rows[i].Field<Int32>("IdServico").ToString(), PdfPCell.ALIGN_CENTER);
                            CriarCelulaTexto(tabela, dt.Rows[i].Field<Int32>("IdObra").ToString(), PdfPCell.ALIGN_CENTER);
                            CriarCelulaTexto(tabela, dt.Rows[i].Field<string>("turno"), PdfPCell.ALIGN_CENTER);
                            CriarCelulaTexto(tabela, dt.Rows[i].Field<string>("detalhes"), PdfPCell.ALIGN_CENTER);
                            CriarCelulaTexto(tabela, dt.Rows[i].Field<DateTime>("datainicio").ToString(), PdfPCell.ALIGN_CENTER);
                            CriarCelulaTexto(tabela, dt.Rows[i].Field<DateTime?>("datafinal").ToString(), PdfPCell.ALIGN_CENTER);
                            CriarCelulaTexto(tabela, dt.Rows[i].Field<string>("precoproduto"), PdfPCell.ALIGN_CENTER);
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
            else
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

                        var tit = new Paragraph("Relatório Serviços", ti);
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



                        CriarCelulaTexto(tabela, "Id_Servico", PdfPCell.ALIGN_CENTER, true);
                        CriarCelulaTexto(tabela, "Id_Obra", PdfPCell.ALIGN_CENTER, true);
                        CriarCelulaTexto(tabela, "Turno", PdfPCell.ALIGN_CENTER, true);
                        CriarCelulaTexto(tabela, "Detalhes", PdfPCell.ALIGN_CENTER, true);
                        CriarCelulaTexto(tabela, "Datainicio", PdfPCell.ALIGN_CENTER, true);
                        CriarCelulaTexto(tabela, "Datafinal", PdfPCell.ALIGN_CENTER, true);
                        CriarCelulaTexto(tabela, "Precoproduto", PdfPCell.ALIGN_CENTER, true);

                        conn = ConectarBanco();

                        string sql = "select * from tbservico where status <> 'Oculto'";



                        MySqlDataAdapter comd = new MySqlDataAdapter(sql, conn);
                        DataTable dt = new DataTable();
                        comd.Fill(dt);


                        for (int i = 0; i < dtServ.Rows.Count; i++)
                        {
                            CriarCelulaTexto(tabela, dt.Rows[i].Field<Int32>("IdServico").ToString(), PdfPCell.ALIGN_CENTER);
                            CriarCelulaTexto(tabela, dt.Rows[i].Field<Int32>("IdObra").ToString(), PdfPCell.ALIGN_CENTER);
                            CriarCelulaTexto(tabela, dt.Rows[i].Field<string>("turno"), PdfPCell.ALIGN_CENTER);
                            CriarCelulaTexto(tabela, dt.Rows[i].Field<string>("detalhes"), PdfPCell.ALIGN_CENTER);
                            CriarCelulaTexto(tabela, dt.Rows[i].Field<DateTime>("datainicio").ToString(), PdfPCell.ALIGN_CENTER);
                            CriarCelulaTexto(tabela, dt.Rows[i].Field<DateTime?>("datafinal").ToString(), PdfPCell.ALIGN_CENTER);
                            CriarCelulaTexto(tabela, dt.Rows[i].Field<string>("precoproduto"), PdfPCell.ALIGN_CENTER);
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


                // Relatóio 2



                using (SaveFileDialog sfd2 = new SaveFileDialog() { Filter = "PDF File|*.pdf", ValidateNames = true })
                {
                    if (sfd2.ShowDialog() == DialogResult.OK)
                    {

                        var pxPorMm = 72 / 25.2F;

                        var fonteParagrafo = new iTextSharp.text.Font(fonteBase, 11, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                        var fonteParagrafo2 = new iTextSharp.text.Font(fonteBase, 11, iTextSharp.text.Font.NORMAL, BaseColor.LIGHT_GRAY);
                        var fonteParagrafo3 = new iTextSharp.text.Font(fonteBase, 13, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);



                        var ti = new iTextSharp.text.Font(fonteBase, 20, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

                        var tit = new Paragraph("Relatório Produtos e Serviços", ti);
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



                        CriarCelulaTexto(tabela, "IdMS_Serviço", PdfPCell.ALIGN_CENTER, true);
                        CriarCelulaTexto(tabela, "Id_Serviço", PdfPCell.ALIGN_CENTER, true);
                        CriarCelulaTexto(tabela, "Id_Material", PdfPCell.ALIGN_CENTER, true);
                        CriarCelulaTexto(tabela, "IdLst_Serviço", PdfPCell.ALIGN_CENTER, true);
                        CriarCelulaTexto(tabela, "Nome Produto", PdfPCell.ALIGN_CENTER, true);
                        CriarCelulaTexto(tabela, "Quantidade", PdfPCell.ALIGN_CENTER, true);
                        CriarCelulaTexto(tabela, "Preço Unitário", PdfPCell.ALIGN_CENTER, true);


                        conn = ConectarBanco();



                        string sql2 = "select * from tbmsservico where IdServico='" + IdServ + "'";

                        MySqlDataAdapter comd2 = new MySqlDataAdapter(sql2, conn);
                        DataTable dt2 = new DataTable();
                        comd2.Fill(dt2);


                        for (int i = 0; i < dtmsservico.Rows.Count; i++)
                        {
                            CriarCelulaTexto(tabela, dt2.Rows[i].Field<Int32>("IdMSServico").ToString(), PdfPCell.ALIGN_CENTER);
                            CriarCelulaTexto(tabela, dt2.Rows[i].Field<Int32>("IdServico").ToString(), PdfPCell.ALIGN_CENTER);
                            CriarCelulaTexto(tabela, dt2.Rows[i].Field<Int32?>("IdMaterial").ToString(), PdfPCell.ALIGN_CENTER);
                            CriarCelulaTexto(tabela, dt2.Rows[i].Field<Int32?>("IdLstServico").ToString(), PdfPCell.ALIGN_CENTER);
                            CriarCelulaTexto(tabela, dt2.Rows[i].Field<string>("nomeproduto"), PdfPCell.ALIGN_CENTER);
                            CriarCelulaTexto(tabela, dt2.Rows[i].Field<Int32>("quantidade").ToString(), PdfPCell.ALIGN_CENTER);
                            CriarCelulaTexto(tabela, dt2.Rows[i].Field<string>("precounitario"), PdfPCell.ALIGN_CENTER);
                        }

                        iTextSharp.text.Document doc = new iTextSharp.text.Document(PageSize.A4, 15 * pxPorMm, 15 * pxPorMm, 15 * pxPorMm, 20 * pxPorMm);


                        iTextSharp.text.Image logo = iTextSharp.text.Image.GetInstance("img\\Empresa.png");
                        logo.Alignment = Element.ALIGN_CENTER;


                        try
                        {
                            PdfWriter.GetInstance(doc, new FileStream(sfd2.FileName, FileMode.Create));
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
                            doc.Close(); ;
                        }
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


    }
}
