using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using AutoGidas;
namespace AutogidasTest
{
    [TestClass]
    public class CarInfoTest
    {
       
        [TestMethod]
        public void PriceTest()
        {
            CarInfo carmatest = new CarInfo();
            carmatest.run_cmd("https://autogidas.lt/skelbimas/seat-ibiza-dyzelinas--2010-m-iv-tdi-dpf-style-0132464261.html");

            int expected = 3800;

            Assert.AreEqual(expected, carmatest.price, "Price not equals");

        }
        [TestMethod]
        public void AveragePriceTest()
        {
            CarInfo carmatest = new CarInfo();
            carmatest.run_cmd("https://autogidas.lt/skelbimas/seat-ibiza-dyzelinas--2010-m-iv-tdi-dpf-style-0132464261.html");

            int expected = 3378;

            Assert.AreEqual(expected, carmatest.averagePrice, "Average price not equal");

        }
        [TestMethod]
        public void AverageTest()
        {
            CarInfo carmatest = new CarInfo();
            carmatest.run_cmd("https://autogidas.lt/skelbimas/seat-ibiza-dyzelinas--2010-m-iv-tdi-dpf-style-0132464261.html");

            string expected = "Brangesne uz vidutine";

            Assert.AreEqual(expected, carmatest.average, "Average not equal");

        }
        [TestMethod]
        public void AmountTest()
        {
            CarInfo carmatest = new CarInfo();
            carmatest.run_cmd("https://autogidas.lt/skelbimas/seat-ibiza-dyzelinas--2010-m-iv-tdi-dpf-style-0132464261.html");

            int expected = 12;

            Assert.AreEqual(expected, carmatest.amount, "Amount not equal");

        }
        [TestMethod]
        public void LeftTest()
        {
            CarInfo carmatest = new CarInfo();
            carmatest.run_cmd("https://autogidas.lt/skelbimas/seat-ibiza-dyzelinas--2010-m-iv-tdi-dpf-style-0132464261.html");

            string expected = "Kaina";

            Assert.IsTrue(carmatest.left.Contains(expected));

        }
        [TestMethod]
        public void RightTest()
        {
            CarInfo carmatest = new CarInfo();
            carmatest.run_cmd("https://autogidas.lt/skelbimas/seat-ibiza-dyzelinas--2010-m-iv-tdi-dpf-style-0132464261.html");

            string expected = "Dyzelinas";

            Assert.IsTrue(carmatest.right.Contains(expected));

        }
        [TestMethod]
        public void CofficientToBuyTest()
        {
            CarInfo carmatest = new CarInfo();
            carmatest.run_cmd("https://autogidas.lt/skelbimas/bmw-320-dyzelinas--2008-m-e90-0131954786.html");

            double expected = 1.11;

            Assert.AreEqual(expected, carmatest.coefficient, "Coefficient not equal");

        }
        [TestMethod]
        public void CofficientToMaybeTest()
        {
            CarInfo carmatest = new CarInfo();
            carmatest.run_cmd("https://autogidas.lt/skelbimas/seat-ibiza-dyzelinas--2010-m-iv-tdi-dpf-style-0132464261.html");

            double expected = 1.08;

            Assert.AreEqual(expected, carmatest.coefficient, "Coefficient not equal");

        }
        [TestMethod]
        public void CofficientToNotBuyTest()
        {
            CarInfo carmatest = new CarInfo();
            carmatest.run_cmd("https://autogidas.lt/skelbimas/volkswagen-golf-benzinas--1999-m-iv-comfortline-0132232133.html");

            double expected = 0.76;

            Assert.AreEqual(expected, carmatest.coefficient, "Coefficient not equal");

        }
    }
}
