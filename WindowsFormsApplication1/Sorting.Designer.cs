﻿namespace Sorting
{
    partial class Sorting
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
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
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
            this.listBoxAlgorithms = new System.Windows.Forms.ListBox();
            this.countInput = new System.Windows.Forms.TextBox();
            this.countLabel = new System.Windows.Forms.Label();
            this.inputBox = new System.Windows.Forms.TextBox();
            this.randomInputButton = new System.Windows.Forms.Button();
            this.outputBox = new System.Windows.Forms.TextBox();
            this.inputLabel = new System.Windows.Forms.Label();
            this.outputLabel = new System.Windows.Forms.Label();
            this.loggingBox = new System.Windows.Forms.TextBox();
            this.statusLabel = new System.Windows.Forms.Label();
            this.statusTextBox = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // listBoxAlgorithms
            // 
            this.listBoxAlgorithms.FormattingEnabled = true;
            this.listBoxAlgorithms.ItemHeight = 20;
            this.listBoxAlgorithms.Items.AddRange(new object[] {
            "SelectionSort",
            "BubbleSort",
            "InsertionSort",
            "MergeSort",
            "LomutoQuickSort",
            "HoareQuickSort"});
            this.listBoxAlgorithms.Location = new System.Drawing.Point(12, 12);
            this.listBoxAlgorithms.Name = "listBoxAlgorithms";
            this.listBoxAlgorithms.Size = new System.Drawing.Size(171, 524);
            this.listBoxAlgorithms.TabIndex = 0;
            this.listBoxAlgorithms.SelectedIndexChanged += new System.EventHandler(this.ListBoxSelectedIndexChanged);
            // 
            // countInput
            // 
            this.countInput.Location = new System.Drawing.Point(454, 25);
            this.countInput.Name = "countInput";
            this.countInput.Size = new System.Drawing.Size(55, 26);
            this.countInput.TabIndex = 1;
            this.countInput.Text = "11";
            this.countInput.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this.countInput.TextChanged += new System.EventHandler(this.CountInputTextChanged);
            // 
            // countLabel
            // 
            this.countLabel.AutoSize = true;
            this.countLabel.Location = new System.Drawing.Point(395, 28);
            this.countLabel.Name = "countLabel";
            this.countLabel.Size = new System.Drawing.Size(56, 20);
            this.countLabel.TabIndex = 2;
            this.countLabel.Text = "Count:";
            // 
            // inputBox
            // 
            this.inputBox.Location = new System.Drawing.Point(189, 95);
            this.inputBox.Multiline = true;
            this.inputBox.Name = "inputBox";
            this.inputBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.inputBox.Size = new System.Drawing.Size(641, 194);
            this.inputBox.TabIndex = 3;
            this.inputBox.TextChanged += new System.EventHandler(this.InputBoxTextChanged);
            // 
            // randomInputButton
            // 
            this.randomInputButton.Location = new System.Drawing.Point(563, 14);
            this.randomInputButton.Name = "randomInputButton";
            this.randomInputButton.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.randomInputButton.Size = new System.Drawing.Size(216, 49);
            this.randomInputButton.TabIndex = 2;
            this.randomInputButton.Text = "Random input";
            this.randomInputButton.UseVisualStyleBackColor = true;
            this.randomInputButton.Click += new System.EventHandler(this.RandomInputButtonClick);
            // 
            // outputBox
            // 
            this.outputBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.outputBox.Location = new System.Drawing.Point(189, 319);
            this.outputBox.Multiline = true;
            this.outputBox.Name = "outputBox";
            this.outputBox.ReadOnly = true;
            this.outputBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.outputBox.Size = new System.Drawing.Size(641, 194);
            this.outputBox.TabIndex = 5;
            this.outputBox.WordWrap = false;
            // 
            // inputLabel
            // 
            this.inputLabel.AutoSize = true;
            this.inputLabel.Location = new System.Drawing.Point(185, 72);
            this.inputLabel.Name = "inputLabel";
            this.inputLabel.Size = new System.Drawing.Size(46, 20);
            this.inputLabel.TabIndex = 6;
            this.inputLabel.Text = "Input";
            // 
            // outputLabel
            // 
            this.outputLabel.AutoSize = true;
            this.outputLabel.Location = new System.Drawing.Point(185, 296);
            this.outputLabel.Name = "outputLabel";
            this.outputLabel.Size = new System.Drawing.Size(58, 20);
            this.outputLabel.TabIndex = 7;
            this.outputLabel.Text = "Output";
            // 
            // loggingBox
            // 
            this.loggingBox.BackColor = System.Drawing.SystemColors.ControlLightLight;
            this.loggingBox.Location = new System.Drawing.Point(13, 551);
            this.loggingBox.Multiline = true;
            this.loggingBox.Name = "loggingBox";
            this.loggingBox.ReadOnly = true;
            this.loggingBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.loggingBox.Size = new System.Drawing.Size(817, 145);
            this.loggingBox.TabIndex = 8;
            // 
            // statusLabel
            // 
            this.statusLabel.AutoSize = true;
            this.statusLabel.Location = new System.Drawing.Point(218, 28);
            this.statusLabel.Name = "statusLabel";
            this.statusLabel.Size = new System.Drawing.Size(60, 20);
            this.statusLabel.TabIndex = 9;
            this.statusLabel.Text = "Status:";
            // 
            // statusTextBox
            // 
            this.statusTextBox.Location = new System.Drawing.Point(284, 25);
            this.statusTextBox.Name = "statusTextBox";
            this.statusTextBox.ReadOnly = true;
            this.statusTextBox.Size = new System.Drawing.Size(85, 26);
            this.statusTextBox.TabIndex = 10;
            this.statusTextBox.Text = "Ready!";
            this.statusTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // Sorting
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(857, 715);
            this.Controls.Add(this.statusTextBox);
            this.Controls.Add(this.statusLabel);
            this.Controls.Add(this.loggingBox);
            this.Controls.Add(this.outputLabel);
            this.Controls.Add(this.inputLabel);
            this.Controls.Add(this.outputBox);
            this.Controls.Add(this.randomInputButton);
            this.Controls.Add(this.inputBox);
            this.Controls.Add(this.countLabel);
            this.Controls.Add(this.countInput);
            this.Controls.Add(this.listBoxAlgorithms);
            this.Name = "Sorting";
            this.Text = "Sorting Algorithms";
            this.Load += new System.EventHandler(this.Sorting_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListBox listBoxAlgorithms;
        private System.Windows.Forms.TextBox countInput;
        private System.Windows.Forms.Label countLabel;
        private System.Windows.Forms.TextBox inputBox;
        private System.Windows.Forms.Button randomInputButton;
        private System.Windows.Forms.TextBox outputBox;
        private System.Windows.Forms.Label inputLabel;
        private System.Windows.Forms.Label outputLabel;
        private System.Windows.Forms.TextBox loggingBox;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.TextBox statusTextBox;
    }
}