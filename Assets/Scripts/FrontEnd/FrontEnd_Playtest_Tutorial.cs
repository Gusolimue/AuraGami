using UnityEngine;
using UnityEngine.UI;

public class FrontEnd_Playtest_Tutorial : MonoBehaviour
{
    [SerializeField] GameObject[] tutorialSlides;
    [SerializeField] Button nextButton;
    [SerializeField] Button backButton;
    [SerializeField] Button understoodButton;

    public int currentSlideIndex = 0;
    private bool isLastSlide;

    private void Awake()
    {
        nextButton.onClick.AddListener(NextSlide);
        backButton.onClick.AddListener(PreviousSlide);

        ShowSlide(currentSlideIndex);

        nextButton.gameObject.SetActive(true);
        backButton.gameObject.SetActive(false);
        understoodButton.gameObject.SetActive(false);
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
            if (currentSlideIndex == 0)
            {
                nextButton.gameObject.SetActive(true);
                backButton.gameObject.SetActive(false);
                understoodButton.gameObject.SetActive(false);
            }
            if (currentSlideIndex == 1)
            {
                nextButton.gameObject.SetActive(true);
                backButton.gameObject.SetActive(true);
                understoodButton.gameObject.SetActive(false);
            }
            if (currentSlideIndex == 2)
            {
                nextButton.gameObject.SetActive(false); 
                understoodButton.gameObject.SetActive(true);
            }
        }
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

    public void OnUnderstoodButtonPressed()
    {
        CanvasManager.Instance.ShowCanvasFE(); 
        Destroy(this.gameObject);
    }
}
