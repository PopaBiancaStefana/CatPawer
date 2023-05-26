using Stats;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class AreaManager : MonoBehaviour
{
    [SerializeField] private GameObject trashbin;
    [SerializeField] private GameObject spawnPoints;
    [SerializeField] private GameObject EnemyHealthBarPrefab;
    
    public GameObject[] enemyPrefabs;
    private bool enemiesSpawned = false;
    [SerializeField] private int difficulty = 1; // 0 - easy, 1 - normal, 2 - hard

    void Update()
    {
        if (enemiesSpawned)
            if (CheckIfAllEnemiesAreDead())
            {
                SpawnLoot();
                OpenDoor();
            }

    }

    void AddHealthBar(GameObject go)
    {
        GameObject body = go.transform.Find("Body").gameObject;

        var height = body.GetComponent<Collider>().bounds.size.y;
        Vector3 offset = new (0, height, 0);

        Instantiate(EnemyHealthBarPrefab, body.transform.position + offset, Quaternion.identity, body.transform);
    }

    public void SetDifficutly(int diff)
    { difficulty = diff; }
    
    public void SpawnEnemies()
    {
        foreach (Transform child in spawnPoints.transform)
        {

            if (child != null)
            {
                int randomIndex = Random.Range(0, enemyPrefabs.Length);
                var enemy = Instantiate(enemyPrefabs[randomIndex], child.transform.position, Quaternion.identity);
                AddHealthBar(enemy);

                var body = enemy.transform.Find("Body");
                EnemyBonus bonus = body.gameObject.AddComponent<EnemyBonus>();
                bonus.SetDifficulty(difficulty);
            }
        }
        enemiesSpawned = true;
    }

    bool CheckIfAllEnemiesAreDead()
    {
        return GameObject.FindGameObjectsWithTag("Enemy").Length == 0;
    }

    void SpawnLoot()
    {
        trashbin.SetActive(true);
    }

    void OpenDoor()
    {
        // Open door
    }
}