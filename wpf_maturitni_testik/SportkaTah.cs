using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_maturitni_testik
{
    internal class SportkaTah
    {
        public int rok = 0;
        public int tyden = 0;

        public int[] prvniTah = new int[6];
        public int[] druhyTah = new int[6];
        
        public int dodatkoveCilo1 = 0;
        public int dodatkoveCilo2 = 0;
        public SportkaTah(string[] sportkaString)
        {
            int.TryParse(sportkaString[0], out rok);
            int.TryParse(sportkaString[1], out tyden);
            for (int i = 0; i < 6; i++)
            {
                int.TryParse(sportkaString[i + 2], out prvniTah[i]);
            }
            int.TryParse(sportkaString[6], out dodatkoveCilo1);
            for (int i = 0; i < 6; i++)
            {
                int.TryParse(sportkaString[i + 8], out druhyTah[i]);
            }
            int.TryParse(sportkaString[13], out dodatkoveCilo2);
        }
    }
}
