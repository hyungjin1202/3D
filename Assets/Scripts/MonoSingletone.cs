using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class MonoSingletone<T> : MonoBehaviour where T : MonoBehaviour 
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if(_instance == null)
            {
                GameObject go = new GameObject("singtone" + typeof(T));

                _instance = go.AddComponent<T>();
            }

            return _instance;
        }
    }    
}

public class Singletone<T> where T : class, new()
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance =  new T();
            }

            return _instance;
        }
    }
}
