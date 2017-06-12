using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace FinanceManager.DataAccess.Tests
{
    [TestClass]
    public class RepositoryTests
    {
        private IRepository<Dictionary<DateTime, Dictionary<string, List<double>>>> _repo = new Repository<Dictionary<DateTime, Dictionary<string, List<double>>>>();
        private Dictionary<DateTime, Dictionary<string, List<double>>> _testData = new Dictionary<DateTime, Dictionary<string, List<double>>>();
        private Dictionary<string, List<double>> _testDataInner;
        private DateTime _testDate;
        List<double> _testAmount;

        [TestInitialize]
        public void InitialiseData()
        {
            _repo.SaveLocation = "..\\..\\UnitTests\\testLocation.txt";
            _testDate = new DateTime(2017, 06, 12);

            _testAmount = new List<double>(){
                22,
                777
            };

            _testDataInner = new Dictionary<string, List<double>>()
            {
                { "Bananas", _testAmount }
            };
        }


        [TestMethod]
        public void CheckThatSaveDoesNotThrowException()
        {
            _testData.Add(_testDate, _testDataInner);
            _repo.SaveData(_testData);
        }

        [TestMethod]
        public void MultipleSaves()
        {
            _repo.SaveLocation = "..\\..\\UnitTests\\testLocation2.txt";

            _testDate = new DateTime(2017, 05, 12);
            _testDataInner.Add("Apples", new List<double>() { 128074923, 1 });
            _testData.Add(_testDate, _testDataInner);

            _repo.SaveData(_testData);

            _repo.SaveLocation = "..\\..\\UnitTests\\UnitTests\\testLocation2BeforeSecondSave.txt";
            _repo.SaveData(_testData);

            _repo.SaveLocation = "..\\..\\UnitTests\\UnitTests\\testLocation2.txt";
            _testDataInner.Add("Pears", new List<double>() { 11.2 });
            _repo.SaveData(_testData);
        }


        //[TestMethod]
        //public void GetDataTest()
        //{
        //    //First save data to a file
        //    //Then try to read data and compare to what we put into file
        //    _repo.SaveLocation = "UnitTests/getDataTestLocation.txt";
        //    double housePrice = 111222333;
        //    _testData.Add("A house", housePrice);
        //    _repo.SaveData(_testData);
        //    Dictionary<string, double> retrievedData = (Dictionary<string, double>)_repo.GetData();
        //    Assert.IsTrue(retrievedData.ContainsKey("A house"));
        //    Assert.AreEqual(housePrice, retrievedData["A house"]);
        //}

        //TODO Write unit test for removing data and then saving to ensure that 
        //the datafile remains valid
    }
}
