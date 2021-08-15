using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnClickAssign : MonoBehaviour
{
    void Start()
    {
        if (gameObject.transform.tag == "StartButton")
        {
            gameObject.GetComponent<Button>().onClick.AddListener((UnityEngine.Events.UnityAction)GameObject.FindWithTag("GameController").GetComponent<GameManager>().startButtonClick);
        }
        else if (gameObject.transform.tag == "ShopButton")
        {
            gameObject.GetComponent<Button>().onClick.AddListener((UnityEngine.Events.UnityAction)GameObject.FindWithTag("GameController").GetComponent<GameManager>().shopButtonClick);
        }
        else if (gameObject.transform.tag == "QuitButton")
        {
            gameObject.GetComponent<Button>().onClick.AddListener((UnityEngine.Events.UnityAction)GameObject.FindWithTag("GameController").GetComponent<GameManager>().quitButtonClick);
        }
        else if (gameObject.transform.tag == "SettingButton")
        {
            gameObject.GetComponent<Button>().onClick.AddListener((UnityEngine.Events.UnityAction)GameObject.FindWithTag("GameController").GetComponent<GameManager>().settingButtonClick);
        }
        else if (gameObject.transform.tag == "RestartButton")
        {
            gameObject.GetComponent<Button>().onClick.AddListener((UnityEngine.Events.UnityAction)GameObject.FindWithTag("GameController").GetComponent<GameManager>().restartButtonClick);
        }
        else if (gameObject.transform.tag == "MenuButton")
        {
            gameObject.GetComponent<Button>().onClick.AddListener((UnityEngine.Events.UnityAction)GameObject.FindWithTag("GameController").GetComponent<GameManager>().menuButtonClick);
        }
        else if (gameObject.transform.tag == "Button1")
        {
            gameObject.GetComponent<Button>().onClick.AddListener((UnityEngine.Events.UnityAction)GameObject.FindWithTag("GameController").GetComponent<GameManager>().button1Click);
        }
        else if (gameObject.transform.tag == "Button2")
        {
            gameObject.GetComponent<Button>().onClick.AddListener((UnityEngine.Events.UnityAction)GameObject.FindWithTag("GameController").GetComponent<GameManager>().button2Click);
        }
        else if (gameObject.transform.tag == "Button3")
        {
            gameObject.GetComponent<Button>().onClick.AddListener((UnityEngine.Events.UnityAction)GameObject.FindWithTag("GameController").GetComponent<GameManager>().button3Click);
        }
        else if (gameObject.transform.tag == "Button4")
        {
            gameObject.GetComponent<Button>().onClick.AddListener((UnityEngine.Events.UnityAction)GameObject.FindWithTag("GameController").GetComponent<GameManager>().button4Click);
        }
        else if (gameObject.transform.tag == "LeftButton")
        {
            gameObject.GetComponent<Button>().onClick.AddListener((UnityEngine.Events.UnityAction)GameObject.FindWithTag("GameController").GetComponent<GameManager>().leftButtonClick);
        }
        else if (gameObject.transform.tag == "RightButton")
        {
            gameObject.GetComponent<Button>().onClick.AddListener((UnityEngine.Events.UnityAction)GameObject.FindWithTag("GameController").GetComponent<GameManager>().rightButtonClick);
        }
    }
}
