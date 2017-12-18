namespace Parloire
{
    partial class ChatWindow
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatWindow));
            this.chatContent = new System.Windows.Forms.TextBox();
            this.rbServer = new System.Windows.Forms.RadioButton();
            this.rbClient = new System.Windows.Forms.RadioButton();
            this.btnStart = new System.Windows.Forms.Button();
            this.txtNickname = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtServerIp = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMessage = new System.Windows.Forms.TextBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // chatContent
            // 
            this.chatContent.Location = new System.Drawing.Point(22, 41);
            this.chatContent.Multiline = true;
            this.chatContent.Name = "chatContent";
            this.chatContent.ReadOnly = true;
            this.chatContent.Size = new System.Drawing.Size(228, 187);
            this.chatContent.TabIndex = 0;
            // 
            // rbServer
            // 
            this.rbServer.AutoSize = true;
            this.rbServer.Location = new System.Drawing.Point(12, 19);
            this.rbServer.Name = "rbServer";
            this.rbServer.Size = new System.Drawing.Size(62, 17);
            this.rbServer.TabIndex = 1;
            this.rbServer.TabStop = true;
            this.rbServer.Text = "Serveur";
            this.rbServer.UseVisualStyleBackColor = true;
            this.rbServer.CheckedChanged += new System.EventHandler(this.rbServer_CheckedChanged);
            // 
            // rbClient
            // 
            this.rbClient.AutoSize = true;
            this.rbClient.Location = new System.Drawing.Point(12, 42);
            this.rbClient.Name = "rbClient";
            this.rbClient.Size = new System.Drawing.Size(51, 17);
            this.rbClient.TabIndex = 2;
            this.rbClient.TabStop = true;
            this.rbClient.Text = "Client";
            this.rbClient.UseVisualStyleBackColor = true;
            this.rbClient.CheckedChanged += new System.EventHandler(this.rbClient_CheckedChanged);
            // 
            // btnStart
            // 
            this.btnStart.Location = new System.Drawing.Point(416, 142);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(46, 30);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "Start";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // txtNickname
            // 
            this.txtNickname.Location = new System.Drawing.Point(318, 148);
            this.txtNickname.Name = "txtNickname";
            this.txtNickname.Size = new System.Drawing.Size(92, 20);
            this.txtNickname.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(274, 151);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(43, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Pseudo";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.txtServerIp);
            this.groupBox1.Controls.Add(this.rbClient);
            this.groupBox1.Controls.Add(this.rbServer);
            this.groupBox1.Location = new System.Drawing.Point(277, 41);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(185, 93);
            this.groupBox1.TabIndex = 6;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Mode";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(9, 62);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 13);
            this.label4.TabIndex = 4;
            this.label4.Text = "IP serveur";
            // 
            // txtServerIp
            // 
            this.txtServerIp.Enabled = false;
            this.txtServerIp.Location = new System.Drawing.Point(70, 59);
            this.txtServerIp.Name = "txtServerIp";
            this.txtServerIp.Size = new System.Drawing.Size(109, 20);
            this.txtServerIp.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(166, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(84, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Parloire 0.5 beta";
            // 
            // txtMessage
            // 
            this.txtMessage.Location = new System.Drawing.Point(75, 240);
            this.txtMessage.Name = "txtMessage";
            this.txtMessage.Size = new System.Drawing.Size(112, 20);
            this.txtMessage.TabIndex = 8;
            this.txtMessage.TextChanged += new System.EventHandler(this.txtMessage_TextChanged);
            // 
            // btnSend
            // 
            this.btnSend.Enabled = false;
            this.btnSend.Location = new System.Drawing.Point(193, 237);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(57, 25);
            this.btnSend.TabIndex = 9;
            this.btnSend.Text = "Send";
            this.btnSend.UseVisualStyleBackColor = true;
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 243);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 13);
            this.label3.TabIndex = 10;
            this.label3.Text = "Message";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(318, 192);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(114, 70);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 11;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.Click += new System.EventHandler(this.pictureBox1_Click);
            // 
            // ChatWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(481, 274);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.txtMessage);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtNickname);
            this.Controls.Add(this.btnStart);
            this.Controls.Add(this.chatContent);
            this.Name = "ChatWindow";
            this.Text = "ChatWindow";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox chatContent;
        private System.Windows.Forms.RadioButton rbServer;
        private System.Windows.Forms.RadioButton rbClient;
        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.TextBox txtNickname;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMessage;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtServerIp;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}