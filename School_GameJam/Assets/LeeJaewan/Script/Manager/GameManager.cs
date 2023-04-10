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
    const int basicFund = 2000;

    [Header("시간")]
    public int currentTime = 0;

    //골드 감소 시간 변수입니다.
    public float MoneyMinusTime, MoneyMinusTime2 = 5f, MoneyMinusValue = 100;
    public static float GameTotalGoldValue = 0;

    public List<Computer> computers;

    void Start()
    {
        currentGold = basicFund;
    }

    void Update()
    {
        // 모든 그 컴퓨터가 고장이 났을 때, 게임 오버를 해주는 구문
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
        // 돈 증가
        if (Input.GetKeyDown(KeyCode.G))
            currentGold += 1000;

        // 돈 차감
        if (Input.GetKeyDown(KeyCode.F))
            currentGold -= 1000;
    }
}