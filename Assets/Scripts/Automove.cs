using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class Automove : MonoBehaviour {

    private int counter;
    private Vector3 forward = new Vector3(0,0,1),backward = new Vector3(0,0-1), left = new Vector3(1,0,0),
                            right = new Vector3(-1,0,0);

    private void Start()
    {
        counter = 0;
    }

    private void FixedUpdate()
    {
        counter++;
        if(counter%480 == 0)
        {
            gameObject.GetComponent<Rigidbody>().velocity = right;
            counter = 0;
        }
        else if(counter%240 == 0)
        {
            gameObject.GetComponent<Rigidbody>().velocity = left;
            counter = 0;
        }
        else if (counter % 120 == 0)
        {
            gameObject.GetComponent<Rigidbody>().velocity = backward;
            counter = 0;
        }
        else if (counter % 60 == 0)
        {
            gameObject.GetComponent<Rigidbody>().velocity = forward;
            counter = 0;
        }
    }
}
