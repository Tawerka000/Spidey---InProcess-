using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private List<GameObject> enemies = new List<GameObject>();

    public GameObject GetNearestEnemy()
    {
        GameObject nearestEnemy = null;
        float shortestDistance = Mathf.Infinity;

        foreach (var enemy in enemies)
        {
            if (enemy == null) continue; // Пропускаем нулевые значения

            float distance = Vector3.Distance(gameObject.transform.position, enemy.transform.position);

            if (distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestEnemy = enemy;
            }
        }
        return nearestEnemy;
    }
        public void AddEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
    }

    public void RemoveEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
    }
}
