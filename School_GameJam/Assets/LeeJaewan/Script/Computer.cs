using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Computer : MonoBehaviour
{
    Image computer;

    [Header("얻는 골드")]
    [SerializeField] int getGold = 5;
    [SerializeField] float getGoldCoolTime = 1;

    [Header("컴퓨터 상태")]
    [SerializeField] Sprite idel;
    [SerializeField] Sprite work;
    [SerializeField] Sprite broken;

    // 컴퓨터가 고장나는 시간 변수입니다.
    [SerializeField] float breakTime = 0;
    [SerializeField] float brokenTime = 10;

    float moneyGetTime;
    bool isSit = false; // 고양이가 앉아 있는지 확인
    public bool isBreak = false; // 컴퓨터가 부셔졌는지 확인
    public bool isWork = false; // 고양이가 일을 하고 있는지 확인


    void Start()
    {
        computer = GetComponent<Image>();
    }

    void Update()
    {
        GoldCount();
        ComputerSetting();
    }

    void ComputerSetting()
    {
        // 기본
        if (!isSit)
        {
            isBreak = false;
            isWork = false;

            breakTime = 0;
            computer.sprite = idel;
        }

        // 망가졌을 시
        if (isBreak)
        {
            isWork = false;

            moneyGetTime = 0;
            computer.sprite = broken;
        }

        // 일하고 있을 시
        else if (!isBreak && isSit)
        {
            isWork = true;
            computer.sprite = work;
        }
    }

    void GoldCount()
    {
        if (isWork)
        {
            BrokenTimeCount();

            if (!UIManager.instance.isGameOver)
            {
                if (moneyGetTime >= getGoldCoolTime)
                {
                    moneyGetTime -= getGoldCoolTime;
                    GameManager.instance.currentGold += getGold;
                    //GameManager.GameTotalGoldValue+= GoldValue;
                }
            }
        }

        void BrokenTimeCount()
        {
            StartCoroutine(BreakTimeCountPlus());
            if (breakTime >= brokenTime)
            {
                breakTime -= brokenTime;
                isBreak = true;
            }
        }

        IEnumerator GoldCountPlus()
        {
            moneyGetTime++;
            yield return new WaitForSecondsRealtime(1);
            StartCoroutine(GoldCountPlus());
        }

        IEnumerator BreakTimeCountPlus()
        {
            breakTime++;
            yield return new WaitForSecondsRealtime(1);
            StartCoroutine(BreakTimeCountPlus());
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Cat"))
        {
            isSit = true;

            if (isSit)
                isWork = true;

            else if (isSit && isBreak)
                isWork = false;

            Debug.Log("지금 작동중입니다.");
        }

        // 2층일과 닿고 있을때 
        if (collision.CompareTag("Floor2")) 
        {
            // 2층의 불이 켜져있는지 확인
            if (Cat_Manager.instance.buyFloorIndex == 1)
            {
                // 불이 꺼져있다면 고양이가 있을지라도 일을 안 함
                isWork = false;
            }
            else 
            {
                isWork = true;
                //Sp.sprite = Sprite[2];
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Cat"))
            isSit = false;
    }

    
}
