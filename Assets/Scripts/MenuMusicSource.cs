using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuMusicSource : MonoBehaviour
{
    void Start()
    {
        GameObject[] menuSource = GameObject.FindGameObjectsWithTag("MusicSource");
        if (menuSource.Length > 1)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        
    }
}
