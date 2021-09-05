using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    public int privateStockIndex = -1;

    GameObject gameManager;

    Text costText;

    GameManager gameManagerCom;

    private void Awake()
    {
        gameManager = GameObject.FindWithTag("GameController");
        gameManagerCom = gameManager.GetComponent<GameManager>();
        costText = transform.GetChild(0).GetComponent<Text>();
    }

    private void Update()
    {
        if (privateStockIndex != -1)
        {
            if (gameManagerCom.characterPossession[privateStockIndex])
            {
                if (gameManagerCom.characterIndex == privateStockIndex)
                {
                    setCostText("EQUIPPED", 55, new Color(100f/255f, 1, 78f/255f));
                }
                else
                {
                    setCostText("EQUIP", 65, new Color(1, 1, 1));
                }
            }
            else
            {
                setCostText("G" + gameManagerCom.GetComponent<GameManager>().characterCost[privateStockIndex], 70, new Color(254f/255f, 1, 78f/255f));
            }
        }
    }

    private void setCostText(string _content, int _size, Color _color)
    {
        costText.text = _content;
        costText.fontSize = _size;
        costText.color = _color;
    }

    public void buyButtonOnClick()
    {
        gameManagerCom.playSoundEffect(gameManagerCom.buttonClick);
        if (gameManagerCom.characterPossession[privateStockIndex])
        {
            if (gameManagerCom.characterIndex != privateStockIndex)
            {
                gameManagerCom.characterIndex = privateStockIndex;
            }
        }
        else
        {
            if (gameManagerCom.gold >= gameManagerCom.characterCost[privateStockIndex])
            {
                gameManagerCom.characterPossession[privateStockIndex] = true;
                gameManagerCom.gold -= gameManagerCom.characterCost[privateStockIndex];
            }
        }
    }
}
