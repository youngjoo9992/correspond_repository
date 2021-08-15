using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour
{
    public int privateStockIndex;

    GameObject gameManager;

    GameManager gameManagerCom;

    private void Start()
    {
        gameManager = GameObject.FindWithTag("GameController");
        gameManagerCom = gameManager.GetComponent<GameManager>();
    }

    private void Update()
    {
        if (gameManagerCom.characterPossession[privateStockIndex])
        {
            if (gameManagerCom.characterIndex == privateStockIndex)
            {
                transform.GetChild(0).GetComponent<Text>().text = "O";
            }
            else
            {
                transform.GetChild(0).GetComponent<Text>().text = "EQUIP";
            }
        }
    }

    public void buyButtonOnClick()
    {
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
