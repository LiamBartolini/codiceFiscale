using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CodiceFiscale.Models
{
    class Cf
    {
        readonly List<char> vocali = new List<char>() { 'A', 'E', 'I', 'O', 'U' };
        StringBuilder sb = new StringBuilder();

        string _nome;
        string _cognome;
        DateTime _dataDiNascita;
        string _regione;
        string _comune;

        string _codiceFiscale;
        public string CodiceFiscale { get; }
        
        public Cf()
        {
        }

        public Cf(string nome, string cognome, DateTime dataDiNascita, string regione, string comune)
        {
            _nome = nome;
            _cognome = cognome;
            _dataDiNascita = dataDiNascita;
            _regione = regione;
            _comune = comune;
            Debug.WriteLine(CalcolaCognomeCf());
        }

        void CalcoloCodiceFiscale()
        {
            List<char> mesi = new List<char>() { 'A', 'B', 'C', 'D', 'E', 'H', 'L', 'M', 'P', 'R', 'S', 'T' };
            
        }

        int CalcolaCognomeCf()
        {
            char[] cognomeChar = _cognome.ToUpper().ToCharArray();
            int contCognome = 0;
            int numConsonanti = ContaConsonanti(_cognome);

            if(numConsonanti >= 3)
            {
                for (int i = 0; i < cognomeChar.Length; i++)
                {
                    if (contCognome < 3)
                    {
                        if(!vocali.Contains(cognomeChar[i]))
                        {
                            sb.Append(cognomeChar[i]);
                            contCognome++;
                        }
                    }
                }
            }
            else if (numConsonanti == 2)
            {
                for (int i = 0; i < cognomeChar.Length; i++)
                {
                    if (!vocali.Contains(cognomeChar[i]))
                    {
                        sb.Append(cognomeChar[i]);
                        contCognome++;
                    }
                }
            }
            else if (numConsonanti < 2)
            {
                for (int i = 0; i < cognomeChar.Length; i++)
                {
                    if (!vocali.Contains(cognomeChar[i]))
                    {
                        sb.Append(cognomeChar[i]);
                        contCognome++;
                        break;
                    }
                }
            }

            // Aggiungi vocali e/o X
            if (contCognome < 3)
                for (int i = 0; i < cognomeChar.Length; i++)
                    if (contCognome <= 3)
                        if (vocali.Contains(cognomeChar[i]))
                        {
                            sb.Append(cognomeChar[i]);
                            contCognome++;
                        }

            if (contCognome < 3)
                for (int i = contCognome; i < 3; i++)
                    sb.Append('X');

            return contCognome;
        }

        int ContaConsonanti(string s)
        {
            int retVal = 0;
            char[] word = s.ToUpper().ToCharArray();
            for (int i = 0; i < word.Length; i++)
                if (!vocali.Contains(word[i]))
                    retVal++;
            return retVal;
        }
    }
}