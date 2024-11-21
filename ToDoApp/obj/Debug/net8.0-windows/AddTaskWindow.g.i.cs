﻿#pragma checksum "..\..\..\AddTaskWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1C8F4FF77E056F03593B8F7E5B44A02B3C22B356"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace ToDoApp {
    
    
    /// <summary>
    /// AddTaskWindow
    /// </summary>
    public partial class AddTaskWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 8 "..\..\..\AddTaskWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TaskTitleTextBox;
        
        #line default
        #line hidden
        
        
        #line 12 "..\..\..\AddTaskWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TaskDescriptionTextBox;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\..\AddTaskWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker TaskDueDatePicker;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\AddTaskWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox TaskDueTimeTextBox;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\AddTaskWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox PriorityComboBox;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.8.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ToDoApp;component/addtaskwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\AddTaskWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "8.0.8.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.TaskTitleTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 2:
            this.TaskDescriptionTextBox = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.TaskDueDatePicker = ((System.Windows.Controls.DatePicker)(target));
            return;
            case 4:
            this.TaskDueTimeTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 21 "..\..\..\AddTaskWindow.xaml"
            this.TaskDueTimeTextBox.GotFocus += new System.Windows.RoutedEventHandler(this.RemovePlaceholder);
            
            #line default
            #line hidden
            
            #line 21 "..\..\..\AddTaskWindow.xaml"
            this.TaskDueTimeTextBox.LostFocus += new System.Windows.RoutedEventHandler(this.AddPlaceholder);
            
            #line default
            #line hidden
            
            #line 21 "..\..\..\AddTaskWindow.xaml"
            this.TaskDueTimeTextBox.PreviewTextInput += new System.Windows.Input.TextCompositionEventHandler(this.ValidateTimeInput);
            
            #line default
            #line hidden
            return;
            case 5:
            this.PriorityComboBox = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            
            #line 36 "..\..\..\AddTaskWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.SaveButton_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 37 "..\..\..\AddTaskWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.CancelButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

