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
    const int basicFund = 2000;

    [Header("�ð�")]
    public int currentTime = 0;

    //��� ���� �ð� �����Դϴ�.
    public float MoneyMinusTime, MoneyMinusTime2 = 5f, MoneyMinusValue = 100;
    public static float GameTotalGoldValue = 0;

    public List<Computer> computers;

    void Start()
    {
        currentGold = basicFund;
    }

    void Update()
    {
        // ��� �� ��ǻ�Ͱ� ������ ���� ��, ���� ������ ���ִ� ����
        //if (!computers.Exists((Computer x) => !x.IsBreak)) 
        //    GameOver();


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
}