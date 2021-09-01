using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    private int direction = 1;
    public int index;

    private float start;
    private float max;
    public float obstacleSpeed = 0.1f;

    private bool sign;

    void Start()
    {
        start = transform.position.x;
        if (transform.position.x > 0)
        {
            sign = true;
        }
        else
        {
            sign = false;
        }
    }

    void Update()
    {
        if (sign)
        {
            transform.Translate(new Vector2(obstacleSpeed * -transform.position.x / Mathf.Abs(transform.position.x) * direction, 0));
            if (transform.position.x < start - 2.5f)
            {
                direction *= -1;
            }
            if (transform.position.x > start)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            transform.Translate(new Vector2(obstacleSpeed * -transform.position.x / Mathf.Abs(transform.position.x) * direction, 0));
            if (transform.position.x > start + 2.5f)
            {
                direction *= -1;
            }
            if (transform.position.x < start)
            {
                Destroy(gameObject);
            }
        }
    }
}
