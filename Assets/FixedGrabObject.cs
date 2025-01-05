using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FixedGrabObject : MonoBehaviour
{
    private XRGrabInteractable grabInteractable;
    private Rigidbody rb;

    private void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        rb = GetComponent<Rigidbody>();

        // Disable movement when the object is grabbed
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        // Freeze position and rotation when grabbed
        rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        // Optionally, you can allow movement again when released (if needed)
        rb.constraints = RigidbodyConstraints.None;
    }
}