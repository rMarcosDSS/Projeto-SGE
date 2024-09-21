using MySql.Data.MySqlClient;
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
using System.Xml.Linq;
using MySqlX.XDevAPI.Relational;
using iTextSharp.text.pdf;
using iTextSharp.text;
using System.Security.Cryptography;
using System.Windows.Forms.VisualStyles;
using System.Reflection.Emit;

namespace Projeto_SGE_Testes
{
    public partial class ConsOrcamentos : Form
    {
        int cell = 0;
        static BaseFont fonteBase = BaseFont.CreateFont(BaseFont.HELVETICA, BaseFont.CP1252, false);


        public string strConn;
        public MySqlConnection conn;
        public string merro;
        public string IdOrcamento;
        public string IdServ;
        public int vmmat;

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

        public ConsOrcamentos()
        {
            InitializeComponent();
         

        }

        public void dtOrcamento()
        {
            conn = ConectarBanco();

            string sql = "select IdOrcamento, IdCliente, nomecliente, cpf, cnpj, total from tborcamento";

            MySqlDataAdapter comd = new MySqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            comd.Fill(dt);
            dtgOrca.DataSource = dt;
        }

        public void dtMSOrcamento(string idmsorca)
        {
            string sql = "select * from tbmsorcamento where IdOrcamento='" + idmsorca + "'";

            

            MySqlDataAdapter comd = new MySqlDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            comd.Fill(dt);
            dtgMSorca.DataSource = dt;
        }

        private void ConsOrcamentos_Load(object sender, EventArgs e)
        {
            dtOrcamento();
        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            CadOrcamentos cadOrcamentos = new CadOrcamentos();
            cadOrcamentos.ShowDialog();
            dtOrcamento();
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            AltOrcamento altOrcamentos = new AltOrcamento(IdOrcamento);
            altOrcamentos.ShowDialog();
            dtOrcamento();
        }

        public void pesqminid()
        {
            conn = ConectarBanco();

            string sql = "select min(IdOrcamento) from tborcamento";

            MySqlCommand comd2 = new MySqlCommand(sql, conn);
            MySqlDataReader resul = comd2.ExecuteReader();
            if (resul.HasRows)
            {
                while (resul.Read())
                {
                    IdOrcamento = Convert.ToString(resul["min(IdOrcamento)"]);
                }
                comd2.Connection.Close();
            }
        }

        private void dtgOrca_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            cell = 1;
            if (e.RowIndex == -1)
            {
                pesqminid();
            }
            else
            {
                IdOrcamento = Convert.ToString(dtgOrca.Rows[e.RowIndex].Cells[0].Value);
                dtMSOrcamento(IdOrcamento);
            }
        }


