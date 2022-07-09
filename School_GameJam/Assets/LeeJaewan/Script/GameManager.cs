using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    GameObject GameOverPanel;

    [SerializeField]
    GameObject EventPanel_1;
    [SerializeField]
    GameObject EventPanel_2;
    [SerializeField]
    GameObject EventPanel_3;
    [SerializeField]
    GameObject EventPanel_4;
    [SerializeField]
    GameObject EventPanel_5;

    // 게임 플레이 시간 변수입니다.
    public float GamePlayTimeCount, GamePlayTimeCount2;
    public float GamePlayTimeCountSum;
    public int GamePlayTimeCountMin;

    public static bool IsGameOver = false;

    public bool IsPaUp = false;
    public bool IsPiro = false;

    public float PiRoValue;

    //골드 감소 시간 변수입니다.
    public float MoneyMinusTime, MoneyMinusTime2 = 20f,MoneyMinusValue = 100;

    bool IsEvent;

    public float EventIconTimelimit = 5f, EventIconTime;

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
    TextMeshProUGUI goldText;

    [SerializeField]
    Text Timer_Text;

    [SerializeField]
    private List<GameObject> computers = new List<GameObject>();
    void Start()
    {
        GamePlayTimeCountSum = 0;
        GamePlayTimeCount = 0;
        GamePlayTimeCountMin = 0;
        RandomEvent = 0;
        gold = 2000;

        GameOverPanel.SetActive(false);
        
    }

    void Update()
    {
        if (IsGameOver == false)
        Timer_System();

        EventText.text = "Event" + RandomEvent;
        GamePlayTimeCount += Time.deltaTime;
        goldText.text = gold + "$";

        // 모든 그 컴퓨터가 고장이 났을 때, 게임 오버를 해주는 구문
       // if (!computers.Exists((Computer x) => !x.IsBreak)) 
           // GameOver();


        // 플레이어의 골드가 0이 되었을 때 , 게임 오버를 해주는 구문
       if (GameManager.gold <= 0) 
            GameOver();


        EventTime += Time.deltaTime;
        if (EventTime >= EventTime2) 
        {
            EventTime -= EventTime2;
            if (GamePlayTimeCountSum <= 179) 
            {
                RandomEvent = Random.Range(1, 4);
                Debug.Log("초반부 입니다.");
            }
            else if (GamePlayTimeCountSum >= 180 && GamePlayTimeCountSum <= 419)
            {
                RandomEvent = Random.Range(2, 5);
                Debug.Log("중반부 입니다.");
            }
            else if (GamePlayTimeCountSum >= 420 ) 
            {
                RandomEvent = Random.Range(3, 6);
                Debug.Log("후반부 입니다.");
            }
            switch (RandomEvent)
            {
                case 1:
                    // Event1();
                    Event5();
                    break;
                case 2:
                    Event5();
                    break;
                case 3:
                    Event5();
                    break;
                case 4:
                    Event5();
                    break;
                case 5:
                    Event5();
                    break;
                default:
                    break;
            }
            StartCoroutine(SetAtiveF());
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

    // 1번 : 로또 당첨 이벤트입니다. ( 긍정 이벤트 )    
    void Event1()
    {
        Debug.Log("이벤트 1입니다 ( 로또 당첨 )");
            EventPanel_1.SetActive(true);
        StartCoroutine(SetAtiveF());
        gold += (gold / 2);
    }
    // 2번 : 컴퓨터 수리 이벤트 입니다. ( 긍정 이벤트 )
    void Event2()
    {
        Debug.Log("이벤트 2입니다 ( 컴퓨터 수리 )");
        EventPanel_2.SetActive(true);
        StartCoroutine(SetAtiveF());
    }
    // 3번 : 파업 이벤트 입니다. ( 부정 이벤트 )
    void Event3()
    {
        Debug.Log("이벤트 3입니다 ( 파업 )");
        EventPanel_3.SetActive(true);
        StartCoroutine(SetAtiveF());
        IsPaUp = true;
        StartCoroutine(PaUpOff());
    }
    // 4번 : 도둑 고양이 이벤트입니다. ( 부정 이벤트 )
    void Event4()
    {
        Debug.Log("이벤트 4입니다 ( 도둑 고양이 ");
        EventPanel_4.SetActive(true);
        StartCoroutine(SetAtiveF());
        gold -= (gold / 5);
        
    }
    // 5번 : 정전 이벤트입니다. ( 부정 이벤트 )
    void Event5()
    {
        Debug.Log("이벤트 5입니다 ( 피로 이벤트 )");
        EventPanel_5.SetActive(true);
        StartCoroutine(SetAtiveF());
        IsPiro = true;
        PiRoValue = Computer.GoldValue * 0.3f;
        StartCoroutine(PiroOff());
        if (IsPiro == true)
            Computer.GoldValue -= PiRoValue;



    }
    public void Timer_System()
    {
        GamePlayTimeCountSum += Time.deltaTime;
        GamePlayTimeCount =(GamePlayTimeCount2 += Time.deltaTime);
        Timer_Text.text = string.Format("{0:D2}:{1:D2}", GamePlayTimeCountMin, (int)GamePlayTimeCount);

        if ((int)GamePlayTimeCount > 59)
        {
            GamePlayTimeCount2 = 0;
            GamePlayTimeCountMin++;
        }
    }
    IEnumerator SetAtiveF() 
    {
        yield return new WaitForSeconds(EventIconTimelimit);
        EventPanel_1.SetActive(false);
        EventPanel_2.SetActive(false);
        EventPanel_3.SetActive(false);
        EventPanel_4.SetActive(false);
        EventPanel_5.SetActive(false);
    }
    IEnumerator PaUpOff() 
    {
        yield return new WaitForSeconds(10);
        IsPaUp = false;
    }
    IEnumerator PiroOff() {
        yield return new WaitForSeconds(20);
        Computer.GoldValue += PiRoValue;

    }



}
