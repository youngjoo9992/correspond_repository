using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private float pOriginalObsFormationSpeed;
    private float pOriginalObsDelay;

    public bool isGameStarted = true;

    GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        pOriginalObsFormationSpeed = gameManager.originalObsFormationSpeed;
        pOriginalObsDelay = gameManager.originalObsDelay;
        gameManager.showBlocks();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Obstacle>().index == 3)
        {
            if (gameManager.shield)
            {
                gameManager.shield = false;
                Destroy(collision.gameObject);
            }
            else
            {
                gameManager.stamina--;
                Instantiate(gameManager.playerDeathParticle, transform.position, Quaternion.identity);
                gameManager.playSoundEffect(gameManager.playerDeath);
                if (gameManager.stamina == 0)
                {
                    isGameStarted = false;
                }
                else
                {
                    gameManager.shuffleBool = true;
                    gameManager.obstacleFormationSpeed = Mathf.Lerp(gameManager.obstacleFormationSpeed, pOriginalObsFormationSpeed, 0.7f);
                    gameManager.obstacleDelay = gameManager.obstacleFormationSpeed * pOriginalObsDelay / pOriginalObsFormationSpeed;
                    Destroy(collision.gameObject);
                }

            }
        }
        else if (collision.GetComponent<Obstacle>().index == 2)
        {
            gameManager.doubleBool = true;
        }
        else if (collision.GetComponent<Obstacle>().index == 1)
        {
            gameManager.shieldBool = true;
        }
        else if (collision.GetComponent<Obstacle>().index == 0)
        {
            gameManager.shuffleBool = true;
        }
        if (collision.GetComponent<Obstacle>().index != 3)
        {
            Destroy(collision.gameObject);
        }
    }

}
