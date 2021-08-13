using System.Collections.Generic;
using UnityEngine;

public class Tasks : MonoBehaviour
{
    public bool TaskCompleted;
    void Start()
    {
        
    }
    void Update()
    {
        if(TaskCompleted)
        {
            Debug.Log("Task Has Completed");
        }
    }
}