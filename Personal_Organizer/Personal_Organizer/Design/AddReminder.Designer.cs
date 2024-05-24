namespace Personal_Organizer
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
            this.titletextbox = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.descriptiontxtbox = new System.Windows.Forms.TextBox();
            this.dateTimePicker1 = new System.Windows.Forms.DateTimePicker();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.summarytextbox = new System.Windows.Forms.TextBox();
            this.addbtn = new System.Windows.Forms.Button();
            this.meetingradiobtn = new System.Windows.Forms.RadioButton();
            this.taskradiobtn = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // titletextbox
            // 
            this.titletextbox.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.titletextbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.titletextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.titletextbox.Location = new System.Drawing.Point(48, 109);
            this.titletextbox.Name = "titletextbox";
            this.titletextbox.Size = new System.Drawing.Size(739, 31);
            this.titletextbox.TabIndex = 0;
            this.titletextbox.TextChanged += new System.EventHandler(this.titletextbox_TextChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label1.Location = new System.Drawing.Point(42, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 31);
            this.label1.TabIndex = 1;
            this.label1.Text = "Title";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label2.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label2.Location = new System.Drawing.Point(42, 171);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(151, 31);
            this.label2.TabIndex = 3;
            this.label2.Text = "Description";
            // 
            // descriptiontxtbox
            // 
            this.descriptiontxtbox.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.descriptiontxtbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.descriptiontxtbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.descriptiontxtbox.Location = new System.Drawing.Point(48, 215);
            this.descriptiontxtbox.Multiline = true;
            this.descriptiontxtbox.Name = "descriptiontxtbox";
            this.descriptiontxtbox.Size = new System.Drawing.Size(739, 105);
            this.descriptiontxtbox.TabIndex = 2;
            this.descriptiontxtbox.TextChanged += new System.EventHandler(this.descriptiontxtbox_TextChanged);
            // 
            // dateTimePicker1
            // 
            this.dateTimePicker1.AllowDrop = true;
            this.dateTimePicker1.CalendarMonthBackground = System.Drawing.SystemColors.ButtonHighlight;
            this.dateTimePicker1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.dateTimePicker1.Location = new System.Drawing.Point(48, 536);
            this.dateTimePicker1.Name = "dateTimePicker1";
            this.dateTimePicker1.Size = new System.Drawing.Size(739, 38);
            this.dateTimePicker1.TabIndex = 4;
            this.dateTimePicker1.ValueChanged += new System.EventHandler(this.dateTimePicker1_ValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label3.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label3.Location = new System.Drawing.Point(42, 502);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 31);
            this.label3.TabIndex = 5;
            this.label3.Text = "Date";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.ForeColor = System.Drawing.SystemColors.ControlDarkDark;
            this.label4.Location = new System.Drawing.Point(42, 344);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(129, 31);
            this.label4.TabIndex = 7;
            this.label4.Text = "Summary";
            // 
            // summarytextbox
            // 
            this.summarytextbox.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.summarytextbox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.summarytextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.summarytextbox.Location = new System.Drawing.Point(48, 389);
            this.summarytextbox.Multiline = true;
            this.summarytextbox.Name = "summarytextbox";
            this.summarytextbox.Size = new System.Drawing.Size(739, 97);
            this.summarytextbox.TabIndex = 6;
            this.summarytextbox.TextChanged += new System.EventHandler(this.summarytextbox_TextChanged);
            // 
            // addbtn
            // 
            this.addbtn.FlatAppearance.BorderSize = 0;
            this.addbtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.addbtn.Font = new System.Drawing.Font("Microsoft Tai Le", 10.125F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.addbtn.ForeColor = System.Drawing.SystemColors.Highlight;
            this.addbtn.Location = new System.Drawing.Point(205, 669);
            this.addbtn.Name = "addbtn";
            this.addbtn.Size = new System.Drawing.Size(363, 48);
            this.addbtn.TabIndex = 8;
            this.addbtn.Text = "ADD";
            this.addbtn.UseVisualStyleBackColor = true;
            this.addbtn.Click += new System.EventHandler(this.addbtn_Click);
            // 
            // meetingradiobtn
            // 
            this.meetingradiobtn.AutoSize = true;
            this.meetingradiobtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.meetingradiobtn.Location = new System.Drawing.Point(145, 609);
            this.meetingradiobtn.Name = "meetingradiobtn";
            this.meetingradiobtn.Size = new System.Drawing.Size(141, 35);
            this.meetingradiobtn.TabIndex = 9;
            this.meetingradiobtn.TabStop = true;
            this.meetingradiobtn.Text = "Meeting";
            this.meetingradiobtn.UseVisualStyleBackColor = true;
            this.meetingradiobtn.CheckedChanged += new System.EventHandler(this.meetingradiobtn_CheckedChanged);
            // 
            // taskradiobtn
            // 
            this.taskradiobtn.AutoSize = true;
            this.taskradiobtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.125F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.taskradiobtn.Location = new System.Drawing.Point(518, 609);
            this.taskradiobtn.Name = "taskradiobtn";
            this.taskradiobtn.Size = new System.Drawing.Size(105, 35);
            this.taskradiobtn.TabIndex = 10;
            this.taskradiobtn.TabStop = true;
            this.taskradiobtn.Text = "Task";
            this.taskradiobtn.UseVisualStyleBackColor = true;
            this.taskradiobtn.CheckedChanged += new System.EventHandler(this.taskradiobtn_CheckedChanged);
            // 
            // AddReminder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.ClientSize = new System.Drawing.Size(837, 757);
            this.Controls.Add(this.taskradiobtn);
            this.Controls.Add(this.meetingradiobtn);
            this.Controls.Add(this.addbtn);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.summarytextbox);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.dateTimePicker1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.descriptiontxtbox);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.titletextbox);
            this.Name = "AddReminder";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "AddReminder";
            this.Load += new System.EventHandler(this.AddReminder_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox titletextbox;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox descriptiontxtbox;
        private System.Windows.Forms.DateTimePicker dateTimePicker1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox summarytextbox;
        private System.Windows.Forms.Button addbtn;
        private System.Windows.Forms.RadioButton meetingradiobtn;
        private System.Windows.Forms.RadioButton taskradiobtn;
    }
}