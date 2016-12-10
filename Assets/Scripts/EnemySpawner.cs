﻿using UnityEngine;
using System.Collections;

public class EnemySpawner : MonoBehaviour {

    public GameObject enemyPrefab;
    public float warmUpTime;
    public float timeBetweenEnemies;
    public float randomRange;
    public Transform target;

    BoxCollider2D c;
    float minX;
    float maxX;
    float minY;
    float maxY;
    Vector3 targetPosition;

    void Start() {
        c = GetComponent<BoxCollider2D>();
        minX = transform.position.x + c.offset.x - c.size.x / 2;
        maxX = transform.position.x + c.offset.x + c.size.x / 2;
        minY = transform.position.y + c.offset.y - c.size.y / 2;
        maxY = transform.position.y + c.offset.y + c.size.y / 2;
        targetPosition = target.position;
        targetPosition.z = 0;

        StartCoroutine(spawnEnemies());
    }

    IEnumerator spawnEnemies()
    {
        yield return new WaitForSeconds(warmUpTime + Random.Range(-randomRange, randomRange));
        while (true) {
            Vector3 randomPoint = new Vector3(Random.Range(minX, maxX), Random.Range(minY, maxY), 0);
            GameObject enemy = Instantiate(enemyPrefab, randomPoint, Quaternion.identity) as GameObject;
            enemy.transform.LookAt(targetPosition, -Vector3.forward);
            enemy.transform.parent = transform;
            yield return new WaitForSeconds(timeBetweenEnemies + Random.Range(-randomRange, randomRange));
            yield return 0;
        }
    }

}