using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApplication1
{

    public partial class Sorting : Form
    {
        private const int DEFAULT_COUNT = 11;
        private BackgroundWorker bw;

        public Sorting()
        {
            InitializeComponent();

            bw = new BackgroundWorker();
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);

            this.inputBox.Text = this.randomList(DEFAULT_COUNT.ToString());
        }

        private string randomList(string count)
        {
            var output = "";
            Random rand = new Random();
            for (int i = 0; i < Int16.Parse(this.countInput.Text); i++)
            {
                var r = rand.Next(0, 1000);
                if (output.Length == 0)
                {
                    output = r.ToString();
                }
                else
                {
                    output += ", " + r.ToString();
                }
            }

            return output;
        }

        private void randomInputButton_Click(object sender, EventArgs e)
        {
            var output = this.randomList(this.countInput.Text);

            this.inputBox.Text = output;
        }

        private void countInput_TextChanged(object sender, EventArgs e)
        {
            int count;
            if (!Int32.TryParse(this.countInput.Text, out count))
            {
                this.append_logging("InputCount: Only numbers allowed.");
                this.countInput.Text = DEFAULT_COUNT.ToString();
            }

            if (this.countInput.Text.Length >= 3)
            {
                this.append_logging("InputCount: Only up to 99 allowed");
            }
        }

        private void append_logging(string msg)
        {
            this.loggingBox.Text += msg + Environment.NewLine;
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bw.IsBusy)
            {
                this.append_logging("Error: Should not be ble to select algorithm list while sorting is taking place.");
            }
            else
            {
                this.listBoxAlgorithms.Enabled = false;
                this.randomInputButton.Enabled = false;
                this.inputBox.Enabled = false;
                this.countInput.Enabled = false;
            }

            var alg = this.listBoxAlgorithms.SelectedItem.ToString();               
            this.append_logging("ListBox: " + alg + " selected.");
        }

        private void Sorting_Load(object sender, EventArgs e)
        {

        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {

        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {

        }

        private void cancelInputButton_Click(object sender, EventArgs e)
        {
            if (bw.WorkerSupportsCancellation)
            {
                bw.CancelAsync();
            }
        }
    }
}
