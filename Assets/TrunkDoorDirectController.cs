using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class TrunkDoorDirectController : MonoBehaviour
{
    public Transform trunkDoorTransform;  // Reference to the trunk door that will open
    public float openSpeed = 2f;          // Speed of trunk opening
    public float maxOpenAngle = 60f;      // Maximum trunk opening angle (adjust as needed)
    public MeshCollider trunkDoorCollider; // Reference to the mesh collider for touch detection

    private XRGrabInteractable grabInteractable;
    private bool isGrabbed = false;
    private float currentAngle = 0f;
    private Quaternion initialRotation;

    void Start()
    {
        grabInteractable = GetComponent<XRGrabInteractable>();
        grabInteractable.selectEntered.AddListener(OnGrabbed);
        grabInteractable.selectExited.AddListener(OnReleased);

        initialRotation = trunkDoorTransform.localRotation;

        // Ensure the trunk door collider is a trigger to detect touch
        if (trunkDoorCollider != null)
        {
            trunkDoorCollider.isTrigger = true;
        }
    }

    void Update()
    {
        if (isGrabbed)
        {
            // Rotate the trunk door upwards when grabbing it directly
            if (currentAngle < maxOpenAngle)
            {
                currentAngle += openSpeed * Time.deltaTime;
                trunkDoorTransform.localRotation = Quaternion.Euler(currentAngle, 0, 0);
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
            Debug.Log("Trunk touched by hand");
            // Additional logic for touch detection can go here
        }
    }
}
