using UnityEngine;
using UnityEngine.InputSystem;

public class LevelOrbInteractionBehavior : MonoBehaviour
{
    public int orbToHold;
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Zephyr_Hands"))
        {
            
        }
    }

    public void OnExplorationLevelOrbPressed()
    {
        orbToHold = 1;

        if (orbToHold == 1)
        {
            
        }
    }
}
