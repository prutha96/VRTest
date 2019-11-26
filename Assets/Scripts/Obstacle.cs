using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Obstacle : MonoBehaviour {
    public GameObject obj1;
    public GameObject obj2;

    //private Collider collider = null;

    void Awake() {
        //collider = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider other) {
        Debug.Log("Collision");

        // Is it a lightsaber
        //if (other.CompareTag("Lightsaber")) {
            Debug.Log("Lightsaber");
            Split();
        //}

        

        //Lightsaber lightSaber = collider.gameObject.GetComponent<Lightsaber>();

        // Is the lightsaber in use?
        //if (lightSaber.device == null) {
        //    return;
        //}

        // Check for split
        //Split(lightSaber);

        //Split();
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Debug.Log("Collision");
        if(collision.collider.CompareTag("Lightsaber"))
        {
            Debug.Log("Light Saber");
            Split();
        }
    }

    //private void Split(Lightsaber lightSaber) {
    private void Split() { 
        // Split obstacle
        //if (lightSaber.device.velocity.magnitude > 3.0f) {
            // Disable collision, so we only split once
            GetComponent<Collider>().enabled = false;

            // Enable physics for both sides
            EnablePhysics(obj1);
            EnablePhysics(obj2);
        //}
    }

    private void EnablePhysics(GameObject obj) {
        obj1.transform.parent = null;

        Rigidbody rb = obj.GetComponent<Rigidbody>();

        rb.useGravity = true;
        rb.isKinematic = false;
    }
}
