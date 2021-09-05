
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;

public class GameManager : MonoBehaviour
{
    private int destination;//0 = 1번 구역, 1 = 2번 구역, 2 = 3번 구역, 3 = 4번 구역
    private int shopPage = 1;//상점 페이지
    private int gameGold;
    private int score;
    private int highScore;
    private int scoreMultiplier = 1;
    private int shieldNum;  
    private int[] obstacleIndex = { 0, 0, 0 };
    private int[] obsBlackList = { 4, 4, 4 };
    public int stamina;
    public int gold;//골드
    public int characterIndex;//현재 장착 캐릭터 인덱스
    public int itemIndex;
    public int[] characterCost;//상품별 가격
   
    private float playerSpeed = 0.3f;//플레이어 이동속도
    public float obstacleFormationSpeed;
    public float obstacleDelay;
    public float musicVolume = 1;//음악 볼륨
    public float soundEffectVolume = 1;//효과음 볼륨
    public float originalObsFormationSpeed;
    public float originalObsDelay;

    private bool shopLoop = false;
    private bool allowObstacle;
    private bool allowObstacleCoroutine;
    private bool endGame;
    private bool endGameCor;
    private bool loadSceneBool;
    public bool shield;
    public bool shieldBool;
    public bool doubleBool;
    public bool shuffleBool;
    public bool[] characterPossession;

    private string sceneName;

    private int[] shuffleBlackList = {4, 4, 4, 4};

    public GameObject player;//플레이어 게임오브젝트
    private GameObject standHolder;
    private GameObject doubleItem;
    private GameObject shieldItem;
    private GameObject menuAudioSource;
    public GameObject[] blocks = { null, null, null, null };
    public GameObject button1;
    public GameObject button2;
    public GameObject button3;
    public GameObject button4;
    public GameObject stamina1;
    public GameObject stamina2;
    public GameObject stamina3;
    public GameObject standHolderPrefab;//상품 진열대
    public GameObject obstacle;
    public GameObject warning;
    public GameObject playerDeathParticle;
    public GameObject soundEffectSource;

    public Slider musicVolumeSlider;
    public Slider effectVolumeSlider;

    private Text scoreText;
    private Text resultText;


    private Vector2 shopCoordinate;//상점 좌표
    public Vector2[] buttonCoordinates = {new Vector2(-255, -255), new Vector2(255, -255), new Vector2(-255, -710), new Vector2(255, -710)};
    //구역별 플레이어 좌표
    public Vector2[] playerCoordinate = { new Vector2(-1.34375f, 3.6875f), new Vector2(1.34375f, 3.6875f), new Vector2(-1.34375f, 1.3125f), new Vector2(1.34375f, 1.3125f) };

    public Sprite[] characterSprite;//캐릭터 상품 리소스

    public AudioClip buttonClick;
    public AudioClip playerDeath;

