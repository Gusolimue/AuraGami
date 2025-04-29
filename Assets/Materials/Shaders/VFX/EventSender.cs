using UnityEngine;
using UnityEngine.Events;

[ExecuteAlways]
public class EventSender : MonoBehaviour
{
    public UnityEvent myEvent;
    public bool triggerTheDamnThing = false;


    public void Update()
    {
        if(triggerTheDamnThing)
        {
            myEvent.Invoke();
        }
    }
}
