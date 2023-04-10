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

    [Header("��")]
    public int currentGold = 0;
    [SerializeField] Text GameTotalGold;

    [Header("�ð�")]
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

    //��� ���� �ð� �����Դϴ�.
    public float MoneyMinusTime, MoneyMinusTime2 = 5f, MoneyMinusValue = 100;

    public static float GameTotalGoldValue = 0;
    // �̺�Ʈ �ҷ����� �ð� ���Դϴ�.
    public float EventTime, EventTime2 = 45f;

    //�̺�Ʈ ��ȣ�� �������� �����ݴϴ�.
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

        // ��� �� ��ǻ�Ͱ� ������ ���� ��, ���� ������ ���ִ� ����
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
        // �� ����
        if (Input.GetKeyDown(KeyCode.G))
            currentGold += 1000;

        // �� ����
        if (Input.GetKeyDown(KeyCode.F))
            currentGold -= 1000;
    }

    #region �̺�Ʈ
    void Event1()
    {
        Debug.Log("�̺�Ʈ 1�Դϴ� ( �ζ� ��÷ )");
        EventPanel1.SetActive(true);
        currentGold += (currentGold / 2);
        StartCoroutine(EventPanelOff());

    }
    void Event2()
    {
        Debug.Log("�̺�Ʈ 2�Դϴ� ( ��ǻ�� ���� )");
        EventPanel2.SetActive(true);
        //Computer.isBreak = false;
        StartCoroutine(EventPanelOff());
    }

    void Event3()
    {
        Debug.Log("�̺�Ʈ 3�Դϴ� ( �ľ� )");
        EventPanel3.SetActive(true);
        StartCoroutine(EventPanelOff());
    }
    void Event4()
    {
        Debug.Log("�̺�Ʈ 4�Դϴ� ( ���� ����� ");
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


