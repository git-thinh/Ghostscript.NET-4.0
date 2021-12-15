
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
            this.cropBottom = new System.Windows.Forms.TextBox();
            this.cropLeft = new System.Windows.Forms.TextBox();
            this.cropTop = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.cropRight = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button2 = new System.Windows.Forms.Button();
            this.labelFile = new System.Windows.Forms.Label();
            this.labelOK = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.textBoxDPI = new System.Windows.Forms.TextBox();
            this.textBoxAPI = new System.Windows.Forms.TextBox();
            this.ddlRedis_IP = new System.Windows.Forms.ComboBox();
            this.txtRedis_Port = new System.Windows.Forms.TextBox();
            this.txtRedis_DB = new System.Windows.Forms.TextBox();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(187, 104);
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
            this.ddlService.Location = new System.Drawing.Point(8, 106);
            this.ddlService.Name = "ddlService";
            this.ddlService.Size = new System.Drawing.Size(173, 21);
            this.ddlService.TabIndex = 1;
            this.ddlService.SelectedIndexChanged += new System.EventHandler(this.ddlService_SelectedIndexChanged);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.cropBottom);
            this.groupBox1.Controls.Add(this.cropLeft);
            this.groupBox1.Controls.Add(this.cropTop);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.cropRight);
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Location = new System.Drawing.Point(8, 49);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(564, 50);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Selection Input";
            // 
            // cropBottom
            // 
            this.cropBottom.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cropBottom.Location = new System.Drawing.Point(306, 20);
            this.cropBottom.Name = "cropBottom";
            this.cropBottom.Size = new System.Drawing.Size(89, 20);
            this.cropBottom.TabIndex = 3;
            this.cropBottom.Text = "50";
            this.cropBottom.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cropLeft
            // 
            this.cropLeft.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cropLeft.Location = new System.Drawing.Point(442, 19);
            this.cropLeft.Name = "cropLeft";
            this.cropLeft.Size = new System.Drawing.Size(100, 20);
            this.cropLeft.TabIndex = 4;
            this.cropLeft.Text = "69";
            this.cropLeft.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // cropTop
            // 
            this.cropTop.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cropTop.Location = new System.Drawing.Point(42, 20);
            this.cropTop.Name = "cropTop";
            this.cropTop.Size = new System.Drawing.Size(65, 20);
            this.cropTop.TabIndex = 5;
            this.cropTop.Text = "30";
            this.cropTop.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(417, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 13);
            this.label2.TabIndex = 1;
            this.label2.Text = "Left";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(260, 24);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(40, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Bottom";
            // 
            // cropRight
            // 
            this.cropRight.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cropRight.Location = new System.Drawing.Point(171, 20);
            this.cropRight.Name = "cropRight";
            this.cropRight.Size = new System.Drawing.Size(70, 20);
            this.cropRight.TabIndex = 6;
            this.cropRight.Text = "99";
            this.cropRight.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(10, 24);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(26, 13);
            this.label4.TabIndex = 3;
            this.label4.Text = "Top";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(133, 24);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(32, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Right";
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(268, 104);
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
            this.labelFile.Location = new System.Drawing.Point(17, 27);
            this.labelFile.Name = "labelFile";
            this.labelFile.Size = new System.Drawing.Size(0, 13);
            this.labelFile.TabIndex = 4;
            // 
            // labelOK
            // 
            this.labelOK.AutoSize = true;
            this.labelOK.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.labelOK.ForeColor = System.Drawing.Color.Red;
            this.labelOK.Location = new System.Drawing.Point(357, 103);
            this.labelOK.Name = "labelOK";
            this.labelOK.Padding = new System.Windows.Forms.Padding(5);
            this.labelOK.Size = new System.Drawing.Size(32, 23);
            this.labelOK.TabIndex = 5;
            this.labelOK.Text = "OK";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(460, 33);
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
            this.textBoxDPI.Location = new System.Drawing.Point(491, 33);
            this.textBoxDPI.Name = "textBoxDPI";
            this.textBoxDPI.Size = new System.Drawing.Size(59, 13);
            this.textBoxDPI.TabIndex = 7;
            this.textBoxDPI.Text = "70";
            this.textBoxDPI.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBoxAPI
            // 
            this.textBoxAPI.BackColor = System.Drawing.SystemColors.Control;
            this.textBoxAPI.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBoxAPI.Dock = System.Windows.Forms.DockStyle.Top;
            this.textBoxAPI.Location = new System.Drawing.Point(0, 0);
            this.textBoxAPI.Name = "textBoxAPI";
            this.textBoxAPI.Size = new System.Drawing.Size(579, 13);
            this.textBoxAPI.TabIndex = 8;
            // 
            // ddlRedis_IP
            // 
            this.ddlRedis_IP.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlRedis_IP.FormattingEnabled = true;
            this.ddlRedis_IP.Items.AddRange(new object[] {
            "127.0.0.1",
            "10.1.1.117"});
            this.ddlRedis_IP.Location = new System.Drawing.Point(226, 30);
            this.ddlRedis_IP.Name = "ddlRedis_IP";
            this.ddlRedis_IP.Size = new System.Drawing.Size(121, 21);
            this.ddlRedis_IP.TabIndex = 9;
            this.ddlRedis_IP.SelectedIndexChanged += new System.EventHandler(this.ddlRedis_IP_SelectedIndexChanged);
            // 
            // txtRedis_Port
            // 
            this.txtRedis_Port.Location = new System.Drawing.Point(350, 30);
            this.txtRedis_Port.Name = "txtRedis_Port";
            this.txtRedis_Port.Size = new System.Drawing.Size(53, 20);
            this.txtRedis_Port.TabIndex = 10;
            this.txtRedis_Port.Text = "1000";
            this.txtRedis_Port.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // txtRedis_DB
            // 
            this.txtRedis_DB.Location = new System.Drawing.Point(404, 30);
            this.txtRedis_DB.Name = "txtRedis_DB";
            this.txtRedis_DB.Size = new System.Drawing.Size(46, 20);
            this.txtRedis_DB.TabIndex = 11;
            this.txtRedis_DB.Text = "8";
            this.txtRedis_DB.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // fMain
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(579, 135);
            this.Controls.Add(this.txtRedis_DB);
            this.Controls.Add(this.txtRedis_Port);
            this.Controls.Add(this.ddlRedis_IP);
            this.Controls.Add(this.textBoxAPI);
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
        private System.Windows.Forms.TextBox cropBottom;
        private System.Windows.Forms.TextBox cropLeft;
        private System.Windows.Forms.TextBox cropTop;
        private System.Windows.Forms.TextBox cropRight;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Label labelFile;
        private System.Windows.Forms.Label labelOK;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textBoxDPI;
        private System.Windows.Forms.TextBox textBoxAPI;
        private System.Windows.Forms.ComboBox ddlRedis_IP;
        private System.Windows.Forms.TextBox txtRedis_Port;
        private System.Windows.Forms.TextBox txtRedis_DB;
    }
}

