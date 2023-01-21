﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DailyPoetryM.Services;
using Moq;

namespace DailyPoetryM.UnitTest.Services
{
    public class PoetryStorageTest
    {
        [Fact]
        public void IsInitialized_Default()
        {
            //傀儡实例
            var preferenceStorageMock = new Mock<IPreferenceStorage>();
            preferenceStorageMock.Setup(p => p.Get(PoetryStorageConst.VersionKey, 0)).Returns(PoetryStorageConst.Version);
            var mockPreferenceStorage = preferenceStorageMock.Object;

            var poetryStorage = new PoetryStorage(mockPreferenceStorage);
            Assert.True(poetryStorage.IsInitialized);
        }






    }
}
