using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceManager.DataAccess
{
    //A repository should implement SaveData and GetData functions.
    //The SaveData function takes the current data as input and returns a boolean that indicates
    //whether the save went well.
    //The GetData function has no input; it reads the store location from 
    //the settings and returns an IDictionary<string, double>.
    public interface IRepository
    {
        string SaveLocation { get; set; }
        IDictionary<string, double> GetData();

        bool SaveData(IDictionary<string, double> data);


    }
}
