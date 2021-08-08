using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private int destination;//0 = 1번 구역, 1 = 2번 구역, 2 = 3번 구역, 3 = 4번 구역

    private float playerSpeed = 0.3f;//플레이어 이동속도

    public GameObject player;//플레이어 게임오브젝트

    //구역별 플레이어 좌표
    public Vector2[] playerCoordinate = { new Vector2(-1.34375f, 3.6875f), new Vector2(1.34375f, 3.6875f), new Vector2(-1.34375f, 1.3125f), new Vector2(1.34375f, 1.3125f) };
    
    void Start()
    {
        startSettings();
    }

    void Update()
    {
        allScene();
        menuScene();
        gameScene();
        gameOverScene();
        settingScene();
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

    //플레이어 지정 좌표 이동 함수 - 윤영주
    private void movePlayer()
    {
        player.transform.position = Vector3.Lerp(player.transform.position, playerCoordinate[destination], playerSpeed);
    }

    //1번 버튼 클릭 함수 - 윤영주
    public void button1Click()
    {
        destination = 0;
    }

    //2번 버튼 클릭 함수
    public void button2Click()
    {
        destination = 1;
    }

    //3번 버튼 클릭 함수
    public void button3Click()
    {
        destination = 2;
    }

    //4번 버튼 클릭 함수
    public void button4Click()
    {
        destination = 3;
    }

}
