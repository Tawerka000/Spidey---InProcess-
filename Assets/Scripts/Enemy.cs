using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float health = 1f;
    public GameObject player;
    public Vector3 player_pos;
    public float speed = 1f;
    public GameObject web;
    public GameObject ant;
    private GameObject spider;
    private bool isAlive = true;
    public GameObject SlowEffect;
    private GameObject slow;
    private bool slowed = false;
    private EnemyManager enemyManager;
    private bool removed = false;
    public GameObject bubbles;
    public GameObject BodyTexture;
    void Start()
    {
        enemyManager = FindObjectOfType<EnemyManager>();
        enemyManager.AddEnemy(gameObject);
        spider = GameObject.FindGameObjectWithTag("Spider");
        health = spider.GetComponent<SpawnEnemy>().enemyHealth;
        player = GameObject.FindGameObjectWithTag("Player");
        float randomScale = Random.Range(0.80f, 1.46f);
        web.transform.localScale = new Vector2(randomScale, randomScale);

        float randomRotation = Random.Range(0f, 60f);
        web.transform.rotation = Quaternion.Euler(0f, randomRotation, 0f);

        player_pos = player.transform.position;
    }

    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, player_pos, speed * Time.deltaTime);
        Vector3 difference = player_pos - transform.position;
        difference.Normalize();
        // вычисляемый необходимый угол поворота
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        // Применяем поворот вокруг оси Z
        transform.rotation = Quaternion.Euler(0f, 0f, rotation_z);
        if (slowed)
        {
            slow.transform.position = transform.position - transform.right * 0.18f;
        }
    }
    public void Poisoned()
    {
        
    }
    public void Death(float damage)
    {
        health = health - damage;
        if (health <= 0 && isAlive)
        {
            Remove();
            if (slowed)
            {
                Destroy(slow);
                slowed = false;
            }
            isAlive = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            web.SetActive(  true);
            player.GetComponent<Level>().addXP();
            StartCoroutine(DeleteEnemy());
        }
        if(speed != 1f && isAlive && !slowed)
        {
            slowed = true;  
            slow = Instantiate(SlowEffect, transform.position, Quaternion.identity);
        }
        ant.GetComponent<GetDamage>().Damage(isAlive, damage);
    }
    IEnumerator DeleteEnemy()
    {
        yield return new WaitForSeconds(2f);
        Destroy(ant);
    }
    public void Remove()
    {
        if (!removed)
        {
            enemyManager.RemoveEnemy(gameObject);
            removed = true;
        }
    }
}
