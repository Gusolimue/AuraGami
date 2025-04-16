using UnityEngine;
using UnityEngine.InputSystem;

public class AnimateHandController : MonoBehaviour
{
    public InputActionReference gripInputActionReference;

    private Animator handAnimator;
    private float gripValue;

    private void Start()
    {
        handAnimator = GetComponent<Animator>();
    }

    private void Update()
    {
        AnimateGrip();
    }

    private void AnimateGrip()
    {
        gripValue = gripInputActionReference.action.ReadValue<float>();
        handAnimator.SetFloat("Grip", gripValue);
    }
}
