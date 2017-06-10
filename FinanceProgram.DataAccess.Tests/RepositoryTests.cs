using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace FinanceProgram.DataAccess.Tests
{
    [TestClass]
    public class RepositoryTests
    {
        private IRepository _repo = new Repository();
        private Dictionary<string, double> _testData = new Dictionary<string, double>();


        [TestMethod]
        public void CheckThatSaveDoesNotThrowException()
        {
            _repo.SaveLocation = "testLocation.txt";
            _testData.Add("Bananas", 22.2);

            _repo.SaveData(_testData);
        }

        [TestMethod]
        public void MultipleSaves()
        {
            _repo.SaveLocation = "testLocation2.txt";
            _testData.Add("Bananas", 22.2);
            _testData.Add("Apples", 22);
            _repo.SaveData(_testData);
            _repo.SaveLocation = "testLocation2BeforeSecondSave.txt";
            _repo.SaveData(_testData);
            _repo.SaveLocation = "testLocation2.txt";
            _testData.Add("Pears", 11.2);
            _repo.SaveData(_testData);
        }


        [TestMethod]
        public void GetDataTest()
        {
            //First save data to a file
            //Then try to read data and compare to what we put into file
            _repo.SaveLocation = "getDataTestLocation.txt";
            double housePrice = 111222333;
            _testData.Add("A house", housePrice);
            _repo.SaveData(_testData);
            Dictionary<string, double> retrievedData = (Dictionary<string, double>)_repo.GetData();
            Assert.IsTrue(retrievedData.ContainsKey("A house"));
            Assert.AreEqual(housePrice, retrievedData["A house"]);
        }

        //TODO Write unit test for removing data and then saving to ensure that 
        //the datafile remains valid
    }
}
