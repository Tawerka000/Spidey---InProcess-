using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowingWeb : MonoBehaviour
{
    public float slow = 0.35f;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Enemy")
        {
            if (col.GetComponent<Enemy>().speed == 1f || col.GetComponent<Enemy>().speed == 0.7f)
            {
                col.GetComponent<Enemy>().speed -= slow;
                col.GetComponent<Enemy>().Death(0);
            }   
        }
    }
}
