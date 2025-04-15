using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New Level", menuName = "Create Level")]
public class SoLevel : ScriptableObject
{
    public soTrack track;
    public List<Beat> beats = new List<Beat>(0);
    public void RecordBeat()
    {
        beats.Add(new Beat(BeatManager.lastMarkerString));
    }
    //[Header("Stages")]
    //[SerializeField]
    //List<Board> stage1;
    //[SerializeField]
    //List<Board> stage2;
    //[SerializeField]
    //List<Board> stage3;


    //public List<Board> GetStage(int _index)
    //{
    //    switch (_index)
    //    {
    //        case 1:
    //            return stage1;
    //        case 2:
    //            return stage2;
    //        case 3:
    //            return stage3;
    //        default:
    //            return stage1;
    //    }
    //}
}

[System.Serializable]
public class Beat
{
    public string eventString;

    public Beat(string _eventString)
    {
        eventString = _eventString;
    }
}

