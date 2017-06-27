using FinanceManager.BusinessLogic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
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
        private readonly IDataManager _dataManager;
        public MainWindow()
        {
            InitializeComponent();
            _dataManager = new DataManager();
            _dataManager.SyncDataUI(_dates, _names, _amounts);

            lbItemsDate.ItemsSource = _dates;
            lbItemsName.ItemsSource = _names;
            lbItemsAmount.ItemsSource = _amounts;
            
        }

        public void btnAddItem_Click(object sender, RoutedEventArgs e) {
            string dateToAdd = this.InputDate.Text;
            string nameToAdd = this.InputName.Text;
            string amountToAdd = this.InputAmount.Text;
            string infoMessage = _dataManager.AddData(dateToAdd, nameToAdd, amountToAdd);
            _dataManager.SyncDataUI(_dates, _names, _amounts);

            tbInfoMessage.Text = infoMessage;
        }
        
        public void btnDeleteItem_Click(object sendr, RoutedEventArgs e)
        {
            var selectedIndex = lbItemsAmount.SelectedIndex >= lbItemsName.SelectedIndex
                ? lbItemsAmount.SelectedIndex : lbItemsName.SelectedIndex;

            string infoMessage = _dataManager.RemoveAtIndex(_names[selectedIndex], _dates[selectedIndex]);
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

        private void lbDate_DoubleClick(object sender, MouseEventArgs e)
        {
            var orderedLists = _dataManager.OrderListsBy(_dates, _names, _amounts, tup => tup.date);

            _dates = orderedLists.orderedDates;
            _names = orderedLists.orderedNames;
            _amounts = orderedLists.orderedAmounts;
        }
        private void lbName_DoubleClick(object sender, MouseEventArgs e)
        {

            var orderedLists = _dataManager.OrderListsBy(_dates, _names, _amounts, tup => tup.name);

            _dates = orderedLists.orderedDates;
            _names = orderedLists.orderedNames;
            _amounts = orderedLists.orderedAmounts;
        }
        private void lbAmount_DoubleClick(object sender, MouseEventArgs e)
        {
            var orderedLists = _dataManager.OrderListsBy(_dates, _names, _amounts, tup => tup.amount);

            _dates = orderedLists.orderedDates;
            _names = orderedLists.orderedNames;
            _amounts = orderedLists.orderedAmounts;
        }

        private void lbItemsDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}