// -----------------------------------------------------------------------
// <copyright file="FormTask.Designer.cs" company="Peter Chapman">
// Copyright 2015 Peter Chapman. See https://github.com/pmachapman/TaskMe/ for licence details.
// </copyright>
// -----------------------------------------------------------------------

namespace Conglomo.TaskMe
{
    partial class FormTask
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FormTask));
            this.AddButton = new System.Windows.Forms.Button();
            this.TasksCheckedListBox = new System.Windows.Forms.CheckedListBox();
            this.TaskTextBox = new System.Windows.Forms.TextBox();
            this.ToolTips = new System.Windows.Forms.ToolTip(this.components);
            this.SuspendLayout();
            // 
            // AddButton
            // 
            this.AddButton.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.AddButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.AddButton.Location = new System.Drawing.Point(314, 12);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(40, 21);
            this.AddButton.TabIndex = 1;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // TasksCheckedListBox
            // 
            this.TasksCheckedListBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TasksCheckedListBox.FormattingEnabled = true;
            this.TasksCheckedListBox.IntegralHeight = false;
            this.TasksCheckedListBox.Location = new System.Drawing.Point(12, 39);
            this.TasksCheckedListBox.Name = "TasksCheckedListBox";
            this.TasksCheckedListBox.Size = new System.Drawing.Size(341, 131);
            this.TasksCheckedListBox.TabIndex = 2;
            this.TasksCheckedListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.TasksCheckedListBox_ItemCheck);
            this.TasksCheckedListBox.KeyUp += new System.Windows.Forms.KeyEventHandler(this.TasksCheckedListBox_KeyUp);
            // 
            // TaskTextBox
            // 
            this.TaskTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TaskTextBox.Location = new System.Drawing.Point(13, 13);
            this.TaskTextBox.Name = "TaskTextBox";
            this.TaskTextBox.Size = new System.Drawing.Size(295, 20);
            this.TaskTextBox.TabIndex = 0;
            // 
            // FormTask
            // 
            this.AcceptButton = this.AddButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(366, 182);
            this.Controls.Add(this.TaskTextBox);
            this.Controls.Add(this.TasksCheckedListBox);
            this.Controls.Add(this.AddButton);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "FormTask";
            this.Text = "TaskMe";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FormTask_FormClosing);
            this.Load += new System.EventHandler(this.FormTask_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.CheckedListBox TasksCheckedListBox;
        private System.Windows.Forms.TextBox TaskTextBox;
        private System.Windows.Forms.ToolTip ToolTips;
    }
}