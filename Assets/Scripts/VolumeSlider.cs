using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSlider : MonoBehaviour
{
    void Awake()
    {
        if (gameObject.tag == "MusicVolumeSlider")
        {
            gameObject.GetComponent<Slider>().value = GameObject.FindWithTag("GameController").GetComponent<GameManager>().musicVolume;
        }
        else if (gameObject.tag == "EffectVolumeSlider")
        {
            gameObject.GetComponent<Slider>().value = GameObject.FindWithTag("GameController").GetComponent<GameManager>().soundEffectVolume;
        }
    }
}
