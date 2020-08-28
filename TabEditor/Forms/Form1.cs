using System;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Threading.Tasks;
using System.IO;
using System.Security;
using ABCConverter;
using ABC;
using ABC.Domain;
using MusicXml;
using MusicXml.Domain;
using XML2ABCConverter;
using System.Collections.Generic;

namespace DBCourse
{
    public partial class MainWindow : Form
    {
        public Boolean loadedFromDatabase;
        public Boolean isABC;
        public Boolean isXML;
        public Boolean hasAuthorized = false;
        public Boolean inserted;
        public string selectedFile = "";
        public string xmlFormat = ".musicxml";
        public string compressedXmlFormat = ".mxml";
        public string abcFormat = ".abc";
        public string date = "";
        public string newVersionPath = "";
        public string currentUserLogin = "";
        public string newLogin = "";
        public string ressurectionPath = "";
        public int ownerId;
        public int originalVersionId = 0;
        private DataTables data;
        Authorization frm2;
        Info frm4;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            data = new DataTables();
            data.SetTables();
            Show();
        }

        private void ABCExample_Click(object sender, EventArgs e)
        {
            WriteTextBox.Text = File.ReadAllText(@"J:\DBCourse\DBCourse\DBCourse\abcexample.abc");
            isABC = true;
        }

        private void XmlExample_Click(object sender, EventArgs e)
        {
            WriteTextBox.Text = File.ReadAllText(@"J:\DBCourse\DBCourse\DBCourse\xmlexample.musicxml");
            isXML = true;
        }

        public void meterChange(object sender, EventArgs e)
        {
            if (loadedFromDatabase && isXML)
            {
                ChangeMeter.ChangeMeterXml(selectedFile, comboBox1.SelectedItem.ToString());
                WriteTextBox.Text = File.ReadAllText(selectedFile);
            }
            else if (loadedFromDatabase && isABC)
            {
                ChangeMeter.ChangeMeterABC(selectedFile, comboBox1.SelectedItem.ToString());
                WriteTextBox.Text = File.ReadAllText(selectedFile);
            }
        }

        // Выводит список файлов партитур(последние версии)
        private void loadFileNames(object sender, EventArgs e)
        {
            paths.Items.Clear();
            List<string> filenames = data.LoadFiles();
            foreach (string path in filenames)
            {
                paths.Items.Add(path);
            }
        }

