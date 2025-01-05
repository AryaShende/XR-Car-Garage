using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class DoorHandleController : MonoBehaviour
{
    public Transform doorTransform; // Reference to the door that will open
    public float openSpeed = 2f;    // Speed of door opening
    public float maxOpenAngle = 80f; // Maximum door opening angle
    public MeshCollider handleCollider; // Reference to the mesh collider for touch detection

    private XRGrabInteractable grabInteractable;
    private bool isGrabbed = false;
    private float currentAngle = 0f;
    private Quaternion initialRotation;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrabbed);
        grabInteractable.selectExited.AddListener(OnReleased);

        initialRotation = doorTransform.localRotation;

        // Ensure the handle collider is a trigger to detect touch
        if (handleCollider != null)
        {
            handleCollider.isTrigger = true;
        }
    }

    void Update()
    {
        if (isGrabbed)
        {
            // Rotate the door smoothly
            if (currentAngle < maxOpenAngle)
            {
                currentAngle += openSpeed * Time.deltaTime;
                doorTransform.localRotation = Quaternion.Euler(0, currentAngle, 0);
            }
        }
    }

    private void OnGrabbed(SelectEnterEventArgs args)
    {
        isGrabbed = true;
    }

    private void OnReleased(SelectExitEventArgs args)
    {
        isGrabbed = false;
    }

    // Detect touch interaction using the mesh collider
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("PlayerHand")) // Assuming the hand is tagged as "PlayerHand"
        {
            Debug.Log("Handle touched by hand");
            // Optionally, you can add any response for touch detection here
        }
    }
}
