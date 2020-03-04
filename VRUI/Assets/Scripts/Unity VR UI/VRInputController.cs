using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WindowsInput.Native;
using WindowsInput;
public class VRInputController : MonoBehaviour
{
    public InputSimulator VRInput { get; private set; }
    // Start is called before the first frame update
    void Awake()
    {
        VRInput = new InputSimulator();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
