﻿namespace Personal_Organizer.Design
{
    partial class AddReminder
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
            this.label1 = new System.Windows.Forms.Label();
            this.titletextbox = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.summarytextbox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.descriptiontextbox = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.meetingradiobtn = new System.Windows.Forms.RadioButton();
            this.taskradiobtn = new System.Windows.Forms.RadioButton();
            this.addbtn = new System.Windows.Forms.Button();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.label1.Location = new System.Drawing.Point(115, 147);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 31);
            this.label1.TabIndex = 6;
            this.label1.Text = "Title";
            // 
            // titletextbox
            // 
            this.titletextbox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.titletextbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.titletextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.titletextbox.Location = new System.Drawing.Point(121, 181);
            this.titletextbox.Name = "titletextbox";
            this.titletextbox.Size = new System.Drawing.Size(578, 31);
            this.titletextbox.TabIndex = 4;
            this.titletextbox.TextChanged += new System.EventHandler(this.titletextbox_TextChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.label2.Location = new System.Drawing.Point(113, 366);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(122, 31);
            this.label2.TabIndex = 8;
            this.label2.Text = "Summary";
            // 
            // summarytextbox
            // 
            this.summarytextbox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.summarytextbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.summarytextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.summarytextbox.Location = new System.Drawing.Point(119, 400);
            this.summarytextbox.Multiline = true;
            this.summarytextbox.Name = "summarytextbox";
            this.summarytextbox.Size = new System.Drawing.Size(578, 100);
            this.summarytextbox.TabIndex = 7;
            this.summarytextbox.TextChanged += new System.EventHandler(this.summarytextbox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.label3.Location = new System.Drawing.Point(115, 225);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(146, 31);
            this.label3.TabIndex = 10;
            this.label3.Text = "Description";
            // 
            // descriptiontextbox
            // 
            this.descriptiontextbox.BackColor = System.Drawing.SystemColors.ControlLight;
            this.descriptiontextbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.descriptiontextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.descriptiontextbox.Location = new System.Drawing.Point(121, 259);
            this.descriptiontextbox.Multiline = true;
            this.descriptiontextbox.Name = "descriptiontextbox";
            this.descriptiontextbox.Size = new System.Drawing.Size(578, 90);
            this.descriptiontextbox.TabIndex = 9;
            this.descriptiontextbox.TextChanged += new System.EventHandler(this.descriptiontextbox_TextChanged);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.dateTimePicker1.Location = new System.Drawing.Point(119, 558);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(580, 38);
            this.dateTimePicker1.TabIndex = 11;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Tai Le", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.SystemColors.ButtonShadow;
            this.label4.Location = new System.Drawing.Point(115, 524);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(67, 31);
            this.label4.TabIndex = 12;
            this.label4.Text = "Date";
            // 
            // meetingradiobtn
            // 
            this.meetingradiobtn.AutoSize = true;
            this.meetingradiobtn.Font = new System.Drawing.Font("Microsoft Tai Le", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.meetingradiobtn.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.meetingradiobtn.Location = new System.Drawing.Point(121, 623);
            this.meetingradiobtn.Name = "meetingradiobtn";
            this.meetingradiobtn.Size = new System.Drawing.Size(125, 31);
            this.meetingradiobtn.TabIndex = 13;
            this.meetingradiobtn.TabStop = true;
            this.meetingradiobtn.Text = "Meeting";
            this.meetingradiobtn.UseVisualStyleBackColor = true;
            this.meetingradiobtn.CheckedChanged += new System.EventHandler(this.meetingradiobtn_CheckedChanged);
            // 
            // taskradiobtn
            // 
            this.taskradiobtn.AutoSize = true;
            this.taskradiobtn.Font = new System.Drawing.Font("Microsoft Tai Le", 7.875F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.taskradiobtn.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.taskradiobtn.Location = new System.Drawing.Point(121, 675);
            this.taskradiobtn.Name = "taskradiobtn";
            this.taskradiobtn.Size = new System.Drawing.Size(87, 31);
            this.taskradiobtn.TabIndex = 14;
            this.taskradiobtn.TabStop = true;
            this.taskradiobtn.Text = "Task";
            this.taskradiobtn.UseVisualStyleBackColor = true;
            this.taskradiobtn.CheckedChanged += new System.EventHandler(this.taskradiobtn_CheckedChanged);
            // 
            // addbtn
            // 
            this.addbtn.FlatAppearance.BorderSize = 0;
            this.addbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addbtn.Font = new System.Drawing.Font("Microsoft Tai Le", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addbtn.ForeColor = System.Drawing.SystemColors.Highlight;
            this.addbtn.Location = new System.Drawing.Point(279, 730);
            this.addbtn.Name = "addbtn";
            this.addbtn.Size = new System.Drawing.Size(243, 53);
            this.addbtn.TabIndex = 5;
            this.addbtn.Text = "ADD";
            this.addbtn.UseVisualStyleBackColor = true;
            this.addbtn.Click += new System.EventHandler(this.addbtn_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::Personal_Organizer.Properties.Resources.note2;
            this.pictureBox1.Location = new System.Drawing.Point(329, 21);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(141, 139);
            this.pictureBox1.TabIndex = 15;
            this.pictureBox1.TabStop = false;
            // 
            // AddReminder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(800, 795);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.taskradiobtn);
            this.Controls.Add(this.meetingradiobtn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.descriptiontextbox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.summarytextbox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.addbtn);
            this.Controls.Add(this.titletextbox);
            this.Name = "AddReminder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddReminder";
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox titletextbox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox summarytextbox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox descriptiontextbox;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.RadioButton meetingradiobtn;
        private System.Windows.Forms.RadioButton taskradiobtn;
        private System.Windows.Forms.Button addbtn;
        private System.Windows.Forms.PictureBox pictureBox1;
    }
}