using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace FinanceManager.DataAccess
{
    public class Repository : IRepository
    {
        private FileStream _fileStream;
        private StreamWriter _streamWriter;
        private StreamReader _streamReader;
        private string _saveLocation = SettingsRetriever.SaveLocation;

        public string SaveLocation
        {
            get
            {
                return _saveLocation;
            }
            set
            {
                _saveLocation = value;
            }
        }


        public IDictionary<string, double> GetData()
        {
            try
            {
                using (_fileStream = new FileStream(SaveLocation, FileMode.Open))
                using (_streamReader = new StreamReader(_fileStream))
                {
                    string storedJsonString = _streamReader.ReadToEnd();
                    Dictionary<string, double> storedData = JsonConvert.DeserializeObject<Dictionary<string, double>>(storedJsonString);
                    return storedData;
                }
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine("No data was found at specified location. Returning empty data.");
                return new Dictionary<string, double>();
            }
                
        }

        public bool SaveData(IDictionary<string, double> data)
        {

            //Serialize data to json-string
            //Put json-string into filestream and save
            FileMode mode = FileMode.Create;
            try
            {
                string jsonString = JsonConvert.SerializeObject(data);

                using (_fileStream = new FileStream(_saveLocation, mode))
                using (_streamWriter = new StreamWriter(_fileStream))
                {
                    _streamWriter.Write(jsonString);
                }

                return true;
            }
            catch(Exception e)
            {
                return false;
            }
            
        }
    }
}
