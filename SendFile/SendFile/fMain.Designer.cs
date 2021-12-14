
namespace SendFile
{
    partial class fMain
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
            this.button1 = new System.Windows.Forms.Button();
            this.ddlService = new System.Windows.Forms.ComboBox();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.selectionText_Width = new System.Windows.Forms.TextBox();
            this.selectionText_Height = new System.Windows.Forms.TextBox();
            this.selectionText_X = new System.Windows.Forms.TextBox();
            this.selectionText_Y = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.labelFile = new System.Windows.Forms.Label();
            this.labelOK = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxDPI = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(187, 86);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "Upload File";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.btnBrowserFile);
            // 
            // ddlService
            // 
            this.ddlService.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlService.FormattingEnabled = true;
            this.ddlService.Location = new System.Drawing.Point(8, 88);
            this.ddlService.Name = "ddlService";
            this.ddlService.Size = new System.Drawing.Size(173, 21);
            this.ddlService.TabIndex = 1;
            this.ddlService.SelectedIndexChanged += new System.EventHandler(this.ddlService_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.selectionText_Width);
            this.groupBox1.Controls.Add(this.selectionText_Height);
            this.groupBox1.Controls.Add(this.selectionText_X);
            this.groupBox1.Controls.Add(this.selectionText_Y);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Location = new System.Drawing.Point(8, 31);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(564, 50);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Selection Input";
            // 
            // selectionText_Width
            // 
            this.selectionText_Width.Location = new System.Drawing.Point(44, 19);
            this.selectionText_Width.Name = "selectionText_Width";
            this.selectionText_Width.Size = new System.Drawing.Size(100, 20);
            this.selectionText_Width.TabIndex = 3;
            // 
            // selectionText_Height
            // 
            this.selectionText_Height.Location = new System.Drawing.Point(191, 18);
            this.selectionText_Height.Name = "selectionText_Height";
            this.selectionText_Height.Size = new System.Drawing.Size(100, 20);
            this.selectionText_Height.TabIndex = 4;
            // 
            // selectionText_X
            // 
            this.selectionText_X.Location = new System.Drawing.Point(328, 19);
            this.selectionText_X.Name = "selectionText_X";
            this.selectionText_X.Size = new System.Drawing.Size(80, 20);
            this.selectionText_X.TabIndex = 5;
            // 
            // selectionText_Y
            // 
            this.selectionText_Y.Location = new System.Drawing.Point(450, 19);
            this.selectionText_Y.Name = "selectionText_Y";
            this.selectionText_Y.Size = new System.Drawing.Size(92, 20);
            this.selectionText_Y.TabIndex = 6;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(311, 23);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(14, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "X";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(434, 23);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(14, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Y";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(155, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(38, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Height";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Width";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(268, 86);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 3;
            this.button2.Text = "Send File";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.btnSendFile);
            // 
            // labelFile
            // 
            this.labelFile.AutoSize = true;
            this.labelFile.Location = new System.Drawing.Point(17, 9);
            this.labelFile.Name = "labelFile";
            this.labelFile.Size = new System.Drawing.Size(0, 13);
            this.labelFile.TabIndex = 4;
            // 
            // labelOK
            // 
            this.labelOK.AutoSize = true;
            this.labelOK.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelOK.ForeColor = System.Drawing.Color.Red;
            this.labelOK.Location = new System.Drawing.Point(357, 85);
            this.labelOK.Name = "labelOK";
            this.labelOK.Padding = new System.Windows.Forms.Padding(5);
            this.labelOK.Size = new System.Drawing.Size(32, 23);
            this.labelOK.TabIndex = 5;
            this.labelOK.Text = "OK";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(460, 15);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 13);
            this.label5.TabIndex = 6;
            this.label5.Text = "DPI";
            // 
            // textBoxDPI
            // 
            this.textBoxDPI.BackColor = System.Drawing.SystemColors.Info;
            this.textBoxDPI.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxDPI.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBoxDPI.Location = new System.Drawing.Point(491, 15);
            this.textBoxDPI.Name = "textBoxDPI";
            this.textBoxDPI.Size = new System.Drawing.Size(59, 13);
            this.textBoxDPI.TabIndex = 7;
            this.textBoxDPI.Text = "70";
            this.textBoxDPI.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 117);
            this.Controls.Add(this.textBoxDPI);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.labelOK);
            this.Controls.Add(this.labelFile);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ddlService);
            this.Controls.Add(this.button1);
            this.Name = "fMain";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.fMain_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox ddlService;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox selectionText_Width;
        private System.Windows.Forms.TextBox selectionText_Height;
        private System.Windows.Forms.TextBox selectionText_X;
        private System.Windows.Forms.TextBox selectionText_Y;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label labelFile;
        private System.Windows.Forms.Label labelOK;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxDPI;
    }
}

