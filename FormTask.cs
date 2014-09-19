// -----------------------------------------------------------------------
// <copyright file="FormTask.cs" company="Peter Chapman">
// Copyright 2014 Peter Chapman. See https://github.com/pmachapman/TaskMe/ for licence details.
// </copyright>
// -----------------------------------------------------------------------

namespace Conglomo.TaskMe
{
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
    using System.Security;
    using System.Windows.Forms;

    /// <summary>
    /// THe main task manager form.
    /// </summary>
    public partial class FormTask : Form
    {
        /// <summary>
        /// Initialises a new instance of the <see cref="FormTask"/> class.
        /// </summary>
        public FormTask()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Recreates Tasks.xml with sample data.
        /// </summary>
        [SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "This program is only in English")]
        private static void CreateSampleDatabase()
        {
            try
            {
                using (StreamWriter xmlFile = new StreamWriter("Tasks.xml"))
                {
                    xmlFile.WriteLine("<?xml version=\"1.0\" standalone=\"yes\"?>");
                    xmlFile.WriteLine("<Tasks>");
                    xmlFile.WriteLine("  <Task Name=\"Welcome To Task Me\" Completed=\"False\" />");
                    xmlFile.WriteLine("  <Task Name=\"©2014 Peter Chapman\" Completed=\"False\" />");
                    xmlFile.WriteLine("  <Task Name=\"To delete a task, click on it then press delete\" Completed=\"False\" />");
                    xmlFile.WriteLine("</Tasks>");
                }
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException
                    || ex is ArgumentNullException
                    || ex is DirectoryNotFoundException
                    || ex is FileNotFoundException
                    || ex is IOException
                    || ex is PathTooLongException
                    || ex is SecurityException
                    || ex is UnauthorizedAccessException)
                {
                    MessageBox.Show("Tasks.xml could not be created", "Task Me", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Handles the Click event of the Add Button.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        [SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "This program is only in English")]
        private void AddButton_Click(object sender, EventArgs e)
        {
            try
            {
                // Ensure we have a value
                if (!string.IsNullOrEmpty(this.TaskTextBox.Text))
                {
                    // Create the data set
                    using (DataSet tasks = new DataSet("Tasks"))
                    {
                        // Set the culture to the user's culture
                        tasks.Locale = CultureInfo.CurrentCulture;

                        // Load the Tasks from the XML file
                        tasks.ReadXml("Tasks.xml", XmlReadMode.Auto);

                        // Ensure we have a task table
                        if (tasks.Tables["Task"] == default(DataTable))
                        {
                            // Tasks do not exist, so go add them
                            using (DataTable table = new DataTable("Task"))
                            {
                                table.Locale = CultureInfo.CurrentCulture;

                                // Add the columns containing the task data needing to be stored
                                table.Columns.Add("Name", typeof(string));
                                table.Columns.Add("Completed", typeof(bool));

                                // Go through each column and make it an attribute
                                foreach (DataColumn column in table.Columns)
                                {
                                    column.ColumnMapping = MappingType.Attribute;
                                }

                                // Add the table to the dataset
                                tasks.Tables.Add(table);
                            }
                        }

                        // Create a new row
                        DataRow row = tasks.Tables["Task"].NewRow();

                        // Set the new row's name
                        row["Name"] = this.TaskTextBox.Text;

                        // Set the new row's completed status to default (false)
                        row["Completed"] = false;

                        // Add the row to the dataset
                        tasks.Tables["Task"].Rows.Add(row);

                        // Store changes to the dataset
                        tasks.AcceptChanges();

                        // Write the updated dataset to the XML file
                        tasks.WriteXml("Tasks.xml");

                        // Add the new task to the checked list box
                        this.TasksCheckedListBox.Items.Add(this.TaskTextBox.Text, false);

                        // Select the Task textbox's contents
                        this.TaskTextBox.SelectAll();
                        this.TaskTextBox.Focus();
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex is FileNotFoundException
                    || ex is SecurityException)
                {
                    MessageBox.Show("Your task could not be added", "Task Me", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Handles the FormClosing event of the Task Form.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="FormClosingEventArgs"/> instance containing the event data.</param>
        private void FormTask_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                // Create the data set
                using (DataSet tasks = new DataSet("Tasks"))
                {
                    // Set the culture to the user's culture
                    tasks.Locale = CultureInfo.CurrentCulture;

                    // Load the Tasks from the XML file
                    tasks.ReadXml("Tasks.xml", XmlReadMode.Auto);

                    // Save the window dimensions
                    this.WriteSettings(tasks);
                }
            }
            catch (Exception ex)
            {
                if (!(ex is FileNotFoundException
                    || ex is SecurityException))
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Handles the Load event of the Task Form.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        [SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "This program is only in English")]
        private void FormTask_Load(object sender, EventArgs e)
        {
            // Set the name of the window to the name of the executable
            this.Text = typeof(Program).Assembly.GetName().Name;

            // Create the data set
            using (DataSet tasks = new DataSet("Tasks"))
            {
                // Set the culture to the user's culture
                tasks.Locale = CultureInfo.CurrentCulture;

                // Load the Tasks from the XML file if possible
                try
                {
                    tasks.ReadXml("Tasks.xml", XmlReadMode.Auto);
                }
                catch (Exception ex)
                {
                    if (ex is FileNotFoundException
                        || ex is SecurityException)
                    {
                        // Does not exist, recreate the tasks XML file
                        CreateSampleDatabase();

                        // Load the Tasks XML file (this time it should work!)
                        tasks.ReadXml("Tasks.xml", XmlReadMode.Auto);
                    }
                    else
                    {
                        throw;
                    }
                }

                // Load the tasks from the DataSet into the checked list box
                // This will fail if Tasks.xml contains an empty tasks element
                try
                {
                    this.LoadTasks(tasks);
                }
                catch (Exception ex)
                {
                    if (ex is FileNotFoundException
                        || ex is SecurityException)
                    {
                        // Recreate the tasks XML file
                        CreateSampleDatabase();

                        // Load the Tasks XML file (this time it should work!)
                        tasks.ReadXml("Tasks.xml", XmlReadMode.Auto);

                        // Try to load the tasks again
                        this.LoadTasks(tasks);
                    }
                    else
                    {
                        throw;
                    }
                }

                // Set the Tooltips
                this.ToolTips.SetToolTip(this.TasksCheckedListBox, "Select a task, then press Delete on your keyboard to delete it");

                // Load window settings
                this.ReadSettings(tasks);
            }
        }

        /// <summary>
        /// Loads the tasks from the dataset into the checked list box.
        /// </summary>
        /// <param name="tasks">The tasks.</param>
        private void LoadTasks(DataSet tasks)
        {
            if (tasks.Tables["Task"] != default(DataTable))
            {
                foreach (DataRow row in tasks.Tables["Task"].Rows)
                {
                    // Task name
                    string taskName = row["Name"].ToString();

                    // Task completion status (true or false)
                    bool completed = row["Completed"] as bool? ?? false;

                    // Add the task to the checked list box
                    this.TasksCheckedListBox.Items.Add(taskName, completed);
                }
            }
        }

        /// <summary>
        /// Reads Window Settings from the XML file.
        /// </summary>
        /// <param name="tasks">The tasks.</param>
        [SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "This program is only in English")]
        private void ReadSettings(DataSet tasks)
        {
            try
            {
                // Open the row containing the window settings
                DataTable table = tasks.Tables["Settings"];
                if (table == default(DataTable))
                {
                    // Settings do not exist, so go add them
                    using (DataTable newTable = new DataTable("Settings"))
                    {
                        newTable.Locale = CultureInfo.CurrentCulture;

                        // Add the columns containing the settings needing to be stored
                        newTable.Columns.Add("Left", typeof(int));
                        newTable.Columns.Add("Top", typeof(int));
                        newTable.Columns.Add("Height", typeof(int));
                        newTable.Columns.Add("Width", typeof(int));
                        newTable.Columns.Add("WindowState", typeof(int));

                        // Go through each column and make it an attribute
                        foreach (DataColumn column in newTable.Columns)
                        {
                            column.ColumnMapping = MappingType.Attribute;
                        }

                        // Create a new row
                        DataRow newRow = newTable.NewRow();

                        // Set the dimensions from the window
                        newRow["Left"] = this.Left;
                        newRow["Top"] = this.Top;
                        newRow["Height"] = this.Height;
                        newRow["Width"] = this.Width;
                        newRow["WindowState"] = this.WindowState;

                        // Add the row to the dataset
                        newTable.Rows.Add(newRow);

                        // Add the new settings table to the dataset
                        tasks.Tables.Add(newTable);

                        // Store changes to the dataset
                        tasks.AcceptChanges();

                        // Write the updated dataset to the XML file
                        tasks.WriteXml("Tasks.xml");
                    }

                    // Load the table
                    table = tasks.Tables["Settings"];
                }

                // Get the row
                DataRow row = table.Rows[0];

                // Get the window's settings
                this.Left = row["Left"] as int? ?? this.Left;
                this.Top = row["Top"] as int? ?? this.Top;
                this.Height = row["Height"] as int? ?? this.Height;
                this.Width = row["Width"] as int? ?? this.Width;
                this.WindowState = row["WindowState"] as FormWindowState? ?? this.WindowState;
            }
            catch (Exception ex)
            {
                if (ex is ArgumentException
                    || ex is ArgumentNullException
                    || ex is ConstraintException
                    || ex is DuplicateNameException
                    || ex is FileNotFoundException
                    || ex is InvalidEnumArgumentException
                    || ex is InvalidExpressionException
                    || ex is NoNullAllowedException
                    || ex is SecurityException)
                {
                    MessageBox.Show("There was a problem writing to the Tasks.xml - Task Me will probably not function properly.", "Task Me", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Handles the ItemCheck event of the Tasks CheckedListBox.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="ItemCheckEventArgs"/> instance containing the event data.</param>
        [SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "This program is only in English")]
        private void TasksCheckedListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            try
            {
                // Create the data set
                using (DataSet tasks = new DataSet("Tasks"))
                {
                    // Set the culture to the user's culture
                    tasks.Locale = CultureInfo.CurrentCulture;

                    // Load the Tasks from the XML file
                    tasks.ReadXml("Tasks.xml", XmlReadMode.Auto);

                    // Get the new value
                    bool newValue = e.NewValue == CheckState.Checked;

                    // If the new value is the same as the value in the database
                    if (tasks.Tables["Task"].Rows[e.Index]["Completed"] as bool? ?? false != newValue && e.Index != -1)
                    {
                        // Update the row in the dataset's checked value
                        tasks.Tables["Task"].Rows[e.Index]["Completed"] = newValue;

                        // Store changes to the dataset
                        tasks.AcceptChanges();

                        // Write the updated dataset to the XML file
                        tasks.WriteXml("Tasks.xml");
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex is FileNotFoundException
                    || ex is SecurityException)
                {
                    MessageBox.Show("The status of your task could not be changed", "Task Me", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                }
                else
                {
                    throw;
                }
            }
        }

        /// <summary>
        /// Handles the KeyUp event of the Tasks CheckedListBox.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="KeyEventArgs"/> instance containing the event data.</param>
        [SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", Justification = "This program is only in English")]
        private void TasksCheckedListBox_KeyUp(object sender, KeyEventArgs e)
        {
            // If the delete or backspace keys were pressed
            if ((e.KeyCode == Keys.Back || e.KeyCode == Keys.Delete) && this.TasksCheckedListBox.SelectedIndex != -1)
            {
                try
                {
                    // Create the data set
                    using (DataSet tasks = new DataSet("Tasks"))
                    {
                        // Set the culture to the user's culture
                        tasks.Locale = CultureInfo.CurrentCulture;

                        // Load the Tasks from the XML file
                        tasks.ReadXml("Tasks.xml", XmlReadMode.Auto);

                        // Delete the selected row
                        tasks.Tables["Task"].Rows[this.TasksCheckedListBox.SelectedIndex].Delete();

                        // Store changes to the dataset
                        tasks.AcceptChanges();

                        // Write the updated dataset to the XML file
                        tasks.WriteXml("Tasks.xml");

                        // Remove the selected item from the checked list box
                        this.TasksCheckedListBox.Items.RemoveAt(this.TasksCheckedListBox.SelectedIndex);
                    }
                }
                catch (Exception ex)
                {
                    if (ex is FileNotFoundException
                        || ex is SecurityException)
                    {
                        MessageBox.Show("Your task could not be deleted", "Task Me", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.DefaultDesktopOnly);
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }

        /// <summary>
        /// Writes the window settings to the XML file.
        /// </summary>
        /// <param name="tasks">The tasks.</param>
        private void WriteSettings(DataSet tasks)
        {
            try
            {
                // Open the row containing the window settings
                DataRow row = tasks.Tables["Settings"].Rows[0];

                // Get the window's state
                row["WindowState"] = this.WindowState;

                // Make the window normal so the right dimensions are saved
                this.WindowState = FormWindowState.Normal;

                // Get the dimensions from the window
                row["Left"] = this.Left;
                row["Top"] = this.Top;
                row["Height"] = this.Height;
                row["Width"] = this.Width;

                // Store changes to the dataset
                tasks.AcceptChanges();

                // Write the updated dataset to the XML file
                tasks.WriteXml("Tasks.xml");
            }
            catch (Exception ex)
            {
                // Not being able to save the settings isn't a biggie
                if (!(ex is InvalidEnumArgumentException
                    || ex is FileNotFoundException
                    || ex is SecurityException))
                {
                    throw;
                }
            }
        }
    }
}