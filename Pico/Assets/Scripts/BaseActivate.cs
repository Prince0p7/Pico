using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseActivate : MonoBehaviour
{
    [SerializeField]Button button;
    float xScale;
    void Update()
    {
        if(button.Pressed)
        {
            xScale += 2 * Time.deltaTime;
            xScale = Mathf.Clamp(xScale, 0, 25);
        }
        transform.localScale = new Vector3(xScale, transform.localScale.y, transform.localScale.z);
    }
}