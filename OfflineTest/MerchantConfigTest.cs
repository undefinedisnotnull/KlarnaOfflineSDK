﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Klarna.Offline.Entities;

namespace OfflineTest
{
    [TestClass]
    public class MerchantConfigTest
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MustNotBeAbleToInitWrongCountry()
        {
            var t = new MerchantConfig(new System.Globalization.CultureInfo(1),"SEK","DE","test","test",MerchantConfig.Server.Live);
        }
        [TestMethod]
        public void MustBeAbleToInitWithSweden()
        {
            var t = new MerchantConfig(new System.Globalization.CultureInfo(1), "SEK", "SE", "test", "test", MerchantConfig.Server.Live);
            Assert.AreNotEqual(null, t);
        }
        [TestMethod]
        public void MustBeAbleToInitWithFinland()
        {
            var t = new MerchantConfig(new System.Globalization.CultureInfo(1), "EUR", "FI", "test", "test", MerchantConfig.Server.Live);
            Assert.AreNotEqual(null, t);
        }
        [TestMethod]
        public void MustBeAbleToInitWithNorway()
        {
            var t = new MerchantConfig(new System.Globalization.CultureInfo(1), "NOK", "NO", "test", "test", MerchantConfig.Server.Live);
            Assert.AreNotEqual(null, t);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void MustNotBeAbleToInitWrongCurrency()
        {
            var t = new MerchantConfig(new System.Globalization.CultureInfo(1), "EUR", "SE", "test", "test", MerchantConfig.Server.Live);
        }
        [TestMethod]
        public void MustBeAbleToInitWithSwedishSEK()
        {
            var t = new MerchantConfig(new System.Globalization.CultureInfo(1), "SEK", "SE", "test", "test", MerchantConfig.Server.Live);
            Assert.AreNotEqual(null, t);
        }
    }
}
