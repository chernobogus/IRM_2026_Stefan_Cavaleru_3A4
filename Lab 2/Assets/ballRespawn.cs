using UnityEngine;
using System.Collections;

public class BallRespawn : MonoBehaviour
{
    [Header("Setari Resetare")]
    [SerializeField] private float fallThreshold = -0.5f; // Daca pica sub podea
    [SerializeField] private float respawnDelay = 2.0f;    // Secunde dupa ce atinge podeaua

    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Rigidbody rb;
    private Coroutine respawnCoroutine;

    private void Start()
    {
        // Memoreaza automat pozitia de pe masa la pornirea jocului
        initialPosition = transform.position;
        initialRotation = transform.rotation;
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // 1. Daca bila a cazut pe langa podea in gol (in afara hartii)
        if (transform.position.y < fallThreshold)
        {
            ReseteazaBila();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 2. Daca atinge podeaua, porneste un timer scurt
        if (collision.gameObject.name.ToLower().Contains("floor") || collision.gameObject.CompareTag("Respawn"))
        {
            if (respawnCoroutine == null)
            {
                respawnCoroutine = StartCoroutine(AsteaptaSiReseteaza());
            }
        }
    }

    private IEnumerator AsteaptaSiReseteaza()
    {
        yield return new WaitForSeconds(respawnDelay);
        ReseteazaBila();
        respawnCoroutine = null;
    }

    public void ReseteazaBila()
    {
        // Opreste orice forta reziduala din zbor/rostogolire
        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }

        // Teleporteaza bila inapoi pe masa
        transform.position = initialPosition;
        transform.rotation = initialRotation;
    }
}