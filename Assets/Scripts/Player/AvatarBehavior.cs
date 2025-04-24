using System.Collections;
using UnityEngine;
//this is currently a dummy behavior used for collision detection and instantiating the avatar model

public class AvatarBehavior : MonoBehaviour
{
    //[Header("Variables to Adjust")]
    [Header("Variables to Set")]
    public eSide side;
    public Renderer evolveSphereRenderer;
    Color startColor;
    Color transparentColor;
    Color failColor;
    float evolveTime = 8;
    [SerializeField] GameObject avatarPrefab;
    //[Header("Variables to Call")]
    
    //
    private void Awake()
    {
        transparentColor = new Color(1, 1, 1, 0);
        startColor = new Color(1, 1, 1, 1);
        failColor = new Color(1, .5f, .5f, 1);
        evolveSphereRenderer.material.color = transparentColor;
        //Instantiate(avatarPrefab).transform.SetParent(this.transform);
    }

    //
    public void ObstacleCollision()
    {

    }

    IEnumerator CoEvolve(bool _pass)
    {

        yield return new WaitForSeconds(1f);
        float count = 0;
        while(count < 1)
        {
            count += Time.deltaTime;
            evolveSphereRenderer.material.color = Color.Lerp(transparentColor, startColor, count / (60f / LevelManager.Instance.level.track.bpm) * 2);
            yield return null;
        }
        count = 0;
        yield return new WaitForSeconds(1f);
        if (_pass)
        {
            while (count < 1)
            {
                count += Time.deltaTime;
                evolveSphereRenderer.material.color = Color.Lerp(startColor, transparentColor, count/(60f / LevelManager.Instance.level.track.bpm) * 2);
                yield return null;
            }
            if (eSide.right == side)
            {
                APManager.Instance.ResetAP();
            }
        }
        else
        {

            while (count < 1)
            {
                count += Time.deltaTime;
                evolveSphereRenderer.material.color = Color.Lerp(startColor, failColor, count / (60f / LevelManager.Instance.level.track.bpm) * 2);
                yield return null;
            }
            count = 0;
            while (count < 1)
            {
                count += Time.deltaTime;
                evolveSphereRenderer.material.color = Color.Lerp(failColor, transparentColor, count / (60f / LevelManager.Instance.level.track.bpm) * 2);
                yield return null;
            }
            if(eSide.right == side)
            {
                CanvasManager.Instance.ShowCanvasStageFail();
                PauseManager.Instance.isPaused = true;
                BeatManager.Instance.PauseMusicTMP(true);
            }
        }
    }
    public void StartEvolve()
    {
        StartCoroutine(CoEvolve(APManager.Instance.StagePassCheck()));
    }
    //
    public void TargetCollision()
    {

    }
}
