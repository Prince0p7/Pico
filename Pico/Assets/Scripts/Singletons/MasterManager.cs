using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Singletons/MasterManager")]
public class MasterManager : SingletonScriptableObjects<MasterManager>
{
    [SerializeField] GameSettings _GameSettings;
    public static GameSettings GameSettings { get { return Instance._GameSettings; } }
}