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
        private ObservableCollection<double> _lAmount;
        private ObservableCollection<DateTime> _lDates;
        public DataManager(ObservableCollection<DateTime> dates, ObservableCollection<string> names,
            ObservableCollection<double> amounts)
        {
            //Upon initialization get data from repo
            _data = _repo.GetData();
            _lNames = names;
            _lDates = dates;
            _lAmount = amounts;
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
                _lAmount.Add(amountToAdd);
                return "(name, date) tuple already existed. Amount appended to list.";
            }
            else
            {
                _data.Add((nameToAdd, dateToAdd), new List<double>() { amountToAdd });
                _lNames.Add(nameToAdd);
                _lDates.Add(dateToAdd);
                _lAmount.Add(amountToAdd);
                return "(name, date) tuple did not exist. Created (key, value) pair and initialized list.";
            }



            return returnMsg;
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
            
            throw new NotImplementedException();
        }
    }
}
