using UnityEngine;

public class UITransitionBehavior : MonoBehaviour
{
    [SerializeField] GameObject transitionPoint;
    public float transitionTime = 15f;
    private float count;

    private void Update()
    {
        count += Time.deltaTime;
        this.gameObject.transform.position = Vector3.Lerp(this.gameObject.transform.position, transitionPoint.transform.position, count / transitionTime);
    }
}
