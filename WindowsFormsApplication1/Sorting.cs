using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Sorting
{
    /// <summary>
    /// Windows Forms app to run various sorting algorithms using random or custom input
    /// </summary>
    public partial class Sorting : Form
    {
        private const int DEFAULT_COUNT = 11;
        private BackgroundWorker bw;
        private Stopwatch sw;
        private string result;

        /// <summary>
        /// Initialize, register event handlers, and create random input.
        /// </summary>
        public Sorting()
        {
            this.InitializeComponent();

            this.bw = new BackgroundWorker();
            this.bw.DoWork += new DoWorkEventHandler(this.BackgroundWorkDoWork);
            this.bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.BackgroundWorkerRunWorkerCompleted);

            this.RandomInputList();
        }

        /// <summary>
        /// Friendly parameter names for BackgroundWorker.
        /// </summary>
        enum WorkerParam
        {
            name, input_list
        }

        /// <summary>
        /// Friendly names for tracking this app's state.
        /// </summary>
        enum AppState
        {
            Idle, CreatingRandom, Sorting
        }

        /// <summary>
        /// Drive UI components depending on app state.
        /// </summary>
        /// <param name="newState"></param>
        private void UpdateAppState(AppState newState)
        {
            switch (newState)
            {
                case AppState.CreatingRandom:
                    this.randomInputButton.Enabled = false;
                    this.inputBox.Text = string.Empty;
                    this.outputBox.Text = string.Empty;
                    this.listBoxAlgorithms.ClearSelected();
                    this.statusTextBox.Text = "Done.";
                    break;
                case AppState.Idle:
                    this.randomInputButton.Enabled = true;
                    this.listBoxAlgorithms.Enabled = true;
                    this.randomInputButton.Enabled = true;
                    this.inputBox.Enabled = true;
                    this.countInput.Enabled = true;
                    if (this.statusTextBox.Text != "Failure.")
                    {
                        this.statusTextBox.Text = "Done.";
                    }

                    break;
                case AppState.Sorting:
                    this.randomInputButton.Enabled = false;
                    this.listBoxAlgorithms.Enabled = false;
                    this.randomInputButton.Enabled = false;
                    this.inputBox.Enabled = false;
                    this.countInput.Enabled = false;
                    this.statusTextBox.Text = "Running.";
                    this.outputBox.Text = string.Empty;
                    break;
            }
        }

        /// <summary>
        /// Create random input for sorting.
        /// </summary>
        private void RandomInputList()
        {
            this.UpdateAppState(AppState.CreatingRandom);
            this.AppendLogging("Creating random input.");
            List<int> items = new List<int>();
            Random rand = new Random();

            for (int i = 0; i < int.Parse(this.countInput.Text); i++)
            {
                items.Add(rand.Next(0, 100));
            }

            this.inputBox.Text = string.Join(",", items);
            this.AppendLogging("Done creating random input.");
            this.UpdateAppState(AppState.Idle);
        }

        /// <summary>
        /// Event handler for Random Input button.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RandomInputButtonClick(object sender, EventArgs e)
        {
            this.RandomInputList();
            this.outputBox.Text = string.Empty;
        }

        /// <summary>
        /// Event handler to validate count of random input.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CountInputTextChanged(object sender, EventArgs e)
        {
            int count;
            if (!int.TryParse(this.countInput.Text, out count))
            {
                this.AppendLogging("InputCount: Only numbers allowed.");
                this.countInput.Text = DEFAULT_COUNT.ToString();
            }

            if (this.countInput.Text.Length > 5)
            {
                this.AppendLogging("InputCount: Only up to 99999 allowed");
                this.countInput.Text = DEFAULT_COUNT.ToString();
            }
        }

        /// <summary>
        /// Append text to log text box and append newline at the end.
        /// </summary>
        /// <param name="msg"></param>
        private void AppendLogging(string msg)
        {
           this.loggingBox.Text = this.loggingBox.Text.Insert(
               0, DateTime.Now.ToLongTimeString() + ": " + msg + Environment.NewLine);
        }

        /// <summary>
        /// Event handler for list of sorting algorithms. Prepare app, then execute sorting algorithm.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBoxAlgorithms.SelectedItem == null)
            {
                return;
            }

            if (this.bw.IsBusy)
            {
                this.AppendLogging("Error: Should not be ble to select algorithm list while sorting is taking place.");
            }
            else
            {
                this.UpdateAppState(AppState.Sorting);
                var alg = this.listBoxAlgorithms.SelectedItem.ToString();
                this.AppendLogging("Sorting: " + alg + " started.");

                object[] parameters = new object[] { alg, this.inputBox.Text };
                this.sw = new Stopwatch();
                this.sw.Start();
                this.bw.RunWorkerAsync(parameters);
            }
        }

        /// <summary>
        /// Autogenerated method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Sorting_Load(object sender, EventArgs e)
        {
        }

        /// <summary>
        /// Run sorting algorithm in the background.
        /// TODO: add support for cancellation.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackgroundWorkDoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            object[] backgroundWorkerParameters = e.Argument as object[];

            string methodName = (string)backgroundWorkerParameters[(int)WorkerParam.name];
            var inputType = new Type[] { typeof(string), typeof(string).MakeByRefType() };
            MethodInfo method = typeof(Algorithms).GetMethod(methodName, inputType);

            object[] algorithmParameters = new object[] { backgroundWorkerParameters[(int)WorkerParam.input_list], null };
            method.Invoke(null, algorithmParameters);
            this.result = (string)algorithmParameters[1];
            e.Result = "Success.";
        }

        /// <summary>
        /// Event handler for when background workder completes.
        /// Update logging, app state, and process result.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackgroundWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                this.AppendLogging("Sorting thread was cancelled.");
            }
            else if (e.Error != null)
            {
                this.AppendLogging("Error in sorting thread");
                this.AppendLogging(e.Error.ToString());
            }
            else
            {
                this.sw.Stop();
                var output = (string)e.Result;
                this.AppendLogging("Sorting: completed: " + output);
                this.AppendLogging("Sorting: total time " + this.sw.Elapsed.TotalMilliseconds + " ms");

                this.outputBox.SuspendLayout();
                this.outputBox.Text = this.result;
                this.outputBox.ResumeLayout();
                this.AppendLogging("Done marshalling data to UI thread.");
            }

            this.UpdateAppState(AppState.Idle);
        }

        /// <summary>
        /// Ensure only command separated integers in input box.
        /// </summary>
        /// <returns></returns>
        private bool ValidateInputBox()
        {
            List<string> numbers = this.inputBox.Text.Split(',').ToList();
            List<int> nums = new List<int>();
            for (int i = 0; i < numbers.Count; i++)
            {
                if (i + 1 == numbers.Count && string.IsNullOrWhiteSpace(numbers[i]))
                {
                    continue;
                }

                int conversion;
                var success = int.TryParse(numbers[i].Trim(), out conversion);
                if (!success)
                {
                    return false;
                }

                nums.Add(conversion);
            }

            return true;
        }

        /// <summary>
        /// Event handler for validating input text box's contents.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void InputBoxTextChanged(object sender, EventArgs e)
        {
            if (!this.ValidateInputBox())
            {
                this.AppendLogging("Invalid input provided, reverting.");
                this.RandomInputList();
            }
        }
    }
}
