﻿using System;
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
    public partial class Sorting : Form
    {
        enum WorkerParam { name, input_list };
        enum AppState { Idle, CreatingRandom, Sorting };
        private const int DEFAULT_COUNT = 11;
        private BackgroundWorker bw;
        private Stopwatch sw;
        private string result;

        public Sorting()
        {
            InitializeComponent();

            bw = new BackgroundWorker();
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);

            this.randomInputList();
        }

        private void UpdateAppState(AppState newState)
        {
            switch (newState)
            {
                case AppState.CreatingRandom:
                    this.randomInputButton.Enabled = false;
                    this.inputBox.Text = "";
                    this.outputBox.Text = "";
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
                    this.outputBox.Text = "";
                    break;
            }
        }

        private void randomInputList()
        {
            UpdateAppState(AppState.CreatingRandom);
            this.append_logging("Creating random input.");
            List<int> items = new List<int>();
            Random rand = new Random();

            for (int i = 0; i < Int32.Parse(this.countInput.Text); i++)
            {
                items.Add(rand.Next(0,100));
            }

            this.inputBox.Text = String.Join(",", items);
            this.append_logging("Done creating random input.");
            UpdateAppState(AppState.Idle);
        }

        private void randomInputButton_Click(object sender, EventArgs e)
        {
            this.randomInputList();
            this.outputBox.Text = "";
        }

        private void countInput_TextChanged(object sender, EventArgs e)
        {
            int count;
            if (!Int32.TryParse(this.countInput.Text, out count))
            {
                this.append_logging("InputCount: Only numbers allowed.");
                this.countInput.Text = DEFAULT_COUNT.ToString();
            }

            if (this.countInput.Text.Length > 5)
            {
                this.append_logging("InputCount: Only up to 99999 allowed");
                this.countInput.Text = DEFAULT_COUNT.ToString();
            }
        }

        private void append_logging(string msg)
        {
           this.loggingBox.Text = this.loggingBox.Text.Insert(0, 
                    DateTime.Now.ToLongTimeString() + ": " + msg + Environment.NewLine);
        }

        private void listBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.listBoxAlgorithms.SelectedItem == null)
            {
                return;
            }
            if (this.bw.IsBusy)
            {
                this.append_logging("Error: Should not be ble to select algorithm list while sorting is taking place.");
            }
            else
            {
                UpdateAppState(AppState.Sorting);
                var alg = this.listBoxAlgorithms.SelectedItem.ToString();
                this.append_logging("Sorting: " + alg + " started.");

                object[] parameters = new object[] { alg, this.inputBox.Text };
                sw = new Stopwatch();
                sw.Start();
                this.bw.RunWorkerAsync(parameters);
            }
        }

        private void Sorting_Load(object sender, EventArgs e)
        {

        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;
            object[] backgroundWorkerParameters = e.Argument as object[];

            string methodName = (string)backgroundWorkerParameters[(int)WorkerParam.name];
            var inputType = new Type[] { typeof(System.String), typeof(string).MakeByRefType() };
            MethodInfo method = typeof(Algorithms).GetMethod(methodName, inputType);

            object[] algorithmParameters = new object[] { backgroundWorkerParameters[(int)WorkerParam.input_list], null };
            method.Invoke(null, algorithmParameters);
            this.result = (string)algorithmParameters[1];
            e.Result = "Success.";
        }

        private void bw_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Cancelled)
            {
                this.append_logging("Sorting thread was cancelled.");
            }
            else if (e.Error != null)
            {
                this.append_logging("Error in sorting thread");
                this.append_logging(e.Error.ToString());
            }
            else
            {
                sw.Stop();
                var output = (string)e.Result;
                this.append_logging("Sorting: completed: " + output);
                this.append_logging("Sorting: total time " + sw.Elapsed.TotalMilliseconds + " ms");

                this.outputBox.SuspendLayout();
                this.outputBox.Text = this.result;
                this.outputBox.ResumeLayout();
                this.append_logging("Done marshalling data to UI thread.");
            }

            this.UpdateAppState(AppState.Idle);
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

    
}
