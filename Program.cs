// -----------------------------------------------------------------------
// <copyright file="Program.cs" company="Peter Chapman">
// Copyright 2015 Peter Chapman. See https://github.com/pmachapman/TaskMe/ for licence details.
// </copyright>
// -----------------------------------------------------------------------

namespace Conglomo.TaskMe
{
    using System;
    using System.Windows.Forms;

    /// <summary>
    /// The program.
    /// </summary>
    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new FormTask());
        }
    }
}
