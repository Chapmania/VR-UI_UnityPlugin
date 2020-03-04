using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WindowsInput.Native;
using WindowsInput;

public class VRMouseInput : MonoBehaviour
{
    private MouseSimulator mouseSim;
    public RectTransform fauxMouse;
    [SerializeField] private string inputAxisName;
    [SerializeField] private float buttonThreshold = 0.5f;
    [SerializeField] private Canvas affectedCanvas;
    [SerializeField] private Camera affectedCamera;

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
        //Debug.DrawLine(transform.position, affectedCamera.WorldToScreenPoint(transform.position), Color.blue);
        var viewPortRaw = affectedCamera.WorldToViewportPoint(transform.position);
        

        mousePosition = new Vector2(viewPortRaw.x * affectedCanvas.pixelRect.size.x, viewPortRaw.y * affectedCanvas.pixelRect.size.y);
        fauxMouse.localPosition = mousePosition - uiCanvasOffset;
        //mouseSim.MoveMouseTo(mousePos.x, mousePos.y);
    }
}
