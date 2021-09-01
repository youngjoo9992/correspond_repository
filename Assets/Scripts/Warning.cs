using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Warning : MonoBehaviour
{
    public int index;
    public int warningItemIndex;

    private float delay;

    private bool timeUp;

    private GameManager gameManager;

    public GameObject obstaclePref;

    public Sprite[] obstacleSprites;

    void Start()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        delay = gameManager.obstacleDelay;
    }

    void Update()
    {
        if (timeUp)
        {
            GameObject obstacle = Instantiate(obstaclePref, new Vector2(transform.position.x + transform.position.x / Mathf.Abs(transform.position.x) * 2.5f, transform.position.y), Quaternion.identity);
            obstacle.GetComponent<SpriteRenderer>().sprite = obstacleSprites[warningItemIndex];
            obstacle.GetComponent<Obstacle>().index = warningItemIndex;
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
