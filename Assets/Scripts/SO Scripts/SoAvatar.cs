using UnityEngine;

[CreateAssetMenu(fileName = "New Avatar", menuName = "Create Avatar")]
public class SoAvatar : ScriptableObject
{
    public string avatarName;
    public GameObject[] AvatarPrefab;
    public GameObject CursorPrefab;
}
