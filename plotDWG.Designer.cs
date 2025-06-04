namespace plotDWG
{
    partial class plotDWG
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(plotDWG));
            addDrawingText = new Label();
            layoutTypeText = new Label();
            outputFolderText = new Label();
            dwgBrowseBtn = new Button();
            manageButton = new Button();
            layoutSizeDropDownBtn = new ComboBox();
            layoutOrientationDropDownBtn = new ComboBox();
            outputBrowseBtn = new Button();
            prefixCheckBtn = new CheckBox();
            suffixCheckBtn = new CheckBox();
            prefixEntry = new TextBox();
            suffixEntry = new TextBox();
            lauchBtn = new Button();
            ctbLabel = new Label();
            ctbBrowseBtn = new Button();
            ctbDropBtn = new ComboBox();
            checkBox1 = new CheckBox();
            SuspendLayout();
            // 
            // addDrawingText
            // 
            addDrawingText.AutoSize = true;
            addDrawingText.BackColor = Color.FromArgb(64, 64, 64);
            addDrawingText.Font = new Font("Arial Narrow", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            addDrawingText.ForeColor = Color.GhostWhite;
            addDrawingText.Location = new Point(12, 29);
            addDrawingText.Name = "addDrawingText";
            addDrawingText.Size = new Size(145, 24);
            addDrawingText.TabIndex = 0;
            addDrawingText.Text = "ADD DRAWINGS:";
            // 
            // layoutTypeText
            // 
            layoutTypeText.AutoSize = true;
            layoutTypeText.BackColor = Color.FromArgb(64, 64, 64);
            layoutTypeText.Font = new Font("Arial Narrow", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            layoutTypeText.ForeColor = Color.GhostWhite;
            layoutTypeText.Location = new Point(12, 80);
            layoutTypeText.Name = "layoutTypeText";
            layoutTypeText.Size = new Size(128, 24);
            layoutTypeText.TabIndex = 1;
            layoutTypeText.Text = "LAYOUT TYPE:";
            // 
            // outputFolderText
            // 
            outputFolderText.AutoSize = true;
            outputFolderText.BackColor = Color.FromArgb(64, 64, 64);
            outputFolderText.Font = new Font("Arial Narrow", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            outputFolderText.ForeColor = Color.GhostWhite;
            outputFolderText.Location = new Point(12, 135);
            outputFolderText.Name = "outputFolderText";
            outputFolderText.Size = new Size(156, 24);
            outputFolderText.TabIndex = 2;
            outputFolderText.Text = "OUTPUT FOLDER:";
            // 
            // dwgBrowseBtn
            // 
            dwgBrowseBtn.BackColor = Color.SkyBlue;
            dwgBrowseBtn.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            dwgBrowseBtn.ForeColor = Color.Black;
            dwgBrowseBtn.Location = new Point(174, 24);
            dwgBrowseBtn.Name = "dwgBrowseBtn";
            dwgBrowseBtn.Size = new Size(95, 36);
            dwgBrowseBtn.TabIndex = 5;
            dwgBrowseBtn.Text = "BROWSE";
            dwgBrowseBtn.UseVisualStyleBackColor = false;
            dwgBrowseBtn.Click += dwgBrowseBtn_Click;
            // 
            // manageButton
            // 
            manageButton.BackColor = Color.SkyBlue;
            manageButton.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            manageButton.ForeColor = Color.Black;
            manageButton.Location = new Point(275, 24);
            manageButton.Name = "manageButton";
            manageButton.Size = new Size(138, 36);
            manageButton.TabIndex = 6;
            manageButton.Text = "MANAGE";
            manageButton.UseVisualStyleBackColor = false;
            manageButton.Click += manageButton_Click;
            // 
            // layoutSizeDropDownBtn
            // 
            layoutSizeDropDownBtn.BackColor = Color.SkyBlue;
            layoutSizeDropDownBtn.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            layoutSizeDropDownBtn.FormattingEnabled = true;
            layoutSizeDropDownBtn.Location = new Point(174, 77);
            layoutSizeDropDownBtn.Name = "layoutSizeDropDownBtn";
            layoutSizeDropDownBtn.Size = new Size(95, 27);
            layoutSizeDropDownBtn.TabIndex = 7;
            layoutSizeDropDownBtn.Text = "SIZE";
            // 
            // layoutOrientationDropDownBtn
            // 
            layoutOrientationDropDownBtn.BackColor = Color.SkyBlue;
            layoutOrientationDropDownBtn.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            layoutOrientationDropDownBtn.FormattingEnabled = true;
            layoutOrientationDropDownBtn.Location = new Point(275, 77);
            layoutOrientationDropDownBtn.Name = "layoutOrientationDropDownBtn";
            layoutOrientationDropDownBtn.Size = new Size(138, 27);
            layoutOrientationDropDownBtn.TabIndex = 8;
            layoutOrientationDropDownBtn.Text = "ORIENTATION";
            // 
            // outputBrowseBtn
            // 
            outputBrowseBtn.BackColor = Color.SkyBlue;
            outputBrowseBtn.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            outputBrowseBtn.ForeColor = Color.Black;
            outputBrowseBtn.Location = new Point(174, 130);
            outputBrowseBtn.Name = "outputBrowseBtn";
            outputBrowseBtn.Size = new Size(95, 36);
            outputBrowseBtn.TabIndex = 9;
            outputBrowseBtn.Text = "BROWSE";
            outputBrowseBtn.UseVisualStyleBackColor = false;
            outputBrowseBtn.Click += outputBrowseBtn_Click;
            // 
            // prefixCheckBtn
            // 
            prefixCheckBtn.AutoSize = true;
            prefixCheckBtn.BackColor = Color.FromArgb(64, 64, 64);
            prefixCheckBtn.Font = new Font("Arial Narrow", 12F, FontStyle.Bold);
            prefixCheckBtn.ForeColor = Color.GhostWhite;
            prefixCheckBtn.Location = new Point(12, 294);
            prefixCheckBtn.Name = "prefixCheckBtn";
            prefixCheckBtn.Size = new Size(96, 28);
            prefixCheckBtn.TabIndex = 10;
            prefixCheckBtn.Text = "PREFIX:";
            prefixCheckBtn.UseVisualStyleBackColor = false;
            // 
            // suffixCheckBtn
            // 
            suffixCheckBtn.AutoSize = true;
            suffixCheckBtn.BackColor = Color.FromArgb(64, 64, 64);
            suffixCheckBtn.Font = new Font("Arial Narrow", 12F, FontStyle.Bold);
            suffixCheckBtn.ForeColor = Color.GhostWhite;
            suffixCheckBtn.Location = new Point(12, 335);
            suffixCheckBtn.Name = "suffixCheckBtn";
            suffixCheckBtn.Size = new Size(95, 28);
            suffixCheckBtn.TabIndex = 11;
            suffixCheckBtn.Text = "SUFFIX:";
            suffixCheckBtn.UseVisualStyleBackColor = false;
            // 
            // prefixEntry
            // 
            prefixEntry.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            prefixEntry.Location = new Point(168, 292);
            prefixEntry.Name = "prefixEntry";
            prefixEntry.PlaceholderText = "PREFIX FOR OUTPUT PDF";
            prefixEntry.Size = new Size(233, 27);
            prefixEntry.TabIndex = 12;
            // 
            // suffixEntry
            // 
            suffixEntry.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            suffixEntry.Location = new Point(168, 335);
            suffixEntry.Name = "suffixEntry";
            suffixEntry.PlaceholderText = "SUFFIX OF OUTPUT PDF";
            suffixEntry.Size = new Size(233, 27);
            suffixEntry.TabIndex = 13;
            // 
            // lauchBtn
            // 
            lauchBtn.BackColor = Color.SkyBlue;
            lauchBtn.Font = new Font("Arial", 13.8F, FontStyle.Bold, GraphicsUnit.Point, 0);
            lauchBtn.ForeColor = Color.Black;
            lauchBtn.Location = new Point(144, 388);
            lauchBtn.Name = "lauchBtn";
            lauchBtn.Size = new Size(155, 41);
            lauchBtn.TabIndex = 14;
            lauchBtn.Text = "LAUNCH";
            lauchBtn.UseVisualStyleBackColor = false;
            // 
            // ctbLabel
            // 
            ctbLabel.AutoSize = true;
            ctbLabel.BackColor = Color.FromArgb(64, 64, 64);
            ctbLabel.Font = new Font("Arial Narrow", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            ctbLabel.ForeColor = Color.GhostWhite;
            ctbLabel.Location = new Point(12, 189);
            ctbLabel.Name = "ctbLabel";
            ctbLabel.Size = new Size(49, 24);
            ctbLabel.TabIndex = 15;
            ctbLabel.Text = "CTB:";
            // 
            // ctbBrowseBtn
            // 
            ctbBrowseBtn.BackColor = Color.SkyBlue;
            ctbBrowseBtn.Font = new Font("Arial", 10.2F, FontStyle.Bold);
            ctbBrowseBtn.ForeColor = Color.Black;
            ctbBrowseBtn.Location = new Point(318, 184);
            ctbBrowseBtn.Name = "ctbBrowseBtn";
            ctbBrowseBtn.Size = new Size(95, 36);
            ctbBrowseBtn.TabIndex = 17;
            ctbBrowseBtn.Text = "BROWSE";
            ctbBrowseBtn.UseVisualStyleBackColor = false;
            // 
            // ctbDropBtn
            // 
            ctbDropBtn.FormattingEnabled = true;
            ctbDropBtn.Items.AddRange(new object[] { "GCS-ATK-COLS", "ATKINSGLOBAL", "TEST1ASDFASDFASDFASDF", "TEST2DAFSDFASDFASDF", "TEST3FASDFASDFASDFazsdFASDFASDF" });
            ctbDropBtn.Location = new Point(174, 189);
            ctbDropBtn.Name = "ctbDropBtn";
            ctbDropBtn.Size = new Size(132, 28);
            ctbDropBtn.TabIndex = 18;
            // 
            // checkBox1
            // 
            checkBox1.AutoSize = true;
            checkBox1.BackColor = Color.FromArgb(64, 64, 64);
            checkBox1.Font = new Font("Arial Narrow", 12F, FontStyle.Bold);
            checkBox1.ForeColor = Color.GhostWhite;
            checkBox1.Location = new Point(12, 243);
            checkBox1.Name = "checkBox1";
            checkBox1.Size = new Size(425, 28);
            checkBox1.TabIndex = 19;
            checkBox1.Text = "DO YOU WANT TO PLOT WITH LINE WEIGHT ON?";
            checkBox1.UseVisualStyleBackColor = false;
            // 
            // plotDWG
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(64, 64, 64);
            ClientSize = new Size(439, 450);
            Controls.Add(checkBox1);
            Controls.Add(ctbDropBtn);
            Controls.Add(ctbBrowseBtn);
            Controls.Add(ctbLabel);
            Controls.Add(lauchBtn);
            Controls.Add(suffixEntry);
            Controls.Add(prefixEntry);
            Controls.Add(suffixCheckBtn);
            Controls.Add(prefixCheckBtn);
            Controls.Add(outputBrowseBtn);
            Controls.Add(layoutOrientationDropDownBtn);
            Controls.Add(layoutSizeDropDownBtn);
            Controls.Add(manageButton);
            Controls.Add(dwgBrowseBtn);
            Controls.Add(outputFolderText);
            Controls.Add(layoutTypeText);
            Controls.Add(addDrawingText);
            ForeColor = Color.GhostWhite;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Name = "plotDWG";
            Text = "plotDWG";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label addDrawingText;
        private Label layoutTypeText;
        private Label outputFolderText;
        private Button dwgBrowseBtn;
        private Button manageButton;
        private ComboBox layoutSizeDropDownBtn;
        private ComboBox layoutOrientationDropDownBtn;
        private Button outputBrowseBtn;
        private CheckBox prefixCheckBtn;
        private CheckBox suffixCheckBtn;
        private TextBox prefixEntry;
        private TextBox suffixEntry;
        private Button lauchBtn;
        private Label ctbLabel;
        private Button ctbBrowseBtn;
        private ComboBox ctbDropBtn;
        private CheckBox checkBox1;
    }
}
