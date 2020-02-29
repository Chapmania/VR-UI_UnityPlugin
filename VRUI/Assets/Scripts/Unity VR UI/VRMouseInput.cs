using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRMouseInput : MonoBehaviour
{
    [SerializeField] private string inputAxisName;
    [SerializeField] private float buttonThreshold = 0.5f;
    [SerializeField] private Canvas[] affectedCanvases;
    [SerializeField] private Camera[] affectedCameras;

    // Start is called before the first frame update
    void Start()
    {
        var col = new int[7];

        //col.
    }
    
    // Update is called once per frame
    void Update()
    {
        foreach (var camera in affectedCameras)
        {
            //Physics.Raycast
            Debug.DrawLine(transform.position, camera.WorldToScreenPoint(transform.position), Color.blue);

            //affectedCanvases[0].
        }
        if(Input.GetButton(inputAxisName) || Input.GetAxis(inputAxisName) > buttonThreshold)
        {
            foreach (var canvas in affectedCanvases)
            {
                //Debug.DrawLine(transform.position, canvas.transform.position, Color.blue);
            }

            foreach (var cam in affectedCameras)
            {
                //cam.WorldToScreenPoint(transform.position);
                

                //Debug.DrawLine(transform.position, cam.WorldToScreenPoint(transform.position), Color.red);
            }
        }
    }
}
