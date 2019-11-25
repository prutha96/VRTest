using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Lightsaber : MonoBehaviour
{
    public SteamVR_Controller.Device device;

    [HideInInspector]
    public Interaction interaction;

    public void Setup(SteamVR_Controller.Device newDevice, Interaction newInteraction)
    {
        device = newDevice;
        interaction = newInteraction;
    }
}
