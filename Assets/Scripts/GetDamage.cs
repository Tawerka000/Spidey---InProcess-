using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDamage : MonoBehaviour
{
    public Animator Anim;
    public void Damage(bool isAlive, float damage)
    {
        if(isAlive && damage != 0)
            Anim.SetTrigger("Damage");
    }
}
