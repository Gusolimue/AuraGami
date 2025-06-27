using UnityEngine;
using EditorAttributes;
public class AvatarBanking : MonoBehaviour
{
    [Header("Banking Options")]
    [SerializeField] bool bankingEnabled;
    [SerializeField] float bankMax;
    [SerializeField] float bankSpeed;
    [SerializeField] float bankAmount;

    Quaternion startRot;
    Quaternion leftRot;
    Quaternion rightRot;
    Vector3 lastPos;
    float velocity;
    private void Awake()
    {
        startRot = this.transform.rotation;
        leftRot.eulerAngles = startRot.eulerAngles + new Vector3( 0, 0, bankMax);
        rightRot.eulerAngles = startRot.eulerAngles - new Vector3( 0, 0, bankMax);
    }

    private void Update()
    {
        if(bankingEnabled)
        {
            velocity = this.transform.position.x - lastPos.x;
            bankAmount += velocity;
            bankAmount = Mathf.Lerp(bankAmount, .5f, Time.deltaTime * bankSpeed);
            this.transform.localRotation = Quaternion.Slerp(leftRot, rightRot, bankAmount);
            lastPos = this.transform.position;
        }
    }
}
