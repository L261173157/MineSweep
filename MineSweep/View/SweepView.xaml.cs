﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Shapes;
using GalaSoft.MvvmLight.Messaging;

namespace MineSweep.View
{
    /// <summary>
    /// SweepView.xaml 的交互逻辑
    /// </summary>
    public partial class SweepView : Window
    {
       
        public SweepView()
        {
            InitializeComponent();
            
            Messenger.Default.Register<string>(this, "ShowWin", Showbox);
        }

        

        private void Showbox(string obj)
        {
            MessageBox.Show(obj);
        }        

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            OptionView optionView = new OptionView();
            optionView.ShowDialog();
        }

        private void Button_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            MessageBox.Show("双击");
        }


    }
}
