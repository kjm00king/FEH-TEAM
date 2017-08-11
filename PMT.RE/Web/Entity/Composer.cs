using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace PMT.RE.Web.Entity
{
    public class Composer
    {
        public Units db { get; set; }

        public Composer()
        {
            db = new Units();
        }

        public T deserialize<T>()
        {
            return deserialize<T>(string.Empty);
        }
        public T deserialize<T>(string root_key)
        {
            Unit root = CML.ComUtility.QueryFirst<Unit>(
                            from x in db
                            where x.key == root_key
                            select x, null);

            if (root == null)
            {
                root = new Unit()
                {
                    key = root_key
                };
            }

            T rv = (T)Activator.CreateInstance(typeof(T));

            writeField(root, rv);

            return rv;
        }
        public T deserialize<T>(Units _data)
        {
            db = _data;

            return deserialize<T>(string.Empty);
        }
        public T deserialize<T>(string root_key, Units _data)
        {
            db = _data;

            return deserialize<T>(root_key);
        }
        public void writeField(Unit unit, Object obj)
        {
            FieldInfo[] fields = obj.GetType().GetFields();

            foreach (FieldInfo field in fields)
            {
                BaseField field_attr = field.GetCustomAttribute<BaseField>();
                if (field_attr != null)
                {
                    Object value = field.GetValue(obj);
                    if (value == null)
                    {
                        value = Activator.CreateInstance(field.FieldType);
                        field.SetValue(obj, value);
                    }

                    Unit uProp = CML.ComUtility.QueryFirst<Unit>(
                        from x in db
                        where x.parent == unit.key
                        where x.name == field.Name
                        select x, null);

                    if (field_attr.type == UnitType.Field)
                    {
                        if (uProp != null)
                        {
                            PMT.RE.IEntity entity = value as PMT.RE.IEntity;
                            if (entity != null)
                            {
                                entity.FromJson(uProp.content);
                            }
                        }
                    }
                    else if (field_attr.type == UnitType.Container)
                    {
                        if (uProp != null)
                        {
                            writeChildren(uProp, value);

                            writeField(uProp, value);
                        }
                    }
                }
            }
        }
        private void writeChildren(Unit unit, Object obj)
        {
            IList children = (obj as IList);

            if (children != null)
            {
                IEnumerable<Unit> uChildren = (from x in db
                                               where x.parent == unit.key
                                               where x.isArrayElement
                                               select x);
                if (uChildren.Any())
                {
                    Type tChild = getGenType(obj.GetType());

                    foreach (Unit uChild in uChildren)
                    {
                        object child = Activator.CreateInstance(tChild);

                        children.Add(child);

                        writeChildren(uChild, child);

                        writeField(uChild, child);
                    }
                }
            }
        }

        public Units serialization(Object obj)
        {
            return serialization(string.Empty, obj);
        }
        public Units serialization(string parent, Object obj)
        {
            db.Clear();

            readField(parent, obj);

            return db;
        }
        private void readField(string key, Object obj)
        {
            FieldInfo[] fields = obj.GetType().GetFields();

            foreach (FieldInfo field in fields)
            {
                BaseField field_attr = field.GetCustomAttribute<BaseField>();
                if (field_attr != null)
                {
                    Object value = field.GetValue(obj);

                    if (value == null)
                    {
                        value = Activator.CreateInstance(field.FieldType);
                    }

                    Unit add = new Unit(field_attr)
                    {
                        key = getKey(key, field.Name),
                        parent = key,
                        name = field.Name,
                    };

                    if (field_attr.type == UnitType.Field)
                    {
                        add.type = UnitType.Field;
                        add.content = value.ToString();
                        db.Add(add);
                    }
                    else if (field_attr.type == UnitType.Container)
                    {
                        add.type = UnitType.Container;

                        IList children = (value as IList);

                        if (children != null)
                        {
                            add.isArrayContainer = true;

                            for (int i = 0; i < children.Count; i++)
                            {
                                readChild(add.key, i, children[i]);
                            }
                        }

                        readField(add.key, value);

                        db.Add(add);
                    }
                }
            }
        }
        private void readChild(string key, int no, Object value)
        {
            Unit add = new Unit()
            {
                type = UnitType.Container,
                key = getKey(key, no.ToString()),
                parent = key,
                name = no.ToString(),
                sort = no,
                no = no,
                isArrayElement = true,
            };

            IList children = (value as IList);

            if (children != null)
            {
                add.isArrayContainer = true;

                for (int i = 0; i < children.Count; i++)
                {
                    readChild(add.key, i, children[i]);
                }
            }

            db.Add(add);

            readField(add.key, value);
        }
        private string getKey(string parent, string name)
        {
            return (parent != string.Empty ? parent + "_" + name : name);
        }
        private Type getGenType(Type p)
        {
            if (p.GenericTypeArguments.Length > 0)
                return p.GenericTypeArguments[0];
            else if (p.BaseType == null)
                return null;
            else
                return getGenType(p.BaseType);
        }

    }
}
