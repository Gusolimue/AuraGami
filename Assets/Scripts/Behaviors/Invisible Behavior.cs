using UnityEngine;

public class InvisibleBehavior : MonoBehaviour
{
    private void Awake()
    {
        if (GetComponent<Renderer>())
        {
            GetComponent<Renderer>().enabled = false;
        }
    }
}
