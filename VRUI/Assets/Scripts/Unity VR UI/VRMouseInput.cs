using UnityEngine;
using WindowsInput;

public class VRMouseInput : MonoBehaviour
{
    private MouseSimulator mouseSim;
    public FakeMouse fauxMouse;
    [SerializeField] private string inputAxisName;
    [Range (0,3)][SerializeField] private int mouseInput = 0;
    [Range(0, 1)] [SerializeField] private float axisRequirement = 0.5f;
    [SerializeField] private Canvas affectedCanvas;
    [SerializeField] private Camera affectedCamera;

    public float inputAxis;
    private bool mouseDown;
    private Vector2 uiCanvasOffset;

    public Vector2 mousePosition;
    // Start is called before the first frame update
    void Start()
    {
        mouseSim = new MouseSimulator(FindObjectOfType<VRInputController>().VRInput);

        uiCanvasOffset = new Vector2(affectedCanvas.pixelRect.size.x / 2, affectedCanvas.pixelRect.size.y / 2);        
    }
    
    // Update is called once per frame
    void Update()
    {
        var viewPortRaw = affectedCamera.WorldToViewportPoint(transform.position);        

        mousePosition = new Vector2(viewPortRaw.x * affectedCanvas.pixelRect.size.x, viewPortRaw.y * affectedCanvas.pixelRect.size.y);
        mousePosition -= uiCanvasOffset;

        //Working approximation of the handheld position to the canvas, exaggerates a little too much near edges, clamping required.
        fauxMouse.transform.localPosition = new Vector2(mousePosition.x, mousePosition.y);

        inputAxis = Input.GetAxis(inputAxisName);
         
        if(inputAxis < axisRequirement && mouseDown)
        {
            mouseDown = false;
            fauxMouse.EndClick();
        }
        else if(inputAxis >= axisRequirement)
        {
            mouseDown = true;
            fauxMouse.Click();
        }

        //Not working as of yet.
        //var simMousePos = affectedCamera.WorldToScreenPoint(fauxMouse.position); //+ new Vector3(uiCanvasOffset.x,0,uiCanvasOffset.y); //was mousePosition
    }
}
