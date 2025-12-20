using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactors;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

public class BowController : MonoBehaviour
{
    [Header("Prefabs")]
    public GameObject arrowPrefab; // The arrow we will spawn

    [Header("References")]
    public XRGrabInteractable bowGrabInteractable; // The Bow Handle
    public XRGrabInteractable nockGrabInteractable; // The String Handle
    public LineRenderer lineRenderer; // The visual string

    [Header("String Anchors")]
    public Transform stringTop;    // Top of bow
    public Transform stringBottom; // Bottom of bow
    public Transform stringNock;   // The moving part of the string

    [Header("Settings")]
    public float fireForceMultiplier = 100f; // Power setting

    [Header("DEBUG")]
    public float nockTiltX;
    public float nockTiltY;
    public float nockTiltZ;

    // Private logic variables
    private GameObject currentArrow;
    private Vector3 nockRestLocalPosition;
    private IXRInteractor nockGrabber;
    private AudioSource fireSoundEffect;

    void Start()
    {
        lineRenderer.positionCount = 3; // Top, Nock, Bottom
        nockRestLocalPosition = stringNock.localPosition; // Remember where "zero" is
        TryGetComponent<AudioSource>(out fireSoundEffect);


        // Hook up our custom functions to the VR events
        bowGrabInteractable.selectEntered.AddListener(OnBowGrabbed);
        bowGrabInteractable.selectExited.AddListener(OnBowReleased);
        nockGrabInteractable.selectEntered.AddListener(OnNockGrabbed);
        nockGrabInteractable.selectExited.AddListener(OnNockReleased);
    }

    void Update()
    {
        // Update the visual string every frame to follow the nock
        lineRenderer.SetPosition(0, stringTop.position);
        lineRenderer.SetPosition(1, stringNock.position);
        lineRenderer.SetPosition(2, stringBottom.position);

        // If nock grabbed rotate based on that, otherwise track rotation
        if (nockGrabber != null)
        {
            bowGrabInteractable.trackRotation = false;
            Vector3 direction = transform.position - nockGrabber.transform.position;
            Quaternion lookRotation = Quaternion.LookRotation(direction.normalized, Vector3.up);
            if (currentArrow != null) currentArrow.transform.rotation = lookRotation * Quaternion.Euler(-10.0f, 0.0f, 0.0f);
            transform.rotation = lookRotation * Quaternion.Euler(nockTiltX, nockTiltY, nockTiltZ);
        }
        else { bowGrabInteractable.trackRotation = true; }


    }

    private void OnBowGrabbed(SelectEnterEventArgs args)
    {
        SpawnArrow();
    }

    private void OnBowReleased(SelectExitEventArgs args)
    {
        // If we drop the bow with an arrow still in it, delete the arrow
        if (currentArrow != null)
        {
            Destroy(currentArrow);
            currentArrow = null;
        }
    }
    
    private void OnNockGrabbed(SelectEnterEventArgs args)
    {
        nockGrabber = args.interactorObject;
    }

    private void OnNockReleased(SelectExitEventArgs args)
    {
        // Only fire if an arrow is actually nocked
        if (currentArrow != null) _fire();
        nockGrabber = null;
    }

    private void _fire()
    {
        if (fireSoundEffect != null) fireSoundEffect.Play();

        // Calculate power based on pull distance
        float pullDistance = Vector3.Distance(stringNock.localPosition, nockRestLocalPosition);
       
        // Get the Arrow script and command it to Fire
        currentArrow.GetComponent<Arrow>().Fire(pullDistance * fireForceMultiplier);
       
        currentArrow = null; // We no longer have an arrow nocked


        // Reset the string position and rotation immediately
        stringNock.localPosition = nockRestLocalPosition;
        stringNock.localRotation = Quaternion.identity;

        // Wait 1 second, then spawn a new arrow automatically
        Invoke("SpawnArrow", 1.0f);
    }

    public void SpawnArrow()
    {
        // Don't spawn if bow isn't held OR if we already have an arrow
        if (!bowGrabInteractable.isSelected || currentArrow != null) return;

        // Create the arrow as a child of the nock
        currentArrow = Instantiate(arrowPrefab, stringNock);
       
        // Zero out position and set rotation to point forward
        currentArrow.transform.localPosition = Vector3.zero;
        currentArrow.transform.localRotation = Quaternion.Euler(90f, 0, 0);
    }
}
