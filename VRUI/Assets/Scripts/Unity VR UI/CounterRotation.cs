using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterRotation : MonoBehaviour
{
    [SerializeField] GameObject pointTo;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {      
        var pos = transform.parent.transform;

        transform.rotation = Quaternion.FromToRotation(pos.position, pointTo.transform.position);
    }
}
