using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;


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

        [TestMethod]
        public void SaveTuples()
        {
            var tuplerepo = new Repository<Dictionary<(string, DateTime), List<double>>>();

            var testdata = new Dictionary<(string, DateTime), List<double>>()
            {
                {("test", DateTime.Now), new List<double>() {22.2 } }
            };
            bool success = tuplerepo.SaveData(testdata);
            Assert.IsTrue(success);
            
        }

        [TestMethod]
        public void testest()
        {
            List<string> testlist = new List<string>()
            {
                "Test",
                "Wooo",
                "BLUH",
                "Test"
            };
            var testvar = testlist.IndexOf("Test");
            var testvar2 = testlist.IndexOf("Test");
            var eoekgpo = testlist.Where(e =>e == "Test").ToList();

            Dictionary<(string, DateTime), string> newdick = new Dictionary<(string, DateTime), string>()
            {
                {("test3", DateTime.Now), "1" },
                {("test2", DateTime.Now), "1" },
                {("test222", DateTime.Now), "2" }
            };
            var newtestlist = newdick.Where(e => e.Value == "1").ToList();
            var selecttest = newdick.Select(e => e.Value == "1").ToList();

            List<int> aw = new List<int>()
            {
                1,
                0,
                2
            };

            var testvar3 = newdick.OrderBy(e => e.Key.Item1).ToList();
            var etst = "";
            var waijdjiw = testlist.OrderBy(e => e).Select(e => e + Guid.NewGuid().ToString()).ToList();
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
