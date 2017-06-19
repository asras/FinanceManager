using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.BusinessLogic
{
    /// <summary>
    /// The DataManager's job is to retrieve data from storage (via repo), fill the UI-connected objects with data,
    /// validate incoming data and return infomessages.
    /// </summary>
    public interface IDataManager
    {
        /// <summary>
        /// Takes entries in data and fills out ObservableCollections to connec to UI
        /// </summary>
        /// <param name="dates"></param>
        /// <param name="names"></param>
        /// <param name="amounts"></param>
        void FillObservableCollections(ObservableCollection<DateTime> dates, ObservableCollection<string> names,
            ObservableCollection<double> amounts);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dateToAdd"></param>
        /// <param name="nameToAdd"></param>
        /// <param name="amountToAdd"></param>
        /// <returns>Info message</returns>
        string AddData(string dateToAdd, string nameToAdd, string amountToAdd);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectedIndex"></param>
        /// <returns>Info Message</returns>
        string RemoveAtIndex(int selectedIndex);
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Info Message</returns>
        string SaveData();
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Info Message</returns>
        void SyncDataUI();
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Info Message</returns>
        (ObservableCollection<DateTime> orderedDates, ObservableCollection<string> orderedNames, ObservableCollection<double> orderAmounts)
                    OrderListsBy<T>(Func<(DateTime date, string name, double amount), T> orderBySelector);
    }
}
