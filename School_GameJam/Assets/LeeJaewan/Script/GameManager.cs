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

    // ���� �÷��� �ð� �����Դϴ�.
    public float GamePlayTimeCount, GamePlayTimeCount2;

    public int GamePlayTimeCountMin;

    public static bool IsGameOver = false;


    //��� ���� �ð� �����Դϴ�.
    public float MoneyMinusTime, MoneyMinusTime2 = 20f,MoneyMinusValue = 100;


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
        // ��� �� ��ǻ�Ͱ� ������ ���� ��, ���� ������ ���ִ� ����
        if (!computers.Exists((Computer x) => !x.IsBreak)) 
            GameOver();


        // �÷��̾��� ��尡 0�� �Ǿ��� �� , ���� ������ ���ִ� ����
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
        Debug.Log("�̺�Ʈ 1�Դϴ� ( �ζ� ��÷ )");
    }
    void Event2()
    {
        Debug.Log("�̺�Ʈ 2�Դϴ� ( ��ǻ�� ���� )");
    }
    
    void Event3()
    {
        Debug.Log("�̺�Ʈ 3�Դϴ� ( �ľ� )");
    }
    void Event4()
    {
        Debug.Log("�̺�Ʈ 4�Դϴ� ( ���� ����� ");
    }
    void Event5()
    {
        Debug.Log("�̺�Ʈ 5�Դϴ� ( ���� �̺�Ʈ )");
        
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
