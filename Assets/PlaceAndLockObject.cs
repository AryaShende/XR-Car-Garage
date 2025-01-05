using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PlaceAndLockObject : MonoBehaviour
{
    public Transform placementPoint;   // The point where the object will be placed
    public float placementRadius = 0.1f;   // How close the object needs to be to the point to lock

    private XRGrabInteractable grabInteractable;  // XR grab interactable component
    private Rigidbody rb;
    private bool isPlaced = false;

    private void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();  // Grab XR grab interactable component
        rb = GetComponent<Rigidbody>();

        // Add listeners for grabbing and releasing events
        grabInteractable.selectEntered.AddListener(OnGrab);
        grabInteractable.selectExited.AddListener(OnRelease);
    }

    private void OnGrab(SelectEnterEventArgs args)
    {
        // Only allow grabbing if the object hasn't been placed yet
        if (!isPlaced)
        {
            rb.constraints = RigidbodyConstraints.None;
        }
    }

    private void OnRelease(SelectExitEventArgs args)
    {
        // Check if the object is near the placement point when released
        if (Vector3.Distance(transform.position, placementPoint.position) <= placementRadius)
        {
            // Move the object exactly to the placement point and freeze its position/rotation
            transform.position = placementPoint.position;
            rb.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;

            // Mark the object as placed
            isPlaced = true;

            // Disable grabbing after placement
            grabInteractable.enabled = false;  // Disable interaction to prevent future grabs
        }
        else
        {
            // Optionally, allow the object to remain in place but not lock it if it's not placed
            rb.constraints = RigidbodyConstraints.None;
        }
    }
}
