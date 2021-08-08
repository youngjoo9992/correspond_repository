using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject player;

    public Vector2[] playerCoordinate = { new Vector2(-1.34375f, 3.6875f), new Vector2(1.34375f, 3.6875f), new Vector2(-1.34375f, 1.3125f), new Vector2(1.34375f, 1.3125f) };
    
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void button1Click()
    {
        player.transform.position = playerCoordinate[0];
    }

    public void button2Click()
    {
        player.transform.position = playerCoordinate[1];
    }

    public void button3Click()
    {
        player.transform.position = playerCoordinate[2];
    }

    public void button4Click()
    {
        player.transform.position = playerCoordinate[3];
    }

}
