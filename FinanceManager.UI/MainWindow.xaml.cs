using FinanceManager.BusinessLogic;
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

namespace FinanceManager.UI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ObservableCollection<DateTime> _dates = new ObservableCollection<DateTime>();
        private ObservableCollection<string> _names = new ObservableCollection<string>();
        private ObservableCollection<double> _amounts = new ObservableCollection<double>(); //Sum of transactions for Date+Name
        private readonly IDataManager _dataManager = new DataManager();
        public MainWindow()
        {
            InitializeComponent();
            _dataManager.InitializeManager(_dates, _names, _amounts);
            _dataManager.FillObservableCollections(_dates, _names, _amounts);

            lbItemsDate.ItemsSource = _dates;
            lbItemsName.ItemsSource = _names;
            lbItemsAmount.ItemsSource = _amounts;
            
        }

        public void btnAddItem_Click(object sender, RoutedEventArgs e) {
            string dateToAdd = this.InputDate.Text;
            string nameToAdd = this.InputName.Text;
            string amountToAdd = this.InputAmount.Text;
            string infoMessage =_dataManager.AddData(dateToAdd, nameToAdd, amountToAdd);

            tbInfoMessage.Text = infoMessage;
        }
        
        public void btnDeleteItem_Click(object sendr, RoutedEventArgs e)
        {
            var selectedIndex = lbItemsAmount.SelectedIndex >= lbItemsName.SelectedIndex
                ? lbItemsAmount.SelectedIndex : lbItemsName.SelectedIndex;

            string infoMessage = _dataManager.RemoveAtIndex(selectedIndex);
            tbInfoMessage.Text = infoMessage;
        }

        private void btnSaveData_Click(object sender, RoutedEventArgs e)
        {

            string infoMessage = _dataManager.SaveData();
            tbInfoMessage.Text = infoMessage;
        }

        private void btnSearchData_Click(object sender, RoutedEventArgs e)
        {
            tbInfoMessage.Text = "Not implemented yet.";
        }
    }
}