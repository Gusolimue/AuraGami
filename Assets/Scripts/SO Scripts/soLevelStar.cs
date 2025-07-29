using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "New Level Star", menuName = "Create Level Star")]
public class soLevelStar : ScriptableObject
{
    [Header("Level Star Attributes")]
    public Color levelStarColor;
    public string levelName;
    public int whichLevel;

}

