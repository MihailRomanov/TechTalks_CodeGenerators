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
using VsScaffolders.Model;

namespace VsScaffolders.UI
{
    /// <summary>
    /// Interaction logic for Dialog.xaml
    /// </summary>
    public partial class SelectCodeModelDialog : Microsoft.VisualStudio.PlatformUI.DialogWindow
    {
        public SelectCodeModelDialog()
        {
            InitializeComponent();
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedItems = ((ListBox)sender).SelectedItems;
            ((SelectCodeModelViewModel)DataContext).SelectedProperties = selectedItems.Cast<CodePropertyModel>().ToArray();
        }

        private void ОкButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }
    }
}
