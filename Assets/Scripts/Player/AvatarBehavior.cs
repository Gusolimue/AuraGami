using System.Collections;
using UnityEngine;

public class AvatarBehavior : MonoBehaviour
{
    [Header("Variables to Set")]
    public eSide side;
    public Renderer evolveSphereRenderer;
    public Animator animator;
    [SerializeField] GameObject avatarPrefab;
    public GameObject avatarObject;
    public bool BirdBankingBehavior;
    public int bankingMaxAngle;
    public float bankingLerpTime;

    float lerpTime;
    Vector3 previousPosition;

    private void Update()
    {
        // Assign the current direction of movement
        Vector3 currentPosition = avatarObject.transform.position;
        Vector3 velocity = currentPosition - previousPosition;
        Vector3 direction = velocity.normalized;

        Debug.Log(avatarObject.name + "currentPosition: " + currentPosition);
        Debug.Log(avatarObject.name + "velocity: " + velocity);
        Debug.Log(avatarObject.name + "direction: " + direction);

        Vector3 rotation;

        if (BirdBankingBehavior)
        {
            // BIRD
            // Banking up (Y position goes up) - X rotation goes down
            // Banking down (Y position goes down) - X rotation goes up
            // Banking left (X position goes down) - Z rotation goes up
            // Banking right (X position goes up) - Z rotation goes down

            rotation = new Vector3(-direction.y * bankingMaxAngle, 0, -direction.x * bankingMaxAngle);
        }
        else
        {
            // SNAKE
            // Banking up (Y position goes up) - X rotation goes down
            // Banking down (Y position goes down) - X rotation goes up
            // Banking left (X position goes down) - Y rotation goes down
            // Banking right (X position goes up) - Y rotation goes up

            rotation = new Vector3(-direction.y * bankingMaxAngle, direction.x * bankingMaxAngle, 0);
        }

        Quaternion quaternionRotation = Quaternion.Euler(rotation);

        lerpTime = 0;
        while (lerpTime < bankingLerpTime)
        {
            lerpTime += Time.deltaTime;
            avatarObject.transform.rotation = Quaternion.Lerp(avatarObject.transform.rotation, quaternionRotation, lerpTime);
        }
        lerpTime = 0;

        //avatarObject.transform.rotation = quaternionRotation;

        previousPosition = currentPosition;
    }

    public void ObstacleCollision()
    {
        animator.SetTrigger("OnDamage");
    }

    public void StreakEnabled()
    {
        animator.SetTrigger("OnStreak");
    }

    public void TargetCollision()
    {

    }
}
