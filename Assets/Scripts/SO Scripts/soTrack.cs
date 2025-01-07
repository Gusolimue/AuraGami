using UnityEngine;

[CreateAssetMenu(fileName = "New Track", menuName = "Create Track")]
public class soTrack : ScriptableObject
{
    public string trackName;
    public AudioClip trackClip;
    public float trackBpm;
}
