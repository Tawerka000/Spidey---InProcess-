using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonProjectile : MonoBehaviour
{
    public float Speed = 5f;
    public float delete_speed = 5f;
    public GameObject target;
    public float hitDistance = 1f; 
    private EnemyManager enemyManager;
    public Color newColor;
    void Start()
    {
        enemyManager = FindObjectOfType<EnemyManager>();
        target = enemyManager.GetNearestEnemy();
        if (target == null)
        {
            Destroy(gameObject);
        }
        else
        {
            target.GetComponent<Enemy>().Remove();
            StartCoroutine(DeleteObj());
        }
    }
    IEnumerator DeleteObj()
    {
        yield return new WaitForSeconds(delete_speed);
        Destroy(gameObject);
    }
    private void FixedUpdate()
    {
        if (target != null)
        {
            float distance = Vector3.Distance(transform.position, target.transform.position);

            if (distance <= hitDistance)
            {
                // Логика поражения врага
                Enemy enemy = target.GetComponent<Enemy>();
                enemy.health = 0.1f;
                Destroy(gameObject);
                enemy.bubbles.SetActive(true);
                enemy.BodyTexture.GetComponent<SpriteRenderer>().material.color = newColor;
            }
            else
            {
                // Перемещение объекта к цели
                transform.position = Vector2.MoveTowards(transform.position, target.transform.position, Speed * Time.deltaTime);

                // Поворот объекта в сторону цели
                Vector2 direction = target.transform.position - transform.position;
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}

