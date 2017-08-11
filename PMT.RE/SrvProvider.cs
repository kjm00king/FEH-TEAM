using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PMT.RE
{    
    public class DictTmp : Dictionary<string, string>
    {
        public DictTmp()
        {

        }

        public DictTmp(params object[] its)
        {
            for (int i = 0; i < its.Length / 2; i++)
            {
                if (its.Length > 2 * i + 1)
                {
                    string k = its[i * 2].ToString();
                    string v = its[i * 2 + 1] == null ? string.Empty : its[i * 2 + 1].ToString();

                    if (!Keys.Contains(k))
                        Add(k, v);
                }
            }
        }

        public DictTmp(string content)
        {
            try
            {
                Dictionary<string,string> tmp = CML.ComUtility.ToDict(content);
                Clone(tmp);
            }
            catch
            {

            }
        }

        public void Clone(Dictionary<string, string> it)
        {
            foreach (string k in it.Keys)
                this[k] = it[k];
        }

        public int GetAsInt(string k)
        {
            return GetAsInt(k, 0);
        }

        public int GetAsInt(string k, int @default)
        {
            int rv = @default;
            if (Keys.Contains(k) && int.TryParse(this[k], out rv))
                return rv;
            else
                return @default;
        }

        public float GetAsFloat(string k)
        {
            return GetAsFloat(k, 0);
        }

        public float GetAsFloat(string k, float @default)
        {
            float rv = @default;
            if (Keys.Contains(k) && float.TryParse(this[k], out rv))
                return rv;
            else
                return @default;
        }
        
        public bool GetAsBool(string k)
        {
            return GetAsBool(k, false);
        }

        public bool GetAsBool(string k, bool @default)
        {
            bool rv = @default;
            if (Keys.Contains(k) && bool.TryParse(this[k], out rv))
                return rv;
            else
                return @default;
        }

        public string Get(string k, string @default)
        {
            if (Keys.Contains(k))
                return this[k];
            else
                return @default;
        }
        
        public string Get(string k)
        {
            return Get(k, string.Empty);
        }

        public override string ToString()
        {
            return CML.ComUtility.ToJson(this);
        }
    }

    public class ListTmp : List<string>
    {
        public ListTmp()
        {

        }

        public ListTmp(IList its)
        {
            foreach (object it in its)
                Add(it == null ? string.Empty : it.ToString());
        }

        public ListTmp(params object[] its)
        {
            foreach (object it in its)
                Add(it == null ? string.Empty : it.ToString());
        }

        public ListTmp(List<IEntity> its)
        {
            foreach (IEntity it in its)
                Add(it.ToString());
        }

        public ListTmp(string content)
        {
            try
            {
                List<string> tmp = CML.ComUtility.FromJson<List<string>>(content);
                AddRange(tmp);
            }
            catch
            {

            }
        }

        public int GetAsInt(int k)
        {
            return GetAsInt(k, 0);
        }

        public int GetAsInt(int k, int @default)
        {
            int rv = @default;            
            if (Count > k && int.TryParse(this[k], out rv))
                return rv;
            else
                return @default;
        }

        public float GetAsFloat(int k)
        {
            return GetAsFloat(k, 0);
        }

        public float GetAsFloat(int k, float @default)
        {
            float rv = @default;
            if (Count > k && float.TryParse(this[k], out rv))
                return rv;
            else
                return @default;
        }

        public DateTime GetAsDateTime(int k)
        {
            return GetAsDateTime(k, DateTime.MinValue);
        }

        public DateTime GetAsDateTime(int k, DateTime @default)
        {
            DateTime rv = @default;
            if (Count > k && DateTime.TryParse(this[k], out rv))
                return rv;
            else
                return @default;
        }

        public bool GetAsBool(int k)
        {
            return GetAsBool(k, false);
        }

        public bool GetAsBool(int k, bool @default)
        {
            bool rv = @default;
            if (Count > k && bool.TryParse(this[k], out rv))
                return rv;
            else
                return @default;
        }

        public T GetAsEntity<T>(int k)
        {
            try
            {
                T tmp = Activator.CreateInstance<T>();

                if (Count > k)
                {
                    IEntity entity = tmp as IEntity;
                    if (entity != null)
                        entity.FromJson(this[k]);
                }

                return tmp;
            }
            catch
            {
                return default(T);
            }
        }

        public Guid GetAsGuid(int k)
        {
            return GetAsGuid(k, Guid.Empty);
        }

        public Guid GetAsGuid(int k, Guid @default)
        {
            Guid rv = @default;
            if (Count > k && Guid.TryParse(this[k], out rv))
                return rv;
            else
                return @default;
        }

        public string Get(int k, string @default)
        {
            if (Count > k)
                return this[k];
            else
                return @default;
        }

        public string Get(int k)
        {
            return Get(k, string.Empty);
        }

        public override string ToString()
        {
            return CML.ComUtility.ToJson(this);
        }        
    }

    public class IntArray : List<int>, IEntity
    {
        public void FromJson(string content)
        {
            ListTmp tmp = new ListTmp(content);
            Clear();
            for(int i = 0; i < tmp.Count; i++)
            {
                Add(tmp.GetAsInt(i));
            }
        }

        public override string ToString()
        {
            return new ListTmp(this).ToString();
        }
    }

    public class SrvProvider : ISrvProvider, IZip
    {
        protected static string GetAsmKey(Assembly obj)
        {
            GuidAttribute uuid_attr = obj.GetCustomAttribute<GuidAttribute>();
            if (uuid_attr != null)
            {
                return uuid_attr.Value;
            }
            else
                return string.Empty;
        }
        
        //public bool IsDebug { get { return false; } }
        public string StoreFile { get { return "IoC.bin"; } }

        private string _store_dir = string.Empty;
        public string StoreDir {
            get { return _store_dir.TrimEnd('/').TrimEnd('\\') + "/"; }
            set { _store_dir = value; } }

        protected class Configuration : IEntity
        {
            public string Namespace { get; set; } = string.Empty;
            public bool Debug { get; set; } = true;
            public int AssemblyCount { get; set; } = 0;
            public int ModelCount { get; set; } = 0;

            public void FromJson(string content)
            {
                DictTmp tmp = new DictTmp(content);
                Namespace = tmp.Get("Namespace");
                Debug = tmp.GetAsBool("IsDebug", true);
                AssemblyCount = tmp.GetAsInt("AssemblyCount", 0);
                ModelCount = tmp.GetAsInt("ModelCount", 0);
            }

            public override string ToString()
            {
                return new DictTmp(
                    "Namespace",Namespace
                    , "IsDebug", Debug
                    , "AssemblyCount", AssemblyCount
                    , "ModelCount", ModelCount).ToString();
            }
        }

        protected class AsmObj
        {
            public byte[] buff { get; set; }
            public string key { get; set; }
            public string name { get; set; }
            public string fullname { get; set; }
            public long length { get; set; }
            public string sha1 { get; set; }
            //public string base64 { get; set; }
            public Assembly obj { get; set; }
            public string version { get; set; }
            public bool isEmpty { get { return _is_empty; } }

            private bool _is_empty = true;
            private bool _load_fail = false;

            public AsmObj()
            {
                loadNA();
            }

            //public AsmObj(string content)
            //{
            //    load(content);
            //}
            public AsmObj(byte[] buff)
            {
                load(buff);
            }

            private void loadUid()
            {
                key = string.Empty;
                try
                {
                    if (obj != null)
                    {
                        key = SrvProvider.GetAsmKey(obj);
                    }
                }
                catch
                {
                    _load_fail = true;
                }
            }
            private void loadVersion()
            {
                version = "0.0.0.0";
                try
                {
                    if (obj != null)
                    {
                        AssemblyVersionAttribute version_attr = obj.GetCustomAttribute<AssemblyVersionAttribute>();
                        if (version_attr != null)
                        {
                            version = version_attr.Version;
                        }
                    }
                }
                catch
                {
                    _load_fail = true;
                }
            }
            private void loadObj(byte[] buff)
            {
                obj = null;
                try
                {
                    if (buff == null)
                    {
                        obj = null;
                        name = string.Empty;
                        fullname = string.Empty;
                    }
                    else
                    {
                        //obj = AppDomain.CurrentDomain.Load(buff);
                        obj = Assembly.Load(buff);
                        name = obj.GetName().Name;
                        fullname = obj.FullName;
                    }
                }
                catch (Exception)
                {
                    _load_fail = true;
                }
            }
            private void loadObj(Assembly asm)
            {
                try
                {
                    if (asm == null)
                    {
                        obj = null;
                        name = string.Empty;
                        fullname = string.Empty;
                    }
                    else
                    {
                        obj = asm;
                        name = obj.GetName().Name;
                        fullname = obj.FullName;
                    }
                }
                catch (Exception)
                {
                    _load_fail = true;
                }
            }

            //private void load(string content)
            //{
            //    _load_fail = false;

            //    byte[] buff = Convert.FromBase64String(content);

            //    length = buff.Length;
            //    sha1 = getSHA1(buff);
            //    base64 = content;

            //    loadObj(buff);
            //    loadUid();
            //    loadVersion();

            //    _is_empty = _load_fail;
            //}

            public string getSHA1(byte[] buff)
            {
                if (buff == null)
                    return string.Empty;
                else
                {
                    string rv = string.Empty;
                    SHA1 algorithm = SHA1.Create();
                    byte[] data = algorithm.ComputeHash(buff);
                    for (int i = 0; i < data.Length; i++)
                    {
                        rv += data[i].ToString("x2").ToLowerInvariant();
                    }
                    return rv;
                }
            }

            public void loadNA()
            {
                length = 0;
                sha1 = string.Empty;
                buff = null;
            }

            public void load(byte[] _buff)
            {
                _load_fail = false;

                if (_buff == null)
                {
                    loadNA();
                }
                else
                {
                    length = _buff.Length;
                    sha1 = getSHA1(buff);
                    buff = _buff;
                    //base64 = Convert.ToBase64String(buff);

                    loadObj(buff);
                    loadUid();
                    loadVersion();

                    _is_empty = _load_fail;
                }
            }

            public void load(Assembly asm)
            {
                _load_fail = false;
                if (asm == null)
                {
                    loadNA();
                }
                else
                {
                    length = 0;
                    sha1 = string.Empty;
                    buff = null;
                    //base64 = string.Empty;

                    loadObj(asm);
                    loadUid();
                    loadVersion();
                }

                _is_empty = _load_fail;
            }
        }

        protected class TypeObj
        {
            public string asm_name { get; set; }
            public string cls_name { get; set; }

            public TypeObj()
            {
                asm_name = string.Empty;
                cls_name = string.Empty;
            }
        }

        protected class ModelObj : IEntity
        {
            private SrvProvider _owner = null;

            //public bool local { get; set; }
            public string asm { get; set; }
            //public string asmkey { get; set; }
            public string cls { get; set; }
            public string key { get; set; }
            public IModel model { get; set; }
            
            public ModelObj(SrvProvider provider)
            {
                _owner = provider;                
                //local = false;
                cls = string.Empty;
                asm = string.Empty;
                //asmkey = string.Empty;
                key = string.Empty;
                model = null;
            }

            public void FromJson(string content)
            {
                if (!_owner.IsDebug)
                    content = CML.ComUtility.GetString(content);

                DictTmp tmp = new DictTmp(content);
                cls = tmp.Get("type");
                key = tmp.Get("key");
                asm = tmp.Get("asm");

                model = _owner.GetService(asm, cls) as IModel;
                model.owner = _owner;
                model.FromJson(tmp.Get("model"));

                //local = tmp.GetAsBool("local");
                //asmkey = tmp.Get("asmid");

                //if (!local && _owner != null)
                //{
                //    model = _owner.GetModel(typekey);
                //    if (model != null)
                //    {
                //        model.FromString(tmp.Get("model"));
                //        //model.FromString(CML.ComUtility.GetString(tmp.Get("model")));
                //    }
                //}
                //else if (local)
                //{
                //    //Assembly asm = _owner.FindAsm(asmname);

                //    //if (asm != null)
                //    //{
                //    //    Type t = asm.GetType(typekey);
                //    //    if (t != null)
                //    //    {
                //    //        model = Activator.CreateInstance(t) as IModel;
                //    //        if (model != null)
                //    //        {
                //    //            model.FromString(tmp.Get("model"));
                //    //            //model.FromString(CML.ComUtility.GetString(tmp.Get("model")));
                //    //        }
                //    //    }
                //    //}
                //}

            }

            public override string ToString()
            {                
                string content = new DictTmp(
                         "type", cls
                        , "key", key
                        , "model", model
                        //"model", CML.ComUtility.GetBase64(model),
                        //"local", local,
                        , "asm", asm
                        //"asmid", asmkey
                        ).ToString();

                if (_owner.IsDebug)
                    return content;
                else
                    return CML.ComUtility.GetBase64(content);
            }

            public object Exec(string key, params object[] arguments)
            {
                return null;
            }
        }
        
        protected class AsmContainer : List<AsmObj>
        {
            private SrvProvider _owner = null;

            public AsmContainer(SrvProvider provider)
            {
                _owner = provider;
            }

            public AsmObj Merge(byte[] buff)
            {
                AsmObj it = new AsmObj();
                it.load(buff);
                Merge(it);
                return it;
            }

            public AsmObj Merge(Assembly asm)
            {
                AsmObj it = new AsmObj();
                it.load(asm);
                Merge(it);
                return it;
            }

            public void Merge(AsmObj it)
            {
                IEnumerable<AsmObj> exist = (from x in this where x.key == it.key select x);

                if (!exist.Any())
                {
                    Add(it);
                }
                else
                {
                    int i = IndexOf(exist.First());

                    AsmObj old = this[i];
                    
                    this[i] = it;
                }
            }

            //public void FromJson(string content)
            //{
            //    Clear();

            //    if (!_owner.IsDebug)
            //    {
            //        content = CML.ComUtility.FromBase64Gzip(content);
            //    }

            //    ListTmp tmp = new ListTmp(content);
            //    foreach (string base64 in tmp)
            //    {
            //        AsmObj it = new AsmObj(base64);
            //        Merge(it);
            //    }
            //}

            //public override string ToString()
            //{
            //    List<string> tmp = new List<string>();
            //    foreach (AsmObj it in this)
            //    {
            //        tmp.Add(it.base64);
            //    }

            //    string content = CML.ComUtility.ToJson(tmp);

            //    if (_owner.IsDebug)
            //        return content;
            //    else
            //        return CML.ComUtility.ToBase64Gzip(content);
            //}

            public Assembly Get(string fullname)
            {
                Assembly rv = CML.ComUtility.QueryFirst<Assembly>(
                    from x in this where x.fullname == fullname select x.obj, null);
                return rv;
            }

            //public byte[] Zip()
            //{
            //    using (MemoryStream ms = new MemoryStream())
            //    {
            //        ZipArchive zip = new ZipArchive(ms, ZipArchiveMode.Create, true, Encoding.UTF8);

            //        for (int i = 0; i < Count; i++)
            //        {
            //            ZipArchiveEntry it = zip.CreateEntry(i.ToString());
            //            using (Stream itms = it.Open())
            //            {
            //                itms.Write(this[i].buff, 0, this[i].buff.Length);
            //            }
            //        }

            //        return ms.ToArray();
            //    }
            //}

            //public void FromZip(byte[] buff)
            //{
            //    Clear();

            //    ZipArchive zip = new ZipArchive(new MemoryStream(buff), ZipArchiveMode.Read);

            //    foreach (ZipArchiveEntry it in zip.Entries)
            //    {
            //        using (MemoryStream ms = it.Open() as MemoryStream)
            //        {
            //            AsmObj child = new AsmObj();
            //            child.load(ms.ToArray());
            //            Add(child);
            //        }
            //    }
            //}
        }

        protected class TypeContainer : List<TypeObj>
        {
            public TypeContainer()
            {
            }

            public void Merge(AsmObj it)
            {
                foreach (Type t in it.obj.GetTypes())
                {
                    Add(new TypeObj()
                    {
                        asm_name = it.name,
                        cls_name = t.FullName,
                    });
                }
            }
        }

        protected class ModelContainer : List<ModelObj>
        {
            private SrvProvider _owner = null;

            public ModelContainer(SrvProvider provider)
            {
                _owner = provider;
            }

            //public void FromJson(string content)
            //{
            //    Clear();

            //    if (!_owner.IsDebug)
            //    {
            //        content = CML.ComUtility.FromBase64Gzip(content);
            //    }

            //    ListTmp tmp = new ListTmp(content);
            //    foreach (string base64 in tmp)
            //    {
            //        ModelObj it = new ModelObj(_owner);
            //        it.FromJson(base64);
            //        Add(it);
            //    }
            //}

            //public override string ToString()
            //{
            //    List<string> tmp = new List<string>();
            //    foreach (ModelObj it in this)
            //    {
            //        tmp.Add(it.ToString());
            //    }

            //    string content = CML.ComUtility.ToJson(tmp);

            //    if (_owner.IsDebug)
            //        return content;
            //    else
            //        return CML.ComUtility.ToBase64Gzip(content);
            //}

            //public byte[] Zip()
            //{
            //    using (MemoryStream ms = new MemoryStream())
            //    {
            //        using (ZipArchive zip = new ZipArchive(ms, ZipArchiveMode.Create, true, Encoding.UTF8))
            //        {

            //            for (int i = 0; i < Count; i++)
            //            {
            //                ZipArchiveEntry it = zip.CreateEntry(i.ToString());
                            
            //                using (Stream s = it.Open())
            //                using (StreamWriter sw = new StreamWriter(s, Encoding.UTF8))
            //                {
            //                    sw.Write(this[i].ToString());
            //                }
            //            }

            //            return ms.ToArray();
            //        }
            //    }
            //}

            //public void FromZip(byte[] buff)
            //{
            //    Clear();

            //    ZipArchive zip = new ZipArchive(new MemoryStream(buff), ZipArchiveMode.Read);
                
            //    foreach(ZipArchiveEntry it in zip.Entries)
            //    {
            //        using (MemoryStream ms = it.Open() as MemoryStream)
            //        {
            //            ModelObj child = new ModelObj(_owner);
            //            child.FromJson(CML.ComUtility.GetString(ms.ToArray()));
            //            Add(child);
            //        }
            //    }
            //}

            public object Exec(string name, params object[] arg)
            {
                return null;
            }            
        }

        protected AsmContainer AsmMap;
        protected TypeContainer TypeMap;
        protected ModelContainer ModelMap;
        protected Configuration Config = new Configuration();

        //private Dictionary<string, Assembly> asm_find_cache = new Dictionary<string, Assembly>();
        //public Assembly FindAsm(string name)
        //{
        //    try
        //    {
        //        if (!asm_find_cache.Keys.Contains(name))
        //        {
        //            asm_find_cache.Add(name, Assembly.Load(name));
        //        }

        //        return asm_find_cache[name];
        //    }
        //    catch
        //    {
        //        asm_find_cache.Add(name, null);
        //        return null;
        //    }
        //}

        public SrvProvider()
        {
            AsmMap = new AsmContainer(this);
            TypeMap = new TypeContainer();
            ModelMap = new ModelContainer(this);

            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(GetAssembly);
        }
        
        private Assembly GetAssembly(object sender, ResolveEventArgs args)
        {
            return AsmMap.Get( args.Name );
        }

        public Assembly GetAsm(Assembly arg)
        {
            return AsmMap.Get(GetAsmKey(arg));
        }

        public object GetService(Type t)
        {
            return Activator.CreateInstance(t);
        }

        public object GetService(string asm_name, string cls_name)
        {
            IEnumerable<AsmObj> find = (from x in AsmMap where x.name == asm_name select x);

            if (find.Any())
            {
                Type t = find.First().obj.GetType(cls_name);
                if (t != null)
                {
                    return GetService(t);
                }
            }
            else
            {
                Assembly asm = Assembly.Load(asm_name);
                if (asm != null)
                {
                    Type t = asm.GetType(cls_name);
                    if (t != null)
                    {
                        return GetService(t);
                    }
                }
            }
            return null;
        }

        public object GetService(string cls_name)
        {
            IEnumerable<TypeObj> find = (from x in TypeMap where x.cls_name == cls_name select x);

            if (find.Any())
            {
                return GetService(find.First().asm_name, cls_name);
            }

            return null;
        }

        public IModel GetModel(Type t)
        {
            IModel tmp = GetService(t) as IModel;
            if (tmp != null)
            {
                tmp.owner = this;
            }
            return tmp;
        }

        public T GetModel<T>(string key)
        {
            return (T)GetModel(typeof(T), key);
        }

        public T GetModel<T>(object key)
        {
            if (key != null)
            {
                return GetModel<T>(key.ToString());
            }
            else {
                return GetDefaultModel<T>();
            }
        }

        public T GetDefaultModel<T>()
        {
            return GetModel<T>("DEFAULT");
        }

        public void SetDefaultModel(IModel replace)
        {
            SetModel("DEFAULT", replace);
        }

        public IModel GetModel(Type t, string key)
        {
            if (key != null)
            {
                ModelObj it = CML.ComUtility.QueryFirst<ModelObj>(
                    from x in ModelMap where x.key == key where x.cls == t.FullName select x, null);

                if (it == null)
                {
                    ModelObj tmp = new ModelObj(this)
                    {
                        key = key,
                        asm = t.Assembly.GetName().Name,

                        cls = t.FullName,
                        model = GetModel(t),
                        //local = true,
                    };
                    ModelMap.Add(tmp);
                    return tmp.model;
                }
                else
                {
                    if (it.model == null)
                    {
                        it.asm = t.Assembly.GetName().Name;
                        it.model = GetModel(t);
                    }

                    return it.model;
                }
            }
            else
            {
                return GetModel(t);
            }
        }

        public void SetModel(object key, IModel obj)
        {
            if (key != null) { SetModel(key.ToString(), obj); }
        }

        public void SetModel(string key, IModel obj)
        {
            if (key != null && obj !=null)
            {
                Type t = obj.GetType();

                ModelObj it = CML.ComUtility.QueryFirst<ModelObj>(
                    from x in ModelMap where x.key == key where x.cls == t.FullName select x, null);

                if (it == null)
                {
                    ModelObj tmp = new ModelObj(this)
                    {
                        key = key,
                        asm = t.Assembly.GetName().Name,

                        cls = t.FullName,
                        model = obj,
                        //local = true,
                    };

                    if (tmp.model == null)
                    {
                        tmp.model = GetModel(t);
                    }

                    ModelMap.Add(tmp);
                }
                else
                {
                    it.model = obj;

                    if (it.model == null)
                    {
                        it.asm = t.Assembly.GetName().Name;
                        it.model = GetModel(t);
                    }
                }
            }
        }

        public IModel GetModel(string cls)
        {
            IModel tmp = GetService(cls) as IModel;
            if (tmp != null)
            {
                tmp.owner = this;
            }
            return tmp;
        }

        public IModel GetModel(string cls, string key)
        {
            if (key != null)
            {
                IEnumerable<ModelObj> it = (from x in ModelMap where x.key == key where x.cls == cls select x);
                if (it.Any())
                {
                    return it.First().model;
                }
                else
                {
                    IEnumerable<TypeObj> find = (from x in TypeMap where x.cls_name == cls select x);

                    if (find.Any())
                    {
                        ModelObj tmp = new ModelObj(this)
                        {
                            asm = find.First().asm_name,
                            key = key,
                            cls = cls,
                            model = GetService(find.First().asm_name, cls) as IModel,                            
                            //local = false,
                        };
                        tmp.model.owner = this;

                        ModelMap.Add(tmp);
                        return tmp.model;
                    }
                    else
                        return null;
                }
            }
            else
            {
                return GetModel(cls);
            }
        }

        //private void LoadFromByte(byte[] buff)
        //{
        //    //FromString(
        //    //    CML.ComUtility.GetString(
        //    //        CML.ComUtility.Decompress(buff)
        //    //        )
        //    //    );
        //    FromJson(UTF8Encoding.UTF8.GetString(buff));
        //}

        public void RegisterAsm(byte[] buff)
        {
            AsmObj it = AsmMap.Merge(buff);
            TypeMap.Merge(it);
        }

        public void RegisterAsm(Assembly asm)
        {
            AsmObj it = AsmMap.Merge(asm);
            TypeMap.Merge(it);
        }

        public void Restart()
        {

            byte[] buff = Zip();

            FromZip(buff);

            //string content = ToString();

            //FromJson(content);
        }

        public void Save()
        {
            string tmp_path = StoreDir + Guid.NewGuid().ToString().Replace("-", "");
            string store_path = StoreDir + StoreFile;

            CML.ComFile.New(tmp_path, Zip());
            File.Copy(tmp_path, store_path, true);
            File.Delete(tmp_path);
        }

        public void Load()
        {
            using (FileStream fs = new FileStream(StoreDir + StoreFile, FileMode.Open))
            {
                byte[] buff = new byte[fs.Length];
                fs.Read(buff, 0, buff.Length);

                FromZip(buff);
            }

            //FromJson(CML.ComFile.Read(StoreDir + StoreFile));
        }

        //public void Load(byte[] buff)
        //{
        //    using (MemoryStream ms = new MemoryStream())
        //    {
        //        ZipArchive zip = new ZipArchive(ms, ZipArchiveMode.Create, true, Encoding.UTF8);

        //        ZipArchiveEntry iConfig = zip.CreateEntry("config.json");
        //        using(Stream sm = iConfig.Open())
        //        {
        //            CML.ComFile.Write(sm, Config.ToString());
        //        }

        //        ZipArchiveEntry iModels = zip.CreateEntry("models");
        //        using (Stream sm = iModels.Open())
        //        {
        //            byte[] tmpbuff = ModelMap.Zip();
        //            sm.Write(tmpbuff, 0, tmpbuff.Length);
        //        }
        //    }
        //}

        public void SaveBuff()
        {
            MemoryStream ms = new MemoryStream();
            ms.WriteByte(0);
        }

        //public void FromJson(string content)
        //{
        //    DictTmp tmp = new DictTmp(content);

        //    Config.FromJson(tmp.Get("config"));

        //    AsmMap = new AsmContainer(this);
        //    AsmMap.FromJson(tmp.Get("asm"));

        //    TypeMap = new TypeContainer();
        //    foreach (AsmObj it in AsmMap)
        //    {
        //        TypeMap.Merge(it);
        //    }

        //    ModelMap = new ModelContainer(this);
        //    //ModelMap.FromJson(tmp.Get("models"));
        //}

        //public override string ToString()
        //{
        //    return new DictTmp(
        //        "config", Config,
        //        "models", ModelMap,
        //        "asm", AsmMap).ToString();
        //}

        public byte[] Zip()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (ZipArchive zip = new ZipArchive(ms, ZipArchiveMode.Create, true, Encoding.UTF8))
                {
                    ZipArchiveEntry iConfig = zip.CreateEntry("config.json", CompressionLevel.Optimal);

                    using (Stream s = iConfig.Open())
                    using (StreamWriter sw = new StreamWriter(s, Encoding.UTF8))
                    {
                        Config.AssemblyCount = AsmMap.Count;
                        Config.ModelCount = ModelMap.Count;
                        sw.Write(Config.ToString());
                    }

                    for (int i = 0; i < ModelMap.Count; i++)
                    {
                        ZipArchiveEntry entry = zip.CreateEntry("models/" + i.ToString() + ".json", CompressionLevel.Optimal);

                        using (Stream s = entry.Open())
                        using (StreamWriter sw = new StreamWriter(s, Encoding.UTF8))
                        {
                            sw.Write(ModelMap[i].ToString());
                        }
                    }

                    for (int i = 0; i < AsmMap.Count; i++)
                    {
                        ZipArchiveEntry entry = zip.CreateEntry("assemblies/" + i.ToString() + ".dll", CompressionLevel.Optimal);

                        using (Stream s = entry.Open())
                        using (BinaryWriter bw = new BinaryWriter(s, Encoding.UTF8))
                        {
                            bw.Write(AsmMap[i].buff);
                        }
                    }
                }

                return ms.ToArray();
            }
        }

        public void FromZip(byte[] buff)
        {
            using (ZipArchive zip = new ZipArchive(new MemoryStream(buff)))
            {
                ZipArchiveEntry iConfig = zip.GetEntry("config.json");
                if (iConfig != null)
                {
                    using (Stream s = iConfig.Open())
                    using (StreamReader sr = new StreamReader(s, Encoding.UTF8))
                    {
                        Config = new Configuration();
                        Config.FromJson(sr.ReadToEnd());
                    }
                }

                AsmMap = new AsmContainer(this);
                for (int i = 0; i < Config.AssemblyCount; i++)
                {
                    ZipArchiveEntry it = zip.GetEntry("assemblies/" + i.ToString() + ".dll");
                    if (it != null)
                    {
                        using (Stream s = it.Open())
                        {
                            byte[] buffer = new byte[it.Length];
                            s.Read(buffer, 0, buffer.Length);

                            AsmObj add = new AsmObj();
                            add.load(buffer);
                            AsmMap.Add(add);
                        }
                    }
                }
                
                TypeMap = new TypeContainer();
                foreach (AsmObj it in AsmMap)
                {
                    TypeMap.Merge(it);
                }

                ModelMap = new ModelContainer(this);
                for (int i = 0; i < Config.ModelCount; i++)
                {
                    ZipArchiveEntry it = zip.GetEntry("models/" + i.ToString() + ".json");
                    if (it != null)
                    {
                        using (Stream s = it.Open())
                        using (StreamReader sr = new StreamReader(s, Encoding.UTF8))
                        {
                            ModelObj add = new ModelObj(this);
                            add.FromJson(sr.ReadToEnd());
                            ModelMap.Add(add);
                        }
                    }
                }
            }

            //ZipArchiveEntry iModels = zip.GetEntry("models");
            //if (iModels != null)
            //{
            //    using (MemoryStream ms = iModels.Open() as MemoryStream)
            //    {
            //        ModelMap = new ModelContainer(this);
            //        ModelMap.FromZip(ms.ToArray());
            //    }
            //}

            //ZipArchiveEntry iAssemblies = zip.GetEntry("assemblies");
            //if (iAssemblies != null)
            //{
            //    using (MemoryStream ms = iAssemblies.Open() as MemoryStream)
            //    {
            //        AsmMap = new AsmContainer(this);
            //        AsmMap.FromZip(ms.ToArray());
            //    }
            //}
        }

        public byte[] ToBytes()
        {
            return UTF8Encoding.UTF8.GetBytes(ToString());
        }

        public bool IsDebug
        {
            get { return Config.Debug; }
            set { Config.Debug = value; }
        }

        public string Namespace
        {
            get
            {
                return Config.Namespace;
            }
            set
            {
                Config.Namespace = value;
            }
        }
    }
}
