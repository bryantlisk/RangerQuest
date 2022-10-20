using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour
{
    [SerializeField] float spawnCooldownInitial; //The value the spawn cooldown starts at in seconds.
    [SerializeField] float spawnCooldownAccel; //Subtracts from the spawn cooldown each time an enemy is spawned..
    [SerializeField] float spawnCooldownMin; //The minimum value of the spawn cooldown with the spawnCooldownAccel
    float spawnCooldown;
    Transform spawnPoint;
    [SerializeField] GameObject spawnedObject;
    bool onCooldown = false;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = GetComponent<Transform>();
        spawnCooldown = spawnCooldownInitial;
    }

    // Update is called once per frame
    void Update()
    {
        if (!onCooldown)
        {
            StartCoroutine("SpawnEnemy");
        }
    }

    IEnumerator SpawnEnemy()
    {
        onCooldown = true;
        Instantiate(spawnedObject, spawnPoint);
        yield return new WaitForSeconds(spawnCooldown);
        spawnCooldown -= spawnCooldownAccel;
        if (spawnCooldown < spawnCooldownMin)
            spawnCooldown = spawnCooldownMin;
        onCooldown = false;

    }
}
