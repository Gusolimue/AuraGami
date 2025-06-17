using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum eTutorial { leftTarget, rightTarget, bothTarget}
public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;
    public TutorialCanvasManager tc;
    [NamedArray(typeof(eTutorial))]
    public SoTutorial[] tutorialList;
    public soTrack tutorialTrack;
    List<List<GameObject>> instantiatedTutorials;
    GameObject[] tutorialContainers;
    int boardIndex;
    bool isSubscribed;
    int tutorialIndex;
    private void Awake()
    {
        Instance = this;
        instantiatedTutorials = new List<List<GameObject>>(0);
    }
    private void Start()
    {
        InitTutorialStages();
        StartCoroutine(COPlayNextTutorial(tutorialList[tutorialIndex]));
    }
    void InitTutorialStages()
    {
        tutorialContainers = new GameObject[tutorialList.Length];
        for (int i = 0; i < tutorialList.Length; i++)
        {
            instantiatedTutorials.Add(LevelManager.Instance.InstantiateStage(tutorialList[i].tutorialBoards, 0));
        }
    }
    int lastIndex;
    void PlayTutorial(SoTutorial _tutorial)
    {
        Debug.Log("tutorial started: "+_tutorial.tutorialType);
        LevelManager.Instance.instantiatedStages[0] = instantiatedTutorials[(int)_tutorial.tutorialType];
        APManager.Instance.SetTargetValues();
        //tc.FadeInText(_tutorial.tutorialText);
        boardIndex = 0;
        BeatManager.beatUpdated += ActivateBoard;
        isSubscribed = true;
        lastIndex = tutorialIndex;
    }
    public void EndTutorial()
    {
        Debug.Log("stage finished");
        BeatManager.beatUpdated -= ActivateBoard;
        isSubscribed = false;
        if (AvatarManager.Instance.StartEvolve(true))
        {
            tc.FadeOutText();
            tutorialIndex++;
            StartCoroutine(COPlayNextTutorial(tutorialList[tutorialIndex]));
        }
        else
        {
            StartCoroutine(COPlayNextTutorial(tutorialList[tutorialIndex]));
        }
    }
    void ActivateBoard()
    {
        //Debug.Log("board activated");
        if (boardIndex >= LevelManager.Instance.instantiatedStages[0].Count)
        {
            EndTutorial();
        }
        else
        {
            LevelManager.Instance.instantiatedStages[0][boardIndex].SetActive(true);
            LevelManager.Instance.instantiatedStages[0][boardIndex].GetComponent<BoardBehavior>().Init();
            boardIndex++;
        }
    }
    IEnumerator COPlayNextTutorial(SoTutorial _tutorial)
    {
        yield return new WaitForSeconds(5f);
        if(tutorialIndex < tutorialList.Length)
        {
            PlayTutorial(_tutorial);
        }
        else
        {
            MoveOn();
        }
    }
    void MoveOn()
    {
        Debug.Log("doneWithTutorials");
    }
}
