using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Computer : MonoBehaviour
{
    Animator anim;
    Image img;
    // 골드를 몇 초마다 지급하는지 초를 정해주는 변수입니다.
    public static float GoldGetTime = 1;

    // 골드를 얼마나 얻을지에 대한 변수입니다.
    public static float GoldValue = 5;

    public Sprite[] Sprite = new Sprite[3];

    // 컴퓨터가 고장나는 시간 변수입니다.
    [SerializeField]
    float BreakTime;
    [SerializeField]
    float BrokenTime = 10;

    private float moneyGetTime;

    // 고양이가 앉아 있냐 를 뜻하는 변수입니다.
    public bool IsSit = false;

    // 컴퓨터가 부셔졌나 를 뜻하는 변수
    public static bool IsBreak = false;

    // 고양이가 일을 하고 있는가를 뜻하는 변수입니다
    public bool IsWork = false;


    void Start()
    {
        anim = GetComponent<Animator>();
        img = GetComponent<Image>();
    }
    void Update()
    {

        if (IsSit == true && IsBreak == false && IsWork == true)
        {
            anim.SetBool("IsCatSit", true);
            if (GameManager.IsGameOver == false)
            {
                moneyGetTime += Time.deltaTime;
                if (moneyGetTime >= GoldGetTime)
                {
                    moneyGetTime -= GoldGetTime;
                    GameManager.gold += GoldValue;
                    //GameManager.GameTotalGoldValue+= GoldValue;
                }
                BreakTime += Time.deltaTime;
                if (BreakTime >= BrokenTime)
                {
                    IsBreak = true;
                }
            }
        }

        else if (IsSit == false)
        {
            IsBreak = false;
            IsWork = false;
            BreakTime = 0;
            img.sprite = Sprite[0];
        }

        if (IsBreak == true)
        {
            moneyGetTime = 0;
            IsWork = false;
            img.sprite = Sprite[2];
        }
        else if (IsBreak == false && IsSit == true)
        {
            img.sprite = Sprite[2];
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Cat"))
        {
            IsSit = true;
            if (IsSit == true)
                IsWork = true;
            else if (IsSit == true && IsBreak == true)
                IsWork = false;
            Debug.Log("지금 작동중입니다.");
        }

        // 2층일과 닿고 있을때 
        if (collision.CompareTag("Floor2")) 
        {
            // 2층의 불이 켜져있는지 확인
            if (Cat_Manager.Inst.BuyFloor_Idx == 1)
            {
                // 불이 꺼져있다면 고양이가 있을지라도 일을 안 함
                IsWork = false;
            }
            else 
            {
                IsWork = true;
                //Sp.sprite = Sprite[2];
            }
        }
        


    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Cat"))
        {
            IsSit = false;
            Debug.Log("작동이 끝났습니다.");
        }
    }
}
