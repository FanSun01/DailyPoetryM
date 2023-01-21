using DailyPoetryM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DailyPoetryM.Services
{
    public class PoetryStorage : IPoetryStorage
    {

        private readonly IPreferenceStorage _preferenceStorage;
        public bool IsInitialized => _preferenceStorage.Get(PoetryStorageConst.VersionKey, 0) == PoetryStorageConst.Version;

        public PoetryStorage(IPreferenceStorage preferenceStorage)
        {
            _preferenceStorage = preferenceStorage;
        }

        public Task InitializeAsync()
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(Poetry poetry)
        {
            throw new NotImplementedException();
        }

        public Task<Poetry> GetPoetryAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Poetry>> GetPoetryAsync(Expression<Func<Poetry, bool>> where, int skip, int take)
        {
            throw new NotImplementedException();
        }
    }

    public static class PoetryStorageConst
    {
        public const int Version = 1;
        public const string VersionKey = nameof(PoetryStorageConst) + "." + nameof(Version);


    }
}