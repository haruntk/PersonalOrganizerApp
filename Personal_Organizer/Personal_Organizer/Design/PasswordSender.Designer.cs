namespace Personal_Organizer.Design
{
    partial class PasswordSender
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
            this.emailerrorlbl = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.emailtxt = new System.Windows.Forms.TextBox();
            this.sendbtn = new Personal_Organizer.RoundedButton();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // emailerrorlbl
            // 
            this.emailerrorlbl.AutoSize = true;
            this.emailerrorlbl.ForeColor = System.Drawing.Color.Brown;
            this.emailerrorlbl.Location = new System.Drawing.Point(169, 144);
            this.emailerrorlbl.Name = "emailerrorlbl";
            this.emailerrorlbl.Size = new System.Drawing.Size(0, 25);
            this.emailerrorlbl.TabIndex = 36;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Arial", 10.875F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(167, 62);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 33);
            this.label2.TabIndex = 35;
            this.label2.Text = "E-mail";
            // 
            // emailtxt
            // 
            this.emailtxt.BackColor = System.Drawing.SystemColors.Menu;
            this.emailtxt.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.emailtxt.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.emailtxt.Location = new System.Drawing.Point(173, 101);
            this.emailtxt.Margin = new System.Windows.Forms.Padding(6);
            this.emailtxt.Name = "emailtxt";
            this.emailtxt.Size = new System.Drawing.Size(418, 37);
            this.emailtxt.TabIndex = 34;
            // 
            // sendbtn
            // 
            this.sendbtn.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.sendbtn.FlatAppearance.BorderSize = 0;
            this.sendbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sendbtn.Font = new System.Drawing.Font("Microsoft YaHei", 10.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.sendbtn.ForeColor = System.Drawing.Color.White;
            this.sendbtn.Location = new System.Drawing.Point(231, 190);
            this.sendbtn.Margin = new System.Windows.Forms.Padding(6);
            this.sendbtn.Name = "sendbtn";
            this.sendbtn.Size = new System.Drawing.Size(287, 73);
            this.sendbtn.TabIndex = 37;
            this.sendbtn.Text = "SEND";
            this.sendbtn.UseVisualStyleBackColor = false;
            this.sendbtn.Click += new System.EventHandler(this.sendbtn_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(75, 98);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(631, 71);
            this.progressBar1.TabIndex = 38;
            // 
            // PasswordSender
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(800, 291);
            this.Controls.Add(this.sendbtn);
            this.Controls.Add(this.emailerrorlbl);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.emailtxt);
            this.Controls.Add(this.progressBar1);
            this.Name = "PasswordSender";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Password";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label emailerrorlbl;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox emailtxt;
        private RoundedButton sendbtn;
        private System.Windows.Forms.ProgressBar progressBar1;
    }
}