using PracticaFinalAP1.UI.Consultas;
using PracticaFinalAP1.UI.Registros;
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

namespace PracticaFinalAP1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AmigosMenuItem_Click(object sender, RoutedEventArgs e)
        {
            rAmigos rAmigos = new rAmigos();
            rAmigos.Show();
        }

        private void JuegosMenuItem_Click(object sender, RoutedEventArgs e)
        {
            rJuegos rJuegos = new rJuegos();
            rJuegos.Show();
        }

        private void PrestamosMenuItem_Click(object sender, RoutedEventArgs e)
        {
            rPrestamos rPrestamos = new rPrestamos();
            rPrestamos.Show();

        }

        private void AmigosConsultaMenuItem_Click(object sender, RoutedEventArgs e)
        {
            cAmigos cAmigos = new cAmigos();
            cAmigos.Show();
        }
    }
}
