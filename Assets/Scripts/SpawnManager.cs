﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    [SerializeField]
    private GameObject _trippleshotPowerupPrefab;

    private bool _stopSpawning = false;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SpawnRoutine());
        StartCoroutine(SpawnPowerupRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Spawn game objects every 5 seconds
    IEnumerator SpawnRoutine()
    {
        // waits 1 frame
        //yield return null;

        // Then calls this line
        // yield return new WaitForSeconds(5.0f);

        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, -8f), 7, 0);
            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);
        }
    }

    public void onPlayerDeath()
    {
        _stopSpawning = true;
    }

    IEnumerator SpawnPowerupRoutine()
    {
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            Instantiate(_trippleshotPowerupPrefab, posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3, 8));
        }
    }
}