using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using FinanceManager.DataAccess;

namespace FinanceManager.BusinessLogic.Tests
{
    [TestClass]
    public class BusinessLogicTests
    {
        private IRepository<Dictionary<(string, DateTime), List<double>>> _repo = new Repository<Dictionary<(string, DateTime), List<double>>>();
        private IDataManager _DataManager = new DataManager();

        [TestMethod]
        public void TestAddData()
        {
            string name = "Bananer";
            string date = DateTime.Parse("10/10/2017").ToString();
            string amount = "10";

            string _testinfo = _DataManager.AddData(date, name, amount);

            Assert.IsTrue(String.Equals(_testinfo,"(name, date) tuple did not exist. Created (key, value) pair and initialized list."));

            _testinfo = _DataManager.AddData(date, name, amount);

            Assert.IsTrue(String.Equals(_testinfo, "(name, date) tuple already existed. Amount appended to list."));
        }


    }
}
