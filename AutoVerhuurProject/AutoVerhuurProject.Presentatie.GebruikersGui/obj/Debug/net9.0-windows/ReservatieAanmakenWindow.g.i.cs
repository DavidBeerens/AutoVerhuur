﻿#pragma checksum "..\..\..\ReservatieAanmakenWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "CF46CBB07FDB19DC33C5367EE4A265355B74FAA1"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using AutoVerhuurProject.Persistentie;
using AutoVerhuurProject.Presentatie.GebruikersGui;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Controls.Ribbon;
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


namespace AutoVerhuurProject.Presentatie.GebruikersGui {
    
    
    /// <summary>
    /// ReservatieAanmakenWindow
    /// </summary>
    public partial class ReservatieAanmakenWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 14 "..\..\..\ReservatieAanmakenWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ComboBoxLuchthavens;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\ReservatieAanmakenWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock AantalZitplaatsen;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\ReservatieAanmakenWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider SliderZitplaatsen;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\ReservatieAanmakenWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker StartDatum;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\ReservatieAanmakenWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox BeginTijd;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\ReservatieAanmakenWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DatePicker EindDatum;
        
        #line default
        #line hidden
        
        
        #line 35 "..\..\..\ReservatieAanmakenWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox EindTijd;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\..\ReservatieAanmakenWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox ComboBoxRetour;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\..\ReservatieAanmakenWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock GeenAutosGevonden;
        
        #line default
        #line hidden
        
        
        #line 56 "..\..\..\ReservatieAanmakenWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox LstAutos;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/AutoVerhuurProject.Presentatie.GebruikersGui;component/reservatieaanmakenwindow." +
                    "xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\ReservatieAanmakenWindow.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "9.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.ComboBoxLuchthavens = ((System.Windows.Controls.ComboBox)(target));
            
            #line 14 "..\..\..\ReservatieAanmakenWindow.xaml"
            this.ComboBoxLuchthavens.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.ComboBoxLuchthavens_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 2:
            this.AantalZitplaatsen = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 3:
            this.SliderZitplaatsen = ((System.Windows.Controls.Slider)(target));
            
            #line 22 "..\..\..\ReservatieAanmakenWindow.xaml"
            this.SliderZitplaatsen.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.Slider_ValueChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            
            #line 25 "..\..\..\ReservatieAanmakenWindow.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.StartDatum = ((System.Windows.Controls.DatePicker)(target));
            
            #line 30 "..\..\..\ReservatieAanmakenWindow.xaml"
            this.StartDatum.SelectedDateChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.StartDatum_SelectedDateChanged);
            
            #line default
            #line hidden
            return;
            case 6:
            this.BeginTijd = ((System.Windows.Controls.ComboBox)(target));
            
            #line 31 "..\..\..\ReservatieAanmakenWindow.xaml"
            this.BeginTijd.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.BeginTijd_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.EindDatum = ((System.Windows.Controls.DatePicker)(target));
            
            #line 34 "..\..\..\ReservatieAanmakenWindow.xaml"
            this.EindDatum.SelectedDateChanged += new System.EventHandler<System.Windows.Controls.SelectionChangedEventArgs>(this.EindDatum_SelectedDateChanged);
            
            #line default
            #line hidden
            return;
            case 8:
            this.EindTijd = ((System.Windows.Controls.ComboBox)(target));
            
            #line 35 "..\..\..\ReservatieAanmakenWindow.xaml"
            this.EindTijd.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.EindTijd_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            this.ComboBoxRetour = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 10:
            this.GeenAutosGevonden = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 11:
            this.LstAutos = ((System.Windows.Controls.ListBox)(target));
            
            #line 56 "..\..\..\ReservatieAanmakenWindow.xaml"
            this.LstAutos.MouseDoubleClick += new System.Windows.Input.MouseButtonEventHandler(this.LstAutos_MouseDoubleClick);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

