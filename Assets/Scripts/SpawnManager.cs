using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject[] _powerups;

    private bool _stopSpawning = false;

    private Player player;

    public void StartSpawning()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        StartCoroutine(SpawnRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    // Spawn game objects every 5 seconds
    IEnumerator SpawnRoutine()
    {
        // waits 1 frame
        //yield return null;

        // Then calls this line
        // yield return new WaitForSeconds(5.0f);
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;

            if (player._score < 50)
            {
                yield return new WaitForSeconds(5.0f);
            }
            else if (player._score > 50)
            {
                yield return new WaitForSeconds(4.0f);
            }
            else if (player._score > 100)
            {
                yield return new WaitForSeconds(3.0f);
            }
            else if (player._score > 150)
            {
                yield return new WaitForSeconds(2.0f);
            }
            else if (player._score > 200)
            {
                yield return new WaitForSeconds(1.0f);
            }
            else
            {
                yield return new WaitForSeconds(.5f);

            }
        }

    }

    public void onPlayerDeath()
    {
        _stopSpawning = true;
    }

    IEnumerator SpawnPowerupRoutine()
    {
        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            int randomPowerUp = Random.Range(0, 3);
            Instantiate(_powerups[randomPowerUp], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }
}
