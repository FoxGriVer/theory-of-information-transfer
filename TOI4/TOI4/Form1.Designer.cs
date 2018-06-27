namespace TOI4
{
    partial class Form1
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.listBox2 = new System.Windows.Forms.ListBox();
            this.TextBoxOfMessages = new System.Windows.Forms.TextBox();
            this.ButtonCheck = new System.Windows.Forms.Button();
            this.Connect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.ItemHeight = 29;
            this.listBox1.Location = new System.Drawing.Point(12, 12);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(293, 787);
            this.listBox1.TabIndex = 0;
            // 
            // listBox2
            // 
            this.listBox2.FormattingEnabled = true;
            this.listBox2.ItemHeight = 29;
            this.listBox2.Location = new System.Drawing.Point(323, 12);
            this.listBox2.Name = "listBox2";
            this.listBox2.Size = new System.Drawing.Size(293, 787);
            this.listBox2.TabIndex = 1;
            // 
            // TextBoxOfMessages
            // 
            this.TextBoxOfMessages.Location = new System.Drawing.Point(631, 12);
            this.TextBoxOfMessages.Multiline = true;
            this.TextBoxOfMessages.Name = "TextBoxOfMessages";
            this.TextBoxOfMessages.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TextBoxOfMessages.Size = new System.Drawing.Size(412, 607);
            this.TextBoxOfMessages.TabIndex = 2;
            // 
            // ButtonCheck
            // 
            this.ButtonCheck.Location = new System.Drawing.Point(631, 649);
            this.ButtonCheck.Name = "ButtonCheck";
            this.ButtonCheck.Size = new System.Drawing.Size(412, 76);
            this.ButtonCheck.TabIndex = 3;
            this.ButtonCheck.Text = "Проверить сообщение на спам";
            this.ButtonCheck.UseVisualStyleBackColor = true;
            this.ButtonCheck.Click += new System.EventHandler(this.ButtonCheck_Click);
            // 
            // Connect
            // 
            this.Connect.Location = new System.Drawing.Point(631, 731);
            this.Connect.Name = "Connect";
            this.Connect.Size = new System.Drawing.Size(412, 68);
            this.Connect.TabIndex = 8;
            this.Connect.Text = "Присоединиться";
            this.Connect.UseVisualStyleBackColor = true;
            this.Connect.Click += new System.EventHandler(this.Connect_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(1055, 859);
            this.Controls.Add(this.Connect);
            this.Controls.Add(this.ButtonCheck);
            this.Controls.Add(this.TextBoxOfMessages);
            this.Controls.Add(this.listBox2);
            this.Controls.Add(this.listBox1);
            this.Name = "Form1";
            this.Text = "Провекра на спам";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBox1;
        private System.Windows.Forms.ListBox listBox2;
        private System.Windows.Forms.TextBox TextBoxOfMessages;
        private System.Windows.Forms.Button ButtonCheck;
        private System.Windows.Forms.Button Connect;
    }
}

