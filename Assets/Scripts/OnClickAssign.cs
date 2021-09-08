using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnClickAssign : MonoBehaviour
{
    void Start()
    {
        GameManager gameManager = GameObject.FindWithTag("GameController").GetComponent<GameManager>();
        if (gameObject.CompareTag("StartButton"))
        {
            gameObject.GetComponent<Button>().onClick.AddListener(gameManager.startButtonClick);
        }
        else if (gameObject.CompareTag("ShopButton"))
        {
            gameObject.GetComponent<Button>().onClick.AddListener(gameManager.shopButtonClick);
        }
        else if (gameObject.CompareTag("QuitButton"))
        {
            gameObject.GetComponent<Button>().onClick.AddListener(gameManager.quitButtonClick);
        }
        else if (gameObject.CompareTag("SettingButton"))
        {
            gameObject.GetComponent<Button>().onClick.AddListener(gameManager.settingButtonClick);
        }
        else if (gameObject.CompareTag("RestartButton"))
        {
            gameObject.GetComponent<Button>().onClick.AddListener(gameManager.restartButtonClick);
        }
        else if (gameObject.CompareTag("MenuButton"))
        {
            gameObject.GetComponent<Button>().onClick.AddListener(gameManager.menuButtonClick);
        }
        else if (gameObject.CompareTag("Button1"))
        {
            gameObject.GetComponent<Button>().onClick.AddListener(gameManager.button1Click);
        }
        else if (gameObject.CompareTag("Button2"))
        {
            gameObject.GetComponent<Button>().onClick.AddListener(gameManager.button2Click);
        }
        else if (gameObject.CompareTag("Button3"))
        {
            gameObject.GetComponent<Button>().onClick.AddListener(gameManager.button3Click);
        }
        else if (gameObject.CompareTag("Button4"))
        {
            gameObject.GetComponent<Button>().onClick.AddListener(gameManager.button4Click);
        }
        else if (gameObject.CompareTag("LeftButton"))
        {
            gameObject.GetComponent<Button>().onClick.AddListener(gameManager.leftButtonClick);
        }
        else if (gameObject.CompareTag("RightButton"))
        {
            gameObject.GetComponent<Button>().onClick.AddListener(gameManager.rightButtonClick);
        }
        else if (gameObject.CompareTag("HelpButton"))
        {
            gameObject.GetComponent<Button>().onClick.AddListener(gameManager.helpButtonClick);
        }
        else if (gameObject.CompareTag("DevelopersButton"))
        {
            gameObject.GetComponent<Button>().onClick.AddListener(gameManager.developersButtonClick);
        }
    }
}
