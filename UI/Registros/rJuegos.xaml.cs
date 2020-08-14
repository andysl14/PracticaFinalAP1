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
    /// Interaction logic for rJuegos.xaml
    /// </summary>
    public partial class rJuegos : Window
    {
        private Juegos juegos = new Juegos();
        public rJuegos()
        {
            InitializeComponent();
            this.DataContext = juegos;
        }

        private void Cargar()
        {
            this.DataContext = null;
            this.DataContext = juegos;
        }

        private void Limpiar()
        {
            this.juegos = new Juegos();
            this.DataContext = juegos;
            JuegoIdTextBox.Focus();
            JuegoIdTextBox.SelectAll();
        }

        private bool Validar()
        {
            bool Validado = true;
            if (JuegoIdTextBox.Text.Length == 0)
            {
                Validado = false;
                MessageBox.Show("Transaccion Fallida", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return Validado;
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Juegos encontrado = JuegosBLL.Buscar(Utilidades.ToInt(JuegoIdTextBox.Text));

            if (encontrado != null)
            {
                this.juegos = encontrado;
                Cargar();
            }
            else
            {
                this.juegos = new Juegos();
                this.DataContext = this.juegos;
                MessageBox.Show($"Este Juego no fue encontrado.\n\nAsegúrese que existe o cree uno nuevo.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                Limpiar();
                JuegoIdTextBox.SelectAll();
                JuegoIdTextBox.Focus();

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

            if (JuegoIdTextBox.Text.Trim() == string.Empty)
            {
                MessageBox.Show("El Campo (Juego Id) está vacío.\n\nAsigne un ID al Juego.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                JuegoIdTextBox.Text = "0";
                JuegoIdTextBox.Focus();
                JuegoIdTextBox.SelectAll();
                return;
            }

            if (DescripcionTextBox.Text.Trim() == string.Empty)
            {
                MessageBox.Show("El Campo (Descripcion) está vacío.\n\nAsi Favor de llenar este campo.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                DescripcionTextBox.Clear();
                DescripcionTextBox.Focus();
                return;
            }

            if (FechaCompraDatePicker.Text.Trim() == string.Empty)
            {
                MessageBox.Show("El Campo (Fecha de Compra) está vacío.\n\nAsigne una Fecha de Compra.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                FechaCompraDatePicker.Focus();
                return;
            }

            if (PrecioTextBox.Text.Trim() == string.Empty)
            {
                MessageBox.Show("El Campo (Precio) está vacío.\n\nAsigne un Precio al Juego.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                PrecioTextBox.Text = "0";
                PrecioTextBox.Focus();
                PrecioTextBox.SelectAll();
                return;
            }

            if (ExistenciaTextBox.Text.Trim() == string.Empty)
            {
                MessageBox.Show("El Campo (Existencia) está vacío.\n\nAsigne una Cantidad en existencia del juego.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                ExistenciaTextBox.Text = "0";
                ExistenciaTextBox.Focus();
                ExistenciaTextBox.SelectAll();
                return;
            }



            var paso = JuegosBLL.Guardar(juegos);
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
            if (JuegosBLL.Eliminar(Utilidades.ToInt(JuegoIdTextBox.Text)))
            {
                Limpiar();
                MessageBox.Show("Registro Eliminado", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("No se pudo eliminar el registro", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void JuegoIdTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void ExistenciaTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void PrecioTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            var PrecioTextBox = sender as TextBox;
            if (PrecioTextBox == null)
                return;

            var text = PrecioTextBox.Text;
            var output = new StringBuilder();

            //Se determina si un punto(.) existe en el texto.
            var dotEncountered = false;

            for (int i = 0; i < text.Length; i++)
            {
                var c = text[i];
                if (char.IsDigit(c))
                {
                    //adjunta cualquier digito.
                    output.Append(c);
                }
                else if (!dotEncountered && c == '.')
                {
                    //Adjunta el primer punto encontrado en el texto
                    output.Append(c);
                    dotEncountered = true;
                }
            }
            var newText = output.ToString();
            PrecioTextBox.Text = newText;

            PrecioTextBox.CaretIndex = newText.Length;
        }
    }
}
