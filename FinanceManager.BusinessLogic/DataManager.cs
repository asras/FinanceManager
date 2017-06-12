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
        private Repository _repo;
        private Dictionary<DateTime, Dictionary<string, List<double>>> _data;
        public DataManager()
        {
            //Upon initialization get data from repo
            _data = new Dictionary<DateTime, Dictionary<string, List<double>>>();

        }
        public void FillObservableCollections(ObservableCollection<DateTime> dates, ObservableCollection<string> names,
            ObservableCollection<double> amounts)
        {
            throw new NotImplementedException();
        }

        public string AddData(string dateToAdd, string nameToAdd, string amountToAdd)
        {
            throw new NotImplementedException();
        }

        public string RemoveAtIndex(int selectedIndex)
        {
            throw new NotImplementedException();
        }

        public string SaveData()
        {
            throw new NotImplementedException();
        }

        public void InitializeManager(ObservableCollection<DateTime> dates, ObservableCollection<string> names,
            ObservableCollection<double> amounts)
        {
            throw new NotImplementedException();
        }
    }
}
