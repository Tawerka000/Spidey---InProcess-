using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeLegMover : MonoBehaviour
{
    public Transform legTarget;
    public float legMoveDist;
    public Vector3 halfWayPoint;
    public float legMovementSpeed = 10f;
    public int posIndex = 0;
    public Vector3 targetPoint;
    Vector3 oldPos;


    void Update()
    {
        //if (Vector2.Distance(transform.position, legTarget.position) > legMoveDist)
        //    legTarget.position = transform.position;

        if (Vector2.Distance(transform.position, legTarget.position) > legMoveDist && posIndex == 0)
        {
            targetPoint = transform.position;
            halfWayPoint = (targetPoint + legTarget.position) / 2;
            posIndex = 1;
        }

        else if (posIndex == 1)
        {
            legTarget.position = Vector3.Lerp(legTarget.position, halfWayPoint, legMovementSpeed * Time.deltaTime);
            if (Vector2.Distance(legTarget.position, halfWayPoint) <= 0.1f)
            {
                posIndex = 2;
            }
        }

        else if (posIndex == 2)
        {
            legTarget.position = Vector3.Lerp(legTarget.position, targetPoint, legMovementSpeed * Time.deltaTime);
            if (Vector2.Distance(legTarget.position, targetPoint) < 0.1f)
            {
                posIndex = 0;
            }
        }
    }

}