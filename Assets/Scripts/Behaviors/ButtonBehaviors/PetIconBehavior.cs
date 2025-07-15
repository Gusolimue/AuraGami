using UnityEngine;
using UnityEngine.UI;

public class PetIconBehavior : MonoBehaviour
{
    [SerializeField] Image petIcon;
    [SerializeField] Image targetSizeIncrease;
    [SerializeField] Image targetSizeDecrease;

    public bool isHover;
    private float sizeSpeed = 5;
    private float count;

    private void Update()
    {
        count += Time.deltaTime;
        if (isHover)
        {
            petIcon.transform.localScale = Vector3.Lerp(petIcon.transform.localScale, targetSizeIncrease.transform.localScale,
                count / sizeSpeed);
        }

        if (!isHover)
        {
            petIcon.transform.localScale = Vector3.Lerp(petIcon.transform.localScale, targetSizeDecrease.transform.localScale,
                count / sizeSpeed);
        }
    }

    public void IconSizeIncrease()
    {
        isHover = true;
        count = 0;
    }

    public void IconSizeDecrease()
    {
        isHover = false;
        count = 0;
    }
}
