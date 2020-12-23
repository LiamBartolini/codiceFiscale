using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CodiceFiscale.Models
{
    class Cf
    {
        readonly List<char> vocali = new List<char>() { 'A', 'E', 'I', 'O', 'U' };
        readonly char[] lettere = new char[] { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z' };
        StringBuilder sb = new StringBuilder();

        string _nome;
        string _cognome;
        DateTime _dataDiNascita;
        string _regione;
        string _comune;
        string _sesso;
        string _codiceFiscale;
        public string CodiceFiscale { get; }
        
        public Cf()
        {
        }

        public Cf(string nome, string cognome, DateTime dataDiNascita, string sesso, string regione, string comune)
        {
            _nome = nome;
            _cognome = cognome;
            _dataDiNascita = dataDiNascita;
            _sesso = sesso;
            _regione = regione;
            _comune = comune;

            CalcoloCodiceFiscale();
            Debug.WriteLine(sb.ToString());
        }

        void CalcoloCodiceFiscale()
        {
            List<char> mesi = new List<char>() { 'A', 'B', 'C', 'D', 'E', 'H', 'L', 'M', 'P', 'R', 'S', 'T' };

            int contCognome = CalcolaCognomeCf();
            CalcolaNomeCf(contCognome);

            // Aggiungo l'anno
            sb.Append($"{_dataDiNascita:yy}");
            // Aggiungo il mese
            sb.Append($"{mesi[_dataDiNascita.Month - 1]}");
            // Aggiungo il giorno
            if (_sesso == "Maschio")
                sb.Append($"{_dataDiNascita:dd}");
            else
                sb.Append($"{_dataDiNascita.Day + 40}");
            // Aggiungo il comune
            sb.Append("H294");
            // Aggiungo il carattere di controllo
            int checkChar = CalcoloCarattereControlloCf();
            sb.Append(lettere[checkChar]);
            _codiceFiscale = sb.ToString();
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

            // Aggiunge vocali e/o X
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

        void CalcolaNomeCf(int contCognome)
        {
            char[] nomeChar = _nome.ToUpper().ToCharArray();
            int contNome = 0;
            int numConsonanti = ContaConsonanti(_nome);
            int contConsonanti = 0;

            bool exit = false;
            if (numConsonanti >= 3)
            {
                for (int i = 0; i < nomeChar.Length; i++)
                {
                    if (contNome <= 3)
                    {
                        if (!vocali.Contains(nomeChar[i]))
                        {
                            if (contConsonanti != 1)
                            {
                                sb.Append(nomeChar[i]);
                                contNome++;
                                contConsonanti++;
                            }
                            else
                            {

                                for (int j = i + 1; j < nomeChar.Length; j++)
                                {
                                    if (!vocali.Contains(nomeChar[j]))
                                    {
                                        sb.Append(nomeChar[j]);
                                        contNome++;
                                        contCognome++;
                                    }

                                    if (contNome == 3)
                                    {
                                        exit = true;
                                        break;
                                    }
                                }
                            }
                        }
                    }

                    if (exit)
                        break;
                }
            }
            else if (numConsonanti == 2)
            {
                for (int i = 0; i < nomeChar.Length; i++)
                {
                    if (!vocali.Contains(nomeChar[i]))
                    {
                        sb.Append(nomeChar[i]);
                        contNome++;
                    }
                }
            }
            else if (numConsonanti < 2)
            {
                for (int i = 0; i < nomeChar.Length; i++)
                {
                    if (!vocali.Contains(nomeChar[i]))
                    {
                        sb.Append(nomeChar[i]);
                        contNome++;
                        break;
                    }
                }
            }

            // Aggiunge vocali e/o X
            bool flagName = false;

            if (contCognome + contNome < 6)
            {
                if (contNome < 3)
                {
                    for (int i = 0; i < nomeChar.Length; i++)
                    {
                        if (contCognome + contNome < 6)
                        {
                            if (contNome <= 3)
                            {
                                if (vocali.Contains(nomeChar[i]))
                                {
                                    sb.Append(nomeChar[i]);
                                    contNome++;
                                }
                                else
                                    flagName = true;
                            }
                        }
                    }
                }

                if (!flagName)
                {
                    if (contNome < 3)
                    {
                        for (int i = contNome; i < 3; i++)
                        {
                            sb.Append('X');
                        }
                    }
                }
            }
        }
        int CalcoloCarattereControlloCf()
        {
            int[] dispari = new int[] { 1, 0, 5, 7, 9, 13, 15, 17, 19, 21, 2, 4, 18, 20, 11, 3, 6, 8, 12, 14, 16, 10, 22, 25, 24, 23 };
            char[] cfChar = sb.ToString().ToCharArray();
            int retVal = 0;

            for (int i = 0; i < cfChar.Length; i++)
            {
                if ((i + 1) == 1)
                {
                    for (int j = 0; j < lettere.Length; j++)
                    {
                        if(cfChar[i] == lettere[j])
                        {
                            retVal += dispari[j];
                            break;
                        }
                    }
                }
                else
                {
                    if ((i + 1) % 2 == 0)
                    {
                        if (Char.IsNumber(cfChar[i]))
                        {
                            int.TryParse(cfChar[i].ToString(), out int parsed);
                            retVal += parsed;
                        }
                        else
                        {
                            for (int j = 0; j < lettere.Length; j++)
                            {
                                if (cfChar[i] == lettere[j])
                                {
                                    retVal += j;
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        if (Char.IsNumber(cfChar[i]))
                        {
                            int.TryParse(cfChar[i].ToString(), out int parsed);
                            retVal += dispari[parsed];
                        }
                        else
                        {
                            for (int j = 0; j < lettere.Length; j++)
                            {
                                if (cfChar[i] == lettere[j])
                                {
                                    retVal += dispari[j];
                                    break;
                                }
                            }
                        }    
                    }
                }
            }
            return retVal % 26;
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