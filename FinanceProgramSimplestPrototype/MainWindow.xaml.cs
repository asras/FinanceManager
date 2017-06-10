using FinanceProgram.DataAccess;
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

namespace FinanceProgramSimplestPrototype
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<string> _names = new ObservableCollection<string>();
        private ObservableCollection<double> _amounts = new ObservableCollection<double>();
        private Dictionary<string, double> _data;
        private Repository _repo = new Repository(); 
        public MainWindow()
        {
            InitializeComponent();
            _data = (Dictionary<string, double>)_repo.GetData();
            foreach (string key in _data.Keys) {
                _names.Add(key);
                _amounts.Add(_data[key]);   
            }

            lbItemsName.ItemsSource = _names;
            lbItemsAmount.ItemsSource = _amounts;
            
        }

        public void btnAddItem_Click(object sender, RoutedEventArgs e) {
            tbInfoMessage.Text = "";
            string nameToAdd = this.InputExpenseType.Text;
            double amountToAdd;

            try
            {
                amountToAdd = Convert.ToDouble(this.InputExpenseAmount.Text);
            }
            catch (FormatException)
            {
                tbInfoMessage.Text = "Amount must be a number, dumbass";
                return;
            }
            if (!_names.Contains(nameToAdd))
            {
                _names.Add(nameToAdd);
                _amounts.Add(amountToAdd);
                _data.Add(nameToAdd, amountToAdd);

            }
            else
            {
                _amounts[_names.IndexOf(nameToAdd)] += amountToAdd;
                _data[nameToAdd] += amountToAdd;
            }
             
            

        }
        
        public void btnDeleteItem_Click(object sendr, RoutedEventArgs e)
        {
            tbInfoMessage.Text = "";
            var selectedIndex = lbItemsAmount.SelectedIndex > lbItemsName.SelectedIndex
                ? lbItemsAmount.SelectedIndex : lbItemsName.SelectedIndex;
            _amounts.RemoveAt(selectedIndex);
            _names.RemoveAt(selectedIndex);
            _data.Remove(_data.ElementAt(selectedIndex).Key);
        }

        private void btnSaveData_Click(object sender, RoutedEventArgs e)
        {
            tbInfoMessage.Text = "";
            bool success = _repo.SaveData(_data);
            if (!success) tbInfoMessage.Text = "Saving was not successful. Contact Jesus for Tech-support.";
            else tbInfoMessage.Text = "You successfully saved. Well, you just pressed a button; I wrote all the code. Thank me later.";
        }
    }
}