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
        enum algorithm
        {
            None,
            BubbleSort
        };

        private const int DEFAULT_COUNT = 11;
        private algorithm selectedAlgorithm = algorithm.None;

        public Sorting()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void randomInputButton_Click(object sender, EventArgs e)
        {
            var output = "";
            Random rand = new Random();
            for (int i=0; i < Int16.Parse(this.countInput.Text); i++)
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
            if (selectedAlgorithm == algorithm.None)
            {
                var alg = this.listBoxAlgorithms.SelectedItem.ToString();

               
                this.append_logging("ListBox: " + alg + " selected.");

            }
        }
    }
}
