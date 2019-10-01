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

namespace AbschlussprojektWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class wndMain : Window
    {
        public string processName;

        // alle booleans initalisieren und auf false setzen
        bool firefox = false;
        bool chrome = false;
        bool ie = false;
        bool word = false;
        bool excel = false;
        bool outlook = false;
        bool jaws = false;
        bool nvda = false;
        bool zoomtext = false;
        bool supernova = false;

        public wndMain()
        {
            InitializeComponent();
        }

        private void BtnQuit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnAnalysis_Click(object sender, RoutedEventArgs e)
        {
            // initialisiere Auswertungsfenster
            wndAnalysis frmAnalysis = new wndAnalysis();
            frmAnalysis.Show();
        }

        private void BtnLog_Click(object sender, RoutedEventArgs e)
        {
            // wenn dieser Button gedrückt wird, soll eine Überprüfung stattfinden, ob der Logvorgang überhaupt gestartet werden soll, wenn nicht
            // soll die Möglichkeit bestehen, JAWS/ZoomText zu starten... zur Überprüfung soll zunächst nur auf JAWS und ZoomText geprüft werden, 
            // Browser /Word können nachher noch gestartet werden 
        }
    }
}
