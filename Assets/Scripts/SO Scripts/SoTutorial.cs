using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Tutorial", menuName = "Create Tutorial")]

public class SoTutorial : ScriptableObject
{
    public string tutorialText;
    public List<Board> tutorialBoards;
}
