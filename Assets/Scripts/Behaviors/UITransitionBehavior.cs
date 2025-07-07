using UnityEngine;

public class UITransitionBehavior : MonoBehaviour
{
    [SerializeField] GameObject transitionPoint;
    public float transitionTime = 15f;
    private float count;

    private void Update() // A simple script that activates when the game object the script is attached to instantiates. Once instantiated, the corresponding game object will move toward the position of the transitionPoint. 
    {
        count += Time.deltaTime;
        this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, transitionPoint.transform.position, count / transitionTime);
    }
}
