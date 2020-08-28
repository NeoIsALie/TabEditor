namespace DBCourse
{
    partial class Registration
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
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.fullnameBox = new System.Windows.Forms.TextBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.passwordCheck = new System.Windows.Forms.TextBox();
            this.passwordBox = new System.Windows.Forms.TextBox();
            this.loginBox = new System.Windows.Forms.TextBox();
            this.registerBtn = new System.Windows.Forms.Button();
            this.errLbl = new System.Windows.Forms.Label();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();

            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.fullnameBox);
            this.groupBox1.Location = new System.Drawing.Point(16, 70);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(265, 142);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Данные о пользователе";
            // 
            // fullnameBox
            // 
            this.fullnameBox.Location = new System.Drawing.Point(6, 40);
            this.fullnameBox.Name = "fullnameBox";
            this.fullnameBox.Size = new System.Drawing.Size(253, 22);
            this.fullnameBox.TabIndex = 1;
            this.fullnameBox.Text = "Полное имя (латинские буквы)";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.passwordCheck);
            this.groupBox2.Controls.Add(this.passwordBox);
            this.groupBox2.Controls.Add(this.loginBox);
            this.groupBox2.Location = new System.Drawing.Point(15, 227);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(265, 182);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Логин/пароль";
            // 
            // passwordCheck
            // 
            this.passwordCheck.Location = new System.Drawing.Point(6, 137);
            this.passwordCheck.Name = "passwordCheck";
            this.passwordCheck.Size = new System.Drawing.Size(253, 22);
            this.passwordCheck.TabIndex = 4;
            this.passwordCheck.Text = "Повторите пароль";
            this.passwordCheck.TextChanged += new System.EventHandler(this.passwordCheck_TextChanged);
            // 
            // passwordBox
            // 
            this.passwordBox.Location = new System.Drawing.Point(6, 85);
            this.passwordBox.Name = "passwordBox";
            this.passwordBox.Size = new System.Drawing.Size(253, 22);
            this.passwordBox.TabIndex = 3;
            this.passwordBox.Text = "Пароль";
            this.passwordBox.TextChanged += new System.EventHandler(this.passwordBox_TextChanged);
            // 
            // loginBox
            // 
            this.loginBox.Location = new System.Drawing.Point(6, 35);
            this.loginBox.Name = "loginBox";
            this.loginBox.Size = new System.Drawing.Size(253, 22);
            this.loginBox.TabIndex = 2;
            this.loginBox.Text = "Логин";
            // 
            // registerBtn
            // 
            this.registerBtn.Location = new System.Drawing.Point(16, 415);
            this.registerBtn.Name = "registerBtn";
            this.registerBtn.Size = new System.Drawing.Size(264, 23);
            this.registerBtn.TabIndex = 3;
            this.registerBtn.Text = "Зарегистрироваться";
            this.registerBtn.UseVisualStyleBackColor = true;
            this.registerBtn.Click += new System.EventHandler(this.registerBtn_Click);
            // 
            // errLbl
            // 
            this.errLbl.AutoSize = true;
            this.errLbl.Location = new System.Drawing.Point(18, 18);
            this.errLbl.Name = "errLbl";
            this.errLbl.Size = new System.Drawing.Size(61, 17);
            this.errLbl.TabIndex = 4;
            this.errLbl.Text = "Ошибка";
            // 
            // Form6
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(292, 450);
            this.Controls.Add(this.errLbl);
            this.Controls.Add(this.registerBtn);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Name = "Form6";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Регистрация";
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TextBox fullnameBox;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TextBox passwordCheck;
        private System.Windows.Forms.TextBox passwordBox;
        private System.Windows.Forms.TextBox loginBox;
        private System.Windows.Forms.Button registerBtn;
        private System.Windows.Forms.Label errLbl;
    }
}