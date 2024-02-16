using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeterMiklosAdatbazisSQLITE
{
    internal class Jatekok
    {
        string nev;
        double ertekeles;
        int ar;
        string kiado;

        public Jatekok(string nev, double ertekeles, int ar, string kiado)
        {
            this.nev = nev;
            this.ertekeles = ertekeles;
            this.ar = ar;
            this.kiado = kiado;
        }

        public string Nev { get => nev; set => nev = value; }
        public double Ertekeles { get => ertekeles; set => ertekeles = value; }
        public int Ar { get => ar; set => ar = value; }
        public string Kiado { get => kiado; set => kiado = value; }
    }
}
