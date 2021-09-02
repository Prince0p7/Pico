using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    Timer _Timer;
    [SerializeField]SceneManager sceneManager;
    private void Start()
    {
        _Timer = FindObjectOfType<Timer>();
    }
    void Update()
    {
        if (_Timer.TimeOver)
        {
            sceneManager.CanvasHideAndShow("Game Over Screen");
        }
    }
}