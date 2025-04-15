using UnityEngine;
using UnityEngine.UI;

public class FrontEnd_Playtest_Tutorial : MonoBehaviour
{
    [SerializeField] GameObject[] tutorialSlides;
    [SerializeField] Button understoodButton;
    [SerializeField] Button backButton;

    public int currentSlideIndex = 0;

    private void Awake()
    {
        understoodButton.onClick.AddListener(NextSlide);
        backButton.onClick.AddListener(PreviousSlide);

        ShowSlide(currentSlideIndex);

        understoodButton.gameObject.SetActive(true);
        backButton.gameObject.SetActive(false);
    }

    private void ShowSlide(int index)
    {
        for (int i = 0; i < tutorialSlides.Length; i++)
        {
            tutorialSlides[i].SetActive(false);
        }

        if (index >= 0 && index < tutorialSlides.Length)
        {
            tutorialSlides[index].SetActive(true);
        }

        if (currentSlideIndex == 0) backButton.gameObject.SetActive(false); understoodButton.gameObject.SetActive(true);
        if (currentSlideIndex == 1) backButton.gameObject.SetActive(true);
        if (currentSlideIndex == 3) understoodButton.gameObject.SetActive(false);
    }

    private void NextSlide()
    {
        if (currentSlideIndex < tutorialSlides.Length - 1)
        {
            currentSlideIndex++;
            ShowSlide(currentSlideIndex);
        }
    }

    private void PreviousSlide()
    {
        if (currentSlideIndex > 0)
        {
            currentSlideIndex--;
            ShowSlide(currentSlideIndex);
        }
    }

}
