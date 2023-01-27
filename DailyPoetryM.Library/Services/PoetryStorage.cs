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

        public const string DbName = "poetrydb.sqllite3";

        public static readonly string PoetryDbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), DbName);

        public PoetryStorage(IPreferenceStorage preferenceStorage)
        {
            _preferenceStorage = preferenceStorage;
        }

        public async Task InitializeAsync()
        {
            await using var dbFileStream = new FileStream(PoetryDbPath, FileMode.OpenOrCreate);
            await using var dbAssetStream = typeof(PoetryStorage).Assembly.GetManifestResourceStream(DbName);
            await dbAssetStream.CopyToAsync(dbFileStream);
            _preferenceStorage.Set(PoetryStorageConst.VersionKey, PoetryStorageConst.Version);
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