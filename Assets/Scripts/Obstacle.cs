using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Obstacle : MonoBehaviour {
    public GameObject obj1;
    public GameObject obj2;

    private Collider collider = null;

    void Awake() {
        collider = GetComponent<Collider>();
    }

    void OnTriggerEnter(Collider collider) {
        // Is it a lightsaber
        if (!collider.gameObject.CompareTag("Lightsaber")) {
            return;
        }

        Lightsaber lightSaber = collider.gameObject.GetComponent<Lightsaber>();

        // Is the lightsaber in use?
        if (lightSaber.device == null) {
            return;
        }

        // Check for split
        Split(lightSaber);
    }

    private void Split(Lightsaber lightSaber) {
        // Split obstacle
        if (lightSaber.device.velocity.magnitude > 3.0f) {
            // Disable collision, so we only split once
            collider.enabled = false;

            // Enable physics for both sides
            EnablePhysics(obj1);
            EnablePhysics(obj2);
        }
    }

    private void EnablePhysics(GameObject obstacle) {
        obj1.transform.parent = null;

        Rigidbody rb = log.GetComponent<RigidBody>();

        rb.useGravity = true;
        rb.isKinematic = false;
    }
}
