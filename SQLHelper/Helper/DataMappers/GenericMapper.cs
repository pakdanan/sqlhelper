using System;
using System.Collections.Generic;
using System.Data;
using System.Reflection;

namespace SQLHelper.Helper.DataMappers
{
    /// <summary>
    /// Modification from http://aapl.codeplex.com/ and http://rlacovara.blogspot.co.id/2010/01/agile-adonet-persistence-layer-part-1.html
    /// </summary>
    class GenericMapper : IDataMapper
    {
        public System.Type DtoType { get; set; }
        const string SQLSeparator = "_";

        public GenericMapper(System.Type type)
        {
            DtoType = type;
        }

        private Object SetObjectValue(Type type, IDataReader reader, string prefix = "")
        {
            object obj = Activator.CreateInstance(type);
            PropertyInfo[] props = type.GetProperties();
            foreach (PropertyInfo p in props)
            {
                /// ! Property yang langsung bisa diambil nilainya primitive (int,bool,dll), string,datetime, guid, byte[] :
                // Type.IsPrimitive: Boolean, Byte, SByte, Int16, UInt16, Int32, UInt32, Int64, UInt64, IntPtr, UIntPtr, Char, Double, and Single.
                if (p.PropertyType.IsPrimitive || p.PropertyType == typeof(string) || p.PropertyType == typeof(decimal) || p.PropertyType == typeof(DateTime)
                    || p.PropertyType == typeof(Guid) || p.PropertyType.IsArray )  
                {
                    try
                    {
                        int colIndex = reader.GetOrdinal(prefix+p.Name);
                        if (!reader.IsDBNull(colIndex))
                        {
                            p.SetValue(obj, reader.GetValue(colIndex), null);
                        }
                    }
                    catch { }

                }
                else
                {
                    Object oNested = SetObjectValue(p.PropertyType, reader,p.Name+SQLSeparator);
                    p.SetValue(obj, oNested, null);
                }
            }
            return obj;
        }

        public Object GetData(IDataReader reader)
        {
            object dto = SetObjectValue(DtoType, reader);
            return dto;
        }

        public int GetRecordCount(IDataReader reader)
        {
            Object count = reader["RecordCount"];
            return count == null ? 0 : Convert.ToInt32(count);
        }
    }

}

//// Below is Original version from http://aapl.codeplex.com/ and http://rlacovara.blogspot.co.id/2010/01/agile-adonet-persistence-layer-part-1.html

//class GenericMapper : IDataMapper
//{
//    public System.Type DtoType { get; set; }
//    private bool _isInitialized = false;
//    private List<PropertyOrdinalMap> PropertyOrdinalMappings;
//    const string SQLSeparator = "_";

//    public GenericMapper(System.Type type)
//    {
//        DtoType = type;
//    }


//    private void InitializeMapper(IDataReader reader)
//    {
//        PopulatePropertyOrdinalMappings(reader);
//        _isInitialized = true;
//    }


//    public void PopulatePropertyOrdinalMappings(IDataReader reader)
//    {
//        // Get the PropertyInfo objects for our DTO type and map them to 
//        // the ordinals for the fields with the same names in our reader.  
//        PropertyOrdinalMappings = new List<PropertyOrdinalMap>();
//        PropertyInfo[] properties = DtoType.GetProperties();
//        foreach (PropertyInfo property in properties)
//        {
//            PropertyOrdinalMap map = new PropertyOrdinalMap();
//            map.Property = property;
//            try
//            {
//                map.Ordinal = reader.GetOrdinal(property.Name);
//                PropertyOrdinalMappings.Add(map);
//            }
//            catch { }
//        }
//    }

//    public Object GetData(IDataReader reader)
//    {
//        if (!_isInitialized) { InitializeMapper(reader); }
//        object dto = Activator.CreateInstance(DtoType);

//        foreach (PropertyOrdinalMap map in PropertyOrdinalMappings)
//        {
//            if (!reader.IsDBNull(map.Ordinal))
//            {
//                map.Property.SetValue(dto, reader.GetValue(map.Ordinal), null);
//            }
//        }
//        return dto;
//    }

//    private class PropertyOrdinalMap
//    {
//        public PropertyInfo Property { get; set; }
//        public int Ordinal { get; set; }
//    }

//    public int GetRecordCount(IDataReader reader)
//    {
//        Object count = reader["RecordCount"];
//        return count == null ? 0 : Convert.ToInt32(count);
//    }
//}
