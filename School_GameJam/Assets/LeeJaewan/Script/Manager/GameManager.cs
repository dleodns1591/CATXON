using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.UI;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    void Awake() => instance = this;

    [Header("돈")]
    public int currentGold = 0;
    [SerializeField] Text GameTotalGold;

    [Header("시간")]
    public int currentTime = 0;
    [SerializeField] Text GameTotalTime;

    [SerializeField]
    Slider slTimer;

    [SerializeField] GameObject EventPanel1;
    [SerializeField] GameObject EventPanel2;
    [SerializeField] GameObject EventPanel3;
    [SerializeField] GameObject EventPanel4;

    [SerializeField]
    GameObject GameTotalTime2;
    [SerializeField]
    GameObject GameTotalGold2;

    public int GamePlayTimeCountMin;

    //골드 감소 시간 변수입니다.
    public float MoneyMinusTime, MoneyMinusTime2 = 5f, MoneyMinusValue = 100;

    public static float GameTotalGoldValue = 0;
    // 이벤트 불러오는 시간 값입니다.
    public float EventTime, EventTime2 = 45f;

    //이벤트 번호를 랜덤으로 정해줍니다.
    int RandomEvent;
    [SerializeField]
    Text EntityText;
    [SerializeField]
    Text goldText;

    [SerializeField]
    Text Timer_Text;

    public List<Computer> computers;

    void Start()
    {
        currentGold = 2000;

        slTimer = GetComponent<Slider>();
        GamePlayTimeCountMin = 0;
        RandomEvent = 0;

        //GameTotalGold2.SetActive(false);
        GameTotalTime2.SetActive(false);
    }

    void Update()
    {
        /* slTimer.value += Time.deltaTime;
         if (slTimer.value >= EventTime2) 
         {
             slTimer.value -= EventTime2;
         }*/

        // 모든 그 컴퓨터가 고장이 났을 때, 게임 오버를 해주는 구문
        //if (!computers.Exists((Computer x) => !x.IsBreak)) 
        //    GameOver();

        EventTime += Time.deltaTime;
        if (EventTime >= EventTime2)
        {
            EventTime -= EventTime2;

            RandomEvent = Random.Range(1, 6);
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
            currentGold -= (int)MoneyMinusValue;
        }

        Cheat();
    }

    void Cheat()
    {
        // 돈 증가
        if (Input.GetKeyDown(KeyCode.G))
            currentGold += 1000;

        // 돈 차감
        if (Input.GetKeyDown(KeyCode.F))
            currentGold -= 1000;
    }

    #region 이벤트
    void Event1()
    {
        Debug.Log("이벤트 1입니다 ( 로또 당첨 )");
        EventPanel1.SetActive(true);
        currentGold += (currentGold / 2);
        StartCoroutine(EventPanelOff());

    }
    void Event2()
    {
        Debug.Log("이벤트 2입니다 ( 컴퓨터 수리 )");
        EventPanel2.SetActive(true);
        //Computer.isBreak = false;
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
        currentGold -= (int)(currentGold * 0.2f);
        StartCoroutine(EventPanelOff());
    }
    #endregion

    IEnumerator EventPanelOff()
    {
        yield return new WaitForSeconds(10);
        EventPanel1.SetActive(false);
        EventPanel2.SetActive(false);
        EventPanel3.SetActive(false);
        EventPanel4.SetActive(false);
    }

    //IEnumerator TimeSet()
    //{
    //    GamePlayTimeCount++;
    //    yield return new WaitForSecondsRealtime(1);
    //    if (IsGameOver == false)
    //        StartCoroutine(TimeSet());
    //    else
    //        StopCoroutine(TimeSet());

    //}
}


