using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum eTutorial { basicTarget, bothTarget, precisionTarget, multiHitTarget, threadedTarget, test, final}
public class TutorialManager : MonoBehaviour
{
    public static TutorialManager Instance;
    public TutorialCanvasManager tc;
    [NamedArray(typeof(eTutorial))]
    public SoTutorial[] tutorialList;
    public soTrack tutorialTrack;
    [HideInInspector] public FMOD.Studio.EventInstance trackInstance;
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
        LevelManager.Instance.instantiatedStages = new List<GameObject>[1];
        InitTutorialStages();
        StartCoroutine(COTutorialIntro());
    }
    void InitTutorialStages()
    {
        tutorialContainers = new GameObject[tutorialList.Length];
        for (int i = 0; i < tutorialList.Length; i++)
        {
            instantiatedTutorials.Add(LevelManager.Instance.InstantiateStage(tutorialList[i].tutorialBoards, 0));
        }
    }
    void PlayTutorial(SoTutorial _tutorial)
    {
        Debug.Log("tutorial started: "+_tutorial.tutorialType);
        LevelManager.Instance.instantiatedStages[0] = LevelManager.Instance.InstantiateStage(tutorialList[(int)_tutorial.tutorialType].tutorialBoards, 0);
        APManager.Instance.SetTutorialTargetValues(_tutorial);
        boardIndex = 0;
        BeatManager.beatUpdated += ActivateBoard;
        isSubscribed = true;
        trackInstance.setParameterByName("Tutorial Progress", tutorialIndex);
    }
    public void EndTutorial()
    {
        Debug.Log("stage finished");
        BeatManager.beatUpdated -= ActivateBoard;
        isSubscribed = false;
        tc.FadeOutText();
        if (AvatarManager.Instance.StartEvolve(true))
        {
            tutorialIndex++;
        }
        AvatarManager.Instance.readyMove = false;
        if(tutorialIndex >= tutorialList.Length) MoveOn();
        else StartCoroutine(COPlayNextTutorial(tutorialList[tutorialIndex]));
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
    IEnumerator COTutorialIntro()
    {
        yield return new WaitForSeconds(3);
        string[] strings = { "YOU", "You Sleep", "Yet, You Do Not", "Look Around", "Move Your Arms", "Now, Accept Your Other Halves" };
        tc.FadeInText(strings);
        yield return new WaitUntil(() => !tc.textChanging);
        tc.FadeOutText();

        StartCoroutine(AvatarManager.Instance.COTutorialIntro());
        yield return new WaitUntil(() => !AvatarManager.Instance.disableAvatarMovement);
        AvatarManager.Instance.readyMove = true;
        StartCoroutine(COPlayNextTutorial(tutorialList[tutorialIndex]));
    }
    IEnumerator COPlayNextTutorial(SoTutorial _tutorial)
    {
        //if(!AvatarManager.Instance.disableAvatarMovement) yield return new WaitUntil(()=> AvatarManager.Instance.disableAvatarMovement);
        yield return new WaitUntil(()=> AvatarManager.Instance.readyMove);
        Debug.Log("i waitedddd");
        if (tutorialIndex < tutorialList.Length)
        {
            yield return new WaitForSeconds(2f);
            tc.FadeInText(_tutorial.tutorialText);
            yield return new WaitUntil(()=> !tc.textChanging);
            PlayTutorial(_tutorial);
        }
        else
        {
            yield return new WaitForSeconds(2f);
            MoveOn();
        }
    }
    void MoveOn()
    {
        Debug.Log("doneWithTutorials");
        FrontEndSceneTransitionManager.Instance.SceneFadeInTransitionSplash();
    }
}
