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

    // ���� �÷��� �ð� �����Դϴ�.
    public float GamePlayTimeCount, GamePlayTimeCount2;
    public float GamePlayTimeCountSum;
    public int GamePlayTimeCountMin;

    public static bool IsGameOver = false;

    public bool IsPaUp = false;
    public bool IsPiro = false;

    public float PiRoValue;

    //��� ���� �ð� �����Դϴ�.
    public float MoneyMinusTime, MoneyMinusTime2 = 20f,MoneyMinusValue = 100;

    bool IsEvent;

    public float EventIconTimelimit = 5f, EventIconTime;

    // �÷��̾� ��� �Դϴ�,
    [SerializeField]
    public static float gold = 0;



    // �̺�Ʈ �ҷ����� �ð� ���Դϴ�.
    public float EventTime, EventTime2= 45f;

    //�̺�Ʈ ��ȣ�� �������� �����ݴϴ�.
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

        // ��� �� ��ǻ�Ͱ� ������ ���� ��, ���� ������ ���ִ� ����
       // if (!computers.Exists((Computer x) => !x.IsBreak)) 
           // GameOver();


        // �÷��̾��� ��尡 0�� �Ǿ��� �� , ���� ������ ���ִ� ����
       if (GameManager.gold <= 0) 
            GameOver();


        EventTime += Time.deltaTime;
        if (EventTime >= EventTime2) 
        {
            EventTime -= EventTime2;
            if (GamePlayTimeCountSum <= 179) 
            {
                RandomEvent = Random.Range(1, 4);
                Debug.Log("�ʹݺ� �Դϴ�.");
            }
            else if (GamePlayTimeCountSum >= 180 && GamePlayTimeCountSum <= 419)
            {
                RandomEvent = Random.Range(2, 5);
                Debug.Log("�߹ݺ� �Դϴ�.");
            }
            else if (GamePlayTimeCountSum >= 420 ) 
            {
                RandomEvent = Random.Range(3, 6);
                Debug.Log("�Ĺݺ� �Դϴ�.");
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

    // 1�� : �ζ� ��÷ �̺�Ʈ�Դϴ�. ( ���� �̺�Ʈ )    
    void Event1()
    {
        Debug.Log("�̺�Ʈ 1�Դϴ� ( �ζ� ��÷ )");
            EventPanel_1.SetActive(true);
        StartCoroutine(SetAtiveF());
        gold += (gold / 2);
    }
    // 2�� : ��ǻ�� ���� �̺�Ʈ �Դϴ�. ( ���� �̺�Ʈ )
    void Event2()
    {
        Debug.Log("�̺�Ʈ 2�Դϴ� ( ��ǻ�� ���� )");
        EventPanel_2.SetActive(true);
        StartCoroutine(SetAtiveF());
    }
    // 3�� : �ľ� �̺�Ʈ �Դϴ�. ( ���� �̺�Ʈ )
    void Event3()
    {
        Debug.Log("�̺�Ʈ 3�Դϴ� ( �ľ� )");
        EventPanel_3.SetActive(true);
        StartCoroutine(SetAtiveF());
        IsPaUp = true;
        StartCoroutine(PaUpOff());
    }
    // 4�� : ���� ����� �̺�Ʈ�Դϴ�. ( ���� �̺�Ʈ )
    void Event4()
    {
        Debug.Log("�̺�Ʈ 4�Դϴ� ( ���� ����� ");
        EventPanel_4.SetActive(true);
        StartCoroutine(SetAtiveF());
        gold -= (gold / 5);
        
    }
    // 5�� : ���� �̺�Ʈ�Դϴ�. ( ���� �̺�Ʈ )
    void Event5()
    {
        Debug.Log("�̺�Ʈ 5�Դϴ� ( �Ƿ� �̺�Ʈ )");
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
