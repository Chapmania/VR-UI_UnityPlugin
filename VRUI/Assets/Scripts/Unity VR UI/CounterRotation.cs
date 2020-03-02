using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterRotation : MonoBehaviour
{
    [SerializeField] GameObject pointTo;

    private void Start()
    {
        if(pointTo == null)
        {
            pointTo = FindObjectOfType<Camera>().gameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(pointTo.transform);
        transform.Rotate(new Vector3(0, 180, 0));
        //var pos = transform.parent.transform;

        //transform.rotation = Quaternion.FromToRotation(pos.position, pointTo.transform.position);
    }
}