        private void dtgOrca_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void kryptonButton1_Click( object sender, EventArgs e)
        {
            if (cell == 0){
                using (SaveFileDialog sfd = new SaveFileDialog() { Filter = "PDF File|*.pdf", ValidateNames = true })
                {
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        var pxPorMm = 72 / 25.2F;

                        var fonteParagrafo = new iTextSharp.text.Font(fonteBase, 11, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);
                        var fonteParagrafo2 = new iTextSharp.text.Font(fonteBase, 11, iTextSharp.text.Font.NORMAL, BaseColor.LIGHT_GRAY);
                        var fonteParagrafo3 = new iTextSharp.text.Font(fonteBase, 13, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);



                        var ti = new iTextSharp.text.Font(fonteBase, 20, iTextSharp.text.Font.NORMAL, BaseColor.BLACK);

                        var tit = new Paragraph("Relatório Orçamentos", ti);
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



                        PdfPTable tabela = new PdfPTable(6);
                        float[] larguras = { 1.3f, 1.3f, 1.5f, 1.5f, 1.5f, 1f };
                        tabela.SetWidths(larguras);
                        tabela.DefaultCell.BorderWidth = 0;
                        tabela.WidthPercentage = 100;
                        tabela.DefaultCell.FixedHeight = 30;


                        CriarCelulaTexto(tabela, "Id_Orcamento", PdfPCell.ALIGN_CENTER, true);
                        CriarCelulaTexto(tabela, "Id_Cliente", PdfPCell.ALIGN_CENTER, true);
                        CriarCelulaTexto(tabela, "Nome Cliente", PdfPCell.ALIGN_CENTER, true);
                        CriarCelulaTexto(tabela, "CPF", PdfPCell.ALIGN_CENTER, true);
                        CriarCelulaTexto(tabela, "CNPJ", PdfPCell.ALIGN_CENTER, true);
                        CriarCelulaTexto(tabela, "Total", PdfPCell.ALIGN_CENTER, true);

                        conn = ConectarBanco();

                        string sql = "select IdOrcamento, IdCliente, nomecliente, cpf, cnpj, total from tborcamento where status <> 'Oculto'";



                        MySqlDataAdapter comd = new MySqlDataAdapter(sql, conn);
                        DataTable dt = new DataTable();
                        comd.Fill(dt);


                        for (int i = 0; i < dtgOrca.Rows.Count; i++)
                        {
                            CriarCelulaTexto(tabela, dt.Rows[i].Field<Int32>("IdOrcamento").ToString(), PdfPCell.ALIGN_CENTER);
                            CriarCelulaTexto(tabela, dt.Rows[i].Field<Int32?>("IdCliente").ToString(), PdfPCell.ALIGN_CENTER);
                            CriarCelulaTexto(tabela, dt.Rows[i].Field<string>("nomecliente"), PdfPCell.ALIGN_LEFT);
                            CriarCelulaTexto(tabela, dt.Rows[i].Field<string>("cpf"), PdfPCell.ALIGN_LEFT);
                            CriarCelulaTexto(tabela, dt.Rows[i].Field<string>("cnpj"), PdfPCell.ALIGN_CENTER);
                            CriarCelulaTexto(tabela, dt.Rows[i].Field<string>("total").ToString(), PdfPCell.ALIGN_CENTER);
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
            } else
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

                        var tit = new Paragraph("Relatório Orçamentos", ti);
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



                        PdfPTable tabela = new PdfPTable(6);
                        float[] larguras = { 1.3f, 1.3f, 1.5f, 1.5f, 1.5f, 1f };
                        tabela.SetWidths(larguras);
                        tabela.DefaultCell.BorderWidth = 0;
                        tabela.WidthPercentage = 100;
                        tabela.DefaultCell.FixedHeight = 30;


                        CriarCelulaTexto(tabela, "Id_Orcamento", PdfPCell.ALIGN_CENTER, true);
                        CriarCelulaTexto(tabela, "Id_Cliente", PdfPCell.ALIGN_CENTER, true);
                        CriarCelulaTexto(tabela, "Nome Cliente", PdfPCell.ALIGN_CENTER, true);
                        CriarCelulaTexto(tabela, "CPF", PdfPCell.ALIGN_CENTER, true);
                        CriarCelulaTexto(tabela, "CNPJ", PdfPCell.ALIGN_CENTER, true);
                        CriarCelulaTexto(tabela, "Total", PdfPCell.ALIGN_CENTER, true);

                        conn = ConectarBanco();

                        string sql = "select IdOrcamento, IdCliente, nomecliente, cpf, cnpj, total from tborcamento where status <> 'Oculto'";



                        MySqlDataAdapter comd = new MySqlDataAdapter(sql, conn);
                        DataTable dt = new DataTable();
                        comd.Fill(dt);


                        for (int i = 0; i < dtgOrca.Rows.Count; i++)
                        {
                            CriarCelulaTexto(tabela, dt.Rows[i].Field<Int32>("IdOrcamento").ToString(), PdfPCell.ALIGN_CENTER);
                            CriarCelulaTexto(tabela, dt.Rows[i].Field<Int32?>("IdCliente").ToString(), PdfPCell.ALIGN_CENTER);
                            CriarCelulaTexto(tabela, dt.Rows[i].Field<string>("nomecliente"), PdfPCell.ALIGN_LEFT);
                            CriarCelulaTexto(tabela, dt.Rows[i].Field<string>("cpf"), PdfPCell.ALIGN_LEFT);
                            CriarCelulaTexto(tabela, dt.Rows[i].Field<string>("cnpj"), PdfPCell.ALIGN_CENTER);
                            CriarCelulaTexto(tabela, dt.Rows[i].Field<string>("total").ToString(), PdfPCell.ALIGN_CENTER);
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

                        var tit = new Paragraph("Relatório MS Orçamentos", ti);
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


                        PdfPTable tabela = new PdfPTable(8);
                        float[] larguras = { 1.3f, 1.3f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f, 1.5f };
                        tabela.SetWidths(larguras);
                        tabela.DefaultCell.BorderWidth = 0;
                        tabela.WidthPercentage = 100;
                        tabela.DefaultCell.FixedHeight = 30;



                        CriarCelulaTexto(tabela, "IdMS_Orcamento", PdfPCell.ALIGN_CENTER, true);
                        CriarCelulaTexto(tabela, "Id_Orcamento", PdfPCell.ALIGN_CENTER, true);
                        CriarCelulaTexto(tabela, "Id_Material", PdfPCell.ALIGN_CENTER, true);
                        CriarCelulaTexto(tabela, "Id_Serviço", PdfPCell.ALIGN_CENTER, true);
                        CriarCelulaTexto(tabela, "Nome Produto", PdfPCell.ALIGN_CENTER, true);
                        CriarCelulaTexto(tabela, "Quantidade", PdfPCell.ALIGN_CENTER, true);
                        CriarCelulaTexto(tabela, "Preço Unitário", PdfPCell.ALIGN_CENTER, true);
                        CriarCelulaTexto(tabela, "Total", PdfPCell.ALIGN_CENTER, true);

                        conn = ConectarBanco();



                        string sql2 = "select * from tbmsorcamento where IdOrcamento='" + IdOrcamento + "'";

                        MySqlDataAdapter comd2 = new MySqlDataAdapter(sql2, conn);
                        DataTable dt2 = new DataTable();
                        comd2.Fill(dt2);


                        for (int i = 0; i < dtgMSorca.Rows.Count; i++)
                        {
                            CriarCelulaTexto(tabela, dt2.Rows[i].Field<Int32>("IdMSOrcamento").ToString(), PdfPCell.ALIGN_CENTER);
                            CriarCelulaTexto(tabela, dt2.Rows[i].Field<Int32>("IdOrcamento").ToString(), PdfPCell.ALIGN_CENTER);
                            CriarCelulaTexto(tabela, dt2.Rows[i].Field<Int32?>("IdMaterial").ToString(), PdfPCell.ALIGN_CENTER);
                            CriarCelulaTexto(tabela, dt2.Rows[i].Field<Int32?>("IdServico").ToString(), PdfPCell.ALIGN_CENTER);
                            CriarCelulaTexto(tabela, dt2.Rows[i].Field<string>("nomeproduto"), PdfPCell.ALIGN_CENTER);
                            CriarCelulaTexto(tabela, dt2.Rows[i].Field<Int32>("quantidade").ToString(), PdfPCell.ALIGN_CENTER);
                            CriarCelulaTexto(tabela, dt2.Rows[i].Field<string>("precounitario"), PdfPCell.ALIGN_CENTER);
                            CriarCelulaTexto(tabela, dt2.Rows[i].Field<string>("precototal"), PdfPCell.ALIGN_CENTER);
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
            tabela.AddCell(celula1);
        }



        private void dtgOrca_MouseEnter(object sender, EventArgs e)
        {
        
        }

        private void dtgMSorca_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dtgMSorca_MouseEnter(object sender, EventArgs e)
        {

        }


        public void descobriridserv()
        {
            conn = ConectarBanco();

            string sql2 = "select max(IdServico) from tbservico";

            MySqlCommand comd2 = new MySqlCommand(sql2, conn);
            MySqlDataReader resul = comd2.ExecuteReader();
            if (resul.HasRows)
            {
                while (resul.Read())
                {
                    IdServ = Convert.ToString(resul["max(IdServico)"]);
                    if (String.IsNullOrEmpty(IdServ))
                    {
                        IdServ = "1";
                    }
                    else
                    {
                        IdServ = Convert.ToString(Convert.ToInt16(IdServ) + 1);
                    }
                }
                comd2.Connection.Close();
            }
        }

        public void descquantestq(string idmat)
        {
            conn = ConectarBanco();
            string sql = "select * from tbmateriais where (IdMateriais ='" + idmat + "')";
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
                        vmmat = Convert.ToInt32(resul["quant"]);
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

        public void alterarestoque(string idmat, string quant)
        {
            conn = ConectarBanco();

            int nquant = Convert.ToInt32(vmmat) - Convert.ToInt32(quant);

            string sql1 = "update tbmateriais set quant='" + nquant + "' where IdMateriais='" + idmat + "'";
            MySqlCommand comd1 = new MySqlCommand(sql1, conn);

            if (merro == "true")
            {
                MessageBox.Show("Erro na conexão com o banco de dados");
                Application.Exit();
            }
            else
            {
                comd1.ExecuteNonQuery();
                comd1.Connection.Close();
                MessageBox.Show("Alterado com Sucesso");
            }
        }


        public void contratarorca()
        {
            conn = ConectarBanco();

            if (MessageBox.Show("Deseja mesmo contratar este orçamento?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
            {
                string sql = "select IdMaterial, IdServico, nomeproduto, quantidade, precounitario, precototal from tbmsorcamento where IdOrcamento='" + IdOrcamento + "'";

                MySqlDataAdapter comd = new MySqlDataAdapter(sql, conn);
                DataTable dt2 = new DataTable();
                comd.Fill(dt2);


                for (int i = 0; i < dtgMSorca.Rows.Count; i++)
                {
                    string idmat = (dt2.Rows[i].Field<Int32?>("IdMaterial").ToString());
                    string idlstse = (dt2.Rows[i].Field<Int32?>("IdServico").ToString());
                    string nomeprod = (dt2.Rows[i].Field<string>("nomeproduto"));
                    string quant = (dt2.Rows[i].Field<Int32>("quantidade").ToString());
                    string precounit = (dt2.Rows[i].Field<string>("precounitario"));
                    string total = (dt2.Rows[i].Field<string>("precototal"));

                    if (String.IsNullOrEmpty(idmat))
                    {
                        conn = ConectarBanco();
                        string sql2 = "insert into tbmsservico (IdServico, IdLstServico, nomeproduto, quantidade, precounitario, precototal) values ('" + IdServ + "','" + idlstse + "','" + nomeprod + "','" + quant + "','" + precounit + "','" + total + "')";

                        MySqlCommand comd2 = new MySqlCommand(sql2, conn);

                        if (merro == "true")
                        {
                            MessageBox.Show("Erro na conexão com o banco de dados");
                            Application.Exit();
                        }
                        else
                        {
                            comd2.ExecuteNonQuery();
                            comd2.Connection.Close();
                            MessageBox.Show("Cadastrado com Sucesso");
                        }

                    }
                    else if (String.IsNullOrEmpty(idlstse))
                    {
                        conn = ConectarBanco();
                        string sql2 = "insert into tbmsservico (IdServico, IdMaterial, nomeproduto, quantidade, precounitario, precototal) values ('" + IdServ + "','" + idmat + "','" + nomeprod + "','" + quant + "','" + precounit + "','" + total + "')";

                        MySqlCommand comd2 = new MySqlCommand(sql2, conn);

                        if (merro == "true")
                        {
                            MessageBox.Show("Erro na conexão com o banco de dados");
                            Application.Exit();
                        }
                        else
                        {
                            comd2.ExecuteNonQuery();
                            comd2.Connection.Close();
                            MessageBox.Show("Cadastrado com Sucesso");
                            descquantestq(idmat);
                            alterarestoque(idmat, quant);
                        }

                    }


                }

                CadServicos cadservico = new CadServicos();
                cadservico.ShowDialog();
            }
        }

        private void btnContratar_Click(object sender, EventArgs e)
        {
            descobriridserv();
            contratarorca();
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }
    }
}
