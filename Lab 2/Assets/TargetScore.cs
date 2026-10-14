using UnityEngine;
using TMPro;
using System.Collections;

public class TargetScore : MonoBehaviour
{
    [Header("UI si Referinte")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Transform centerPoint;
    [SerializeField] private Renderer targetRenderer;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private ParticleSystem hitParticles;

    [Header("Setari Punctaj")]
    [SerializeField] private float targetRadius = 0.8f;
    [SerializeField] private int maxScore = 100;

    private int totalScore = 0;
    private Color baseColor;

    private void Start()
    {
        if (targetRenderer != null)
            baseColor = targetRenderer.material.color;

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

        if (hitParticles == null)
            hitParticles = GetComponentInChildren<ParticleSystem>();

        ActualizeazaScor();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Throwable"))
        {
            Vector3 hitPoint = collision.contacts[0].point;
            CalculeazaPunctaj(hitPoint);

            if (audioSource != null)
            {
                audioSource.Play();
            }

            // face stelele la impact
            if (hitParticles != null)
            {
                // le pune unde trb (pe stele)
                hitParticles.transform.position = hitPoint; 
                hitParticles.Play();
            }
        }
    }

    private void CalculeazaPunctaj(Vector3 hitPoint)
    {
        float distanta = Vector3.Distance(hitPoint, centerPoint.position);
        float factor = Mathf.Clamp01(distanta / targetRadius);

        int puncte = Mathf.RoundToInt((1f - factor) * maxScore);
        if (puncte < 10) puncte = 10;

        totalScore += puncte;
        ActualizeazaScor();

        StartCoroutine(FlashVizual());
    }

    private void ActualizeazaScor()
    {
        if (scoreText != null)
            scoreText.text = $"Scor: {totalScore}";
    }

    private IEnumerator FlashVizual()
    {
        if (targetRenderer != null)
        {
            targetRenderer.material.color = Color.yellow;
            yield return new WaitForSeconds(0.2f);
            targetRenderer.material.color = baseColor;
        }
    }
}