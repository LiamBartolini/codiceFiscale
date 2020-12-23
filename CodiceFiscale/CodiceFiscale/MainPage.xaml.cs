using System;
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
                    ntrSesso.Text,
                    colonne[0],
                    colonne[2],
                    colonne[1]
                    );
            }
            catch
            {
                Cf cf = new Cf(
                    ntrNome.Text,
                    ntrCognome.Text,
                    dpInput.Date,
                    "Maschio",
                    "EmiliaRomagna",
                    "Rimini",
                    "Coriano"
                    );
            }
        }
    }
}