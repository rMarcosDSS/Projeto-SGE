namespace Projeto_SGE_Testes
{
    partial class ConsMateriais
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConsMateriais));
            this.rbtnServico = new System.Windows.Forms.RadioButton();
            this.rbtnMaterial = new System.Windows.Forms.RadioButton();
            this.label11 = new System.Windows.Forms.Label();
            this.kryptonPanel1 = new ComponentFactory.Krypton.Toolkit.KryptonPanel();
            this.btnAlterar = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.btnCadastrar = new ComponentFactory.Krypton.Toolkit.KryptonButton();
            this.lblPreco = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.lblNome = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cbNome = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.cbNomeServ = new ComponentFactory.Krypton.Toolkit.KryptonComboBox();
            this.txtQtd = new ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtrs = new System.Windows.Forms.Label();
            this.precoServ = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            this.txtPreco = new ComponentFactory.Krypton.Toolkit.KryptonTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).BeginInit();
            this.kryptonPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbNome)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbNomeServ)).BeginInit();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // rbtnServico
            // 
            this.rbtnServico.AutoSize = true;
            this.rbtnServico.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.rbtnServico.Location = new System.Drawing.Point(213, 123);
            this.rbtnServico.Name = "rbtnServico";
            this.rbtnServico.Size = new System.Drawing.Size(84, 25);
            this.rbtnServico.TabIndex = 2;
            this.rbtnServico.Text = "Serviço";
            this.rbtnServico.UseVisualStyleBackColor = true;
            this.rbtnServico.CheckedChanged += new System.EventHandler(this.rbtnServico_CheckedChanged);
            // 
            // rbtnMaterial
            // 
            this.rbtnMaterial.AutoSize = true;
            this.rbtnMaterial.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.rbtnMaterial.Location = new System.Drawing.Point(76, 123);
            this.rbtnMaterial.Name = "rbtnMaterial";
            this.rbtnMaterial.Size = new System.Drawing.Size(93, 25);
            this.rbtnMaterial.TabIndex = 1;
            this.rbtnMaterial.Text = "Material";
            this.rbtnMaterial.UseVisualStyleBackColor = true;
            this.rbtnMaterial.CheckedChanged += new System.EventHandler(this.rbtnMaterial_CheckedChanged);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label11.Location = new System.Drawing.Point(45, 99);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(201, 21);
            this.label11.TabIndex = 104;
            this.label11.Text = "O que deseja adicionar?";
            // 
            // kryptonPanel1
            // 
            this.kryptonPanel1.AutoSize = true;
            this.kryptonPanel1.Controls.Add(this.btnAlterar);
            this.kryptonPanel1.Controls.Add(this.btnCadastrar);
            this.kryptonPanel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.kryptonPanel1.Location = new System.Drawing.Point(0, 486);
            this.kryptonPanel1.Name = "kryptonPanel1";
            this.kryptonPanel1.PanelBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.PanelAlternate;
            this.kryptonPanel1.Size = new System.Drawing.Size(910, 59);
            this.kryptonPanel1.StateCommon.Color1 = System.Drawing.Color.Gold;
            this.kryptonPanel1.TabIndex = 103;
            // 
            // btnAlterar
            // 
            this.btnAlterar.ButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Custom2;
            this.btnAlterar.Location = new System.Drawing.Point(608, 3);
            this.btnAlterar.Name = "btnAlterar";
            this.btnAlterar.Size = new System.Drawing.Size(142, 53);
            this.btnAlterar.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.btnAlterar.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.btnAlterar.StateCommon.Back.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btnAlterar.StateCommon.Border.Color1 = System.Drawing.Color.Gold;
            this.btnAlterar.StateCommon.Border.Color2 = System.Drawing.Color.Gold;
            this.btnAlterar.StateCommon.Border.ColorAngle = 45F;
            this.btnAlterar.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnAlterar.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btnAlterar.StateCommon.Border.Rounding = 20;
            this.btnAlterar.StateCommon.Border.Width = 1;
            this.btnAlterar.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.Black;
            this.btnAlterar.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.Black;
            this.btnAlterar.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnAlterar.StateCommon.Content.ShortText.Hint = ComponentFactory.Krypton.Toolkit.PaletteTextHint.AntiAlias;
            this.btnAlterar.TabIndex = 5;
            this.btnAlterar.Values.Text = "Alterar";
            this.btnAlterar.Click += new System.EventHandler(this.btnCancelar_Click);
            // 
            // btnCadastrar
            // 
            this.btnCadastrar.Location = new System.Drawing.Point(756, 3);
            this.btnCadastrar.Name = "btnCadastrar";
            this.btnCadastrar.Size = new System.Drawing.Size(142, 53);
            this.btnCadastrar.StateCommon.Back.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.btnCadastrar.StateCommon.Back.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.btnCadastrar.StateCommon.Back.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btnCadastrar.StateCommon.Border.Color1 = System.Drawing.Color.Gold;
            this.btnCadastrar.StateCommon.Border.Color2 = System.Drawing.Color.Gold;
            this.btnCadastrar.StateCommon.Border.ColorAngle = 45F;
            this.btnCadastrar.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.btnCadastrar.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.btnCadastrar.StateCommon.Border.Rounding = 20;
            this.btnCadastrar.StateCommon.Border.Width = 1;
            this.btnCadastrar.StateCommon.Content.ShortText.Color1 = System.Drawing.Color.Black;
            this.btnCadastrar.StateCommon.Content.ShortText.Color2 = System.Drawing.Color.Black;
            this.btnCadastrar.StateCommon.Content.ShortText.Font = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCadastrar.StateCommon.Content.ShortText.Hint = ComponentFactory.Krypton.Toolkit.PaletteTextHint.AntiAlias;
            this.btnCadastrar.TabIndex = 4;
            this.btnCadastrar.Values.Text = "Cadastrar";
            this.btnCadastrar.Click += new System.EventHandler(this.btnCadastrar_Click);
            // 
            // lblPreco
            // 
            this.lblPreco.AutoSize = true;
            this.lblPreco.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPreco.Location = new System.Drawing.Point(544, 197);
            this.lblPreco.Name = "lblPreco";
            this.lblPreco.Size = new System.Drawing.Size(54, 21);
            this.lblPreco.TabIndex = 101;
            this.lblPreco.Text = "Preço";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(12, 6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(30, 30);
            this.pictureBox1.TabIndex = 100;
            this.pictureBox1.TabStop = false;
            // 
            // lblNome
            // 
            this.lblNome.AutoSize = true;
            this.lblNome.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblNome.Location = new System.Drawing.Point(45, 197);
            this.lblNome.Name = "lblNome";
            this.lblNome.Size = new System.Drawing.Size(57, 21);
            this.lblNome.TabIndex = 99;
            this.lblNome.Text = "Nome";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Century Gothic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(48, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(193, 23);
            this.label1.TabIndex = 98;
            this.label1.Text = "Materiais e Serviços";
            // 
            // cbNome
            // 
            this.cbNome.DropBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ButtonCustom1;
            this.cbNome.DropButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Custom1;
            this.cbNome.DropDownWidth = 602;
            this.cbNome.Location = new System.Drawing.Point(76, 221);
            this.cbNome.Name = "cbNome";
            this.cbNome.Size = new System.Drawing.Size(379, 37);
            this.cbNome.StateActive.ComboBox.Border.Color1 = System.Drawing.Color.Gold;
            this.cbNome.StateActive.ComboBox.Border.Color2 = System.Drawing.Color.Gold;
            this.cbNome.StateActive.ComboBox.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.cbNome.StateCommon.ComboBox.Back.Color1 = System.Drawing.Color.White;
            this.cbNome.StateCommon.ComboBox.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.cbNome.StateCommon.ComboBox.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(217)))), ((int)(((byte)(217)))), ((int)(((byte)(217)))));
            this.cbNome.StateCommon.ComboBox.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.cbNome.StateCommon.ComboBox.Border.Rounding = 20;
            this.cbNome.StateCommon.ComboBox.Border.Width = 1;
            this.cbNome.StateCommon.ComboBox.Content.Color1 = System.Drawing.Color.Black;
            this.cbNome.StateCommon.ComboBox.Content.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.cbNome.TabIndex = 3;
            this.cbNome.Text = "Selecione o material";
            this.cbNome.SelectedIndexChanged += new System.EventHandler(this.cbNome_SelectedIndexChanged);
            // 
            // cbNomeServ
            // 
            this.cbNomeServ.DropBackStyle = ComponentFactory.Krypton.Toolkit.PaletteBackStyle.ButtonCustom1;
            this.cbNomeServ.DropButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Custom1;
            this.cbNomeServ.DropDownWidth = 602;
            this.cbNomeServ.Location = new System.Drawing.Point(76, 221);
            this.cbNomeServ.Name = "cbNomeServ";
            this.cbNomeServ.Size = new System.Drawing.Size(379, 37);
            this.cbNomeServ.StateCommon.ComboBox.Back.Color1 = System.Drawing.Color.White;
            this.cbNomeServ.StateCommon.ComboBox.Border.Color1 = System.Drawing.Color.Gold;
            this.cbNomeServ.StateCommon.ComboBox.Border.Color2 = System.Drawing.Color.Gold;
            this.cbNomeServ.StateCommon.ComboBox.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.cbNomeServ.StateCommon.ComboBox.Border.Rounding = 20;
            this.cbNomeServ.StateCommon.ComboBox.Border.Width = 1;
            this.cbNomeServ.StateCommon.ComboBox.Content.Color1 = System.Drawing.Color.Black;
            this.cbNomeServ.StateCommon.ComboBox.Content.Font = new System.Drawing.Font("Century Gothic", 12F);
            this.cbNomeServ.TabIndex = 109;
            this.cbNomeServ.Text = "Selecione o Serviço";
            this.cbNomeServ.SelectedIndexChanged += new System.EventHandler(this.cbNomeServ_SelectedIndexChanged);
            // 
            // txtQtd
            // 
            this.txtQtd.Enabled = false;
            this.txtQtd.Location = new System.Drawing.Point(575, 293);
            this.txtQtd.Name = "txtQtd";
            this.txtQtd.Size = new System.Drawing.Size(181, 38);
            this.txtQtd.StateActive.Border.Color1 = System.Drawing.Color.Gold;
            this.txtQtd.StateActive.Border.Color2 = System.Drawing.Color.Gold;
            this.txtQtd.StateActive.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.txtQtd.StateCommon.Back.Color1 = System.Drawing.Color.White;
            this.txtQtd.StateCommon.Border.Color1 = System.Drawing.Color.Gold;
            this.txtQtd.StateCommon.Border.Color2 = System.Drawing.Color.Gold;
            this.txtQtd.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.txtQtd.StateCommon.Border.Rounding = 20;
            this.txtQtd.StateCommon.Border.Width = 1;
            this.txtQtd.StateCommon.Content.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtQtd.TabIndex = 111;
            this.txtQtd.UpDownButtonStyle = ComponentFactory.Krypton.Toolkit.ButtonStyle.Custom1;
            this.txtQtd.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.txtQtd.ValueChanged += new System.EventHandler(this.txtQtd_ValueChanged);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.BackColor = System.Drawing.Color.Transparent;
            this.label12.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.Location = new System.Drawing.Point(544, 269);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(108, 21);
            this.label12.TabIndex = 112;
            this.label12.Text = "Quantidade";
            this.label12.Click += new System.EventHandler(this.label12_Click);
            // 
            // panel1
            // 
            this.panel1.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("panel1.BackgroundImage")));
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.panel1.Controls.Add(this.txtrs);
            this.panel1.Controls.Add(this.precoServ);
            this.panel1.Controls.Add(this.txtPreco);
            this.panel1.Controls.Add(this.label12);
            this.panel1.Controls.Add(this.cbNome);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(910, 545);
            this.panel1.TabIndex = 113;
            // 
            // txtrs
            // 
            this.txtrs.AutoSize = true;
            this.txtrs.BackColor = System.Drawing.Color.White;
            this.txtrs.Enabled = false;
            this.txtrs.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtrs.Location = new System.Drawing.Point(582, 229);
            this.txtrs.Name = "txtrs";
            this.txtrs.Size = new System.Drawing.Size(29, 21);
            this.txtrs.TabIndex = 114;
            this.txtrs.Text = "R$";
            // 
            // precoServ
            // 
            this.precoServ.Enabled = false;
            this.precoServ.Location = new System.Drawing.Point(575, 221);
            this.precoServ.Name = "precoServ";
            this.precoServ.Size = new System.Drawing.Size(198, 34);
            this.precoServ.StateActive.Border.Color1 = System.Drawing.Color.Gold;
            this.precoServ.StateActive.Border.Color2 = System.Drawing.Color.Gold;
            this.precoServ.StateActive.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.precoServ.StateCommon.Back.Color1 = System.Drawing.Color.White;
            this.precoServ.StateCommon.Border.Color1 = System.Drawing.Color.Gold;
            this.precoServ.StateCommon.Border.Color2 = System.Drawing.Color.Gold;
            this.precoServ.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.precoServ.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.precoServ.StateCommon.Border.Rounding = 20;
            this.precoServ.StateCommon.Border.Width = 1;
            this.precoServ.StateCommon.Content.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.precoServ.StateCommon.Content.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.precoServ.StateNormal.Back.Color1 = System.Drawing.Color.White;
            this.precoServ.StateNormal.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.precoServ.StateNormal.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.precoServ.StateNormal.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.precoServ.StateNormal.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.None;
            this.precoServ.StateNormal.Border.Rounding = 12;
            this.precoServ.StateNormal.Border.Width = 1;
            this.precoServ.TabIndex = 116;
            this.precoServ.Tag = "";
            this.precoServ.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // txtPreco
            // 
            this.txtPreco.Enabled = false;
            this.txtPreco.Location = new System.Drawing.Point(575, 221);
            this.txtPreco.Name = "txtPreco";
            this.txtPreco.Size = new System.Drawing.Size(198, 34);
            this.txtPreco.StateActive.Border.Color1 = System.Drawing.Color.Gold;
            this.txtPreco.StateActive.Border.Color2 = System.Drawing.Color.Gold;
            this.txtPreco.StateActive.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.txtPreco.StateCommon.Back.Color1 = System.Drawing.Color.White;
            this.txtPreco.StateCommon.Border.Color1 = System.Drawing.Color.Gold;
            this.txtPreco.StateCommon.Border.Color2 = System.Drawing.Color.Gold;
            this.txtPreco.StateCommon.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.txtPreco.StateCommon.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.AntiAlias;
            this.txtPreco.StateCommon.Border.Rounding = 20;
            this.txtPreco.StateCommon.Border.Width = 1;
            this.txtPreco.StateCommon.Content.Font = new System.Drawing.Font("Century Gothic", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPreco.StateCommon.Content.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.txtPreco.StateNormal.Back.Color1 = System.Drawing.Color.White;
            this.txtPreco.StateNormal.Border.Color1 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.txtPreco.StateNormal.Border.Color2 = System.Drawing.Color.FromArgb(((int)(((byte)(239)))), ((int)(((byte)(239)))), ((int)(((byte)(239)))));
            this.txtPreco.StateNormal.Border.DrawBorders = ((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders)((((ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Top | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Bottom) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Left) 
            | ComponentFactory.Krypton.Toolkit.PaletteDrawBorders.Right)));
            this.txtPreco.StateNormal.Border.GraphicsHint = ComponentFactory.Krypton.Toolkit.PaletteGraphicsHint.None;
            this.txtPreco.StateNormal.Border.Rounding = 12;
            this.txtPreco.StateNormal.Border.Width = 1;
            this.txtPreco.TabIndex = 115;
            this.txtPreco.Tag = "";
            this.txtPreco.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // ConsMateriais
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(910, 545);
            this.Controls.Add(this.txtQtd);
            this.Controls.Add(this.rbtnServico);
            this.Controls.Add(this.rbtnMaterial);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.kryptonPanel1);
            this.Controls.Add(this.lblPreco);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.lblNome);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbNomeServ);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ConsMateriais";
            this.Text = "ConsMateriais";
            this.Load += new System.EventHandler(this.ConsMateriais_Load);
            ((System.ComponentModel.ISupportInitialize)(this.kryptonPanel1)).EndInit();
            this.kryptonPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbNome)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cbNomeServ)).EndInit();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RadioButton rbtnServico;
        private System.Windows.Forms.RadioButton rbtnMaterial;
        private System.Windows.Forms.Label label11;
        private ComponentFactory.Krypton.Toolkit.KryptonPanel kryptonPanel1;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnAlterar;
        private ComponentFactory.Krypton.Toolkit.KryptonButton btnCadastrar;
        private System.Windows.Forms.Label lblPreco;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblNome;
        private System.Windows.Forms.Label label1;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbNome;
        private ComponentFactory.Krypton.Toolkit.KryptonComboBox cbNomeServ;
        private ComponentFactory.Krypton.Toolkit.KryptonNumericUpDown txtQtd;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label txtrs;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox precoServ;
        private ComponentFactory.Krypton.Toolkit.KryptonTextBox txtPreco;
    }
}