using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHand : MonoBehaviour
{
    // referintele catre butoanele controllerului
    public InputActionProperty pinchAnimationAction;
    public InputActionProperty gripAnimationAction;

    private Animator anim;

    void Start()
    {
        // luam animatorul de pe mana
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        // citim cat de apasat e tragaciul din fata (intre 0 si 1)
        float triggerValue = pinchAnimationAction.action.ReadValue<float>();
        anim.SetFloat("Trigger", triggerValue);

        // citim butonul de grip din lateral
        float gripValue = gripAnimationAction.action.ReadValue<float>();
        anim.SetFloat("Grip", gripValue);
    }
}