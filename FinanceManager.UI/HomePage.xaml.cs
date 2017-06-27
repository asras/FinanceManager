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
    /// Interaction logic for HomePage.xaml
    /// </summary>
    public partial class HomePage : Page
    {
        private ObservableCollection<DateTime> _dates = new ObservableCollection<DateTime>();
        private ObservableCollection<string> _names = new ObservableCollection<string>();
        private ObservableCollection<double> _amounts = new ObservableCollection<double>(); //Sum of transactions for Date+Name
        private List<string> testlist = new List<string>();
        private List<string> testlist2 = new List<string>();
        private IDataManager _dataManager;
        public HomePage()
        {
            InitializeComponent();
            _dataManager = new DataManager();
            SyncUI();

            testlist = testlist2;
            lbItemsDate.ItemsSource = _dates;
            lbItemsName.ItemsSource = _names;
            lbItemsAmount.ItemsSource = _amounts;

        }

        public void btnAddItem_Click(object sender, RoutedEventArgs e)
        {
            string dateToAdd = this.InputDate.Text;
            string nameToAdd = this.InputName.Text;
            string amountToAdd = this.InputAmount.Text;
            string infoMessage = _dataManager.AddData(dateToAdd, nameToAdd, amountToAdd);
            SyncUI();
            //_names.Add(nameToAdd);
            tbInfoMessage.Text = infoMessage;
        }

        public void btnDeleteItem_Click(object sendr, RoutedEventArgs e)
        {
            var selectedIndex = lbItemsAmount.SelectedIndex >= lbItemsName.SelectedIndex
                ? lbItemsAmount.SelectedIndex : lbItemsName.SelectedIndex;

            string infoMessage = _dataManager.RemoveAtIndex(_names[selectedIndex], _dates[selectedIndex]);
            SyncUI();
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
            var orderedLists = _dataManager.OrderListsBy(_dates, _names, _amounts, tup => tup.name);

            _dates = orderedLists.orderedDates;
            _names = orderedLists.orderedNames;
            _amounts = orderedLists.orderedAmounts;
        }

        private void lbItemsDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    
        private void btnViewItem_Click(object sender, RoutedEventArgs e)
        {
            //ExpenseReportPage expenseReportPage = new ExpenseReportPage();
            //this.NavigationService.Navigate(expenseReportPage);
        }

        private void SyncUI()
        {
            var lists = _dataManager.SyncDataUI( _dates, _names, _amounts);
            _amounts = lists.orderedAmounts;
            _dates = lists.orderedDates;
            _names = lists.orderedNames;
            lbItemsAmount.ItemsSource = _amounts;
            lbItemsDate.ItemsSource = _dates;
            lbItemsName.ItemsSource = _names;
            //TODO Maybe change sig of function to do
            //_dataManager.SyncDataUI(lbItemsDate.ItemsSource, ...);
            //Can we do completely away with _amounts, _dates and _names?
            //Maybe but we can't define 
        }
    }

}
