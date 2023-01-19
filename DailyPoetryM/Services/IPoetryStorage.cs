using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using DailyPoetryM.Models;

namespace DailyPoetryM.Services
{
    public interface IPoetryStorage
    {
        Task InitializeAsync();
        Task AddAsync(Poetry poetry);
        Task<Poetry> GetPoetryAsync(int id);
        Task<IEnumerable<Poetry>> GetPoetryAsync(Expression<Func<Poetry, bool>> where, int skip, int take);

    }
}
