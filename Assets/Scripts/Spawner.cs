using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject spawnInstance;

    public void Spawn()
    {
        GameObject spawn = Instantiate(spawnInstance, transform.position, Quaternion.identity);
    }
}
