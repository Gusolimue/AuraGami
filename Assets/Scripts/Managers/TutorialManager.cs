using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum eTutorial { leftTarget, rightTarget, bothTarget}
public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;
    public SoTutorial[] tutorialList;
    public soTrack tutorialTrack;
    GameObject[] tutorialContainers;
    private void Awake()
    {
        Instance = this;
    }
    private void Start()
    {
        InitTutorialStages();
    }
    void InitTutorialStages()
    {
        tutorialContainers = new GameObject[tutorialList.Length];
        for (int i = 0; i < tutorialList.Length; i++)
        {
            LevelManager.Instance.InstantiateStage(tutorialList[i].tutorialBoards, 0, tutorialContainers[i].transform);
        }
    }
    void PlayTutorial(SoTutorial _tutorial)
    {
        StartCoroutine(COPlayTutorial(_tutorial));
    }
    IEnumerator COPlayTutorial(SoTutorial _tutorial)
    {
        yield return null;
    }
}
