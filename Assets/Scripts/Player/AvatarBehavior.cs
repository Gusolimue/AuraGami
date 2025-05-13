using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem.DualShock.LowLevel;

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
    public float directionBankingMinimum;
    public int bankingSpeedFactor;

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
        //Debug.Log(avatarObject.name + "direction: " + direction);

        if (Mathf.Abs(direction.x) < directionBankingMinimum)
        {
            direction = new Vector3(0, direction.y, direction.z);
        }
        
        if (Mathf.Abs(direction.y) < directionBankingMinimum)
        {
            direction = new Vector3(direction.x, 0, direction.z);
        }

        //Vector3 rotationAdditionX;
        //Vector3 rotationAdditionY;

        //if (BirdBankingBehavior)
        //{
        //    rotationAdditionX = new Vector3(0, 0, Mathf.Ceil(direction.x) * bankingSpeedFactor);
        //    rotationAdditionY = new Vector3(0, Mathf.Ceil(direction.y) * bankingSpeedFactor, 0);

        //    if (avatarObject.transform.eulerAngles.z < bankingMaxAngle)
        //    {
        //        avatarObject.transform.eulerAngles += rotationAdditionX;
        //    }

        //    if (avatarObject.transform.eulerAngles.y < bankingMaxAngle)
        //    {
        //        avatarObject.transform.eulerAngles += rotationAdditionY;
        //    }
        //}
        //else
        //{
        //    rotationAdditionX = new Vector3(Mathf.Ceil(direction.x) * bankingSpeedFactor, 0, 0);
        //    rotationAdditionY = new Vector3(0, Mathf.Ceil(direction.y) * bankingSpeedFactor, 0);

        //    if (avatarObject.transform.eulerAngles.x < bankingMaxAngle)
        //    {
        //        avatarObject.transform.eulerAngles += rotationAdditionX;
        //    }

        //    if (avatarObject.transform.eulerAngles.y < bankingMaxAngle)
        //    {
        //        avatarObject.transform.eulerAngles += rotationAdditionY;
        //    }
        //}

        Vector3 rotationX;
        Vector3 rotationY;

        if (BirdBankingBehavior)
        {
            // BIRD
            // Banking up (Y position goes up) - X rotation goes down
            // Banking down (Y position goes down) - X rotation goes up
            // Banking left (X position goes down) - Z rotation goes up
            // Banking right (X position goes up) - Z rotation goes down

            rotationX = new Vector3(0, 0, -Mathf.Ceil(direction.x) * bankingSpeedFactor);
            rotationY = new Vector3(-Mathf.Ceil(direction.y) * bankingSpeedFactor, 0, 0);

            if (avatarObject.transform.eulerAngles.z < bankingMaxAngle && avatarObject.transform.eulerAngles.z > -bankingMaxAngle)
            {
                Debug.Log("bird rotationX movement");
                avatarObject.transform.eulerAngles += rotationX;
            }
            else if (Mathf.Abs(direction.x) < directionBankingMinimum && avatarObject.transform.eulerAngles.z != 0)
            {
                Debug.Log("bird no rotationX movement");
                avatarObject.transform.eulerAngles -= rotationX;
            }

            if (avatarObject.transform.eulerAngles.x < bankingMaxAngle && avatarObject.transform.eulerAngles.x > -bankingMaxAngle)
            {
                Debug.Log("bird rotationY movement");
                avatarObject.transform.eulerAngles += rotationY;
            }
            else if (Mathf.Abs(direction.y) < directionBankingMinimum && avatarObject.transform.eulerAngles.x != 0)
            {
                Debug.Log("bird no rotationY movement");
                avatarObject.transform.eulerAngles -= rotationY;
            }

            // rotation = new Vector3(-direction.y * bankingMaxAngle, 0, -direction.x * bankingMaxAngle);
        }
        else
        {
            // SNAKE
            // Banking up (Y position goes up) - X rotation goes down
            // Banking down (Y position goes down) - X rotation goes up
            // Banking left (X position goes down) - Y rotation goes down
            // Banking right (X position goes up) - Y rotation goes up

            rotationX = new Vector3(0, Mathf.Ceil(direction.x) * bankingSpeedFactor, 0);
            rotationY = new Vector3(-Mathf.Ceil(direction.y) * bankingSpeedFactor, 0, 0);

            if (avatarObject.transform.eulerAngles.y < bankingMaxAngle && avatarObject.transform.eulerAngles.y > -bankingMaxAngle)
            {
                Debug.Log("snake rotationX movement");
                avatarObject.transform.eulerAngles += rotationX;
            }
            else if (Mathf.Abs(direction.x) < directionBankingMinimum && avatarObject.transform.eulerAngles.y != 0)
            {
                Debug.Log("snake no rotationX movement");
                avatarObject.transform.eulerAngles -= rotationX;
            }

            if (avatarObject.transform.eulerAngles.x < bankingMaxAngle && avatarObject.transform.eulerAngles.x > -bankingMaxAngle)
            {
                Debug.Log("snake rotationY movement");
                avatarObject.transform.eulerAngles += rotationY;
            }
            else if (Mathf.Abs(direction.y) < directionBankingMinimum && avatarObject.transform.eulerAngles.x != 0)
            {
                Debug.Log("snake no rotationY movement");
                avatarObject.transform.eulerAngles -= rotationY;
            }

            //rotation = new Vector3(-direction.y * bankingMaxAngle, direction.x * bankingMaxAngle, 0);
        }

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
