using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WindowsInput.Native;
using WindowsInput;
//using WindowsInput.Native;
public class VirtualKeyboardButton : MonoBehaviour
{
    IKeyboardSimulator keyboardSim;
    Rigidbody rb;
    [SerializeField] private string Key;
    [SerializeField] private float maxKeyPressDistance , resistence;
    [Range(0f, 1f)] [SerializeField] private float keyPressRange = 0.1f;
    private Vector3 unpressedPos, pressedPos;
    public float previousLerpPos, lerpPos;
    // Start is called before the first frame update
    void Start()
    {
        keyboardSim = FindObjectOfType<VRInputController>().VRInput.Keyboard;
        rb = GetComponent<Rigidbody>();
        unpressedPos = gameObject.transform.localPosition;
        pressedPos = gameObject.transform.localPosition + (gameObject.transform.transform.forward * maxKeyPressDistance);
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.layer == gameObject.layer)
        {
            lerpPos += Time.deltaTime / resistence;
        }        
    }

    // Update is called once per frame
    void Update()
    {
        //Minimising performance hit from active buttons
        if(lerpPos != 0)
        {
            //lerpPos 0 = Button unpressed, lerpPos 1 = Button fully pressed
            transform.localPosition = Vector3.Lerp(unpressedPos, pressedPos, lerpPos);

            if (lerpPos >= keyPressRange)
                keyboardSim.KeyDown(VirtualKeyCode.VK_W);
            else
                keyboardSim.KeyUp(VirtualKeyCode.VK_W);

            if (lerpPos <= previousLerpPos)
            {
                //Button is no longer being pressed
                lerpPos -= Time.deltaTime / resistence;
            }

            Debug.DrawRay(transform.position + unpressedPos, transform.position + unpressedPos - pressedPos, Color.red, 1f);

            lerpPos = Mathf.Clamp(lerpPos, 0, 1);
            previousLerpPos = lerpPos;
        }        
    }
}
