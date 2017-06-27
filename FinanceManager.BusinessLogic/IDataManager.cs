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
        string RemoveAtIndex(string name, DateTime date);
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Info Message</returns>
        string SaveData();
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Info Message</returns>
        (ObservableCollection<DateTime> orderedDates, ObservableCollection<string> orderedNames, ObservableCollection<double> orderedAmounts) SyncDataUI(ObservableCollection<DateTime> dates, ObservableCollection<string> names, ObservableCollection<double> amounts);
        /// <summary>
        /// 
        /// </summary>
        /// <returns>Info Message</returns>
        (ObservableCollection<DateTime> orderedDates, ObservableCollection<string> orderedNames, ObservableCollection<double> orderedAmounts)
                    OrderListsBy<T>(ObservableCollection<DateTime> dates, ObservableCollection<string> names, 
            ObservableCollection<double> amounts, Func<(DateTime date, string name, double amount), T> orderBySelector);
    }
}
