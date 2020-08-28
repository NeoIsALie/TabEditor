using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DBCourse
{
    public partial class Info : Form
    {
        private DataTables data;
        private int originalVersionId;
        public List<string> filepaths;
        public List<string> info;
        public List<string> statistics;
        public string selectedVersionPath = "";

        public Info(DataTables dt, int originalId)
        {
            InitializeComponent();
            data = dt;
            originalVersionId = originalId;
            filepaths = new List<string>();
            info = new List<string>();
            statistics = new List<string>();
            loadPaths();
            showData();
        }

        private void showData()
        {
            listBox1.Items.Clear();
            info = data.getVersionsInfo(originalVersionId);
            statistics = data.GetStatistics(originalVersionId);
            foreach (string i in info)
            {
                listBox1.Items.Add(i);
            }
            foreach(string i in statistics)
            {
                listBox3.Items.Add(i);
            }
        }

        private void loadPaths()
        {
            listBox2.Items.Clear();
            filepaths = data.GetVersionsFilePaths(originalVersionId);
            foreach(string path in filepaths)
            {
                listBox2.Items.Add(path.Trim());
            }
        }

        private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            selectedVersionPath = Convert.ToString(listBox2.SelectedItem);
            System.Diagnostics.Debug.Print(selectedVersionPath);
        }

        private void Info_Closed(object sender, EventArgs e)
        {
            Close();
        }

    }
}
