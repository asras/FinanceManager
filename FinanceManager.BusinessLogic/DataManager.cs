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
        public DataManager()
        {
            //Upon initialization get data from repo
            _data = _repo.GetData();
        }

        public string AddData(string dateToAdd, string nameToAdd, string amountToAdd)
        {
            DateTime tmpDate = new DateTime();
            double tmpAmount;

            var dateConversionResult = DateTime.TryParse(dateToAdd, out tmpDate);
            var amountConversionResult = Double.TryParse(amountToAdd, out tmpAmount);

            if (!dateConversionResult)
            {
                return "Wrong date or dateformat specified!";
            }

            if (!amountConversionResult || tmpAmount < 0)
            {
                return "Wrong amount or amountformat specified!";
            }


            if (_data.Keys.Contains((nameToAdd, tmpDate)))
            {
                _data[(nameToAdd, tmpDate)].Add(tmpAmount);
                return "(name, date) tuple already existed. Amount appended to list.";
            }
            else
            {
                _data.Add((nameToAdd, tmpDate), new List<double>() { tmpAmount });
                return "(name, date) tuple did not exist. Created (key, value) pair and initialized list.";
            }
        }

        public string RemoveAtIndex(string name, DateTime date)
        {
            _data.Remove((name, date));
            return "Specific item removed.";
        }

        public string SaveData()
        {
            throw new NotImplementedException();
        }

        public void SyncDataUI(ObservableCollection<DateTime> dates, ObservableCollection<string> names,
            ObservableCollection<double> amounts)
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

            //names = tmpNames;
            //dates = tmpDates;
            //amounts = tmpAmounts;

            var orderedLists = OrderListsBy(tmpDates, tmpNames, tmpAmounts, tup => tup.date);

            dates = orderedLists.orderedDates;
            names = orderedLists.orderedNames;
            amounts = orderedLists.orderedAmounts;
        }

        public (ObservableCollection<DateTime> orderedDates, ObservableCollection<string> orderedNames, ObservableCollection<double> orderedAmounts)
                    OrderListsBy<T>(ObservableCollection<DateTime> dates, ObservableCollection<string> names, 
            ObservableCollection<double> amounts, Func<(DateTime date, string name, double amount), T> orderBySelector)
        {
            List<(DateTime, string, double)> combinedlist = new List<(DateTime, string, double)>();
            for (int i = 0; i < names.Count; i++)
            {
                combinedlist.Add((dates[i], names[i], amounts[i]));
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
