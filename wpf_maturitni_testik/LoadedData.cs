using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace wpf_maturitni_testik
{
    internal class LoadedData
    {
        private Uri fileUri;
        public List<SportkaTah> tahySportky = new List<SportkaTah>();



        public LoadedData(Uri file)
        {
            fileUri = file;
            string[] loadedFile = File.ReadAllLines(fileUri.LocalPath);

            foreach (string line in loadedFile.Skip(1))
                tahySportky.Add(new SportkaTah(line.Split(';')));
        }
        
    }
}
