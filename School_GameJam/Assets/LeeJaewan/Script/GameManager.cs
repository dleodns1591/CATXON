using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    float sec = 0;

   

    [SerializeField]
    Text GameGoldText;
    [SerializeField]
    Text GameTimerText;

    [SerializeField]
    Text GameTotalTime;
    [SerializeField]
    Text GameTotalGold;

    [SerializeField]
    Slider slTimer;

    [SerializeField]
    GameObject GameOverPanel;

    [SerializeField]
    GameObject EventPanel1;
    [SerializeField]
    GameObject EventPanel2;
    [SerializeField]
    GameObject EventPanel3;
    [SerializeField]
    GameObject EventPanel4;


    [SerializeField]
    GameObject GameTotalTime2;
    [SerializeField]
    GameObject GameTotalGold2;

    // 게임 플레이 시간 변수입니다.
    public float GamePlayTimeCount, GamePlayTimeCount2;

    public int GamePlayTimeCountMin;

    public static bool IsGameOver = false;


    //골드 감소 시간 변수입니다.
    public float MoneyMinusTime, MoneyMinusTime2 = 5f,MoneyMinusValue = 100;


    // 플레이어 골드 입니다,
    [SerializeField]
    public static float gold = 0;

    public static float GameTotalGoldValue = 0;
    // 이벤트 불러오는 시간 값입니다.
    public float EventTime, EventTime2= 45f;

    //이벤트 번호를 랜덤으로 정해줍니다.
    int RandomEvent;
    [SerializeField]
    Text EntityText;
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
        gold = 2000f;

        StartCoroutine(TimeSet());
        GameOverPanel.SetActive(false);
        GameTotalGold2.SetActive(false);
        GameTotalTime2.SetActive(false);
    }

    void Update()
    {

      


        /* slTimer.value += Time.deltaTime;
         if (slTimer.value >= EventTime2) 
         {
             slTimer.value -= EventTime2;
         }*/

        if (Input.GetKeyDown(KeyCode.G))
            gold += 1000;
        else if (Input.GetKeyDown(KeyCode.F)) 
        {
            gold -= 1000;
        }


        if (IsGameOver == false)
          Timer_System();

        EntityText.text = "" + Cat_Manager.Inst.D_Area.Count + "마리";
     

        goldText.text = gold + "$";
        // 모든 그 컴퓨터가 고장이 났을 때, 게임 오버를 해주는 구문
        //if (!computers.Exists((Computer x) => !x.IsBreak)) 
        //    GameOver();


        // 플레이어의 골드가 0이 되었을 때 , 게임 오버를 해주는 구문
        if (GameManager.gold <= 0) 
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
        StopCoroutine(TimeSet());
        if(gold <= 0)
        gold = 0;

        GameTotalTime2.SetActive(true);
        GameTotalGold2.SetActive(true);

        GameTotalTime.text = string.Format("{0:D2}:{1:D2}", GamePlayTimeCountMin, (int)GamePlayTimeCount);
        GameTotalGold.text = goldText.text = GameTotalGoldValue + "$";
    }

    void Event1()
    {
        Debug.Log("이벤트 1입니다 ( 로또 당첨 )");
        EventPanel1.SetActive(true);
        gold += (gold / 2);
        StartCoroutine(EventPanelOff());

    }
    void Event2()
    {
        Debug.Log("이벤트 2입니다 ( 컴퓨터 수리 )");
        EventPanel2.SetActive(true);
        Computer.IsBreak = false;
        StartCoroutine(EventPanelOff());
    }
    
    void Event3()
    {
        Debug.Log("이벤트 3입니다 ( 파업 )");
        EventPanel3.SetActive(true);
        StartCoroutine(EventPanelOff());
    }
    void Event4()
    {
        Debug.Log("이벤트 4입니다 ( 도둑 고양이 ");
        EventPanel4.SetActive(true);
        gold -= (gold * 0.2f);
        StartCoroutine(EventPanelOff());
    }
   
    public void Timer_System()
    {
        //GamePlayTimeCount =(GamePlayTimeCount2 += Time.deltaTime);
        Timer_Text.text = string.Format("{0:D2}:{1:D2}", GamePlayTimeCountMin, (int)GamePlayTimeCount);
       
        if ((int)GamePlayTimeCount > 59)
        {
            GamePlayTimeCount = 0;
            GamePlayTimeCountMin++;
        }
    }
   
    IEnumerator EventPanelOff() 
    {
        yield return new WaitForSeconds(10);
        EventPanel1.SetActive(false);
        EventPanel2.SetActive(false);
        EventPanel3.SetActive(false);
        EventPanel4.SetActive(false);
    }
    IEnumerator TimeSet()
    {
        GamePlayTimeCount++;
        yield return new WaitForSecondsRealtime(1);
        if (IsGameOver == false)
            StartCoroutine(TimeSet());
        else
            StopCoroutine(TimeSet());
        
}

    }
   

