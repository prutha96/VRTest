using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class VRController : MonoBehaviour
{
    public float sensitivity = 0.1f;
    public float maxSpeed = 1.0f;

    public SteamVR_Action_Boolean movePress = null;
    public SteamVR_Action_Vector2 moveValue = null;

    private float speed = 0.0f;

    private CharacterController characterController = null;
    public Transform cameraRig = null;
    public Transform head = null;

    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }

    // Start is called before the first frame update
    private void Start()
    {
        //cameraRig = SteamVR_Render.Top().origin;
        //head = SteamVR_Render.Top().head;
    }

    // Update is called once per frame
    private void Update()
    {
        HandleHead();
        HandleHeight();
        CalculateMovement();
    }

    private void HandleHead()
    {
        // Store current
        Vector3 oldPos = cameraRig.position;
        Quaternion oldRot = cameraRig.rotation;

        // Rotation
        transform.eulerAngles = new Vector3(0.0f, head.rotation.eulerAngles.y, 0.0f);

        // Restore
        cameraRig.position = oldPos;
        cameraRig.rotation = oldRot;
    }

    private void CalculateMovement()
    {
        // Figure out movement orientation
        Vector3 orientationEuler = new Vector3(0.0f, transform.eulerAngles.y, 0.0f);
        Quaternion orientation = Quaternion.Euler(orientationEuler);
        Vector3 movement = Vector3.zero;

        // If not moving
        //if (movePress.GetStateUp(SteamVR_Input_Sources.Any))
        //{
        //    speed = 0.0f;
        //}

        // If button pressed
        if (movePress.state)
        {
            // Add, clamp
            speed += moveValue.axis.y * sensitivity;
            speed = Mathf.Clamp(speed, -maxSpeed, maxSpeed);

            // Orientation
            movement += orientation * (speed * Vector3.forward) * Time.deltaTime;
        }
        else
        {
            speed = 0;
        }

        // Apply
        characterController.Move(movement);
    }

    private void HandleHeight()
    {
        // Get the head in local space
        float headHeight = Mathf.Clamp(head.localPosition.y, 1, 2);
        characterController.height = headHeight;

        // Cut in half
        Vector3 newCentre = Vector3.zero;
        newCentre.y = (characterController.height / 2f) + characterController.skinWidth;

        // Move capsule in local space
        newCentre.x = head.localPosition.x;
        newCentre.z = head.localPosition.z;

        // Rotate
        newCentre = Quaternion.Euler(0, -transform.eulerAngles.y, 0) * newCentre;

        // Apply
        characterController.center = newCentre;
    }
}
