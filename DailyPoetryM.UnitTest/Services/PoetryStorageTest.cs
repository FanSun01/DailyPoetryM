using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DailyPoetryM.Models;
using DailyPoetryM.Services;
using Moq;

namespace DailyPoetryM.UnitTest.Services
{
    public class PoetryStorageTest : IDisposable
    {
        public PoetryStorageTest()
        {
            File.Delete(PoetryStorage.PoetryDbPath);
        }

        public void Dispose()
        {
            File.Delete(PoetryStorage.PoetryDbPath);
        }

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

        [Fact]
        public async Task TestInitializeAsync_Default()
        {
            var poetryStorage = await InitializeTestPoetryStorage();
            Assert.False(File.Exists(PoetryStorage.PoetryDbPath));
            await poetryStorage.InitializeAsync();
            Assert.True(File.Exists(PoetryStorage.PoetryDbPath));
        }

        [Fact]
        public async Task GetPoetryAsync_Default()
        {
            var poetryStorage = await InitializeTestPoetryStorage();
            var portry = await poetryStorage.GetPoetryAsync(10001);
            Assert.Equal("临江仙 · 夜归临皋", portry.Name);
            await poetryStorage.CloseAsync();
        }

        [Fact]
        public async Task GetPoetriesAsync_Default()
        {
            var poetryStorage = await InitializeTestPoetryStorage();
            var poetries = await poetryStorage.GetPoetryAsync(
                Expression.Lambda<Func<Poetry, bool>>(
                    Expression.Constant(true),
                    Expression.Parameter(typeof(Poetry), "p")), 0, int.MaxValue);
            Assert.Equal(30, poetries.Count());
            await poetryStorage.CloseAsync();
        }


        public static async Task<PoetryStorage> InitializeTestPoetryStorage()
        {
            var preferenceStorageMock = new Mock<IPreferenceStorage>();
            var mockPreferenceStorage = preferenceStorageMock.Object;
            var poetryStorage = new PoetryStorage(mockPreferenceStorage);
            await poetryStorage.InitializeAsync();
            return poetryStorage;
        }
    }
}
}
