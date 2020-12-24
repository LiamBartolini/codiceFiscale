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
            Cf cf;
            try
            {
                string[] colonne = ntrComune.Text.Split(',');
                cf = new Cf(
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
                cf = new Cf(
                    "Liam",
                    "Bartolini",
                    Convert.ToDateTime("23/01/2003"),
                    "Maschio",
                    "EmiliaRomagna",
                    "Rimini",
                    "Rimini"
                    );
            }

            lblOutput.Text = cf.CodiceFiscale;
        }
    }
}