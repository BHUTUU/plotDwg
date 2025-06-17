namespace plotDWG
{
    partial class PlotDWG
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PlotDWG));
            this.addDrawingText = new System.Windows.Forms.Label();
            this.dwgBrowseBtn = new System.Windows.Forms.Button();
            this.manageButton = new System.Windows.Forms.Button();
            this.drawingOrientationDropBtn = new System.Windows.Forms.Label();
            this.layoutSizeDropDownBtn = new System.Windows.Forms.ComboBox();
            this.layoutOrientationDropDownBtn = new System.Windows.Forms.ComboBox();
            this.outputFolderText = new System.Windows.Forms.Label();
            this.outputBrowseBtn = new System.Windows.Forms.Button();
            this.ctbLabel = new System.Windows.Forms.Label();
            this.ctbDropBtn = new System.Windows.Forms.ComboBox();
            this.browseCTBbtn = new System.Windows.Forms.Button();
            this.lineWeightCheckBtn = new System.Windows.Forms.CheckBox();
            this.prefixCheckBtn = new System.Windows.Forms.CheckBox();
            this.suffixCheckBtn = new System.Windows.Forms.CheckBox();
            this.prefixEntry = new System.Windows.Forms.TextBox();
            this.suffixEntry = new System.Windows.Forms.TextBox();
            this.launchBtn = new System.Windows.Forms.Button();
            this.scaleLineWeightChkBtn = new System.Windows.Forms.CheckBox();
            this.preLispEntry = new System.Windows.Forms.TextBox();
            this.preLispChkBox = new System.Windows.Forms.CheckBox();
            this.postLispEntry = new System.Windows.Forms.TextBox();
            this.postLispChkBox = new System.Windows.Forms.CheckBox();
            this.progressLabel = new System.Windows.Forms.Label();
            this.paperSizeDropBtn = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // addDrawingText
            // 
            this.addDrawingText.AutoSize = true;
            this.addDrawingText.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold);
            this.addDrawingText.ForeColor = System.Drawing.Color.GhostWhite;
            this.addDrawingText.Location = new System.Drawing.Point(15, 35);
            this.addDrawingText.Name = "addDrawingText";
            this.addDrawingText.Size = new System.Drawing.Size(134, 24);
            this.addDrawingText.TabIndex = 0;
            this.addDrawingText.Text = "ADD DRAWING:";
            // 
            // dwgBrowseBtn
            // 
            this.dwgBrowseBtn.BackColor = System.Drawing.Color.SkyBlue;
            this.dwgBrowseBtn.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold);
            this.dwgBrowseBtn.Location = new System.Drawing.Point(177, 31);
            this.dwgBrowseBtn.Name = "dwgBrowseBtn";
            this.dwgBrowseBtn.Size = new System.Drawing.Size(105, 35);
            this.dwgBrowseBtn.TabIndex = 1;
            this.dwgBrowseBtn.Text = "BROWSE";
            this.dwgBrowseBtn.UseVisualStyleBackColor = false;
            this.dwgBrowseBtn.Click += new System.EventHandler(this.DwgBrowseBtn_Click);
            // 
            // manageButton
            // 
            this.manageButton.BackColor = System.Drawing.Color.SkyBlue;
            this.manageButton.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold);
            this.manageButton.Location = new System.Drawing.Point(290, 31);
            this.manageButton.Name = "manageButton";
            this.manageButton.Size = new System.Drawing.Size(154, 35);
            this.manageButton.TabIndex = 2;
            this.manageButton.Text = "MANAGE";
            this.manageButton.UseVisualStyleBackColor = false;
            this.manageButton.Click += new System.EventHandler(this.ManageButton_Click);
            // 
            // drawingOrientationDropBtn
            // 
            this.drawingOrientationDropBtn.AutoSize = true;
            this.drawingOrientationDropBtn.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold);
            this.drawingOrientationDropBtn.ForeColor = System.Drawing.Color.GhostWhite;
            this.drawingOrientationDropBtn.Location = new System.Drawing.Point(15, 85);
            this.drawingOrientationDropBtn.Name = "drawingOrientationDropBtn";
            this.drawingOrientationDropBtn.Size = new System.Drawing.Size(211, 24);
            this.drawingOrientationDropBtn.TabIndex = 3;
            this.drawingOrientationDropBtn.Text = "DRAWING ORIENTATION:";
            // 
            // layoutSizeDropDownBtn
            // 
            this.layoutSizeDropDownBtn.BackColor = System.Drawing.Color.SkyBlue;
            this.layoutSizeDropDownBtn.DropDownWidth = 320;
            this.layoutSizeDropDownBtn.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold);
            this.layoutSizeDropDownBtn.FormattingEnabled = true;
            this.layoutSizeDropDownBtn.Location = new System.Drawing.Point(19, 165);
            this.layoutSizeDropDownBtn.Name = "layoutSizeDropDownBtn";
            this.layoutSizeDropDownBtn.Size = new System.Drawing.Size(425, 27);
            this.layoutSizeDropDownBtn.TabIndex = 4;
            this.layoutSizeDropDownBtn.Text = "SIZE";
            this.layoutSizeDropDownBtn.SelectedIndexChanged += new System.EventHandler(this.LayoutSizeDropDownBtn_SelectedIndexChanged);
            // 
            // layoutOrientationDropDownBtn
            // 
            this.layoutOrientationDropDownBtn.BackColor = System.Drawing.Color.SkyBlue;
            this.layoutOrientationDropDownBtn.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold);
            this.layoutOrientationDropDownBtn.FormattingEnabled = true;
            this.layoutOrientationDropDownBtn.Location = new System.Drawing.Point(290, 82);
            this.layoutOrientationDropDownBtn.Name = "layoutOrientationDropDownBtn";
            this.layoutOrientationDropDownBtn.Size = new System.Drawing.Size(154, 27);
            this.layoutOrientationDropDownBtn.TabIndex = 5;
            this.layoutOrientationDropDownBtn.Text = "ORIENTATION";
            this.layoutOrientationDropDownBtn.SelectedIndexChanged += new System.EventHandler(this.LayoutOrientationDropDownBtn_SelectedIndexChanged);
            // 
            // outputFolderText
            // 
            this.outputFolderText.AutoSize = true;
            this.outputFolderText.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold);
            this.outputFolderText.ForeColor = System.Drawing.Color.GhostWhite;
            this.outputFolderText.Location = new System.Drawing.Point(15, 219);
            this.outputFolderText.Name = "outputFolderText";
            this.outputFolderText.Size = new System.Drawing.Size(156, 24);
            this.outputFolderText.TabIndex = 7;
            this.outputFolderText.Text = "OUTPUT FOLDER:";
            // 
            // outputBrowseBtn
            // 
            this.outputBrowseBtn.BackColor = System.Drawing.Color.SkyBlue;
            this.outputBrowseBtn.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold);
            this.outputBrowseBtn.Location = new System.Drawing.Point(177, 215);
            this.outputBrowseBtn.Name = "outputBrowseBtn";
            this.outputBrowseBtn.Size = new System.Drawing.Size(105, 35);
            this.outputBrowseBtn.TabIndex = 6;
            this.outputBrowseBtn.Text = "BROWSE";
            this.outputBrowseBtn.UseVisualStyleBackColor = false;
            this.outputBrowseBtn.Click += new System.EventHandler(this.OutputBrowseBtn_Click);
            // 
            // ctbLabel
            // 
            this.ctbLabel.AutoSize = true;
            this.ctbLabel.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold);
            this.ctbLabel.ForeColor = System.Drawing.Color.GhostWhite;
            this.ctbLabel.Location = new System.Drawing.Point(15, 276);
            this.ctbLabel.Name = "ctbLabel";
            this.ctbLabel.Size = new System.Drawing.Size(49, 24);
            this.ctbLabel.TabIndex = 8;
            this.ctbLabel.Text = "CTB:";
            // 
            // ctbDropBtn
            // 
            this.ctbDropBtn.BackColor = System.Drawing.Color.SkyBlue;
            this.ctbDropBtn.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold);
            this.ctbDropBtn.FormattingEnabled = true;
            this.ctbDropBtn.Location = new System.Drawing.Point(82, 273);
            this.ctbDropBtn.Name = "ctbDropBtn";
            this.ctbDropBtn.Size = new System.Drawing.Size(238, 27);
            this.ctbDropBtn.TabIndex = 9;
            this.ctbDropBtn.Text = "SELECT";
            this.ctbDropBtn.SelectedIndexChanged += new System.EventHandler(this.CtbDropBtn_SelectedIndexChanged);
            // 
            // browseCTBbtn
            // 
            this.browseCTBbtn.BackColor = System.Drawing.Color.SkyBlue;
            this.browseCTBbtn.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold);
            this.browseCTBbtn.Location = new System.Drawing.Point(334, 268);
            this.browseCTBbtn.Name = "browseCTBbtn";
            this.browseCTBbtn.Size = new System.Drawing.Size(105, 35);
            this.browseCTBbtn.TabIndex = 10;
            this.browseCTBbtn.Text = "BROWSE";
            this.browseCTBbtn.UseVisualStyleBackColor = false;
            this.browseCTBbtn.Click += new System.EventHandler(this.BrowseCTBbtn_Click);
            // 
            // lineWeightCheckBtn
            // 
            this.lineWeightCheckBtn.AutoSize = true;
            this.lineWeightCheckBtn.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold);
            this.lineWeightCheckBtn.ForeColor = System.Drawing.Color.GhostWhite;
            this.lineWeightCheckBtn.Location = new System.Drawing.Point(19, 323);
            this.lineWeightCheckBtn.Name = "lineWeightCheckBtn";
            this.lineWeightCheckBtn.Size = new System.Drawing.Size(280, 28);
            this.lineWeightCheckBtn.TabIndex = 11;
            this.lineWeightCheckBtn.Text = "PLOT WITH LINEWEIGHTS ON?";
            this.lineWeightCheckBtn.UseVisualStyleBackColor = true;
            this.lineWeightCheckBtn.CheckedChanged += new System.EventHandler(this.LineWeightCheckBtn_CheckedChanged);
            // 
            // prefixCheckBtn
            // 
            this.prefixCheckBtn.AutoSize = true;
            this.prefixCheckBtn.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold);
            this.prefixCheckBtn.ForeColor = System.Drawing.Color.GhostWhite;
            this.prefixCheckBtn.Location = new System.Drawing.Point(19, 411);
            this.prefixCheckBtn.Name = "prefixCheckBtn";
            this.prefixCheckBtn.Size = new System.Drawing.Size(96, 28);
            this.prefixCheckBtn.TabIndex = 12;
            this.prefixCheckBtn.Text = "PREFIX:";
            this.prefixCheckBtn.UseVisualStyleBackColor = true;
            this.prefixCheckBtn.CheckedChanged += new System.EventHandler(this.PrefixCheckBtn_CheckedChanged);
            // 
            // suffixCheckBtn
            // 
            this.suffixCheckBtn.AutoSize = true;
            this.suffixCheckBtn.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold);
            this.suffixCheckBtn.ForeColor = System.Drawing.Color.GhostWhite;
            this.suffixCheckBtn.Location = new System.Drawing.Point(19, 456);
            this.suffixCheckBtn.Name = "suffixCheckBtn";
            this.suffixCheckBtn.Size = new System.Drawing.Size(95, 28);
            this.suffixCheckBtn.TabIndex = 13;
            this.suffixCheckBtn.Text = "SUFFIX:";
            this.suffixCheckBtn.UseVisualStyleBackColor = true;
            this.suffixCheckBtn.CheckedChanged += new System.EventHandler(this.SuffixCheckBtn_CheckedChanged);
            // 
            // prefixEntry
            // 
            this.prefixEntry.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold);
            this.prefixEntry.Location = new System.Drawing.Point(177, 412);
            this.prefixEntry.Name = "prefixEntry";
            this.prefixEntry.Size = new System.Drawing.Size(249, 27);
            this.prefixEntry.TabIndex = 14;
            // 
            // suffixEntry
            // 
            this.suffixEntry.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold);
            this.suffixEntry.Location = new System.Drawing.Point(177, 457);
            this.suffixEntry.Name = "suffixEntry";
            this.suffixEntry.Size = new System.Drawing.Size(249, 27);
            this.suffixEntry.TabIndex = 15;
            // 
            // launchBtn
            // 
            this.launchBtn.BackColor = System.Drawing.Color.SkyBlue;
            this.launchBtn.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold);
            this.launchBtn.Location = new System.Drawing.Point(177, 603);
            this.launchBtn.Name = "launchBtn";
            this.launchBtn.Size = new System.Drawing.Size(132, 42);
            this.launchBtn.TabIndex = 16;
            this.launchBtn.Text = "LAUNCH";
            this.launchBtn.UseVisualStyleBackColor = false;
            this.launchBtn.Click += new System.EventHandler(this.LauchBtn_Click);
            // 
            // scaleLineWeightChkBtn
            // 
            this.scaleLineWeightChkBtn.AutoSize = true;
            this.scaleLineWeightChkBtn.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold);
            this.scaleLineWeightChkBtn.ForeColor = System.Drawing.Color.GhostWhite;
            this.scaleLineWeightChkBtn.Location = new System.Drawing.Point(19, 357);
            this.scaleLineWeightChkBtn.Name = "scaleLineWeightChkBtn";
            this.scaleLineWeightChkBtn.Size = new System.Drawing.Size(370, 28);
            this.scaleLineWeightChkBtn.TabIndex = 17;
            this.scaleLineWeightChkBtn.Text = "SCALE LINEWEIGHTS WITH PLOT SCALE?";
            this.scaleLineWeightChkBtn.UseVisualStyleBackColor = true;
            this.scaleLineWeightChkBtn.CheckedChanged += new System.EventHandler(this.ScaleLineWeightChkBtn_CheckedChanged);
            // 
            // preLispEntry
            // 
            this.preLispEntry.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold);
            this.preLispEntry.Location = new System.Drawing.Point(178, 503);
            this.preLispEntry.Name = "preLispEntry";
            this.preLispEntry.Size = new System.Drawing.Size(249, 27);
            this.preLispEntry.TabIndex = 19;
            // 
            // preLispChkBox
            // 
            this.preLispChkBox.AutoSize = true;
            this.preLispChkBox.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold);
            this.preLispChkBox.ForeColor = System.Drawing.Color.GhostWhite;
            this.preLispChkBox.Location = new System.Drawing.Point(20, 502);
            this.preLispChkBox.Name = "preLispChkBox";
            this.preLispChkBox.Size = new System.Drawing.Size(112, 28);
            this.preLispChkBox.TabIndex = 18;
            this.preLispChkBox.Text = "PRE-LISP:";
            this.preLispChkBox.UseVisualStyleBackColor = true;
            this.preLispChkBox.CheckedChanged += new System.EventHandler(this.PreLispChkBox_CheckedChanged);
            // 
            // postLispEntry
            // 
            this.postLispEntry.Font = new System.Drawing.Font("Arial", 10.2F, System.Drawing.FontStyle.Bold);
            this.postLispEntry.Location = new System.Drawing.Point(177, 550);
            this.postLispEntry.Name = "postLispEntry";
            this.postLispEntry.Size = new System.Drawing.Size(249, 27);
            this.postLispEntry.TabIndex = 21;
            // 
            // postLispChkBox
            // 
            this.postLispChkBox.AutoSize = true;
            this.postLispChkBox.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold);
            this.postLispChkBox.ForeColor = System.Drawing.Color.GhostWhite;
            this.postLispChkBox.Location = new System.Drawing.Point(19, 549);
            this.postLispChkBox.Name = "postLispChkBox";
            this.postLispChkBox.Size = new System.Drawing.Size(123, 28);
            this.postLispChkBox.TabIndex = 20;
            this.postLispChkBox.Text = "POST-LISP:";
            this.postLispChkBox.UseVisualStyleBackColor = true;
            this.postLispChkBox.CheckedChanged += new System.EventHandler(this.PostLispChkBox_CheckedChanged);
            // 
            // progressLabel
            // 
            this.progressLabel.AutoSize = true;
            this.progressLabel.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold);
            this.progressLabel.ForeColor = System.Drawing.Color.GhostWhite;
            this.progressLabel.Location = new System.Drawing.Point(353, 611);
            this.progressLabel.Name = "progressLabel";
            this.progressLabel.Size = new System.Drawing.Size(59, 24);
            this.progressLabel.TabIndex = 22;
            this.progressLabel.Text = "Ready";
            // 
            // paperSizeDropBtn
            // 
            this.paperSizeDropBtn.AutoSize = true;
            this.paperSizeDropBtn.Font = new System.Drawing.Font("Arial Narrow", 12F, System.Drawing.FontStyle.Bold);
            this.paperSizeDropBtn.ForeColor = System.Drawing.Color.GhostWhite;
            this.paperSizeDropBtn.Location = new System.Drawing.Point(15, 132);
            this.paperSizeDropBtn.Name = "paperSizeDropBtn";
            this.paperSizeDropBtn.Size = new System.Drawing.Size(112, 24);
            this.paperSizeDropBtn.TabIndex = 23;
            this.paperSizeDropBtn.Text = "PAPER SIZE:";
            // 
            // PlotDWG
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.ClientSize = new System.Drawing.Size(466, 662);
            this.Controls.Add(this.paperSizeDropBtn);
            this.Controls.Add(this.progressLabel);
            this.Controls.Add(this.postLispEntry);
            this.Controls.Add(this.postLispChkBox);
            this.Controls.Add(this.preLispEntry);
            this.Controls.Add(this.preLispChkBox);
            this.Controls.Add(this.scaleLineWeightChkBtn);
            this.Controls.Add(this.launchBtn);
            this.Controls.Add(this.suffixEntry);
            this.Controls.Add(this.prefixEntry);
            this.Controls.Add(this.suffixCheckBtn);
            this.Controls.Add(this.prefixCheckBtn);
            this.Controls.Add(this.lineWeightCheckBtn);
            this.Controls.Add(this.browseCTBbtn);
            this.Controls.Add(this.ctbDropBtn);
            this.Controls.Add(this.ctbLabel);
            this.Controls.Add(this.outputFolderText);
            this.Controls.Add(this.outputBrowseBtn);
            this.Controls.Add(this.layoutOrientationDropDownBtn);
            this.Controls.Add(this.layoutSizeDropDownBtn);
            this.Controls.Add(this.drawingOrientationDropBtn);
            this.Controls.Add(this.manageButton);
            this.Controls.Add(this.dwgBrowseBtn);
            this.Controls.Add(this.addDrawingText);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(484, 709);
            this.MinimumSize = new System.Drawing.Size(484, 709);
            this.Name = "PlotDWG";
            this.Text = "PLOT DWG";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label addDrawingText;
        private System.Windows.Forms.Button dwgBrowseBtn;
        private System.Windows.Forms.Button manageButton;
        private System.Windows.Forms.Label drawingOrientationDropBtn;
        private System.Windows.Forms.ComboBox layoutSizeDropDownBtn;
        private System.Windows.Forms.ComboBox layoutOrientationDropDownBtn;
        private System.Windows.Forms.Label outputFolderText;
        private System.Windows.Forms.Button outputBrowseBtn;
        private System.Windows.Forms.Label ctbLabel;
        private System.Windows.Forms.ComboBox ctbDropBtn;
        private System.Windows.Forms.Button browseCTBbtn;
        private System.Windows.Forms.CheckBox lineWeightCheckBtn;
        private System.Windows.Forms.CheckBox prefixCheckBtn;
        private System.Windows.Forms.CheckBox suffixCheckBtn;
        private System.Windows.Forms.TextBox prefixEntry;
        private System.Windows.Forms.TextBox suffixEntry;
        private System.Windows.Forms.Button launchBtn;
        private System.Windows.Forms.CheckBox scaleLineWeightChkBtn;
        private System.Windows.Forms.TextBox preLispEntry;
        private System.Windows.Forms.CheckBox preLispChkBox;
        private System.Windows.Forms.TextBox postLispEntry;
        private System.Windows.Forms.CheckBox postLispChkBox;
        private System.Windows.Forms.Label progressLabel;
        private System.Windows.Forms.Label paperSizeDropBtn;
    }
}