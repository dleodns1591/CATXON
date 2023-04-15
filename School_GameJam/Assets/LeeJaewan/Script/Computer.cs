using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Computer : MonoBehaviour
{
    Image computer;

    [Header("얻는 골드")]
    bool isGold = false;

    [Header("컴퓨터 상태")]
    public int comNum = 0;
    public Area currentArea;
    [SerializeField] Sprite idel;
    [SerializeField] Sprite work;
    [SerializeField] Sprite broken;

    // 컴퓨터가 고장나는 시간 변수입니다.
    [SerializeField] float breakTime = 0;
    [SerializeField] float brokenTime = 10;

    float moneyGetTime = 0;
    public bool isBreak = false; // 컴퓨터가 부셔졌는지 확인
    public bool isWork = false; // 고양이가 일을 하고 있는지 확인

    [Header("고양이 상태")]
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
                // 현재 컴퓨터에 고양이가 앉아 있다면
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
