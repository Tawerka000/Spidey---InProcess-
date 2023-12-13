using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector3 mousePosition;
    public GameObject bullet;
    public GameObject gun;
    private Vector3 place;
    public float shotSpeed = 1f;
    private Quaternion rot;
    public bool shooting = false;
    public bool readyToShoot = true;
    public GameObject parentForShield;
    private float rotation_z;
    private GameObject player;
    private GameObject shield;
    public bool ShieldIsActive = false;
    private int numOfBullets = 1;
    void Update()
    {
        mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        player = GameObject.FindGameObjectWithTag("Player");
        shotSpeed = player.GetComponent<Level>().shotSpeed;
        
        Vector3 difference = mousePosition - player.transform.position;
        difference.Normalize();
        
        rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        
        player.transform.rotation = Quaternion.RotateTowards(player.transform.rotation, Quaternion.Euler(0f, 0f, rotation_z + 180f), 250f * Time.deltaTime);

        if (Input.GetMouseButtonDown(0) && readyToShoot)
        {
            shooting = true;
            StartCoroutine(Shoot());
        }
        if (Input.GetMouseButton(0))
        {
            shooting = true;
        }
        if (Input.GetMouseButtonUp(0))
        {
            shooting = false;
        }
    }
    IEnumerator Shoot()
    {
        readyToShoot = false;
        place = gun.transform.position;
        numOfBullets = player.GetComponent<Level>().bullets;

        switch (numOfBullets)
        {
            case 1:
                rot = Quaternion.Euler(0f, 0f, rotation_z);
                Instantiate(bullet, place, rot);
                break;
            case 2:
                rot = Quaternion.Euler(0f, 0f, rotation_z);
                GameObject bullet1 = Instantiate(bullet, place, rot);
                GameObject bullet2 = Instantiate(bullet, place, rot);
                bullet1.transform.Translate(Vector3.up * 0.05f, Space.Self);
                bullet2.transform.Translate(Vector3.down * 0.05f, Space.Self);
                break;
            case 3:
                rot = Quaternion.Euler(0f, 0f, rotation_z);
                Quaternion rot1 = Quaternion.Euler(0f, 0f, rotation_z - 7f);
                Quaternion rot2 = Quaternion.Euler(0f, 0f, rotation_z + 7f);
                Instantiate(bullet, place, rot1);
                Instantiate(bullet, place, rot2);
                Instantiate(bullet, place, rot);
                break;
            case 4:
                rot = Quaternion.Euler(0f, 0f, rotation_z);
                GameObject bullet3 = Instantiate(bullet, place, rot);
                GameObject bullet4 = Instantiate(bullet, place, rot);
                GameObject bullet5 = Instantiate(bullet, place, rot);
                GameObject bullet6 = Instantiate(bullet, place, rot);
                bullet3.transform.Translate(Vector3.up * 0.05f, Space.Self);
                bullet4.transform.Translate(Vector3.down * 0.05f, Space.Self);
                bullet5.transform.Translate(Vector3.up * 0.15f, Space.Self);
                bullet6.transform.Translate(Vector3.down * 0.15f, Space.Self);
                break;
            case 5:
                rot = Quaternion.Euler(0f, 0f, rotation_z);
                Quaternion rot3 = Quaternion.Euler(0f, 0f, rotation_z - 7f);
                Quaternion rot4 = Quaternion.Euler(0f, 0f, rotation_z + 7f);
                Quaternion rot5 = Quaternion.Euler(0f, 0f, rotation_z - 14f);
                Quaternion rot6 = Quaternion.Euler(0f, 0f, rotation_z + 14f);
                Instantiate(bullet, place, rot); 
                Instantiate(bullet, place, rot3);
                Instantiate(bullet, place, rot4);
                Instantiate(bullet, place, rot5);
                Instantiate(bullet, place, rot6);
                break;
        }
        yield return new WaitForSeconds(shotSpeed);
        readyToShoot = true;
        if (shooting)
            StartCoroutine(Shoot());
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (ShieldIsActive)
            {
                print("SHIELDisBROKEN!!!!");
                ShieldIsActive = false;
                float damage = collision.collider.transform.gameObject.GetComponent<Enemy>().health;
                collision.collider.transform.gameObject.GetComponent<Enemy>().Death(damage);
                shield = GameObject.FindGameObjectWithTag("Shield");
                shield.transform.SetParent(null, true);
                shield.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                StartCoroutine(DeleteShield());
            }
            else
                print("dead...");
        }    
    }
    IEnumerator DeleteShield()
    {
        yield return new WaitForSeconds(2f);
        Destroy(shield);
    }
}
