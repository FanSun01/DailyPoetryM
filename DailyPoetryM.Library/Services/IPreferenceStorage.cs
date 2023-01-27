using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DailyPoetryM.Services
{
    public interface IPreferenceStorage
    {
        void Set(string key, int value);

        int Get(string key, int defaultValue);
    }
}
