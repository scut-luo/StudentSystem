﻿#pragma checksum "..\..\..\..\..\Moduls\Course\CourseInfoUpdate.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "1DF6135CEA874D60BAFD798442D3100A"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.1
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
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


namespace ManagementSystem {
    
    
    /// <summary>
    /// CourseInfoUpdate
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class CourseInfoUpdate : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 12 "..\..\..\..\..\Moduls\Course\CourseInfoUpdate.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid Grid_CourseInfo;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\..\..\..\Moduls\Course\CourseInfoUpdate.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tb_Cnum;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\..\..\Moduls\Course\CourseInfoUpdate.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tb_Cname;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\..\..\Moduls\Course\CourseInfoUpdate.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tb_Ccredit;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\..\..\Moduls\Course\CourseInfoUpdate.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tb_Chours;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\..\..\Moduls\Course\CourseInfoUpdate.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cb_Anum;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/ManagementSystem;component/moduls/course/courseinfoupdate.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Moduls\Course\CourseInfoUpdate.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Grid_CourseInfo = ((System.Windows.Controls.Grid)(target));
            return;
            case 2:
            this.tb_Cnum = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.tb_Cname = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.tb_Ccredit = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.tb_Chours = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.cb_Anum = ((System.Windows.Controls.ComboBox)(target));
            
            #line 33 "..\..\..\..\..\Moduls\Course\CourseInfoUpdate.xaml"
            this.cb_Anum.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.AcademySelectionChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 42 "..\..\..\..\..\Moduls\Course\CourseInfoUpdate.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Save_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

