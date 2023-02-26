using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public static class ObjectExtensions
{
    public static string ObjectToStr<T>(T _saveMe)
    {
        BinaryFormatter _bin = new BinaryFormatter();
        MemoryStream _mem = new MemoryStream();
        _bin.Serialize(_mem, _saveMe);

        return Convert.ToBase64String(
            _mem.GetBuffer()
        );
    }

    public static T StrToObject<T>(string _data) where T : class
    {
        if (!String.IsNullOrEmpty(_data))
        {
            BinaryFormatter _bin = new BinaryFormatter();
            try
            {
                MemoryStream _mem = new MemoryStream(Convert.FromBase64String(_data));

                T _obj = _bin.Deserialize(_mem) as T;

                return _obj;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        else
        {
            throw new Exception("_data is null or empty");
        }
    }
}
