using System;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public static class Utils
{
    public static Color ToColor(string _htmlColor)
    {
        Color color;

        ColorUtility.TryParseHtmlString("#" + _htmlColor, out color);

        return color;
    }

    public static string ToJson<T>(T _obj)
    {
        return JsonUtility.ToJson(_obj, true);
    }

    public static T FromJson<T>(string _json)
    {
        return JsonUtility.FromJson<T>(_json);
    }

    public static string CreateMD5(string filePath)
    {
        try
        {
            using (var fileStream = new FileStream(filePath, FileMode.Open))
            {
                var len = (int)fileStream.Length;
                var data = new byte[len];

                fileStream.Read(data, 0, len);


                var md5 = new MD5CryptoServiceProvider();
                var hash = md5.ComputeHash(data);
                var result = "";

                foreach (var bt in hash)
                    result += Convert.ToString(bt, 16);

                return result;
            }
        }
        catch (FileNotFoundException e)
        {
            Debug.LogError(e.StackTrace);
            return "";
        }
    }

    public static string CreateMD5(byte[] bytes)
    {
        try
        {
            var md5 = new MD5CryptoServiceProvider();
            var hash = md5.ComputeHash(bytes);
            var result = "";

            foreach (var bt in hash)
                result += Convert.ToString(bt, 16);

            return result;
        }
        catch (FileNotFoundException e)
        {
            Debug.LogError(e.StackTrace);
            return "";
        }
    }

    public static string GetMd5(string data)
    {
        var md5 = new MD5CryptoServiceProvider();
        byte[] output = md5.ComputeHash(Encoding.ASCII.GetBytes(data));

        return BitConverter.ToString(output).Replace("-", "").ToLower();
    }
    

    public static List<T> RandomList<T>(List<T> _totalList, int _count)
    {
        List<T> result = new List<T>();

        if (_count == 0 || _totalList.Count == 0)
        {
            return result;
        }

        for (int i = 0; i < _count; i++)
        {
            int rand = UnityEngine.Random.Range(0, _totalList.Count);

            result.Add(_totalList[i]);
        }

        return result;
    }

    public static Vector3 StringToVector3(string _str)
    {
        Vector3 result = new Vector3();

        _str = _str.Trim();
        _str = _str.Replace("(", "");
        _str = _str.Replace(")", "");

        string[] array = _str.Split(',');
        if (array.Length == 3)
        {
            result.x = Convert.ToSingle(array[0]) / 1000;
            result.y = Convert.ToSingle(array[1]) / 1000;
            result.z = Convert.ToSingle(array[2]) / 1000;
        }

        return result;
    }
}
