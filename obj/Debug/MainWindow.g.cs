﻿#pragma checksum "..\..\MainWindow.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "B008BE23CFFFE383F42AA3216FB27CA1"
//------------------------------------------------------------------------------
// <auto-generated>
//     這段程式碼是由工具產生的。
//     執行階段版本:4.0.30319.42000
//
//     對這個檔案所做的變更可能會造成錯誤的行為，而且如果重新產生程式碼，
//     變更將會遺失。
// </auto-generated>
//------------------------------------------------------------------------------

using MikuMikuVLC;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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
using Vlc.DotNet.Wpf;


namespace MikuMikuVLC {
    
    
    /// <summary>
    /// MainWindow
    /// </summary>
    public partial class MainWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image Close;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image Question;
        
        #line default
        #line hidden
        
        
        #line 16 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image PlayPause;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image Stop;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image Previous;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image Next;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image OpenPlayerWindow;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image OpenMusicLs;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\MainWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image setting;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/MikuMikuVLC;component/mainwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\MainWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 9 "..\..\MainWindow.xaml"
            ((MikuMikuVLC.MainWindow)(target)).MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Window_MouseDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Close = ((System.Windows.Controls.Image)(target));
            
            #line 14 "..\..\MainWindow.xaml"
            this.Close.MouseEnter += new System.Windows.Input.MouseEventHandler(this.BtnMouseEnter);
            
            #line default
            #line hidden
            
            #line 14 "..\..\MainWindow.xaml"
            this.Close.MouseLeave += new System.Windows.Input.MouseEventHandler(this.BtnMouseLeave);
            
            #line default
            #line hidden
            
            #line 14 "..\..\MainWindow.xaml"
            this.Close.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.Close_MouseDown);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Question = ((System.Windows.Controls.Image)(target));
            
            #line 15 "..\..\MainWindow.xaml"
            this.Question.MouseEnter += new System.Windows.Input.MouseEventHandler(this.BtnMouseEnter);
            
            #line default
            #line hidden
            
            #line 15 "..\..\MainWindow.xaml"
            this.Question.MouseLeave += new System.Windows.Input.MouseEventHandler(this.BtnMouseLeave);
            
            #line default
            #line hidden
            return;
            case 4:
            this.PlayPause = ((System.Windows.Controls.Image)(target));
            return;
            case 5:
            this.Stop = ((System.Windows.Controls.Image)(target));
            
            #line 17 "..\..\MainWindow.xaml"
            this.Stop.MouseEnter += new System.Windows.Input.MouseEventHandler(this.BtnMouseEnter);
            
            #line default
            #line hidden
            
            #line 17 "..\..\MainWindow.xaml"
            this.Stop.MouseLeave += new System.Windows.Input.MouseEventHandler(this.BtnMouseLeave);
            
            #line default
            #line hidden
            return;
            case 6:
            this.Previous = ((System.Windows.Controls.Image)(target));
            
            #line 18 "..\..\MainWindow.xaml"
            this.Previous.MouseEnter += new System.Windows.Input.MouseEventHandler(this.BtnMouseEnter);
            
            #line default
            #line hidden
            
            #line 18 "..\..\MainWindow.xaml"
            this.Previous.MouseLeave += new System.Windows.Input.MouseEventHandler(this.BtnMouseLeave);
            
            #line default
            #line hidden
            return;
            case 7:
            this.Next = ((System.Windows.Controls.Image)(target));
            
            #line 19 "..\..\MainWindow.xaml"
            this.Next.MouseEnter += new System.Windows.Input.MouseEventHandler(this.BtnMouseEnter);
            
            #line default
            #line hidden
            
            #line 19 "..\..\MainWindow.xaml"
            this.Next.MouseLeave += new System.Windows.Input.MouseEventHandler(this.BtnMouseLeave);
            
            #line default
            #line hidden
            return;
            case 8:
            this.OpenPlayerWindow = ((System.Windows.Controls.Image)(target));
            
            #line 20 "..\..\MainWindow.xaml"
            this.OpenPlayerWindow.MouseEnter += new System.Windows.Input.MouseEventHandler(this.BtnMouseEnter);
            
            #line default
            #line hidden
            
            #line 20 "..\..\MainWindow.xaml"
            this.OpenPlayerWindow.MouseLeave += new System.Windows.Input.MouseEventHandler(this.BtnMouseLeave);
            
            #line default
            #line hidden
            
            #line 20 "..\..\MainWindow.xaml"
            this.OpenPlayerWindow.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.OpenPlayerWindow_MouseDown);
            
            #line default
            #line hidden
            return;
            case 9:
            this.OpenMusicLs = ((System.Windows.Controls.Image)(target));
            
            #line 21 "..\..\MainWindow.xaml"
            this.OpenMusicLs.MouseEnter += new System.Windows.Input.MouseEventHandler(this.BtnMouseEnter);
            
            #line default
            #line hidden
            
            #line 21 "..\..\MainWindow.xaml"
            this.OpenMusicLs.MouseLeave += new System.Windows.Input.MouseEventHandler(this.BtnMouseLeave);
            
            #line default
            #line hidden
            return;
            case 10:
            this.setting = ((System.Windows.Controls.Image)(target));
            
            #line 22 "..\..\MainWindow.xaml"
            this.setting.MouseEnter += new System.Windows.Input.MouseEventHandler(this.BtnMouseEnter);
            
            #line default
            #line hidden
            
            #line 22 "..\..\MainWindow.xaml"
            this.setting.MouseLeave += new System.Windows.Input.MouseEventHandler(this.BtnMouseLeave);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
