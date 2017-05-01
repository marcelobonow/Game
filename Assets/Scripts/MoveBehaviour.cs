using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]

public class MoveBehaviour : MonoBehaviour {
    Camera maincamera;
    private Rigidbody rb;

    private void Start()
    {
        maincamera = Camera.main;
        rb = gameObject.GetComponent<Rigidbody>();
    }

    void FixedUpdate() {
        rb.position = new Vector3(rb.position.x +
                    (CrossPlatformInputManager.GetAxis("Horizontal") * gameObject.GetComponent<PlayerClass>().speed * Time.deltaTime),
                    0.5f,
                    rb.position.z + (CrossPlatformInputManager.GetAxis("Depth") * gameObject.GetComponent<PlayerClass>().speed * Time.deltaTime));
        rb.velocity = Vector3.zero;

        foreach (Touch touch in Input.touches)
        {
            Ray ray = maincamera.ScreenPointToRay(touch.position);
            RaycastHit hit;
            if(Physics.Raycast(ray, out hit, 100f))//do a raycast in direction of the ground, where it hits is the new end point
            {
                float distance = Vector3.Distance(gameObject.transform.position,hit.point); //get the distance between where the point hits and the player
                hit.point = (gameObject.GetComponent<PlayerClass>().range / distance) * hit.point; //do some maths to set it on the max distance
                if(Physics.Raycast(new Ray(gameObject.transform.position,hit.point-gameObject.transform.position),out hit,gameObject.GetComponent<PlayerClass>().range))
                {
                    Debug.Log(hit.transform.name);
                }
            }
        }
    }
}
