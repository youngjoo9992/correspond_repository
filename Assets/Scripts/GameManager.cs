using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int destination;//0 = 1, 1 = 2, 2 = 3, 3 = 4

    public float playerSpeed;

    public GameObject player;

    public Vector2[] playerCoordinate = { new Vector2(-1.34375f, 3.6875f), new Vector2(1.34375f, 3.6875f), new Vector2(-1.34375f, 1.3125f), new Vector2(1.34375f, 1.3125f) };
    
    void Start()
    {
        
    }

    void Update()
    {
        gameScene();
    }
    
    private void gameScene()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            movePlayer();
        }
    }

    private void movePlayer()
    {
        player.transform.position = Vector3.Lerp(player.transform.position, playerCoordinate[destination], playerSpeed);
    }

    public void button1Click()
    {
        //player.transform.position = Vector3.Lerp(player.transform.position, playerCoordinate[0], playerSpeed);
        destination = 0;
    }

    public void button2Click()
    {
        //player.transform.position = playerCoordinate[1];
        destination = 1;
    }

    public void button3Click()
    {
        //player.transform.position = playerCoordinate[2];
        destination = 2;
    }

    public void button4Click()
    {
        //player.transform.position = playerCoordinate[3];
        destination = 3;
    }

}
