using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Computer : MonoBehaviour
{
    Image computer;

    [Header("��� ���")]
    bool isGold = false;

    [Header("��ǻ�� ����")]
    public int comNum = 0;
    public Area currentArea;
    [SerializeField] Sprite idel;
    [SerializeField] Sprite work;
    [SerializeField] Sprite broken;
    public bool isBreak = false; // ��ǻ�Ͱ� �μ������� Ȯ��
    public bool isWork = false; // ����̰� ���� �ϰ� �ִ��� Ȯ��
    public bool isSit = false;

    // ��ǻ�Ͱ� ���峪�� �ð� �����Դϴ�.
    [Header("����")]
    [SerializeField] float currentBreakTime = 0;
    const float breakCoolTime = 5;
    bool isBreakCool = false;

    [Header("��")]
    bool isWorkGold = false;

    [Header("����� ����")]
    [SerializeField] GameObject catSit;

    void Start()
    {
        CurrentArea();
        computer = GetComponent<Image>();
    }

    void Update()
    {
        BreakComputer();
        ComputerSetting();
    }

    void CurrentArea()
    {
        for (int floor = 0; floor < 3; floor++)
        {
            for (int area = 0; area < 6; area++)
            {
                var catArea = Cat_Manager.instance.area[floor].areaList[area].GetComponent<Area>();

                if (catArea.area == comNum)
                    currentArea = catArea;
            }
        }
    }

    void ComputerSetting()
    {
        if (isBreak)
        {
            isWork = false;
            computer.sprite = broken;

            isWorkGold = false;
            StopCoroutine("GetGold");
        }

        if (isWork)
        {
            StartCoroutine("GetGold");
            computer.sprite = work;
        }

        if (!isSit)
        {
            isWorkGold = false;
            StopCoroutine("GetGold");

            catSit = null;
            computer.sprite = idel;
        }
    }

    IEnumerator GetGold()
    {
        if(!isWorkGold)
        {
            isWorkGold = true;

            while(isWorkGold)
            {
                GameManager.instance.currentGold += 10;
                yield return new WaitForSeconds(1);
            }
        }
    }

    void BreakComputer()
    {
        if (!isBreak && isSit)
        {
            if (!isBreakCool)
            {
                isBreakCool = true;
                currentBreakTime = breakCoolTime;
                StopCoroutine("BrokenCoolTIme");
                StartCoroutine("BrokenCoolTIme");
            }

            if (currentBreakTime <= 0)
            {
                isBreakCool = false;

                int randomBreak = Random.Range(0, 2);
                switch (randomBreak)
                {
                    case 0:
                        isBreak = true;
                        break;
                }
            }
        }
    }

    IEnumerator BrokenCoolTIme()
    {
        while (currentBreakTime >= 0)
        {
            currentBreakTime -= 1;
            yield return new WaitForSeconds(1);
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Cat"))
        {
            if (catSit == null)
                catSit = collision.gameObject;

            if (currentArea.gameObject == collision.GetComponent<CatDrag>().currentArea.gameObject)
            {
                isSit = true;

                if (!isBreak)
                    isWork = true;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Cat"))
        {
            var cat = collision.GetComponent<CatDrag>();

            if (catSit == collision.gameObject)
            {
                if (!isBreak)
                {
                    isSit = false;
                    isWork = false;
                    computer.sprite = idel;
                }

                else
                {
                    if (gameObject != cat.computer)
                    {
                        isSit = false;
                        isWork = false;
                        isBreak = false;
                        computer.sprite = idel;
                    }
                }
            }
        }
    }
}
