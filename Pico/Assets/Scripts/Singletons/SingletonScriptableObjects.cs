using UnityEngine;

public abstract class SingletonScriptableObjects<T> : ScriptableObject where T : ScriptableObject
{
    public static T _instance = null;
    public static T Instance
    {
        get
        {
            if(_instance == null)
            {
                T[] results = Resources.FindObjectsOfTypeAll<T>();
                if(results.Length == 0)
                {
                    return null;
                }
                if(results.Length > 1)
                {
                    return null;
                }

                _instance = results[0];
                _instance.hideFlags = HideFlags.DontUnloadUnusedAsset;
            }

            return _instance;
        }
    }
}