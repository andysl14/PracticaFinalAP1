using PracticaFinalAP1.BLL;
using PracticaFinalAP1.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PracticaFinalAP1.UI.Registros
{
    /// <summary>
    /// Interaction logic for rPrestamos.xaml
    /// </summary>
    public partial class rPrestamos : Window
    {
        private Prestamos prestamos = new Prestamos();
        public rPrestamos()
        {
            InitializeComponent();
            this.DataContext = prestamos;

            AmigoIdComboBox.ItemsSource = AmigosBLL.GetAmigos();
            AmigoIdComboBox.SelectedValuePath = "AmigoId";
            AmigoIdComboBox.DisplayMemberPath = "Nombres";

            JuegoIdComboBox.ItemsSource = JuegosBLL.GetJuegos();
            JuegoIdComboBox.SelectedValuePath = "JuegoId";
            JuegoIdComboBox.DisplayMemberPath = "Descripcion";

        }

        private void Cargar()
        {
            this.prestamos = null;
            this.DataContext = prestamos;
        }

        private void Limpiar()
        {
            this.prestamos = new Prestamos();
            this.DataContext = prestamos;
        }

        private bool Validar()
        {
            bool Validado = true;
            if(PrestamoIdTextBox.Text.Length == 0)
            {
                Validado = false;
                MessageBox.Show("Transacción Fallida", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return Validado;
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Prestamos encontrado = PrestamosBLL.Buscar(prestamos.PrestamoId);

            if(encontrado != null)
            {
                prestamos = encontrado;
                Cargar();
            }
            else
            {
                MessageBox.Show($"Este Préstamo no fue encontrado.\n\nAsegúrese que existe o cree uno nuevo.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                Limpiar();
                PrestamoIdTextBox.SelectAll();
                PrestamoIdTextBox.Focus();
            }


        }

        private void AgregarFilaButton_Click(object sender, RoutedEventArgs e)
        {
            if (JuegoIdComboBox.Text == string.Empty)
            {
                MessageBox.Show("El Campo (Juego Id) está vacío.\n\nPorfavor, Seleccione el Juego.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                JuegoIdComboBox.IsDropDownOpen = true;
                return;

            }

            var filaDetalle = new PrestamosDetalle
            {
                PrestamoId = this.prestamos.PrestamoId,
                JuegoId = Convert.ToInt32(JuegoIdComboBox.SelectedValue.ToString()),

                juegos = (Juegos)JuegoIdComboBox.SelectedItem,
                Cantidad = Convert.ToInt32(CantidadTextBox.Text),
                
            };

            prestamos.CantidadJuegos += Convert.ToDouble(CantidadTotalTextBox.Text.ToString());

            this.prestamos.Detalle.Add(filaDetalle);
            Cargar();

            JuegoIdComboBox.SelectedIndex = -1;
            CantidadTextBox.Text = "1";
        }

        private void RemoverFilaButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                double total = Convert.ToDouble(CantidadTextBox.Text);
                if(DetalleDataGrid.Items.Count >= 1 && DetalleDataGrid.SelectedIndex <= DetalleDataGrid.Items.Count -1)
                {
                    prestamos.Detalle.RemoveAt(DetalleDataGrid.SelectedIndex);
                    prestamos.CantidadJuegos -= total;
                    Cargar();
                }
            }
            catch(Exception)
            {
                MessageBox.Show("No has seleccionado ninguna Fila\n\nSeleccione la Fila a Remover.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
            CantidadTextBox.Text = "1";
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            if (!Validar())
                return;

            if (PrestamoIdTextBox.Text.Trim() == string.Empty)
            {
                MessageBox.Show("El Campo (Préstamo Id) está vacío.\n\nAsigne un ID al Préstamo.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                PrestamoIdTextBox.Text = "0";
                PrestamoIdTextBox.Focus();
                PrestamoIdTextBox.SelectAll();
                return;
            }

            if (AmigoIdComboBox.Text == string.Empty)
            {
                MessageBox.Show("El Campo (Amigo Id) está vacío.\n\nPorfavor, Seleccione El Nombre del Amigo.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                AmigoIdComboBox.IsDropDownOpen = true;
                return;
            }

            var paso = PrestamosBLL.Guardar(this.prestamos);

            if(paso)
            {
                Limpiar();
                MessageBox.Show("Transacción Exitosa", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("Transacción Fallida", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            if(PrestamosBLL.Eliminar(Utilidades.ToInt(PrestamoIdTextBox.Text)))
            {
                Limpiar();
                MessageBox.Show("Registro Eliminado", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("No se pudo eliminar el registro", "Error", MessageBoxButton.OK, MessageBoxImage.Error);

        }

        private void PrestamoIdTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }
    }
}
