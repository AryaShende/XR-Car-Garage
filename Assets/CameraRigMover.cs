using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRigMover : MonoBehaviour
{
    public OVRCameraRig ovrCameraRig; // Drag your OVRCameraRig here
    public float moveSpeed = 1.0f;    // Adjust movement speed

    void Start()
    {
        // Optionally find the OVRCameraRig if it's not assigned in the Inspector
        if (ovrCameraRig == null)
        {
            ovrCameraRig = FindObjectOfType<OVRCameraRig>();
        }

        if (ovrCameraRig == null)
        {
            Debug.LogError("OVRCameraRig not found! Please assign it in the Inspector.");
        }
    }

    void Update()
    {
        if (ovrCameraRig != null)
        {
            // Move forward using the right thumbstick (up)
            if (OVRInput.Get(OVRInput.RawAxis2D.RThumbstick).y > 0)
            {
                MoveCameraRig(Vector3.forward);
            }

            // Move backward using the right thumbstick (down)
            if (OVRInput.Get(OVRInput.RawAxis2D.RThumbstick).y < 0)
            {
                MoveCameraRig(Vector3.back);
            }

            // Move left using the right thumbstick (left)
            if (OVRInput.Get(OVRInput.RawAxis2D.RThumbstick).x < 0)
            {
                MoveCameraRig(Vector3.left);
            }

            // Move right using the right thumbstick (right)
            if (OVRInput.Get(OVRInput.RawAxis2D.RThumbstick).x > 0)
            {
                MoveCameraRig(Vector3.right);
            }
        }
    }

    void MoveCameraRig(Vector3 direction)
    {
        // Move the entire camera rig in the desired direction
        ovrCameraRig.transform.Translate(direction * moveSpeed * Time.deltaTime);
    }
}
