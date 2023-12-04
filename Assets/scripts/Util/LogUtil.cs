using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LogUtil
{
    public static void Log(string _str)
    {
            Debug.Log(_str);
    }

    public static void Error(string _str)
    {

        Debug.LogError(_str);
        
    }
}
