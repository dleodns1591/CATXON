using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Computer : MonoBehaviour
{
    Image computer;

    [Header("��� ���")]
    [SerializeField] int getGold = 5;
    [SerializeField] float getGoldCoolTime = 1;

    [Header("��ǻ�� ����")]
    [SerializeField] Sprite idel;
    [SerializeField] Sprite work;
    [SerializeField] Sprite broken;

    // ��ǻ�Ͱ� ���峪�� �ð� �����Դϴ�.
    [SerializeField] float breakTime = 0;
    [SerializeField] float brokenTime = 10;

    float moneyGetTime;
    bool isSit = false; // ����̰� �ɾ� �ִ��� Ȯ��
    public bool isBreak = false; // ��ǻ�Ͱ� �μ������� Ȯ��
    public bool isWork = false; // ����̰� ���� �ϰ� �ִ��� Ȯ��


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
        // �⺻
        if (!isSit)
        {
            isBreak = false;
            isWork = false;

            breakTime = 0;
            computer.sprite = idel;
        }

        // �������� ��
        if (isBreak)
        {
            isWork = false;

            moneyGetTime = 0;
            computer.sprite = broken;
        }

        // ���ϰ� ���� ��
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

            Debug.Log("���� �۵����Դϴ�.");
        }

        // 2���ϰ� ��� ������ 
        if (collision.CompareTag("Floor2")) 
        {
            // 2���� ���� �����ִ��� Ȯ��
            if (Cat_Manager.instance.buyFloorIndex == 1)
            {
                // ���� �����ִٸ� ����̰� �������� ���� �� ��
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
