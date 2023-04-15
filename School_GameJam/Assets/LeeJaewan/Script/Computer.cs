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

    // ��ǻ�Ͱ� ���峪�� �ð� �����Դϴ�.
    [SerializeField] float breakTime = 0;
    [SerializeField] float brokenTime = 10;

    float moneyGetTime = 0;
    public bool isBreak = false; // ��ǻ�Ͱ� �μ������� Ȯ��
    public bool isWork = false; // ����̰� ���� �ϰ� �ִ��� Ȯ��

    [Header("����� ����")]
    [SerializeField] GameObject catSit;

    void Start()
    {
        computer = GetComponent<Image>();
        CurrentArea();
    }

    void Update()
    {
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
        if (catSit != null && catSit.GetComponent<CatDrag>().isSit)
        {
            if (isBreak)
            {
                isWork = false;
                isGold = false;
                computer.sprite = broken;
                StopCoroutine("GoldSetting");
            }

            if (isWork)
            {
                isBreak = false;
                computer.sprite = work;
                StartCoroutine("GoldSetting");
            }
        }

        else
            isWork = false;
    }

    IEnumerator GoldSetting()
    {
        if (!isGold)
        {
            isGold = true;

            while (true)
            {
                GameManager.instance.currentGold++;
                yield return new WaitForSeconds(1);
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Cat"))
        {
            var cat = collision.GetComponent<CatDrag>();

            if (catSit == null)
                catSit = collision.gameObject;

            if (currentArea.gameObject == cat.currentArea)
            {
                // ���� ��ǻ�Ϳ� ����̰� �ɾ� �ִٸ�
                if (catSit.GetComponent<CatDrag>().isSit)
                    isWork = true;

                else
                {
                    isWork = false;
                    isBreak = false;
                }
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Cat"))
        {
            var cat = collision.GetComponent<CatDrag>();

            if (currentArea.gameObject == cat.currentArea)
            {
                isWork = false;
                isBreak = false;
                computer.sprite = idel;
            }
        }
    }
}
