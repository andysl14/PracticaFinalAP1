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
    /// Interaction logic for cJuegos.xaml
    /// </summary>
    public partial class cJuegos : Window
    {
        public cJuegos()
        {
            InitializeComponent();
        }

        private void ConsultarButton_Click(object sender, RoutedEventArgs e)
        {
            var listado = new List<Juegos>();

            if (CriterioTextBox.Text.Trim().Length > 0)
            {
                switch (FiltroComboBox.SelectedIndex)
                {
                    case 0:
                        try
                        {
                            listado = JuegosBLL.GetList(e => e.JuegoId == Utilidades.ToInt(CriterioTextBox.Text));
                        }
                        catch (FormatException)
                        {
                            MessageBox.Show("Debes ingresar un Critero valido para aplicar este filtro.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }
                        break;

                    case 1:
                        try
                        {
                            listado = JuegosBLL.GetList(e => e.Descripcion.Contains(CriterioTextBox.Text));
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
                listado = JuegosBLL.GetList(c => true);
            }

            if (DesdeDatePicker.SelectedDate != null)
                listado = JuegosBLL.GetList(c => c.FechaCompra.Date >= DesdeDatePicker.SelectedDate);

            if (HastaDatePicker.SelectedDate != null)
                listado = JuegosBLL.GetList(c => c.FechaCompra.Date <= HastaDatePicker.SelectedDate);

            DatosDataGrid.ItemsSource = null;
            DatosDataGrid.ItemsSource = listado;
        }
    }
}

