using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.DualShock.LowLevel;

public class AvatarBehaviorOld : MonoBehaviour
{
    [Header("Variables to Set")]
    public eSide side;
    public Renderer evolveSphereRenderer;
    public Animator animator;
    [SerializeField] GameObject avatarPrefab;
    public GameObject avatarObject;
    public bool BirdBankingBehavior;
    public int bankingMaxAngle;
    public float directionBankingMinimum;
    public float bankingSpeedFactor;

    float lerpTime;
    Vector3 previousPosition;

    private void Update()
    {
        // Assign the current direction of movement
        Vector3 currentPosition = avatarObject.transform.position;
        Vector3 velocity = currentPosition - previousPosition;
        Vector3 direction = velocity.normalized;

        //Debug.Log(avatarObject.name + "currentPosition: " + currentPosition);
        //Debug.Log(avatarObject.name + "velocity: " + velocity);
        Debug.Log(avatarObject.name + "direction: " + direction);

        if (Mathf.Abs(direction.x) < directionBankingMinimum)
        {
            direction = new Vector3(0, direction.y, direction.z);
        }
        
        if (Mathf.Abs(direction.y) < directionBankingMinimum)
        {
            direction = new Vector3(direction.x, 0, direction.z);
        }

        Vector3 rotation;

        if (BirdBankingBehavior)
        {
            // BIRD
            // Banking up (Y position goes up) - X rotation goes down
            // Banking down (Y position goes down) - X rotation goes up
            // Banking left (X position goes down) - Z rotation goes up
            // Banking right (X position goes up) - Z rotation goes down

            rotation = new Vector3(-Mathf.Ceil(direction.y) * bankingSpeedFactor, 0, -Mathf.Ceil(direction.x) * bankingSpeedFactor) + avatarObject.transform.eulerAngles;

            if (direction.x == 0f)
            {
                // Start rotating the avatar back to 0 on the Z axis
                if (avatarObject.transform.eulerAngles.z > 0)
                {
                    Debug.Log("Bird Subtracting rotation.z");
                    avatarObject.transform.eulerAngles -= new Vector3(0, 0, rotation.z);
                }
                else if (avatarObject.transform.eulerAngles.z < 0)
                {
                    Debug.Log("Bird Adding rotation.z");
                    avatarObject.transform.eulerAngles += new Vector3(0, 0, rotation.z);
                }
                    
            }
            else
            {
                // Rotate avatar in appropriate direction on the Z axis
                Debug.Log("Bird Adding rotation.z");
                avatarObject.transform.eulerAngles += new Vector3(0, 0, rotation.z);
            }

            // Clamp rotation of avatar to a maximum value
            rotation = new Vector3(Mathf.Clamp(rotation.x, -bankingMaxAngle, bankingMaxAngle), Mathf.Clamp(rotation.y, -bankingMaxAngle, bankingMaxAngle), Mathf.Clamp(rotation.z, -bankingMaxAngle, bankingMaxAngle));

        }
        else
        {
            // SNAKE
            // Banking up (Y position goes up) - X rotation goes down
            // Banking down (Y position goes down) - X rotation goes up
            // Banking left (X position goes down) - Y rotation goes down
            // Banking right (X position goes up) - Y rotation goes up

            rotation = new Vector3(-Mathf.Ceil(direction.y) * bankingSpeedFactor, Mathf.Ceil(direction.x) * bankingSpeedFactor, 0) + avatarObject.transform.eulerAngles;

            if (direction.x == 0f)
            {
                Debug.Log("Snake Subtracting rotation.y");
                avatarObject.transform.eulerAngles -= new Vector3(0, rotation.y, 0);
            }
            else
            {
                Debug.Log("Snake Adding rotation.y");
                avatarObject.transform.eulerAngles += new Vector3(0, rotation.y, 0);
            }

            rotation = new Vector3(Mathf.Clamp(rotation.x, -bankingMaxAngle, bankingMaxAngle), Mathf.Clamp(rotation.y, -bankingMaxAngle, bankingMaxAngle), Mathf.Clamp(rotation.z, -bankingMaxAngle, bankingMaxAngle));
        }

        if (direction.y == 0f)
        {
            Debug.Log("Subtracting rotation.x");
            avatarObject.transform.eulerAngles -= new Vector3(rotation.x, 0, 0);
        }
        else
        {
            Debug.Log("Adding rotation.x");
            avatarObject.transform.eulerAngles += new Vector3(rotation.x, 0, 0);
        }

        avatarObject.transform.eulerAngles = rotation;

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
