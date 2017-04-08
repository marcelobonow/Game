using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]

public class MoveBehaviour : MonoBehaviour {

    private Rigidbody rb;
    private Vector2 oldMousePosition;

    void Update () {
        //normally the velocity is zero, if any button was pressed
        //the velocity changes.
        rb.velocity = Vector3.zero;
        if (Input.GetKey(KeyCode.A))
            rb.velocity = new Vector3(-10, rb.velocity.y, rb.velocity.z);
        if(Input.GetKey(KeyCode.D))
            rb.velocity = new Vector3(10, rb.velocity.y, rb.velocity.z);
        if (Input.GetKey(KeyCode.W))
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.y, 10);
        if (Input.GetKey(KeyCode.S))
            rb.velocity = new Vector3(rb.velocity.x, rb.velocity.z, -10);
    }
}
