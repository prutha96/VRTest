using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class VRObject : MonoBehaviour
{
    [SerializeField] private bool usesVR;
    [SerializeField] private XRNode nodeType;

    [Range(0, 1)]
    [SerializeField] private float movementImmediacy;

    private Vector3 targetPosition;
    private Quaternion targetRotation;

    private void Start()
    {
        transform.localPosition = InputTracking.GetLocalPosition(nodeType);
        transform.localRotation = InputTracking.GetLocalRotation(nodeType);
    }

    private void Update()
    {
        if (usesVR)
        {
            targetPosition = InputTracking.GetLocalPosition(nodeType);
            targetRotation = InputTracking.GetLocalRotation(nodeType);

            transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition, movementImmediacy);
            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, movementImmediacy);
        }
    }
}