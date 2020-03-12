using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseActivator : MonoBehaviour
{
    public MonoBehaviour active;
    
    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.W))
        {
            active.enabled = !active.enabled;
        }
    }
}
