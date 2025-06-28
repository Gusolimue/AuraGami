using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StarBehavior : MonoBehaviour
{
    [SerializeField] Image[] starPresets;
    public float fadeSpeed = .1f;
    public float timeBetweenCycles = 1f;

    private int curIndex = -1;
    private int preIndex = -1;

    private void Awake()
    {
        curIndex = Random.Range(0, starPresets.Length);
        preIndex = curIndex;

        for (int i = 0; i < starPresets.Length; i++)
        {
            SetAlpha(starPresets[i], i == curIndex ? 1f : 0f);
        }

        StartCoroutine(StarPresetCycle());
    }

    private IEnumerator StarPresetCycle()
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenCycles);
            do
            {
                curIndex = Random.Range(0, starPresets.Length);
            } while (curIndex == preIndex);

            Image nextImage = starPresets[curIndex];
            Image lastImage = starPresets[curIndex];

            yield return StartCoroutine(FadeStars(lastImage, nextImage));

            preIndex = curIndex;
        }
    }

    private IEnumerator FadeStars(Image lastImage, Image nextImage)
    {
        float count = 0f;

        while (count < fadeSpeed)
        {
            float time = count / fadeSpeed;

            if (lastImage != null) SetAlpha(lastImage, 1 - time);
            if (nextImage != null) SetAlpha(nextImage, time);

            count += Time.deltaTime;
            yield return null;
        }

        if (lastImage != null) SetAlpha(lastImage, 0);
        if (nextImage != null) SetAlpha(nextImage, 1);
    }

    private void SetAlpha(Image image, float alpha)
    {
        Color color = image.color;
        color.a = alpha;
        image.color = color;
    }
}
