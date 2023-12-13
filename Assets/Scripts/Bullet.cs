using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float Speed = 0.1f;
    public float delete_speed = 3f;
    public float damage = 1.5f;
    public GameObject player;
    public float slow = 0.3f;
    private bool slowBullet = false;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        damage = player.GetComponent<Level>().playerDamage;
        slowBullet = player.GetComponent<Level>().slowBullets;
        StartCoroutine(DeleteObj());
    }
    IEnumerator DeleteObj()
    {
        yield return new WaitForSeconds(delete_speed);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(Vector2.right * Speed);
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (slowBullet && collision.collider.GetComponent<Enemy>().speed == 1f || collision.collider.GetComponent<Enemy>().speed == 0.65f)
                collision.collider.GetComponent<Enemy>().speed -= slow;
            collision.collider.GetComponent<Enemy>().Death(damage);
            Destroy(gameObject);
        }
    }
    //.collider.transform.gameObject.
}
