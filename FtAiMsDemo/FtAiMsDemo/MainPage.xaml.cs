using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using FtAiMsDemo.Helpers;

namespace FtAiMsDemo
{
    public partial class MainPage : ContentPage
    {

        public MainPage()
        {
            InitializeComponent();
            this.LogView.ItemsSource = App.Log;
            
        } 
    }
}
