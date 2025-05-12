using UnityEngine;

//Disables the renderer of a gameobject on play.
public class InvisibleBehavior : MonoBehaviour
{
    private void Start()
    {
        if (GetComponent<Renderer>())
        {
            GetComponent<Renderer>().enabled = false;
        }
        else if (GetComponent<Canvas>())
        {
            GetComponent<Canvas>().enabled = false;
        }
    }
}
