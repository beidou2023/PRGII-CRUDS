using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace prueba
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        string saludo = "Holas";
        int n = 19;
        public MainWindow()
        {
            InitializeComponent();
            bool ver = veri(10);
            if (ver)
                MessageBox.Show("TRUE");
            else
                MessageBox.Show("FALSE");
        }

        bool veri(int na) {
            bool veri = true;
            if(n%2==0)
                 veri = false;
            return veri;
        }

    }
}