    void Start()
    {
        startSettings();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            button1Click();
        }
        allScene();
        menuScene();
        shopScene();
        gameScene();
        gameOverScene();
        settingScene();
    }

    //모든 씬에서 실행할 코드들 - 공용
    private void allScene()
    {
        gameSave();
        setListenerVolume();
        loadScene();
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
            formWarning();
            gameEnd();
            showScore();
            useItem();
            showItemUsing();
            showStamina();
        }
    }

    //GameOver씬에서 실행할 코드들 - 공용
    private void gameOverScene()
    {
        if (SceneManager.GetActiveScene().name == "GameOver")
        {
            showResult();
        }
    }

    //Setting씬에서 실행할 코드들 - 공용
    private void settingScene()
    {
        if (SceneManager.GetActiveScene().name == "Setting")
        {
            setVolume();
        }
    }

    //초기 설정 함수 - 공용
    private void startSettings()
    {
        gameLoad();
        dontDestroyGameManager();
        /*Vol = PlayerPrefs.GetFloat("Vol", 1f);
        Volume.value = Vol;
        Audio.volume = Volume.value;*/

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
        if (player.GetComponent<Player>().isGameStarted)
        {
            player.transform.position = Vector3.Lerp(player.transform.position, playerCoordinate[destination], playerSpeed);
        }
    }

    //상점 소환 함수 - 윤영주
    private void summonShop()
    {
        if (!shopLoop)
        {
            /*if (characterSprite.Length % 9 == 0)
            {
                for (int i = 0; i < characterSprite.Length / 9; i++)
                {
                    for (int a = 0; a < 9; a++)
                    {
                        standHolder = GameObject.FindWithTag("StandHolderHolder").transform.Find("StandHolder (" + i + ")").transform.Find("Stand (" + a + ")").gameObject;
                        standHolder.transform.Find("Stock").GetComponent<Image>().sprite = characterSprite[a + 9 * i];
                        standHolder.transform.Find("BuyButton").GetComponent<BuyButton>().privateStockIndex = a + 9 * i;
                        standHolder.transform.Find("BuyButton").transform.Find("CostText").GetComponent<Text>().text = "G" + characterCost[a + 9 * i];
                        Debug.Log((a + 9 * i).ToString());
                    }
                }
                GameObject[] stocks = GameObject.FindGameObjectsWithTag("Stock");
                GameObject[] buyButtons = GameObject.FindGameObjectsWithTag("BuyButton");
                for (int i = 0; i < characterSprite.Length; i++)
                {
                    stocks[i].GetComponent<Image>().sprite = characterSprite[i];
                    buyButtons[i].GetComponent<BuyButton>().privateStockIndex = i;
                }
            }
            else
            {
                for (int i = 0; i < characterSprite.Length / 9 + 1; i++)
                {
                    for (int a = 0; a < 9; a++)
                    {
                        standHolder = GameObject.FindWithTag("StandHolderHolder").transform.Find("StandHolder (" + i + ")").Find("Stand (" + a + ")").gameObject;
                        standHolder.transform.Find("Stock").GetComponent<Image>().sprite = characterSprite[a + 9 * i];
                        standHolder.transform.Find("BuyButton").GetComponent<BuyButton>().privateStockIndex = a + 9 * i;
                        standHolder.transform.Find("BuyButton").transform.Find("CostText").GetComponent<Text>().text = "G" + characterCost[a + 9 * i];
                    }
                }
            }*/
            GameObject[] stocks = GameObject.FindGameObjectsWithTag("Stock");
            GameObject[] buyButtons = GameObject.FindGameObjectsWithTag("BuyButton");
            for (int i = 0; i < characterSprite.Length; i++)
            {
                stocks[i].GetComponent<Image>().sprite = characterSprite[i];
                buyButtons[i].GetComponent<BuyButton>().privateStockIndex = i;
            }
            shopLoop = true;
        }
        if (GameObject.FindWithTag("BuyButton").GetComponent<BuyButton>().privateStockIndex == -1)
        {
            shopLoop = false;
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

    //볼륨 설정 함수
    private void setVolume()
    {
        musicVolumeSlider = GameObject.FindWithTag("MusicVolumeSlider").GetComponent<Slider>();
        effectVolumeSlider = GameObject.FindWithTag("EffectVolumeSlider").GetComponent<Slider>();
        if (musicVolumeSlider != null)
        {
            musicVolume = musicVolumeSlider.value;
            soundEffectVolume = effectVolumeSlider.value;
        }
    }

    //게임리스너 볼륨 설정 함수
    private void setListenerVolume()
    {
        GameObject[] audio = GameObject.FindGameObjectsWithTag("SoundEffectSource");
        for (int i = 0; i < audio.Length; i++)
        {
            if (!audio[i].GetComponent<AudioSource>().isPlaying)
            {
                Destroy(audio[i]);
            }
        }
        if (SceneManager.GetActiveScene().name == "Game")
        {
            if (player != null && player.GetComponent<Player>().isGameStarted)
            {
                GameObject.FindWithTag("MusicSource").GetComponent<AudioSource>().volume = musicVolume;
            }
            else
            {
                GameObject.FindWithTag("MusicSource").GetComponent<AudioSource>().volume = 0f;
            }
        }
        else if (menuAudioSource != null)
        {
            GameObject.FindWithTag("MusicSource").GetComponent<AudioSource>().volume = musicVolume;
        }
        if (SceneManager.GetActiveScene().name == "Menu")
        {
            if (menuAudioSource == null)
            {
                menuAudioSource = GameObject.FindWithTag("MusicSource");
                if (menuAudioSource != null)
                {
                    DontDestroyOnLoad(menuAudioSource);
                }
            }
        }
    }

    //경고 표시 함수 - 윤영주
    private void formWarning()
    {
        if (allowObstacle)
        {
            obstacleIndex = new int[]{ Random.Range(0, 4), Random.Range(0, 4), Random.Range(0, 4) };
            for (int i = 0; i < 3; i++)
            {
                if (obsBlackList.Contains(obstacleIndex[i]))
                {

                }
                else
                {
                    GameObject warningPref = Instantiate(warning, playerCoordinate[obstacleIndex[i]], Quaternion.identity);
                    warningPref.GetComponent<Warning>().index = obstacleIndex[i];
                    itemIndex = Random.Range(0, 101);
                    if (itemIndex <= 5)
                    {
                        itemIndex = 0;
                    }
                    else if (itemIndex <= 10)
                    {
                        itemIndex = 1;
                    }
                    else if (itemIndex <= 15)
                    {
                        itemIndex = 2;
                    }
                    else
                    {
                        itemIndex = 3;
                    }
                    warningPref.GetComponent<Warning>().warningItemIndex = itemIndex;
                    obsBlackList[i] = obstacleIndex[i];
                }
            }
            obsBlackList = new int[] { 4, 4, 4 };
            allowObstacle = false;
            obstacleFormationSpeed *= 0.97f;
            obstacleDelay = obstacleFormationSpeed * originalObsDelay / originalObsFormationSpeed;
            score += scoreMultiplier;
            if (score > highScore)
            {
                highScore = score;
            }
        }
        else if (!allowObstacleCoroutine)
        {
            StartCoroutine(setAllowObstacle());
        }
    }
    
    //게임 종료 함수 - 윤영주
    private void gameEnd()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }
        if (player != null && !player.GetComponent<Player>().isGameStarted)
        {
            if (endGame)
            {
                GameObject[] obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
                for (int i = 0; i < obstacles.Length; i++)
                {
                    Destroy(obstacles[i]);
                }
                obstacleFormationSpeed = originalObsFormationSpeed;
                obstacleDelay = originalObsDelay;
                destination = 0;
                StopAllCoroutines();
                shield = false;
                shieldBool = false;
                doubleBool = false;
                shuffleBool = false;
                scoreMultiplier = 1;
                stamina = 3;
                gameGold = score;
                gold += gameGold;
                endGameCor = false;
                endGame = false;
                SceneManager.LoadScene("GameOver");
            }
            else if (!endGameCor)
            {
                //playSoundEffect(playerDeath);
                endGameCor = true;
                SpriteRenderer playerRenderer = player.GetComponent<SpriteRenderer>();
                playerRenderer.color = new Color(playerRenderer.color.r, playerRenderer.color.g, playerRenderer.color.b, 0f);
                //Instantiate(playerDeathParticle, player.transform.position, Quaternion.identity);
                var obstacles = GameObject.FindGameObjectsWithTag("Obstacle");
                for (int i = 0; i < obstacles.Length; i++)
                {
                    obstacles[i].GetComponent<Obstacle>().obstacleSpeed = 0f;
                }
                var warnings = GameObject.FindGameObjectsWithTag("Warning");
                for (int i = 0; i < warnings.Length; i++)
                {
                    Destroy(warnings[i]);
                }
                
                StopAllCoroutines();
                StartCoroutine(endGameCoroutine());
            }
        }
    }

    //스코어 표시 함수 - 윤영주
    private void showScore()
    {
        if (scoreText == null)
        {
            scoreText = GameObject.FindWithTag("ScoreText").GetComponent<Text>();
        }
        if (scoreText != null)
        {
            scoreText.text = "SCORE: " + score;
        }
    }

    //게임 결과 표시 함수 - 윤영주
    private void showResult()
    {
        if (resultText == null)
        {
            resultText = GameObject.FindWithTag("ResultText").GetComponent<Text>();
        }
        if (resultText != null)
        {
            resultText.text = "SCORE: " + score + "\n" + "HIGH: " + highScore + "\n" + "GOLD: " + gameGold;
        }
    }

    private void useItem()
    {
        if (button1 == null || button2 == null || button3 == null || button4 == null)
        {
            button1 = GameObject.FindWithTag("Button1");
            button2 = GameObject.FindWithTag("Button2");
            button3 = GameObject.FindWithTag("Button3");
            button4 = GameObject.FindWithTag("Button4");
        }
        if (shuffleBool)
        {
            shuffleBool = false;
            int currentNumber = Random.Range(0, 4);
            for (int i = 0; i < 4;)
            {
                if (shuffleBlackList.Contains(currentNumber))
                {
                    currentNumber = Random.Range(0, 4);
                }
                else
                {
                    shuffleBlackList[i] = currentNumber;
                    i++;
                }
            }
            button1.GetComponent<GameButtons>().buttonDestination = buttonCoordinates[shuffleBlackList[0]];
            button2.GetComponent<GameButtons>().buttonDestination = buttonCoordinates[shuffleBlackList[1]];
            button3.GetComponent<GameButtons>().buttonDestination = buttonCoordinates[shuffleBlackList[2]];
            button4.GetComponent<GameButtons>().buttonDestination = buttonCoordinates[shuffleBlackList[3]];
            shuffleBlackList = new int[] { 4, 4, 4, 4 };
            showBlocks();
        }
        if (shieldBool)
        {
            shieldBool = false;
            shieldNum++;
            shield = true;
            StartCoroutine(shieldItemCor());

        }
        if (doubleBool)
        {
            scoreMultiplier++;
            doubleBool = false;
            StartCoroutine(doubleItemCor());
        }
    }

    private void showItemUsing()
    {
        if (doubleItem == null || shieldItem == null)
        {
            doubleItem = GameObject.FindWithTag("Double");
            shieldItem = GameObject.FindWithTag("Shield");
        }
        if (scoreMultiplier != 1)
        {
            doubleItem.SetActive(true);
        }
        else
        {
            doubleItem.SetActive(false);
        }
        if (shield)
        {
            shieldItem.SetActive(true);
        }
        else
        {
            shieldItem.SetActive(false);
        }
    }

    private void showStamina()
    {
        if (stamina1 == null || stamina2 == null || stamina3 == null)
        {
            stamina1 = GameObject.FindWithTag("Stamina1");
            stamina2 = GameObject.FindWithTag("Stamina2");
            stamina3 = GameObject.FindWithTag("Stamina3");
        }
        if (stamina == 2)
        {
            stamina3.SetActive(false);
        }
        if (stamina == 1)
        {
            stamina2.SetActive(false);
        }
        if (stamina == 0)
        {
            stamina1.SetActive(false);
        }
    }

    private void loadScene()
    {
        if (loadSceneBool)
        {
            SceneManager.LoadScene(sceneName);
            loadSceneBool = false;
        }
    }

    private void gameSave()
    {
        if (!PlayerPrefs.HasKey("gold"))
        {
            PlayerPrefs.SetInt("gold", gold);
            PlayerPrefs.SetInt("characterIndex", characterIndex);
            PlayerPrefs.SetInt("highScore", highScore);
            PlayerPrefs.SetFloat("musicVolume", musicVolume);
            PlayerPrefs.SetFloat("soundEffectVolume", soundEffectVolume);
            for (int i = 0; i < characterPossession.Length; i++)
            {
                if (characterPossession[i])
                {
                    PlayerPrefs.SetInt("possesion" + i, 1);
                }
                else
                {
                    PlayerPrefs.SetInt("possesion" + i, 0);
                }
            }
            PlayerPrefs.Save();
        }
        else
        {
            saveIntValue("gold", PlayerPrefs.GetInt("gold"), gold);
            saveIntValue("characterIndex", PlayerPrefs.GetInt("characterIndex"), characterIndex);
            saveIntValue("highScore", PlayerPrefs.GetInt("highScore"), highScore);
            saveFloatValue("musicVolume", PlayerPrefs.GetInt("musicVolume"), musicVolume);
            saveFloatValue("soundEffectVolume", PlayerPrefs.GetInt("soundEffectVolume"), soundEffectVolume);
            for (int i = 0; i < characterPossession.Length; i++)
            {
                if (characterPossession[i] && PlayerPrefs.GetInt("possesion" + i) == 0)
                {
                    PlayerPrefs.SetInt("possesion" + i, 1);
                }
            }
        }
    }

    private void gameLoad()
    {
        if (!PlayerPrefs.HasKey("gold"))
        {
            return;
        }
        gold = PlayerPrefs.GetInt("gold");
        characterIndex = PlayerPrefs.GetInt("characterIndex");
        highScore = PlayerPrefs.GetInt("highScore");
        musicVolume = PlayerPrefs.GetFloat("musicVolume");
        soundEffectVolume = PlayerPrefs.GetFloat("soundEffectVolume");
        for (int i = 0; i < characterPossession.Length; i++)
        {
            if (PlayerPrefs.GetInt("possesion" + i) == 1)
            {
                characterPossession[i] = true;
            }
            else
            {
                characterPossession[i] = false;
            }
        }
    }

    private void saveIntValue(string _name, int _value, int _compare)
    {
        if (_value != _compare)
        {
            PlayerPrefs.SetInt(_name, _compare);
        }
    }

    private void saveFloatValue(string _name, float _value, float _compare)
    {
        if (_value != _compare)
        {
            PlayerPrefs.SetFloat(_name, _compare);
        }
    }

    public void playSoundEffect(AudioClip _resource)
    {
        AudioSource audioSource = Instantiate(soundEffectSource, Vector2.zero, Quaternion.identity).GetComponent<AudioSource>();
        audioSource.volume = soundEffectVolume;
        audioSource.clip = _resource;
        audioSource.Play();
    }

    public void showBlocks()
    {
        if (blocks[0] == null)
        {
            for (int i = 0; i < 4; i++)
            {
                blocks[i] = GameObject.FindWithTag("Block" + (i + 1));
            }
        }
        if (blocks[0] != null)
        {
            for (int i = 1; i < 5; i++)
            {
                blocks[i - 1].SetActive(true);
                blocks[i - 1].GetComponent<Text>().color = new Color(blocks[i - 1].GetComponent<Text>().color.r, blocks[i - 1].GetComponent<Text>().color.g, blocks[i - 1].GetComponent<Text>().color.b, 1);
                StartCoroutine(fadeBlocksOut(i - 1));
            }
        }
    }

    //1번 버튼 onClick 함수 - 윤영주
    public void button1Click()
    {
        destination = 0;
        playSoundEffect(buttonClick);
    }

    //2번 버튼 onClick() 함수 - 윤영주
    public void button2Click()
    {
        destination = 1;
        playSoundEffect(buttonClick);
    }

    //3번 버튼 onClick() 함수 - 윤영주
    public void button3Click()
    {
        destination = 2;
        playSoundEffect(buttonClick);
    }

    //4번 버튼 onClick() 함수 - 윤영주
    public void button4Click()
    {
        destination = 3;
        playSoundEffect(buttonClick);
    }

    //StartButton onClick() 함수 - 윤영주
    public void startButtonClick()
    {
        playSoundEffect(buttonClick);
        sceneName = "Game";
        allowObstacle = true;
        allowObstacleCoroutine = false;
        score = 0;
        obstacleFormationSpeed = originalObsFormationSpeed;
        obstacleDelay = originalObsDelay;
        Destroy(menuAudioSource);
        StartCoroutine(sceneFadeCor());
    }

    //ShopButton onClick() 함수 - 윤영주
    public void shopButtonClick()
    {
        playSoundEffect(buttonClick);
        shopLoop = false;
        sceneName = "Shop";
        StartCoroutine(sceneFadeCor());
    }

    //MenuButton onClick() 함수 - 윤영주
    public void menuButtonClick()
    {
        playSoundEffect(buttonClick);
        sceneName = "Menu";
        StartCoroutine(sceneFadeCor());
    }

    //QuitButton onClick() 함수(게임 종료) - 윤영주
    public void quitButtonClick()
    {
        playSoundEffect(buttonClick);
        Application.Quit();
    }

    //SettingButton onClick() 함수 - 윤영주
    public void settingButtonClick()
    {
        playSoundEffect(buttonClick);
        sceneName = "Setting";
        StartCoroutine(sceneFadeCor());
    }

    //RestartButton onClick() 함수 - 윤영주
    public void restartButtonClick()
    {
        playSoundEffect(buttonClick);
        sceneName = "Game";
        allowObstacle = true;
        allowObstacleCoroutine = false;
        score = 0;
        obstacleFormationSpeed = originalObsFormationSpeed;
        obstacleDelay = originalObsDelay;
        StartCoroutine(sceneFadeCor());
    }

    //RightButton onClick() 함수 - 윤영주
    public void rightButtonClick()
    {
        playSoundEffect(buttonClick);
        if (characterSprite.Length % 9 == 0)
        {
            shopPage = Mathf.Clamp(shopPage + 1, 1, characterSprite.Length / 9);
        }
        else
        {
            shopPage = Mathf.Clamp(shopPage + 1, 1, characterSprite.Length / 9 + 1);
        }
    }

    //LeftButton onClick()함수 - 윤영주
    public void leftButtonClick()
    {
        playSoundEffect(buttonClick);
        if (shopPage != 1)
        {
            shopPage -= 1;
        }
    }
    
    IEnumerator setAllowObstacle()
    {
        allowObstacleCoroutine = true;
        yield return new WaitForSeconds(obstacleFormationSpeed);
        allowObstacle = true;
        allowObstacleCoroutine = false;
    }

    IEnumerator doubleItemCor()
    {
        yield return new WaitForSeconds(10);
        scoreMultiplier--;
    }

    IEnumerator shieldItemCor()
    {
        yield return new WaitForSeconds(10);
        shieldNum--;
        if (shieldNum == 0)
        {
            shield = false;
        }
    }

    IEnumerator fadeBlocksOut(int blockNum)
    {
        Text block = blocks[blockNum].GetComponent<Text>();
        while (block.color.a > 0.0f)
        {
            if (block != null)
            {
                block.color = block.color - new Color(0, 0, 0, Time.deltaTime * 0.5f);
                yield return null;
            }
            else
            {
                showBlocks();
                break;
            }
        }
        if (block.color.a <= 0.0f)
        {
            block.gameObject.SetActive(false);
        }
    }

    IEnumerator endGameCoroutine()
    {
        yield return new WaitForSeconds(2f);
        endGame = true;
    }

    IEnumerator sceneFadeCor()
    {
        Image fader = GameObject.Find("Canvas").transform.Find("SceneLoadingImage").GetComponent<Image>();
        fader.gameObject.SetActive(true);
        while (fader.color.a < 1f)
        {
            fader.color = fader.color + new Color(0, 0, 0, Time.deltaTime * 1.2f);
            yield return null;
        }
        loadSceneBool = true;
    }

    /*
    public void VolumeSlider()
    {
        Audio.volume = Volume.value;

        Vol = Volume.value;
        PlayerPrefs.SetFloat("Vol", Vol);
    }*/

}
