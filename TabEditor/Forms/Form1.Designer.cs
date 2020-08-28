using System.Windows.Forms;

namespace DBCourse
{
    partial class MainWindow
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
            this.WriteTextBox = new System.Windows.Forms.RichTextBox();
            this.XmlExample = new System.Windows.Forms.Button();
            this.ABCExample = new System.Windows.Forms.Button();
            this.tuneup = new System.Windows.Forms.Button();
            this.tunedown = new System.Windows.Forms.Button();
            this.paths = new System.Windows.Forms.ListBox();
            this.save = new System.Windows.Forms.Button();
            this.load = new System.Windows.Forms.Button();
            this.delete = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            this.loginButton = new System.Windows.Forms.Button();
            this.versions = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.listBox1 = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // WriteTextBox
            // 
            this.WriteTextBox.Location = new System.Drawing.Point(12, 37);
            this.WriteTextBox.Name = "WriteTextBox";
            this.WriteTextBox.Size = new System.Drawing.Size(583, 275);
            this.WriteTextBox.TabIndex = 0;
            this.WriteTextBox.Text = "";
            // 
            // XmlExample
            // 
            this.XmlExample.Location = new System.Drawing.Point(500, 333);
            this.XmlExample.Name = "XmlExample";
            this.XmlExample.Size = new System.Drawing.Size(95, 22);
            this.XmlExample.TabIndex = 2;
            this.XmlExample.Text = "Шаблон MXML";
            this.XmlExample.UseVisualStyleBackColor = true;
            this.XmlExample.Click += new System.EventHandler(this.XmlExample_Click);
            // 
            // ABCExample
            // 
            this.ABCExample.Location = new System.Drawing.Point(500, 361);
            this.ABCExample.Name = "ABCExample";
            this.ABCExample.Size = new System.Drawing.Size(95, 25);
            this.ABCExample.TabIndex = 5;
            this.ABCExample.Text = "Шаблон ABC";
            this.ABCExample.UseVisualStyleBackColor = true;
            this.ABCExample.Click += new System.EventHandler(this.ABCExample_Click);
            // 
            // tuneup
            // 
            this.tuneup.Location = new System.Drawing.Point(500, 439);
            this.tuneup.Name = "tuneup";
            this.tuneup.Size = new System.Drawing.Size(95, 23);
            this.tuneup.TabIndex = 15;
            this.tuneup.Text = "На тон выше";
            this.tuneup.UseVisualStyleBackColor = true;
            this.tuneup.Click += new System.EventHandler(this.tuneUp);
            // 
            // tunedown
            // 
            this.tunedown.Location = new System.Drawing.Point(500, 468);
            this.tunedown.Name = "tunedown";
            this.tunedown.Size = new System.Drawing.Size(95, 23);
            this.tunedown.TabIndex = 16;
            this.tunedown.Text = "На тон ниже";
            this.tunedown.UseVisualStyleBackColor = true;
            this.tunedown.Click += new System.EventHandler(this.tuneDown);
            // 
            // paths
            // 
            this.paths.FormattingEnabled = true;
            this.paths.HorizontalScrollbar = true;
            this.paths.Location = new System.Drawing.Point(12, 346);
            this.paths.Name = "paths";
            this.paths.Size = new System.Drawing.Size(300, 95);
            this.paths.TabIndex = 18;
            this.paths.SelectedIndexChanged += new System.EventHandler(this.paths_SelectedIndexChanged);
            // 
            // save
            // 
            this.save.Location = new System.Drawing.Point(325, 432);
            this.save.Name = "save";
            this.save.Size = new System.Drawing.Size(144, 20);
            this.save.TabIndex = 19;
            this.save.Text = "Сохранить новую запись в базу";
            this.save.UseVisualStyleBackColor = true;
            this.save.Click += new System.EventHandler(this.save_Click);
            // 
            // load
            // 
            this.load.Location = new System.Drawing.Point(325, 346);
            this.load.Name = "load";
            this.load.Size = new System.Drawing.Size(144, 22);
            this.load.TabIndex = 20;
            this.load.Text = "Загрузить список файлов\r\n";
            this.load.UseVisualStyleBackColor = true;
            this.load.Click += new System.EventHandler(this.loadFileNames);
            // 
            // delete
            // 
            this.delete.Location = new System.Drawing.Point(325, 458);
            this.delete.Name = "delete";
            this.delete.Size = new System.Drawing.Size(144, 23);
            this.delete.TabIndex = 21;
            this.delete.Text = "Удалить из базы";
            this.delete.UseVisualStyleBackColor = true;
            this.delete.Click += new System.EventHandler(this.deleteFile);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(105, 9);
            this.label1.MinimumSize = new System.Drawing.Size(65, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 15);
            this.label1.TabIndex = 22;
            // 
            // comboBox1
            // 
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Items.AddRange(new object[] {
            "2/8",
            "4/4",
            "3/4",
            "3/8",
            "6/4",
            "6/8"});
            this.comboBox1.Location = new System.Drawing.Point(500, 412);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(95, 21);
            this.comboBox1.TabIndex = 23;
            this.comboBox1.SelectedIndexChanged += new System.EventHandler(this.meterChange);
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(325, 403);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(144, 21);
            this.button3.TabIndex = 24;
            this.button3.Text = "Сохранить изменения";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.updadeDatabase);
            // 
            // loginButton
            // 
            this.loginButton.Location = new System.Drawing.Point(500, 532);
            this.loginButton.Name = "loginButton";
            this.loginButton.Size = new System.Drawing.Size(95, 36);
            this.loginButton.TabIndex = 25;
            this.loginButton.Text = "Авторизация";
            this.loginButton.UseVisualStyleBackColor = true;
            this.loginButton.Click += new System.EventHandler(this.loginButton_Click);
            // 
            // versions
            // 
            this.versions.Location = new System.Drawing.Point(325, 374);
            this.versions.Name = "versions";
            this.versions.Size = new System.Drawing.Size(144, 23);
            this.versions.TabIndex = 26;
            this.versions.Text = "История версий";
            this.versions.UseVisualStyleBackColor = true;
            this.versions.Click += new System.EventHandler(this.versions_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(325, 504);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(144, 38);
            this.button1.TabIndex = 27;
            this.button1.Text = "Собрать статистику изменений по всей базе";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // listBox1
            // 
            this.listBox1.FormattingEnabled = true;
            this.listBox1.Location = new System.Drawing.Point(16, 477);
            this.listBox1.Name = "listBox1";
            this.listBox1.Size = new System.Drawing.Size(296, 95);
            this.listBox1.TabIndex = 28;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(614, 580);
            this.Controls.Add(this.listBox1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.versions);
            this.Controls.Add(this.loginButton);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.comboBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.delete);
            this.Controls.Add(this.load);
            this.Controls.Add(this.save);
            this.Controls.Add(this.paths);
            this.Controls.Add(this.tunedown);
            this.Controls.Add(this.tuneup);
            this.Controls.Add(this.ABCExample);
            this.Controls.Add(this.XmlExample);
            this.Controls.Add(this.WriteTextBox);
            this.Name = "MainWindow";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Нотный редактор";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox WriteTextBox;
        private System.Windows.Forms.Button XmlExample;
        private System.Windows.Forms.Button ABCExample;
        private Button tuneup;
        private Button tunedown;
        private ListBox paths;
        private Button save;
        private Button load;
        private Button delete;
        private Label label1;
        private ComboBox comboBox1;
        private Button button3;
        private Button loginButton;
        private Button versions;
        private Button button1;
        private ListBox listBox1;
    }
}

