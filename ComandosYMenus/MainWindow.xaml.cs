using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Threading;

namespace ComandosYMenus
{
    /// <summary>
    /// Lógica de interacción para MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ObservableCollection<TextBlock> lista;
        TextBlock copiaTextBlock;
        DispatcherTimer timer;

        public MainWindow()
        {
            copiaTextBlock = new TextBlock();
            lista = new ObservableCollection<TextBlock>();
            InitializeComponent();
            ItemsListBox.DataContext = lista;
            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TemporizadorTextBlock.Text = DateTime.Now.ToLongTimeString().ToString();
        }

        private void NewBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TextBlock nuevoTextBlock = new TextBlock();
            nuevoTextBlock.Text = "Item añadido a las " + DateTime.Now.ToLongTimeString();
            lista.Add(nuevoTextBlock);
        }

        private void NewBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (lista.Count == 10)
                e.CanExecute = false;
            else
                e.CanExecute = true;
        }

        private void ExitBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            Close();
        }

        private void ExitBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }

        private void DeleteBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            lista.Clear();
        }

        private void DeleteBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (lista.Count > 0)
                e.CanExecute = true;
            else
                e.CanExecute = false;
        }

        private void CopyBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            copiaTextBlock = (TextBlock)ItemsListBox.SelectedItem;
        }

        private void CopyBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (lista.Count > 0 && ItemsListBox.SelectedItem != null)
                e.CanExecute = true;
            else
                e.CanExecute = false;
        }

        private void PasteBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TextBlock textBlockAux = new TextBlock();
            textBlockAux.Text = copiaTextBlock.Text;
            lista.Add(textBlockAux);
            copiaTextBlock = null;
        }

        private void PasteBinding_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (lista.Count < 10 && copiaTextBlock != null)
                e.CanExecute = true;
            else
                e.CanExecute = false;
        }
    }
}
