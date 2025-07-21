using System.Collections.Generic;
using UnityEngine;
public enum eEvo { firstEvolution, secondEvolution, thirdEvolution }
[CreateAssetMenu(fileName = "New Avatar", menuName = "Create Avatar")]
public class SoAvatar : ScriptableObject
{
    public string avatarName;
    public GameObject[] AvatarPrefab;
    public GameObject CursorPrefab;
    [NamedArray(typeof(eEvo))] public AvatarTextures[] avatarTextures;
    [NamedArray(typeof(eEvo))] public Material[] avatarMats;

}
[System.Serializable]
public class AvatarTextures
{
    public Texture[] textures;
}
