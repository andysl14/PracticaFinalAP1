using PracticaFinalAP1.BLL;
using PracticaFinalAP1.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PracticaFinalAP1.UI.Consultas
{
    /// <summary>
    /// Interaction logic for cPrestamos.xaml
    /// </summary>
    public partial class cPrestamos : Window
    {
        public cPrestamos()
        {
            InitializeComponent();
        }

        private void ConsultarButton_Click(object sender, RoutedEventArgs e)
        {
            var listado = new List<Prestamos>();

            if (CriterioTextBox.Text.Trim().Length > 0)
            {
                switch (FiltroComboBox.SelectedIndex)
                {
                    case 0:
                        try
                        {
                            listado = PrestamosBLL.GetList(p => p.PrestamoId == Utilidades.ToInt(CriterioTextBox.Text));
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Debes ingresar un Critero valido para aplicar este filtro.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        break;
                    case 1:
                        try
                        {
                            listado = PrestamosBLL.GetList(p => p.AmigoId == Utilidades.ToInt(CriterioTextBox.Text));
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Debes ingresar un Critero valido para aplicar este filtro.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        break;
                 
                }
            }
            else
            {
                
                listado = PrestamosBLL.GetList(c => true);
            }

            if (DesdeDatePicker.SelectedDate != null)
                listado = PrestamosBLL.GetList(c => c.Fecha.Date >= DesdeDatePicker.SelectedDate);

            if (HastaDatePicker.SelectedDate != null)
                listado = PrestamosBLL.GetList(c => c.Fecha.Date <= HastaDatePicker.SelectedDate);

            DatosDataGrid.ItemsSource = null;
            DatosDataGrid.ItemsSource = listado;
        }
    }
}