        public void saveFile()
        {
            loadedFromDatabase = false;
            if (hasAuthorized)
            {
                SaveFileDialog dialog = new SaveFileDialog();
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    string path = dialog.FileName;
                    File.WriteAllText(dialog.FileName, WriteTextBox.Text);
                    if (path.Contains(abcFormat))
                    {
                        if (loadedFromDatabase == false)
                            data.insertNewPartitureABC(path, currentUserLogin, 0);
                        else
                            data.insertNewPartitureABC(path, currentUserLogin, originalVersionId);
                    }
                    else if (path.Contains(xmlFormat) || path.Contains(compressedXmlFormat))
                    {
                        if (loadedFromDatabase == false)
                        {
                            data.insertNewPartitureXml(path, currentUserLogin, 0);
                            if (inserted == false)
                            {
                                label1.Text = "Документ не прошел валидацию";
                                return;
                            }
                        }
                        else
                        {
                            data.insertNewPartitureXml(path, currentUserLogin, originalVersionId);
                        }
                    }
                }
                else if (dialog.ShowDialog() == DialogResult.Cancel)
                {
                    dialog.Dispose();
                    return;
                }
            }
            else
            {
                label1.Text = "Необходима авторизация";
                return;
            }
        }

        private void paths_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedFile = Convert.ToString(paths.SelectedItem);
            if (selectedFile.Contains(abcFormat))
            {
                isABC = true;
                isXML = false;
            }
            else if (selectedFile.Contains(xmlFormat) || selectedFile.Contains(compressedXmlFormat))
            {
                isXML = true;
                isABC = false;
            }

            WriteTextBox.Text = File.ReadAllText(selectedFile);

            loadedFromDatabase = true;

            originalVersionId = data.GetPartitureOriginalId(selectedFile);
        }

        private void deleteFile(object sender, EventArgs e)
        {
            if (hasAuthorized == true && selectedFile != "")
                data.deleteFromDatabase(selectedFile);
            else if (selectedFile == "")
                label1.Text = "Для удаления данных нужно выбрать файл";
            else if (hasAuthorized == false)
                label1.Text = "Необходима авторизация";
        }

        private void save_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(WriteTextBox.Text))
            {
                label1.Text = "Нечего сохранять";
                return;
            }
            else
            {
                if (hasAuthorized == true)
                {
                    label1.Text = "";
                    saveFile();
                    data.SetTables();
                }
                else
                {
                    label1.Text = "Необходима авторизация";
                    return;
                }
            }
        }

        private void tuneUp(object sender, EventArgs e)
        {
            if (loadedFromDatabase && isABC)
            {
                ABCTransposer.ChangeKeyABC(selectedFile, @"J:\DBCourse\DBCourse\DBCourse\temp.abc", 1);
                WriteTextBox.Text = File.ReadAllText(@"J:\DBCourse\DBCourse\DBCourse\temp.abc");
            }
            else if (isXML && loadedFromDatabase)
            {
                File.WriteAllText(@"J:\DBCourse\DBCourse\DBCourse\partitures\temp.musicxml", WriteTextBox.Text);
                XMLTransposer.Transpose(@"J:\DBCourse\DBCourse\DBCourse\partitures\temp.musicxml", 1);
                WriteTextBox.Text = File.ReadAllText(@"J:\DBCourse\DBCourse\DBCourse\partitures\temp.musicxml");
            }
            else
            {
                label1.Text = "Для начала сохраните файл в базу данных";
            }
        }

        private void loginButton_Click(object sender, EventArgs e)
        {
            if (frm2 == null)
            {
                frm2 = new Authorization(data);
                frm2.FormClosed += frm2_FormClosed;
            }
            frm2.Show(this);
            Hide();
        }

        private void updadeDatabase(object sender, EventArgs e)
        {
            if (String.IsNullOrWhiteSpace(WriteTextBox.Text))
            {
                label1.Text = "Нечего сохранять";
                return;
            }
            else if (hasAuthorized == true)
            {
                date = DateTime.Now.ToString().Replace('/', '.').Replace(':', ',');
                if (isXML)
                {
                    string[] filename = Path.GetFileNameWithoutExtension(selectedFile).Trim().Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries)[0].Split(new char[] { '(' }, StringSplitOptions.None);
                    System.Diagnostics.Debug.Print(filename[0]);
                    newVersionPath = @"J:\DBCourse\DBCourse\DBCourse\partitures\" + filename[0] + "(" + date + ")" + Path.GetExtension(selectedFile);
                    File.WriteAllText(newVersionPath, WriteTextBox.Text);
                    data.insertNewPartitureXml(newVersionPath, currentUserLogin, originalVersionId);
                }
                else
                {
                    string[] filename = Path.GetFileNameWithoutExtension(selectedFile).Trim().Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries)[0].Split(new char[] { '(' }, StringSplitOptions.None);
                    newVersionPath = @"J:\DBCourse\DBCourse\DBCourse\partitures\" + filename[0] + "(" + date + ")" + Path.GetExtension(selectedFile);
                    File.WriteAllText(newVersionPath, WriteTextBox.Text);
                    data.insertNewPartitureABC(newVersionPath, currentUserLogin, originalVersionId);
                }
            }
            else if (hasAuthorized == false)
            {
                label1.Text = "Необходима авторизация";
                return;
            }

        }

        private void tuneDown(object sender, EventArgs e)
        {
            if (loadedFromDatabase && isABC)
            {
                ABCTransposer.ChangeKeyABC(selectedFile, @"J:\DBCourse\DBCourse\DBCourse\temp.abc", -1);
                WriteTextBox.Text = File.ReadAllText(@"J:\DBCourse\DBCourse\DBCourse\temp.abc");
            }
            else if (loadedFromDatabase && isXML)
            {
                XMLTransposer.Transpose(selectedFile, -1);
                WriteTextBox.Text = File.ReadAllText(selectedFile);
            }
        }

        public void frm2_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!frm2.authorized)
            {
                Show();
                data.SetTables();
            }
            hasAuthorized = frm2.authorized;
            currentUserLogin = frm2.userLogin;
            label1.Text = "";
            frm2 = null;
            Show();
        }

        private void versions_Click(object sender, EventArgs e)
        {
            if (loadedFromDatabase == false)
            {
                label1.Text = "Сначала сохраните файл в базу";
            }
            else
            {
                if (frm4 == null)
                {
                    frm4 = new Info(data, originalVersionId);
                    frm4.FormClosed += versions_FormClosed;
                }
                frm4.Show(this);
                Hide();
            }
        }

        public void versions_FormClosed(object sender, FormClosedEventArgs e)
        {
            ressurectionPath = frm4.selectedVersionPath;
            frm4 = null;
            Show();
            data.SetTables();
            if (ressurectionPath != "")
            {
                WriteTextBox.Text = File.ReadAllText(ressurectionPath);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
                listBox1.Items.Clear();
                List<string> information = data.GetStatisticsWholeBase();
                foreach (string info in information)
                {
                    listBox1.Items.Add(info);
                }
                label1.Text = "";
        }
    }
}