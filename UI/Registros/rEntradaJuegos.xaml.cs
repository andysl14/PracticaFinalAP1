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
    /// Interaction logic for rEntradaJuegos.xaml
    /// </summary>
    public partial class rEntradaJuegos : Window
    {
        private EntradaJuegos entradaJuegos = new EntradaJuegos();
        public rEntradaJuegos()
        {
            InitializeComponent();
            this.DataContext = entradaJuegos;

            JuegoIdComboBox.ItemsSource = JuegosBLL.GetJuegos();
            JuegoIdComboBox.SelectedValuePath = "JuegoId";
            JuegoIdComboBox.DisplayMemberPath = "Descripcion";
        }

        private void Cargar()
        {
            this.DataContext = null;
            this.DataContext = entradaJuegos;
        }

        private void Limpiar()
        {
            this.entradaJuegos = new EntradaJuegos();
            this.DataContext = entradaJuegos;
        }

        private bool Validar()
        {
            bool Validado = true;
            if (EntradaIdTextBox.Text.Length == 0)
            {
                Validado = false;
                MessageBox.Show("Transacción Fallida", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return Validado;
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            EntradaJuegos encontrado = EntradasJuegosBLL.Buscar(Utilidades.ToInt(EntradaIdTextBox.Text));

            if (encontrado != null)
            {
                this.entradaJuegos = encontrado;
                Cargar();
            }
            else
            {
                this.entradaJuegos = new EntradaJuegos();
                this.DataContext = this.entradaJuegos;
                MessageBox.Show($"Esta Entrada de Juego no fue encontrada.\n\nAsegúrese que existe o cree una nueva.", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Limpiar();
                EntradaIdTextBox.SelectAll();
                EntradaIdTextBox.Focus();
            }
        }

        private void NuevoButton_Click(object sender, RoutedEventArgs e)
        {
            Limpiar();
        }

        private void GuardarButton_Click(object sender, RoutedEventArgs e)
        {
            
                if (!Validar())
                    return;

                
                if (EntradaIdTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Entrada Id) está vacío.\n\nDebe asignar un Id a la Entrada del Juego.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    EntradaIdTextBox.Text = "0";
                    EntradaIdTextBox.Focus();
                    EntradaIdTextBox.SelectAll();
                    return;
                }
                
                if (JuegoIdComboBox.Text == string.Empty)
                {
                    MessageBox.Show("El Campo (Juego Id) está vacío.\n\nPorfavor, Seleccione El Nombre del juego.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                    JuegoIdComboBox.IsDropDownOpen = true;
                    return;
                }
                
                
                if (FechaDatePicker.Text.Trim() == string.Empty)
                {
                    MessageBox.Show($"El Campo (Fecha) está vacío.\n\nSeleccione una fecha para la Entrada del Juego.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    FechaDatePicker.Focus();
                    return;
                }
                
                if (CantidadTextBox.Text.Trim() == string.Empty)
                {
                    MessageBox.Show("El Campo (Cantidad) está vacío.\n\nEscriba la cantidad de Juegos.", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                    CantidadTextBox.Text = "0";
                    CantidadTextBox.Focus();
                    CantidadTextBox.SelectAll();
                    return;
                }
                

                JuegosBLL.EntradaJuegos(Convert.ToInt32(JuegoIdComboBox.SelectedValue), Convert.ToInt32(CantidadTextBox.Text)); //-----------------

                var paso = EntradasJuegosBLL.Guardar(entradaJuegos);
                if (paso)
                {
                    Limpiar();
                    MessageBox.Show("Transacción Exitosa", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("Transacción Fallida", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            
        }

        private void EliminarButton_Click(object sender, RoutedEventArgs e)
        {
            
                if (EntradasJuegosBLL.Eliminar(Utilidades.ToInt(EntradaIdTextBox.Text)))
                {
                    JuegosBLL.DisminuirEntradaJuegos(Convert.ToInt32(JuegoIdComboBox.SelectedValue), Convert.ToInt32(CantidadTextBox.Text)); //-----------------
                    Limpiar();
                    MessageBox.Show("Registro Eliminado", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                    MessageBox.Show("No se pudo eliminar el registro", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            
        }


        private void EntradaIdTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void CantidadTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }
    }
}
