
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int destination;//0 = 1번 구역, 1 = 2번 구역, 2 = 3번 구역, 3 = 4번 구역
    private int shopPage = 1;//상점 페이지
    public int gold;//골드
    public int characterIndex;//현재 장착 캐릭터 인덱스
    public int[] characterCost;//상품별 가격

    private float musicVolume;//음악 볼륨
    private float soundEffectVolume;//효과음 볼륨
    private float playerSpeed = 0.3f;//플레이어 이동속도
    private float Vol = 1f;
    
    private bool shopLoop;
    public bool[] characterPossession;

    private GameObject player;//플레이어 게임오브젝트
    private GameObject standHolder;
    public GameObject standHolderPrefab;//상품 진열대
    public Slider Volume;
    public AudioSource Audio;
    public Slider EffectVolume;
    public AudioSource EffectAudio;



    //구역별 플레이어 좌표
    public Vector2[] playerCoordinate = { new Vector2(-1.34375f, 3.6875f), new Vector2(1.34375f, 3.6875f), new Vector2(-1.34375f, 1.3125f), new Vector2(1.34375f, 1.3125f) };
    private Vector2 shopCoordinate;//상점 좌표

    public Sprite[] characterSprite;//캐릭터 상품 리소스

    void Start()
    {
        startSettings();
    }

    void Update()
    {
        allScene();
        menuScene();
        shopScene();
        gameScene();
        gameOverScene();
        settingScene();
        VolumeSlider();
    }

    //모든 씬에서 실행할 코드들 - 공용
    private void allScene()
    {

    }

    //Menu씬에서 실행할 코드들 - 공용
    private void menuScene()
    {
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            showGold();
        }
    }

    //Shop 씬에서 실행할 코드들 - 공용
    private void shopScene()
    {
        if (SceneManager.GetActiveScene().name == "Shop")
        {
            summonShop();
            lerpShop();
            showGold();
        }
    }

    //Game씬에서 실행할 코드들 - 공용
    private void gameScene()
    {
        if (SceneManager.GetActiveScene().name == "Game")
        {
            movePlayer();
        }
    }

    //GameOver씬에서 실행할 코드들 - 공용
    private void gameOverScene()
    {
        if (SceneManager.GetActiveScene().name == "GameOver")
        {

        }
    }

    //Setting씬에서 실행할 코드들 - 공용
    private void settingScene()
    {
        if (SceneManager.GetActiveScene().name == "Setting")
        {

        }
    }

    //초기 설정 함수 - 공용
    private void startSettings()
    {
        dontDestroyGameManager();
        Vol = PlayerPrefs.GetFloat("Vol", 1f);
        Volume.value = Vol;
        Audio.volume = Volume.value;

    }

    //GameManager 복제 방지 코드 - 윤영주
    private void dontDestroyGameManager()
    {
        var gameManager = GameObject.FindGameObjectsWithTag("GameController");

        if (gameManager.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //플레이어 지정 좌표 이동 함수 + 캐릭터 외관 지정 - 윤영주
    private void movePlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (player.GetComponent<SpriteRenderer>().sprite != characterSprite[characterIndex])
        {
            player.GetComponent<SpriteRenderer>().sprite = characterSprite[characterIndex];
        }
        player.transform.position = Vector3.Lerp(player.transform.position, playerCoordinate[destination], playerSpeed);
    }

    //상점 소환 함수 - 윤영주
    private void summonShop()
    {
        if (!shopLoop)
        {
            if (characterSprite.Length % 9 == 0)
            {
                for (int i = 0; i < characterSprite.Length / 9; i++)
                {
                    for (int a = 0; a < 9; a++)
                    {
                        GameObject Canvas = GameObject.FindWithTag("Canvas");
                        standHolder = Canvas.transform.GetChild(0).transform.GetChild(i).transform.GetChild(a).gameObject;
                        standHolder.transform.Find("Stock").GetComponent<Image>().sprite = characterSprite[a + 9 * i];
                        standHolder.transform.Find("BuyButton").GetComponent<BuyButton>().privateStockIndex = a + 9 * i;
                        standHolder.transform.Find("BuyButton").transform.Find("CostText").GetComponent<Text>().text = "G" + characterCost[a + 9 * i];
                    }
                }
            }
            else
            {
                for (int i = 0; i < characterSprite.Length / 9 + 1; i++)
                {
                    for (int a = 0; a < 9; a++)
                    {
                        GameObject Canvas = GameObject.FindWithTag("Canvas");
                        standHolder = Canvas.transform.GetChild(0).transform.GetChild(i).transform.GetChild(a).gameObject;
                        standHolder.transform.Find("Stock").GetComponent<Image>().sprite = characterSprite[a + 9 * i];
                        standHolder.transform.Find("BuyButton").GetComponent<BuyButton>().privateStockIndex = a + 9 * i;
                        standHolder.transform.Find("BuyButton").transform.Find("CostText").GetComponent<Text>().text = "G" + characterCost[a + 9 * i];
                    }
                }
            }
            shopLoop = true;
        }
    }

    //상점 위치조정 함수 - 윤영주
    private void lerpShop()
    {
        if (characterSprite.Length % 9 == 0)
        {
            GameObject.FindWithTag("PageText").GetComponent<Text>().text = shopPage + " / " + (characterSprite.Length / 9);
        }
        else
        {
            GameObject.FindWithTag("PageText").GetComponent<Text>().text = shopPage + " / " + (characterSprite.Length / 9 + 1);
        }

        shopCoordinate = new Vector2((shopPage - 1) * -1200, 0);
        GameObject.FindWithTag("Canvas").transform.GetChild(0).localPosition = Vector2.Lerp(GameObject.FindWithTag("Canvas").transform.GetChild(0).localPosition, shopCoordinate, 0.2f);
    }

    //골드 현황 표시 함수
    private void showGold()
    {
        Text goldText = GameObject.FindWithTag("GoldText").GetComponent<Text>();
        goldText.text = "G" + gold;
    }

    //1번 버튼 onClick 함수 - 윤영주
    public void button1Click()
    {
        destination = 0;
    }

    //2번 버튼 onClick() 함수 - 윤영주
    public void button2Click()
    {
        destination = 1;
    }

    //3번 버튼 onClick() 함수 - 윤영주
    public void button3Click()
    {
        destination = 2;
    }

    //4번 버튼 onClick() 함수 - 윤영주
    public void button4Click()
    {
        destination = 3;
    }

    //StartButton onClick() 함수 - 윤영주
    public void startButtonClick()
    {
        SceneManager.LoadScene("Game");
    }

    //ShopButton onClick() 함수 - 윤영주
    public void shopButtonClick()
    {
        shopLoop = false;
        SceneManager.LoadScene("Shop");
    }

    //MenuButton onClick() 함수 - 윤영주
    public void menuButtonClick()
    {
        SceneManager.LoadScene("Menu");
    }

    //QuitButton onClick() 함수(게임 종료) - 윤영주
    public void quitButtonClick()
    {
        Application.Quit();
    }

    //SettingButton onClick() 함수 - 윤영주
    public void settingButtonClick()
    {
        SceneManager.LoadScene("Setting");
    }

    //RestartButton onClick() 함수 - 윤영주
    public void restartButtonClick()
    {
        SceneManager.LoadScene("Game");
    }

    //RightButton onClick() 함수 - 윤영주
    public void rightButtonClick()
    {
        if (characterSprite.Length % 9 == 0)
        {
            shopPage = Mathf.Clamp(shopPage + 1, 1, characterSprite.Length / 9);
        }
        else
        {
            shopPage = Mathf.Clamp(shopPage + 1, 1, characterSprite.Length / 9 + 1);
        }
    }

    public void leftButtonClick()
    {
        if (shopPage != 1)
        {
            shopPage -= 1;
        }
    }
    
    public void VolumeSlider()
    {
        Audio.volume = Volume.value;

        Vol = Volume.value;
        PlayerPrefs.SetFloat("Vol", Vol);
    }

}
