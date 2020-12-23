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
            try
            {
                string[] colonne = ntrComune.Text.Split(',');
                Cf cf = new Cf(
                    ntrNome.Text,
                    ntrCognome.Text,
                    dpInput.Date,
                    colonne[0],
                    colonne[1]
                    );
            }
            catch
            {
                Cf cf = new Cf(
                    ntrNome.Text,
                    ntrCognome.Text,
                    dpInput.Date,
                    "EmiliaRomagna",
                    "Rimini"
                    );
            }
        }
    }
}