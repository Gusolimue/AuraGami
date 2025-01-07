using UnityEngine;
public enum eTargetType { left, right, obstacle }
public class SpawnerBehavior : MonoBehaviour
{
    public GameObject[] targetsAndObstacles;
    public Transform[] spawnPoints;
    public float beatTime = (60/130)*2;
    private float timer;

    void Update()
    {
        //if (timer>beatTime)
        //{
        //    // Instantiate objects in random positions.
        //    GameObject objects = Instantiate(targetsAndObstacles[Random.Range(0, 2)], spawnPoints[Random.Range(0, 2)]);

        //    // Zeroing out objects in game.
        //    objects.transform.localPosition = Vector3.zero;

        //    // Will rotate spawnPoints (which will rotate objects) at different angles.
        //    //objects.transform.Rotate(transform.forward, 90 * Random.Range(0, 4));
        //    timer -= beatTime;
        //}
        //timer += Time.deltaTime;
    }

    public void SpawnTarget(/*eTargetType _targetType*/)
    {
        //switch (_targetType)
        //{
        //    case eTargetType.left:
        //        break;
        //    case eTargetType.right:
        //        break;
        //    case eTargetType.obstacle:
        //        break;
        //    default:
        //        break;
        //}

        // Instantiate objects in random positions.
        Transform tmpTransform = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject objects = Instantiate(targetsAndObstacles[Random.Range(0, 2)], tmpTransform.position, tmpTransform.rotation);

        // Zeroing out objects in game.

        // Will rotate spawnPoints (which will rotate objects) at different angles.
        //objects.transform.Rotate(transform.forward, 90 * Random.Range(0, 4));
    }
}
