using UnityEngine;

public class DistanceTrigger : MonoBehaviour
{
    [Header("Referinte Cactusi")]
    public Transform cactus1;
    public Transform cactus2;

    [Header("Componente Animator")]
    public Animator animator1;
    public Animator animator2;

    [Header("Distanta de atac (in metri)")]
    public float attackDistance = 0.25f;

    void Start()
    {
        //limiteaza la 30fps sa nu mai moara ventilatorul :)))))))))
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 30;
    }

    void Update()
    {
        if (cactus1 == null || cactus2 == null) return;

        // calculam distanta 3d dintre cele doua pers
        float currentDistance = Vector3.Distance(cactus1.position, cactus2.position);

        // cerificam daca sunt destul de aproape
        bool shouldAttack = currentDistance <= attackDistance;

        if (animator1 != null)
        {
            animator1.SetBool("isAttacking", shouldAttack);
        }

        if (animator2 != null)
        {
            animator2.SetBool("isAttacking", shouldAttack);
        }
    }
}