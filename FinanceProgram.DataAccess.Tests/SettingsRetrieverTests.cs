using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FinanceProgram.DataAccess;
namespace FinanceProgram.DataAccess.Tests
{
    [TestClass]
    public class SettingsRetrieverTests
    {
        [TestMethod]
        public void TestCorrectRetrievalOfSaveLocation()
        {
            string expectedString = "testLocation.txt";
            string actualString = SettingsRetriever.SaveLocation;

            Assert.AreEqual(expectedString, actualString);
        }
    }
}
