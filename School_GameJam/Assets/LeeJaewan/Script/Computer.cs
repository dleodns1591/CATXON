using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Computer : MonoBehaviour
{
    Image computer;

    // 골드를 몇 초마다 지급하는지 초를 정해주는 변수입니다.
    public static float GoldGetTime = 1;

    // 골드를 얼마나 얻을지에 대한 변수입니다.
    public static float GoldValue = 5;

    [SerializeField] Sprite idel;
    [SerializeField] Sprite work;
    [SerializeField] Sprite broken;

    // 컴퓨터가 고장나는 시간 변수입니다.
    [SerializeField] float breakTime = 0;
    [SerializeField] float brokenTime = 10;

    private float moneyGetTime;

    // 고양이가 앉아 있냐 를 뜻하는 변수입니다.
    bool isSit = false;

    // 컴퓨터가 부셔졌나 를 뜻하는 변수
    public static bool isBreak = false;

    // 고양이가 일을 하고 있는가를 뜻하는 변수입니다
    public bool isWork = false;


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
        if (!isSit)
        {
            isBreak = false;
            isWork = false;

            breakTime = 0;
            computer.sprite = idel;
        }

        if (isBreak)
        {
            isWork = false;

            moneyGetTime = 0;
            computer.sprite = broken;
        }

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
                if (moneyGetTime >= GoldGetTime)
                {
                    moneyGetTime -= GoldGetTime;
                    GameManager.instance.currentGold += (int)GoldValue;
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
