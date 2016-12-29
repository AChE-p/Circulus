using System;
using System.Collections.Generic;
using System.IO;
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

namespace saveTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            ByteSave<TestDouble> byteSave = new ByteSave<TestDouble>();
            for (int i=0; i<10; i++)
            {
                TestDouble x = new TestDouble();
                x.d = i + 0.5;
                byteSave.savingData.Add(x);
            }
            byteSave.fileInfo = "1238714";
            File.Delete("double.bin");
            byteSave.SaveToFile("double.bin", 8);
            ByteSave<TestDouble> byteRead = new ByteSave<TestDouble>();
            byteRead.ReadFromFile("double.bin", 8);
        }
    }
}
