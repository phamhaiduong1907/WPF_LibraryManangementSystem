﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace LibraryManagementSystem.Components
{
    /// <summary>
    /// Interaction logic for BindablePasswordBox.xaml
    /// </summary>
    public partial class BindablePasswordBox : UserControl
    {
        bool isPasswordChanging;
        public static readonly DependencyProperty PasswordPropertty 
            = DependencyProperty.Register("Password", typeof(string), typeof(BindablePasswordBox)
                , new PropertyMetadata(string.Empty, PasswordPropertyChanged));

        private static void PasswordPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if(d is BindablePasswordBox passwordBox)
            {
                passwordBox.UpdatePassword();
            }
        }

        private void UpdatePassword()
        {
            if(!isPasswordChanging)
            {
                passwordBox.Password = Password;
            }
        }

        public string Password
        {
            get => (string)GetValue(PasswordPropertty);
            set => SetValue(PasswordPropertty, value);
        }
        public BindablePasswordBox()
        {
            InitializeComponent();
        }

        private void pwb_PasswordChanged(object sender, RoutedEventArgs e)
        {
            isPasswordChanging= true;
            Password = passwordBox.Password;
            isPasswordChanging= false;
        }
    }
}
