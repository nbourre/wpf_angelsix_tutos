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

namespace wpf_basics
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

        private void ApplyButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show($"The description is : {this.DescriptionText.Text}");
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            this.WeldCheckBox.IsChecked = false;
            this.AssemblyCheckBox.IsChecked = false;
            this.PlasmaCheckBox.IsChecked = false;
            this.LaserCheckBox.IsChecked = false;
            this.PurchaseCheckBox.IsChecked = false;

            this.LatheCheckBox.IsChecked = false;
            this.DrillCheckBox.IsChecked = false;
            this.FoldCheckBox.IsChecked = false;
            this.RollCheckBox.IsChecked = false;
            this.SawCheckBox.IsChecked = false;
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            LengthText.Text += ((CheckBox)sender).Content + " ";
        }

        private void FinishComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (this.NoteText == null)
                return;

            var combo = (ComboBox)sender;
            this.NoteText.Text = ((ComboBoxItem)(combo.SelectedValue)).Content.ToString();
        }

        private void SupplierText_TextChanged(object sender, TextChangedEventArgs e)
        {
            this.MassText.Text = ((TextBox)sender).Text;
        }
    }
}
