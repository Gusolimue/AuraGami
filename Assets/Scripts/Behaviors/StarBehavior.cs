using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class StarBehavior : MonoBehaviour
{
    [SerializeField] Image[] starPresets;
    [SerializeField] Color[] starColors;
    public float fadeSpeed = 200f;

    private float count;
    private int curIndex;

    private void Awake()
    {
        foreach (var stars in starPresets)
        {
            stars.color = starColors[0];
        }
        StartCoroutine(ChangeStarCluster());
    }   

    private void Update()
    {
        count += Time.deltaTime;
        if (curIndex == 1)
        {
            starPresets[3].color = Color.Lerp(starPresets[3].color, starColors[0], count / fadeSpeed);
            starPresets[0].color = Color.Lerp(starPresets[0].color, starColors[0], count / fadeSpeed);
            starPresets[1].color = Color.Lerp(starPresets[1].color, starColors[1], count / fadeSpeed);
        }
        if (curIndex == 2)
        {
            starPresets[1].color = Color.Lerp(starPresets[1].color, starColors[0], count / fadeSpeed);
            starPresets[2].color = Color.Lerp(starPresets[2].color, starColors[1], count / fadeSpeed);
        }
        if (curIndex == 3)
        {
            starPresets[2].color = Color.Lerp(starPresets[2].color, starColors[0], count / fadeSpeed);
            starPresets[3].color = Color.Lerp(starPresets[3].color, starColors[1], count / fadeSpeed);
        }
    }

    private IEnumerator ChangeStarCluster()
    {
        yield return new WaitForSeconds(Random.Range(10, 60));
        curIndex = 1;
        count = 0;
        yield return new WaitForSeconds(Random.Range(10, 60));
        curIndex = 2;
        count = 0;
        yield return new WaitForSeconds(Random.Range(10, 60));
        curIndex = 3;
        count = 0;
        StartCoroutine(ChangeStarCluster());
    }
}
