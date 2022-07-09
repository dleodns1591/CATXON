using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    Slider slTimer;

    [SerializeField]
    GameObject GameOverPanel;

    // 게임 플레이 시간 변수입니다.
    public float GamePlayTimeCount, GamePlayTimeCount2;

    public int GamePlayTimeCountMin;

    public static bool IsGameOver = false;


    //골드 감소 시간 변수입니다.
    public float MoneyMinusTime, MoneyMinusTime2 = 20f,MoneyMinusValue = 100;


    // 플레이어 골드 입니다,
    [SerializeField]
    public static float gold = 0;

    // 이벤트 불러오는 시간 값입니다.
    public float EventTime, EventTime2= 45f;

    //이벤트 번호를 랜덤으로 정해줍니다.
    int RandomEvent;
    [SerializeField]
    TextMeshProUGUI EventText;
    [SerializeField]
    Text goldText;

    [SerializeField]
    Text Timer_Text;

    [SerializeField]
    List<Computer> computers;
    void Start()
    {
        slTimer = GetComponent<Slider>();
        GamePlayTimeCount = 0;
        GamePlayTimeCountMin = 0;
        RandomEvent = 0;
        gold = 5000f;

        GameOverPanel.SetActive(false);
    }

    void Update()
    {
       /* slTimer.value += Time.deltaTime;
        if (slTimer.value >= EventTime2) 
        {
            slTimer.value -= EventTime2;
        }*/


        if (IsGameOver == false)
        Timer_System();

        EventText.text = string.Format("{0:D2}", (int)EventTime + "s");
        GamePlayTimeCount += Time.deltaTime;

        goldText.text = gold + "$";
        // 모든 그 컴퓨터가 고장이 났을 때, 게임 오버를 해주는 구문
        if (!computers.Exists((Computer x) => !x.IsBreak)) 
            GameOver();


        // 플레이어의 골드가 0이 되었을 때 , 게임 오버를 해주는 구문
        else if (GameManager.gold <= 0) 
            GameOver();


        EventTime += Time.deltaTime;
        if (EventTime >= EventTime2) 
        {
            EventTime -= EventTime2;
            
            RandomEvent = Random.Range(1,6);
            switch (RandomEvent)
            {
                case 1:
                    Event1();
                    break;
                case 2:
                    Event2();
                    break;
                case 3:
                    Event3();
                    break;
                case 4:
                    Event4();
                    break;
                case 5:
                    Event5();
                    break;
                default:
                    break;
            }
        }



        MoneyMinusTime += Time.deltaTime;
        if (MoneyMinusTime >= MoneyMinusTime2)
        {
            MoneyMinusTime -= MoneyMinusTime2;
            GameManager.gold -= MoneyMinusValue;
        }
    }
    void GameOver() 
    {
        GameOverPanel.SetActive(true);
        IsGameOver = true;
    }

    void Event1()
    {
        Debug.Log("이벤트 1입니다 ( 로또 당첨 )");
    }
    void Event2()
    {
        Debug.Log("이벤트 2입니다 ( 컴퓨터 수리 )");
    }
    
    void Event3()
    {
        Debug.Log("이벤트 3입니다 ( 파업 )");
    }
    void Event4()
    {
        Debug.Log("이벤트 4입니다 ( 도둑 고양이 ");
    }
    void Event5()
    {
        Debug.Log("이벤트 5입니다 ( 정전 이벤트 )");
        
    }
    public void Timer_System()
    {
        GamePlayTimeCount =(GamePlayTimeCount2 += Time.deltaTime);
        Timer_Text.text = string.Format("{0:D2}:{1:D2}", GamePlayTimeCountMin, (int)GamePlayTimeCount);

        if ((int)GamePlayTimeCount > 59)
        {
            GamePlayTimeCount2 = 0;
            GamePlayTimeCountMin++;
        }
    }
   
}
