using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Computer : MonoBehaviour
{
    Animator anim;
    Image img;
    // ��带 �� �ʸ��� �����ϴ��� �ʸ� �����ִ� �����Դϴ�.
    public static float GoldGetTime = 1;

    // ��带 �󸶳� �������� ���� �����Դϴ�.
    public static float GoldValue = 5;

    public Sprite[] Sprite = new Sprite[3];

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
            Debug.Log("���� �۵����Դϴ�.");
        }

        // 2���ϰ� ��� ������ 
        if (collision.CompareTag("Floor2")) 
        {
            // 2���� ���� �����ִ��� Ȯ��
            if (Cat_Manager.Inst.BuyFloor_Idx == 1)
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
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Cat"))
        {
            IsSit = false;
            Debug.Log("�۵��� �������ϴ�.");
        }
    }
}
