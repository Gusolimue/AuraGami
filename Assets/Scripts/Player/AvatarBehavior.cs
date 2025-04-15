using System.Collections;
using UnityEngine;
//this is currently a dummy behavior used for collision detection and instantiating the avatar model

public class AvatarBehavior : MonoBehaviour
{
    //[Header("Variables to Adjust")]
    [Header("Variables to Set")]
    public eSide side;
    public Renderer evolveSphereRenderer;
    float evolveTime = 8;
    [SerializeField] GameObject avatarPrefab;
    //[Header("Variables to Call")]
    
    //
    private void Awake()
    {
        evolveSphereRenderer.material.color = new Color(1, 1, 1, 0);
        Instantiate(avatarPrefab).transform.SetParent(this.transform);
    }

    //
    public void ObstacleCollision()
    {

    }

    IEnumerator CoEvolve()
    {
        evolveSphereRenderer.material.color = new Color(1, 1, 1, 1);
        yield return new WaitForSeconds((60f / LevelManager.Instance.level.soTrack.bpm)*evolveTime);
        evolveSphereRenderer.material.color = new Color(1, 1, 1, 0);
    }
    public void StartEvolve()
    {
        StartCoroutine(CoEvolve());
    }
    //
    public void TargetCollision()
    {

    }
}
