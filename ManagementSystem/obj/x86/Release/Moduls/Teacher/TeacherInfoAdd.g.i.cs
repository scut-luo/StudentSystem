﻿#pragma checksum "..\..\..\..\..\Moduls\Teacher\TeacherInfoAdd.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "44F5A28ECB6D7CA99B7073BDB2A9A41C"
//------------------------------------------------------------------------------
// <auto-generated>
//     此代码由工具生成。
//     运行时版本:4.0.30319.1
//
//     对此文件的更改可能会导致不正确的行为，并且如果
//     重新生成代码，这些更改将会丢失。
// </auto-generated>
//------------------------------------------------------------------------------

using ManagementSystem;
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
    /// TeacherInfoAdd
    /// </summary>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
    public partial class TeacherInfoAdd : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\..\..\..\Moduls\Teacher\TeacherInfoAdd.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid GridTeacherInfo;
        
        #line default
        #line hidden
        
        
        #line 40 "..\..\..\..\..\Moduls\Teacher\TeacherInfoAdd.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tb_Tnum;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\..\..\Moduls\Teacher\TeacherInfoAdd.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tb_Tname;
        
        #line default
        #line hidden
        
        
        #line 42 "..\..\..\..\..\Moduls\Teacher\TeacherInfoAdd.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox tb_Tphone;
        
        #line default
        #line hidden
        
        
        #line 43 "..\..\..\..\..\Moduls\Teacher\TeacherInfoAdd.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox cb_AcademyList;
        
        #line default
        #line hidden
        
        
        #line 52 "..\..\..\..\..\Moduls\Teacher\TeacherInfoAdd.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Media.ImageDrawing img_Teacher;
        
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
            System.Uri resourceLocater = new System.Uri("/ManagementSystem;component/moduls/teacher/teacherinfoadd.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\Moduls\Teacher\TeacherInfoAdd.xaml"
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
            
            #line 8 "..\..\..\..\..\Moduls\Teacher\TeacherInfoAdd.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Save_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.GridTeacherInfo = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.tb_Tnum = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.tb_Tname = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.tb_Tphone = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.cb_AcademyList = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            
            #line 45 "..\..\..\..\..\Moduls\Teacher\TeacherInfoAdd.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.UpdatePhoto_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.img_Teacher = ((System.Windows.Media.ImageDrawing)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

