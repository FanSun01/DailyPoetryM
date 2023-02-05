using DailyPoetryM.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection.PortableExecutable;
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

        private SQLiteAsyncConnection _connection;
        public SQLiteAsyncConnection Connection => _connection ??= new SQLiteAsyncConnection(PoetryDbPath);

        public PoetryStorage(IPreferenceStorage preferenceStorage)
        {
            _preferenceStorage = preferenceStorage;
        }

        public async Task InitializeAsync()
        {
            await using var dbFileStream = new FileStream(PoetryDbPath, FileMode.OpenOrCreate);
            await using var dbAssetStream = typeof(PoetryStorage).Assembly.GetManifestResourceStream(DbName);
            if (dbFileStream is null || dbAssetStream is null)
            {
                throw new NullReferenceException("dbFileStream or dbAssetStream is null");
            }
            await dbAssetStream.CopyToAsync(dbFileStream);
            _preferenceStorage.Set(PoetryStorageConst.VersionKey, PoetryStorageConst.Version);
        }

        public async Task AddAsync(Poetry poetry)
        {
            await Connection.InsertAsync(poetry);
        }

        public async Task<Poetry> GetPoetryAsync(int id)
        {
            return await Connection.Table<Poetry>().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Poetry>> GetPoetryAsync(Expression<Func<Poetry, bool>> where, int skip, int take)
        {
            return await Connection.Table<Poetry>().Where(where).Skip(skip).Take(take).ToListAsync();
        }

        public async Task CloseAsync() => await Connection.CloseAsync();

    }

    public static class PoetryStorageConst
    {
        public const int Version = 1;
        public const string VersionKey = nameof(PoetryStorageConst) + "." + nameof(Version);
    }
}