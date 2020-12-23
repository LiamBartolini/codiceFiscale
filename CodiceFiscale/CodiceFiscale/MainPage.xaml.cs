using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodiceFiscale.Models;
using Xamarin.Forms;

namespace CodiceFiscale
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }
        
        private void btnCalcola_Clicked(object sender, EventArgs e)
        {
            Cf cf = new Cf();
        }
    }
}