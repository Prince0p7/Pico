﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Singletons/GameSettings")]
public class GameSettings : ScriptableObject
{
    [SerializeField] string _gameVersion = "0.0.0";
    public string GameVersion { get { return _gameVersion;} }
    [SerializeField] string _nickName;
    public string NickName
    {
        get
        {
            int value = Random.Range(0, 9999);
            return _nickName + value.ToString(); 
        }
    }
}