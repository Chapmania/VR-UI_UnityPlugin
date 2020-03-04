using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WindowsInput.Native;
using WindowsInput;
//using WindowsInput.Native;
public class VirtualKeyboardButton : MonoBehaviour
{
    InputSimulator virtKeyboard;
    Rigidbody rb;
    [SerializeField] private string Key;
    [SerializeField] private float maxKeyPressDistance , resistence;
    [Range(0f, 1f)] [SerializeField] private float keyPressRange = 0.1f;
    private Vector3 unpressedPos, pressedPos;
    public float previousLerpPos, lerpPos;
    // Start is called before the first frame update
    void Start()
    {
        virtKeyboard = new InputSimulator();
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
        //lerpPos 0 = Button unpressed, lerpPos 1 = Button fully pressed
        transform.localPosition = Vector3.Lerp(unpressedPos, pressedPos, lerpPos);

        if (lerpPos >= keyPressRange)
            virtKeyboard.Keyboard.KeyDown(VirtualKeyCode.VK_W);
        else
            virtKeyboard.Keyboard.KeyUp(VirtualKeyCode.VK_W);

        if(lerpPos <= previousLerpPos)
        {
            //Button is no longer being pressed
            lerpPos -= Time.deltaTime / resistence;
        }       

        Debug.DrawRay(transform.position + unpressedPos, transform.position + unpressedPos - pressedPos, Color.red, 1f);

        lerpPos = Mathf.Clamp(lerpPos, 0, 1);
        previousLerpPos = lerpPos;
    }
}
