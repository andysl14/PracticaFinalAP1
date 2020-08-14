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
    /// Interaction logic for rAmigos.xaml
    /// </summary>
    public partial class rAmigos : Window
    {
        private Amigos amigos = new Amigos();
        public rAmigos()
        {
            InitializeComponent();
            this.DataContext = amigos;
        }

        private void Cargar()
        {
            this.DataContext = null;
            this.DataContext = amigos;
        }

        private void Limpiar()
        {
            this.amigos = new Amigos();
            this.DataContext = amigos;
            AmigoIdTextBox.Focus();
            AmigoIdTextBox.SelectAll();
        }

        private bool Validar()
        {
            bool Validado = true;
            if (AmigoIdTextBox.Text.Length == 0)
            {
                Validado = false;
                MessageBox.Show("Transaccion Fallida", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            return Validado;
        }

        private void BuscarButton_Click(object sender, RoutedEventArgs e)
        {
            Amigos encontrado = AmigosBLL.Buscar(Utilidades.ToInt(AmigoIdTextBox.Text));

            if (encontrado != null)
            {
                this.amigos = encontrado;
                Cargar();
            }
            else
            {
                this.amigos = new Amigos();
                this.DataContext = this.amigos;
                MessageBox.Show($"Este Amigo no fue encontrado.\n\nAsegúrese que existe o cree uno nuevo.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                Limpiar();
                AmigoIdTextBox.SelectAll();
                AmigoIdTextBox.Focus();

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

            if (AmigoIdTextBox.Text.Trim() == string.Empty)
            {
                MessageBox.Show("El Campo (Amigo Id) está vacío.\n\nAsigne un ID al Amigo.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                AmigoIdTextBox.Text = "0";
                AmigoIdTextBox.Focus();
                AmigoIdTextBox.SelectAll();
                return;
            }

            if (NombresTextBox.Text.Trim() == string.Empty)
            {
                MessageBox.Show("El Campo (Nombres) está vacío.\n\nAsi Favor de llenar este campo.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                NombresTextBox.Clear();
                NombresTextBox.Focus();
                return;
            }

            if (ApellidosTextBox.Text.Trim() == string.Empty)
            {
                MessageBox.Show("El Campo (Apellidos) está vacío.\n\nAsi Favor de llenar este campo.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                ApellidosTextBox.Clear();
                ApellidosTextBox.Focus();
                return;
            }

            if (DireccionTextBox.Text.Trim() == string.Empty)
            {
                MessageBox.Show("El Campo (Direccion) está vacío.\n\nAsi Favor de llenar este campo.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                DireccionTextBox.Clear();
                DireccionTextBox.Focus();
                return;
            }

            if (TelefonoTextBox.Text.Trim() == string.Empty)
            {
                MessageBox.Show("El Campo (Teléfono) está vacío.\n\nFavor de llenar este campo.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                TelefonoTextBox.Text = "0";
                TelefonoTextBox.Focus();
                TelefonoTextBox.SelectAll();
                return;
            }

            if (TelefonoTextBox.Text.Length != 10)
            {
                MessageBox.Show($"El Teféfono ({TelefonoTextBox.Text}) no es válido.\n\nEl Teléfono debe tener 10 dígitos (0-9).", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                TelefonoTextBox.Focus();
                return;
            }

            if (CelularTextBox.Text.Trim() == string.Empty)
            {
                MessageBox.Show("El Campo (Celular) está vacío.\n\nFavor de llenar este campo.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                CelularTextBox.Text = "0";
                CelularTextBox.Focus();
                CelularTextBox.SelectAll();
                return;
            }

            if (CelularTextBox.Text.Length != 10)
            {
                MessageBox.Show($"El Celular ({CelularTextBox.Text}) no es válido.\n\nEl Celular debe tener 10 dígitos (0-9).", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                TelefonoTextBox.Focus();
                return;
            }

            if (EmailTextBox.Text.Trim() == string.Empty)
            {
                MessageBox.Show("El Campo (Email) está vacío.\n\nAsi Favor de llenar este campo.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                EmailTextBox.Clear();
                EmailTextBox.Focus();
                return;
            }

            if (FechaNacimientoDatePicker.Text.Trim() == string.Empty)
            {
                MessageBox.Show("El Campo (Fecha de Nacimiento) está vacío.\n\nFavor llenar este campo.", "Advertencia", MessageBoxButton.OK, MessageBoxImage.Warning);
                FechaNacimientoDatePicker.Focus();
                return;
            }

            var paso = AmigosBLL.Guardar(amigos);
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
            if (AmigosBLL.Eliminar(Utilidades.ToInt(AmigoIdTextBox.Text)))
            {
                Limpiar();
                MessageBox.Show("Registro Eliminado", "Éxito", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
                MessageBox.Show("No se pudo eliminar el registro", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void AmigoIdTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void TelefonoTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void CelularTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^0-9]+").IsMatch(e.Text);
        }

        private void NombresTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^a-zA-Z ]+").IsMatch(e.Text);
        }

        private void ApellidosTextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = new Regex("[^a-zA-Z ]+").IsMatch(e.Text);
        }
    }
}
