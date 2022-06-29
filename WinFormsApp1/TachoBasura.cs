using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WinFormsApp1
{
    internal class TachoBasura
    {
        public List<Forma> formaList;
        public List<Flecha> flechaList;

        internal List<Forma> FormaList { get => formaList; }
        internal List<Flecha> FlechaList { get => flechaList;  }

        public TachoBasura()
        {
            formaList = new List<Forma>();
            flechaList = new List<Flecha>();
        }
        public void AddForma(Forma f)
        {
            formaList.Add(f);
        }
        public void AddFlecha(Flecha f)
        {
            flechaList.Add(f);
        }
    }
}
