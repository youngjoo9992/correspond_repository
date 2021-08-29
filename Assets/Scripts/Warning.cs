using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warning : MonoBehaviour
{
    public int index;

    private float delay;

    private bool timeUp;

    private GameManager gameManager;

    public GameObject Obstacle;

    void Start()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        delay = gameManager.obstacleDelay;
    }

    void Update()
    {
        if (timeUp)
        {
            Instantiate(Obstacle, new Vector2(transform.position.x + transform.position.x / Mathf.Abs(transform.position.x) * 2.5f, transform.position.y), Quaternion.identity);
            Destroy(gameObject);
        }
        else
        {
            StartCoroutine(countDelay());
        }
    }

    IEnumerator countDelay()
    {
        yield return new WaitForSeconds(delay);
        timeUp = true;
    }

}
