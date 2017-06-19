using FinanceManager.DataAccess;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.BusinessLogic
{
    public class DataManager : IDataManager
    {
        private Repository<Dictionary<(string, DateTime), List<double>>> _repo = new Repository<Dictionary<(string, DateTime), List<double>>>();
        private Dictionary<(string name, DateTime date), List<double>> _data;
        private ObservableCollection<string> _lNames;
        private ObservableCollection<double> _lAmounts;
        private ObservableCollection<DateTime> _lDates;
        public DataManager(ObservableCollection<DateTime> dates, ObservableCollection<string> names,
            ObservableCollection<double> amounts)
        {
            //Upon initialization get data from repo
            _data = _repo.GetData();
            _lNames = names;
            _lDates = dates;
            _lAmounts = amounts;
        }
        public void FillObservableCollections(ObservableCollection<DateTime> dates, ObservableCollection<string> names,
            ObservableCollection<double> amounts)
        {
            throw new NotImplementedException();
        }

        public string AddData(DateTime dateToAdd, string nameToAdd, double amountToAdd)
        {
            if (_data.Keys.Contains((nameToAdd, dateToAdd)))
            {
                _data[(nameToAdd, dateToAdd)].Add(amountToAdd);
                _lAmounts.Add(amountToAdd);
                return "(name, date) tuple already existed. Amount appended to list.";
            }
            else
            {
                _data.Add((nameToAdd, dateToAdd), new List<double>() { amountToAdd });
                _lNames.Add(nameToAdd);
                _lDates.Add(dateToAdd);
                _lAmounts.Add(amountToAdd);
                return "(name, date) tuple did not exist. Created (key, value) pair and initialized list.";
            }
        }

        public string RemoveAtIndex(int selectedIndex)
        {
            _data.Remove((_lNames[selectedIndex], _lDates[selectedIndex]));
            SyncDataUI();

            return "Item at specified index removed.";
        }

        public string SaveData()
        {
            throw new NotImplementedException();
        }

        public void SyncDataUI()
        {
            ObservableCollection<string> tmpNames = new ObservableCollection<string>();
            ObservableCollection<DateTime> tmpDates = new ObservableCollection<DateTime>();
            ObservableCollection<double> tmpAmounts = new ObservableCollection<double>();

            foreach (KeyValuePair<(string name, DateTime date), List<double>> entry in _data)
            {
                tmpNames.Add(entry.Key.name);
                tmpDates.Add(entry.Key.date);
                tmpAmounts.Add(entry.Value.Sum());
            }

            _lNames = tmpNames;
            _lDates = tmpDates;
            _lAmounts = tmpAmounts;

            OrderListsBy(tup => tup.date);
        }

        public (ObservableCollection<DateTime> orderedDates, ObservableCollection<string> orderedNames, ObservableCollection<double> orderAmounts)
                    OrderListsBy<T>(Func<(DateTime date, string name, double amount), T> orderBySelector)
        {
            List<(DateTime, string, double)> combinedlist = new List<(DateTime, string, double)>();
            for (int i = 0; i < _lNames.Count; i++)
            {
                combinedlist.Add((_lDates[i], _lNames[i], _lAmounts[i]));
            }

            ObservableCollection<DateTime> sortedDates = new ObservableCollection<DateTime>(
                combinedlist.OrderBy(orderBySelector).Select(tup => tup.Item1).ToList());
            ObservableCollection<string> sortedNames = new ObservableCollection<string>(
                combinedlist.OrderBy(orderBySelector).Select(tup => tup.Item2).ToList());
            ObservableCollection<double> sortedAmounts = new ObservableCollection<double>(
                combinedlist.OrderBy(orderBySelector).Select(tup => tup.Item3).ToList());

            return (sortedDates, sortedNames, sortedAmounts);
        }
    }
}
