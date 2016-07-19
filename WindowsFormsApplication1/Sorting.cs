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
        enum param { name = 0, input_list = 1 };
        private const int DEFAULT_COUNT = 11;
        private BackgroundWorker bw;

        public Sorting()
        {
            InitializeComponent();

            bw = new BackgroundWorker();
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);

            this.randomInputList();
        }

        private void randomInputList()
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

            this.inputBox.Text = output;
        }

        private void randomInputButton_Click(object sender, EventArgs e)
        {
            this.randomInputList();
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
                this.countInput.Text = DEFAULT_COUNT.ToString();
            }
        }

        private void append_logging(string msg)
        {
            if (this.loggingBox.InvokeRequired)
            {
                this.loggingBox.Invoke(new updateDelegate(append_logging), msg);
            }
            else
            {
                this.loggingBox.Text = this.loggingBox.Text.Insert(0, 
                    DateTime.Now.ToLongTimeString() + ": " + msg + Environment.NewLine);
            }
        }

        private void changeElementsReadOnly(bool enable)
        {
            this.listBoxAlgorithms.Enabled = enable;
            this.randomInputButton.Enabled = enable;
            this.inputBox.Enabled = enable;
            this.countInput.Enabled = enable;
            this.statusTextBox.Text = enable ? "Done." : "Running...";
        }
        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.bw.IsBusy)
            {
                this.append_logging("Error: Should not be ble to select algorithm list while sorting is taking place.");
            }
            else
            {
                this.changeElementsReadOnly(enable: false);
            }

            var alg = this.listBoxAlgorithms.SelectedItem.ToString();               
            this.append_logging("ListBox: " + alg + " selected.");

            object[] parameters = new object[] { alg, this.inputBox.Text };
            this.bw.RunWorkerAsync(parameters);
        }

        private void Sorting_Load(object sender, EventArgs e)
        {

        }

        delegate void updateDelegate(string val);

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            object[] parameters = e.Argument as object[];

            for (int i = 0; i < 6; i++)
            {
                if (worker.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                else
                {
                    System.Threading.Thread.Sleep(5 * 1000);
                }
            }

            this.append_logging("Message from bw.");
            e.Result = parameters[(int)param.input_list];
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                this.append_logging("Worker: thread was cancelled.");
            }
            else if (e.Error != null)
            {
                this.append_logging("Worker: error in worker thread");
                this.append_logging(e.Error.ToString());
            }
            else
            {
                this.append_logging("Worker: thread exited successfully.");
                this.outputBox.Text= e.Result.ToString();
            }

            this.changeElementsReadOnly(enable: true);
        }

        private void cancelInputButton_Click(object sender, EventArgs e)
        {
            if (!bw.IsBusy)
            {
                this.append_logging("Cancel: nothing is running.");
            }
            else if (bw.WorkerSupportsCancellation)
            {
                bw.CancelAsync();
                this.append_logging("Cancel: called CancelAsync.");
            }
            else
            {
                this.append_logging("Cancel: worker does not suppor cancellation.");
            }
        }

        private bool validate_inputBox(OptionalOut<List<int>> inputList = null)
        {
            List<string> numbers = this.inputBox.Text.Split(',').ToList();
            List<int> nums = new List<int>();
            for (int i = 0; i < numbers.Count; i++)
            {
                if (i+1 == numbers.Count && String.IsNullOrWhiteSpace(numbers[i]))
                {
                    continue;
                }
                int conversion;
                var success = Int32.TryParse(numbers[i].Trim(), out conversion);
                if (!success)
                {
                    return false;
                }
                nums.Add(conversion);
            }

            if (inputList != null)
            {
                inputList.Result = nums;
            }
            return true;
        }

        private void inputBox_TextChanged(object sender, EventArgs e)
        {
            if (!this.validate_inputBox())
            {
                this.append_logging("Invalid input provided, reverting.");
                this.randomInputList();
            }
        }
    }

    public class OptionalOut<Type>
    {
        public Type Result { get; set; }
    }
}
