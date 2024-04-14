using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
                instance = FindAnyObjectByType<T>(); 
            else if(instance!=FindAnyObjectByType<T>())
                Destroy(FindAnyObjectByType(typeof(T)));

          

            return instance;

        }
    }
}
