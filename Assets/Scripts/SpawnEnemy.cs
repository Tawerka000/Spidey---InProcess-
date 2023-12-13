using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject Enemy;
    public float spawnRadius = 10f;
    float timer = 30f;
    float time;
    int wave = 1;
    int numOfEnemies = 1;
    private bool boss = false;
    public float spawn_rate = 3f;
    public float enemyHealth = 1f;
    void Start()
    {
        wave =  0;
        time = timer;   
        StartCoroutine(waveChanger());
        StartCoroutine(space());
    }
    IEnumerator waveChanger()
    {
        yield return new WaitForSeconds(time);
        if (wave % 2 == 0)
        {
            numOfEnemies += 1;
        }
        wave += 1;
        print(wave);
        print(numOfEnemies);
        enemyHealth = enemyHealth + 0.1f;
        spawn_rate += 0.1f;
        if (time > 10)
        {
            time = time - 1.5f;
        }
        if (wave == 20)
        {
            print("Boss coming!!!");
            boss = true;
        }
        StartCoroutine(waveChanger());
    }
    IEnumerator space()
    {
        if (boss != true)
        {
            for (int i = 0; i < numOfEnemies; i++)
            {
                Vector2 spawnPosition = Random.insideUnitCircle.normalized * spawnRadius;
                Instantiate(Enemy, spawnPosition, Quaternion.identity);
                //Vector2 spawnPosition = Random.insideUnitCircle * spawnRadius;
                //Instantiate(Enemy, transform.position + new Vector3(spawnPosition.x, spawnPosition.y, 0), Quaternion.identity);
            }
            yield return new WaitForSeconds(spawn_rate);
            StartCoroutine(space());
        }

    }
}
