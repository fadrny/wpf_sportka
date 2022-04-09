using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace wpf_maturitni_testik
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        LoadedData Data;
        public MainWindow()
        {
            InitializeComponent();
            Data = new LoadedData(new Uri("D:\\01sl\\c#\\wpf_maturitni_testik\\wpf_maturitni_testik\\sportka-data.txt"));   
            foreach (SportkaTah tah in Data.tahySportky)
            {

                cmb_startYear.ItemsSource = Data.tahySportky.Select(x => x.rok).Distinct().ToList();
                cmb_startYear_druhy.ItemsSource = Data.tahySportky.Where(y => y.druhyTah[0] > 0).Select(x => x.rok).Distinct().ToList(); ;
            }
        }
        #region 1.tah logic
        private void cmb_startWeek_DropDownOpened(object sender, EventArgs e)
        {
            if (cmb_startYear.SelectedItem != null)
            {
                cmb_startWeek.ItemsSource = Data.tahySportky.Where(x => x.rok == (int)cmb_startYear.SelectedItem).Select(x => x.tyden).ToList();
            }
        }

        private void cmb_endYear_DropDownOpened(object sender, EventArgs e)
        {
            if (cmb_startWeek.SelectedItem != null)
            {
                cmb_endYear.ItemsSource = Data.tahySportky.Where(x => x.rok >= (int)cmb_startYear.SelectedItem).Select(x => x.rok).Distinct().ToList();
            }
        }

        private void cmb_endWeek_DropDownOpened(object sender, EventArgs e)
        {
            if (cmb_endYear.SelectedItem != null)
            {
                cmb_endWeek.ItemsSource = Data.tahySportky.Where(x => x.rok == (int)cmb_endYear.SelectedItem && x.tyden > (int)cmb_startWeek.SelectedItem).Select(x => x.tyden).ToList();
            }
        }
        
        private void btn_output_Click(object sender, RoutedEventArgs e)
        {
            if (cmb_endYear.SelectedItem == null || cmb_startYear.SelectedItem == null || cmb_endWeek.SelectedItem == null || cmb_startWeek.SelectedItem == null)
                return;
            List<int> allInRange = new List<int>();
            //if ((int)cmb_startYear.SelectedItem == (int)cmb_endYear.SelectedItem)
            //{
            //    foreach (SportkaTah prvniRok in Data.tahySportky.Where(x => x.rok == (int)cmb_startYear.SelectedItem).Skip((int)cmb_startWeek.SelectedItem - 1).Take((int)cmb_endWeek.SelectedItem - (int)cmb_startWeek.SelectedItem))
            //    {
            //        foreach (int vyherni in prvniRok.prvniTah)
            //        {
            //            allInRange.Add(vyherni);
            //        }
            //    }
            //}

            int weeksOfLastYear = Data.tahySportky.Where(x => x.rok == (int)cmb_endYear.SelectedItem).Count();

            foreach (SportkaTah tah in Data.tahySportky.Where(x => x.rok >= (int)cmb_startYear.SelectedItem && x.rok <= (int)cmb_endYear.SelectedItem).
                    Skip((int)cmb_startWeek.SelectedItem - 1).Reverse().Skip(weeksOfLastYear - (int)cmb_endWeek.SelectedItem))
            {
                foreach (int vyherni in tah.prvniTah)
                {
                    allInRange.Add(vyherni);
                }
            }

            int mostOften = allInRange.GroupBy(x => x).OrderByDescending(x => x.Count()).First().Key;
            int count = allInRange.GroupBy(x => x).OrderByDescending(x => x.Count()).First().Count();

            int leastOften = allInRange.GroupBy(x => x).OrderBy(x => x.Count()).First().Key;
            int count2 = allInRange.GroupBy(x => x).OrderBy(x => x.Count()).First().Count();

            tb_output.Text = "Nejvíce vyhrává: " + mostOften + " (" + count + "x)" + Environment.NewLine +
                "Nejméně vyhrává: " + leastOften + " (" + count2 + "x)";
        }
        #endregion

        private void cmb_endYear_druhy_DropDownOpened(object sender, EventArgs e)
        {
            if (cmb_startYear_druhy.SelectedItem != null)
            {
                cmb_endYear_druhy.ItemsSource = Data.tahySportky.Where(x => x.rok >= (int)cmb_startYear_druhy.SelectedItem).Select(x => x.rok).Distinct().ToList();
            }
        }

        private void btn_druhaStatistika_Click(object sender, RoutedEventArgs e)
        {
            if (cmb_endYear_druhy.SelectedItem == null || cmb_startYear_druhy.SelectedItem == null)
                return;

            int start = (int)cmb_startYear_druhy.SelectedItem;
            int stop = (int)cmb_endYear_druhy.SelectedItem;

            SportkaTah[] tydny = Data.tahySportky.Where(x => x.rok >= start && x.rok <= stop).ToArray();
            List<int> allVyherni = new List<int>();

            foreach (SportkaTah tah in tydny)
                foreach (int vyherni in tah.druhyTah)
                    allVyherni.Add(vyherni);

            string output = "";

            int[] counts = new int[49];
            for (int i = 1; i < 49; i++)
            {
                counts[i-1] = 0;
            }
            foreach (int iter in allVyherni)
                counts[iter-1]++;


            for (int cislo = 0; cislo < counts.Length; cislo++)
            {
                int maxValue = counts.Max();
                int maxIndex = counts.ToList().IndexOf(maxValue);

                output += (maxIndex + 1) + "  ( " + maxValue + " výskytů )" + Environment.NewLine;

                counts[maxIndex] = 0;
            }

            tb_output_druhy.Text = output;
        }
    }
}
