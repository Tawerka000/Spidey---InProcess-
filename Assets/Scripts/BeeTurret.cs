using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeeTurret : MonoBehaviour
{

    public Transform centerPoint;   // Центр окружности
    public float radius = 2f;       // Радиус окружности
    public float speed = 2f;        // Скорость движения

    private float angle;            // Текущий угол

    private bool hasReachedRight;   // Флаг достижения правой точки
    private bool hasReachedLeft;    // Флаг достижения левой точки
    public int scaleChangeNumber = 10;
    public float scaleChangeTimer = 0.05f;
    private bool readyToShoot = true;
    public float timer = 2f;
    public GameObject manager;
    private EnemyManager enemyManager;
    private GameObject enemy;
    private GameObject shot;
    public GameObject poison;
    private void Update()
    {
        enemyManager = manager.GetComponent<EnemyManager>();
        // Вычисляем новую позицию по окружности
        angle += speed * Time.deltaTime * -1f;
        float x = centerPoint.position.x + Mathf.Cos(angle) * radius;
        float y = centerPoint.position.y + Mathf.Sin(angle) * radius;
        Vector2 newPosition = new Vector2(x, y);

        // Перемещаем объект
        transform.position = newPosition;

        // Проверяем достижение крайних точек и вызываем функцию
        if (x > radius - 0.1f && x < radius)
        {
            if (!hasReachedRight)
            {
                hasReachedRight = true;
                StartCoroutine(LeftReflection());
            }
        }
        else if (x < radius * -1f + 0.1f && x > radius * -1f)
        {
            if (!hasReachedLeft)
            {
                hasReachedLeft = true;
                StartCoroutine(RightReflection());
            }
        }
        if (readyToShoot)
        {
            readyToShoot = false;
            StartCoroutine(Shoot());
        }
    }
    IEnumerator Shoot()
    {

        shot = Instantiate(poison, gameObject.transform);
        shot.transform.parent = null;
        yield return new WaitForSeconds(timer);
        readyToShoot = true;
    }
    IEnumerator LeftReflection()
    {
        if (transform.localScale.x > 0)
        {
            float j = 2f / scaleChangeNumber;
            for (int i = 0; i <= scaleChangeNumber; i++)
            {
                transform.localScale -= new Vector3(j, 0, 0);
                yield return new WaitForSeconds(scaleChangeTimer);
            }
        }
        hasReachedRight = false;
    }
        IEnumerator RightReflection()
    {
        if (transform.localScale.x < 0)
        {
            float j = 2f / scaleChangeNumber;
            for (int i = 0; i <= scaleChangeNumber; i++)
            {
                transform.localScale += new Vector3(j, 0, 0);
                yield return new WaitForSeconds(scaleChangeTimer);
            }
        }
        hasReachedLeft = false;
    }
}
