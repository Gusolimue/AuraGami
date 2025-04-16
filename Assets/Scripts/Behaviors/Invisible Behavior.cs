using UnityEngine;

//Disables the renderer of a gameobject on play.
public class InvisibleBehavior : MonoBehaviour
{
    private void Awake()
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
