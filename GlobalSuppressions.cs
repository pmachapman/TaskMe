// -----------------------------------------------------------------------
// <copyright file="GlobalSuppressions.cs" company="Peter Chapman">
// Copyright 2015 Peter Chapman. See https://github.com/pmachapman/TaskMe/ for licence details.
// </copyright>
// -----------------------------------------------------------------------

// This file is used by Code Analysis to maintain SuppressMessage 
// attributes that are applied to this project.
// Project-level suppressions either have no target or are given 
// a specific target and scoped to a namespace, type, member, etc.
//
// To add a suppression to this file, right-click the message in the 
// Code Analysis results, point to "Suppress Message", and click 
// "In Suppression File".
// You do not need to add suppressions to this file manually.

using System.Diagnostics.CodeAnalysis;

[assembly: SuppressMessage("Microsoft.Globalization", "CA1303:Do not pass literals as localized parameters", MessageId = "System.Windows.Forms.Control.set_Text(System.String)", Scope = "member", Target = "M:Conglomo.TaskMe.FormTask.InitializeComponent()", Justification = "This is in the designer and cannot be changed")]
[assembly: SuppressMessage("Microsoft.Naming", "CA2204:Literals should be spelled correctly", MessageId = "TaskMe", Scope = "member", Target = "M:Conglomo.TaskMe.FormTask.InitializeComponent()", Justification = "This is in the designer and cannot be changed")]
