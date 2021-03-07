using System.Linq;
using System.Windows;
using VsScaffolders.Model;
using Xceed.Wpf.Toolkit;
using Xceed.Wpf.Toolkit.Primitives;

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

        private void ОкButton_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = true;
        }

        private void modelProperties_ItemSelectionChanged(object sender, ItemSelectionChangedEventArgs e)
        {
            var selectedItems = ((CheckListBox)sender).SelectedItems;
            ((SelectCodeModelViewModel)DataContext).SelectedProperties = selectedItems.Cast<CodePropertyModel>().ToArray();
        }
    }
}
