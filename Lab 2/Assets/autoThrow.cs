using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class AutoThrow : MonoBehaviour
{
    [Header("setari aruncare")]
    [SerializeField] private float throwForce = 12f; // viteza
    [SerializeField] private float upwardArc = 0.1f;  // sus putin

    private Rigidbody rb;
    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grab;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        grab = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
    }

    private void OnEnable()
    {
        if (grab != null)
            grab.selectExited.AddListener(OnDrop);
    }

    private void OnDisable()
    {
        if (grab != null)
            grab.selectExited.RemoveListener(OnDrop);
    }

    private void OnDrop(SelectExitEventArgs args)
    {
        rb.isKinematic = false;

        // unde te uiti
        Vector3 forwardDir = Camera.main.transform.forward;
        Vector3 finalDir = (forwardDir + Vector3.up * upwardArc).normalized;

        // trimite bila
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        rb.AddForce(finalDir * throwForce, ForceMode.Impulse);
    }
}