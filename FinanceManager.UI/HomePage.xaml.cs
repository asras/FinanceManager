﻿using FinanceManager.BusinessLogic;
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
        private ObservableCollection<DateTime> _dates;
        public ObservableCollection<string> _names = new ObservableCollection<string>();
        public ObservableCollection<double> _amounts = new ObservableCollection<double>(); //Sum of transactions for Date+Name
        private readonly IDataManager _dataManager;
        public HomePage()
        {
            InitializeComponent();
            _dataManager = new DataManager(ref _dates, ref _names, ref _amounts);
            _dataManager.SyncDataUI();

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

        private void lbDate_DoubleClick(object sender, MouseEventArgs e)
        {
            var orderedLists = _dataManager.OrderListsBy(tup => tup.date);

            _dates = orderedLists.orderedDates;
            _names = orderedLists.orderedNames;
            _amounts = orderedLists.orderAmounts;
        }
        private void lbName_DoubleClick(object sender, MouseEventArgs e)
        {

            var orderedLists = _dataManager.OrderListsBy(tup => tup.name);

            _dates = orderedLists.orderedDates;
            _names = orderedLists.orderedNames;
            _amounts = orderedLists.orderAmounts;
        }
        private void lbAmount_DoubleClick(object sender, MouseEventArgs e)
        {
            var orderedLists = _dataManager.OrderListsBy(tup => tup.amount);

            _dates = orderedLists.orderedDates;
            _names = orderedLists.orderedNames;
            _amounts = orderedLists.orderAmounts;
        }

        private void lbItemsDate_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnViewItem_Click(object sender, RoutedEventArgs e)
        {
            //ExpenseReportPage expenseReportPage = new ExpenseReportPage();
            //this.NavigationService.Navigate(expenseReportPage);
        }
    }

}
