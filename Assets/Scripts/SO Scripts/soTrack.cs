using UnityEngine;
//This is not currently used, but will be to hold data for each playable song
[CreateAssetMenu(fileName = "New Track", menuName = "Create Track")]
public class soTrack : ScriptableObject
{
    public string trackName;
    public AudioClip trackClip;
    public float trackBpm;
}
