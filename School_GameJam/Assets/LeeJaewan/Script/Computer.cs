using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Computer : MonoBehaviour
{
    Image computer;

    // ��带 �� �ʸ��� �����ϴ��� �ʸ� �����ִ� �����Դϴ�.
    public static float GoldGetTime = 1;

    // ��带 �󸶳� �������� ���� �����Դϴ�.
    public static float GoldValue = 5;
    [SerializeField]
    Sprite Idle;
    [SerializeField]
    Sprite WorkComputer;
    [SerializeField]
    Sprite BrokenComputer;

    // ��ǻ�Ͱ� ���峪�� �ð� �����Դϴ�.
    [SerializeField]
    float BreakTime;
    [SerializeField]
    float BrokenTime = 10;

    private float moneyGetTime;

    // ����̰� �ɾ� �ֳ� �� ���ϴ� �����Դϴ�.
    public bool IsSit = false;

    // ��ǻ�Ͱ� �μ����� �� ���ϴ� ����
    public static bool IsBreak = false;

    // ����̰� ���� �ϰ� �ִ°��� ���ϴ� �����Դϴ�
    public bool IsWork = false;


    void Start()
    {
        computer = GetComponent<Image>();
    }

    void Update()
    {
        GoldCount();



        if (!IsSit)
        {
            IsBreak = false;
            IsWork = false;
            BreakTime = 0;
            computer.sprite = Idle;
        }

        if (IsBreak)
        {
            moneyGetTime = 0;
            IsWork = false;
            computer.sprite = BrokenComputer;
        }

        else if (!IsBreak && IsSit)
            computer.sprite = WorkComputer;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Cat"))
        {
            IsSit = true;

            if (IsSit)
                IsWork = true;

            else if (IsSit && IsBreak)
                IsWork = false;

            Debug.Log("���� �۵����Դϴ�.");
        }

        // 2���ϰ� ��� ������ 
        if (collision.CompareTag("Floor2")) 
        {
            // 2���� ���� �����ִ��� Ȯ��
            if (Cat_Manager.instance.BuyFloor_Idx == 1)
            {
                // ���� �����ִٸ� ����̰� �������� ���� �� ��
                IsWork = false;
            }
            else 
            {
                IsWork = true;
                //Sp.sprite = Sprite[2];
            }
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Cat"))
            IsSit = false;
    }

    void GoldCount() 
    {
        if (IsSit == true && IsBreak == false && IsWork == true)
        {
            Debug.Log(IsBreak);
            BrokenTimeCount();
            computer.sprite = WorkComputer;
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
            if (BreakTime >= BrokenTime)
            {
                BreakTime -= BrokenTime;
                IsBreak = true;
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
            BreakTime++;
            yield return new WaitForSecondsRealtime(1);
            StartCoroutine(BreakTimeCountPlus());
        }
    }
}
