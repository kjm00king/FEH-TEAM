using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PMT.RE
{
    public interface ISrvProvider : IServiceProvider
    {
        string StoreFile { get; }
        string StoreDir { get; set; }

        T GetDefaultModel<T>();
        T GetModel<T>(string key);
        T GetModel<T>(object key);

        void SetDefaultModel(IModel replace);
        void SetModel(string key, IModel replace);
        void SetModel(object key, IModel replace);

        IModel GetModel(string cls);
        IModel GetModel(string cls, string key);

        void Save();
        void Load();

        bool IsDebug { get; set; }
    }
}
