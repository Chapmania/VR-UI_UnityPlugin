using UnityEngine;
using WindowsInput.Native;
using WindowsInput;

//Used to simulate a button on the keyboard via the InputSimulator 
public class VirtualKeyboardButton : MonoBehaviour
{
    private IKeyboardSimulator keyboardSim;
    private Rigidbody rb;    
    private Vector3 unpressedPos, pressedPos;
    private float previousLerpPos, lerpPos;

    [Min(0f)] [SerializeField] private float maxKeyPressDistance , resistence;
    [Range(0f, 1f)] [SerializeField] private float keyPressRange = 0.1f;
    [SerializeField] private VirtualKeyCode key;

    // Start is called before the first frame update
    void Start()
    {
        keyboardSim = FindObjectOfType<VRInputController>().VRInput.Keyboard;
        rb = GetComponent<Rigidbody>();
        unpressedPos = gameObject.transform.localPosition;
        pressedPos = gameObject.transform.localPosition + (gameObject.transform.transform.forward * maxKeyPressDistance);
    }

    //Force the button to immediately be in the pressed state
    public void PressButton()
    {
        lerpPos = 1f;
        transform.localPosition = Vector3.Lerp(unpressedPos, pressedPos, lerpPos);
    }

    //Force the button to immediately be in the unpressed state
    public void ReleaseButton()
    {
        lerpPos = 0f;
        transform.localPosition = Vector3.Lerp(unpressedPos, pressedPos, lerpPos);
    }


    //Requires a constant press from a UI Collider to press the button
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
            //Update position of the button
            transform.localPosition = Vector3.Lerp(unpressedPos, pressedPos, lerpPos);


            //Simulate keyboard input if button is pressed enough
            if (lerpPos >= keyPressRange)
                keyboardSim.KeyDown(key);
            else
                keyboardSim.KeyUp(key);

            //If button is not being pressed down, begin releasing button
            if (lerpPos <= previousLerpPos)
            {
                //Button is no longer being pressed
                lerpPos -= Time.deltaTime / resistence;
            }

            //Limit button position alpha to be between 0 and 1, then update the previous position
            lerpPos = Mathf.Clamp(lerpPos, 0, 1);
            previousLerpPos = lerpPos;
        }        
    }
}
