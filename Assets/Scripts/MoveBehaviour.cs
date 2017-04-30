using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

[RequireComponent(typeof(Rigidbody))]

public class MoveBehaviour : MonoBehaviour {

    private Rigidbody rb;

    private void Start()
    {
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate () {
        rb.velocity = new Vector3(CrossPlatformInputManager.GetAxis("Horizontal") * 10,
                    0, CrossPlatformInputManager.GetAxis("Depth") * 10);
    }
}
