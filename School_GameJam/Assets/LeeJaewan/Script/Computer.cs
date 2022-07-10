using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : MonoBehaviour
{
    Animator anim;

    // ��带 �� �ʸ��� �����ϴ��� �ʸ� �����ִ� �����Դϴ�.
    public static float GoldGetTime = 1;

    // ��带 �󸶳� �������� ���� �����Դϴ�.
    public static float GoldValue = 5;

    

    // ��ǻ�Ͱ� ���峪�� �ð� �����Դϴ�.
    [SerializeField]
    float BreakTime;
    [SerializeField]
    float BrokenTime = 10;

    private float moneyGetTime;

    // ����̰� �ɾ� �ֳ� �� ���ϴ� �����Դϴ�.
    public bool IsSit = false;

    // ��ǻ�Ͱ� �μ����� �� ���ϴ� ����
    public bool IsBreak = false;

    // ����̰� ���� �ϰ� �ִ°��� ���ϴ� �����Դϴ�
    public bool IsWork = false;


    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {

        if (IsSit == true && IsBreak == false && IsWork == true)
        {
            anim.SetBool("IsCatSit", true);
            moneyGetTime += Time.deltaTime;
            if (moneyGetTime >= GoldGetTime)
            {
                moneyGetTime -= GoldGetTime;
                GameManager.gold += GoldValue;
            }
            BreakTime += Time.deltaTime;
            if (BreakTime >= BrokenTime) 
            {
                IsBreak = true;
            }
        }

        else if (IsSit == false)
        {
            IsBreak = false;
            IsWork = false;
            BreakTime = 0;
            anim.SetBool("IsCatSit", false);
        }

        if (IsBreak == true)
        {
            moneyGetTime = 0;
            IsWork = false;
            anim.SetBool("IsBroken", true);
        }
        else if (IsBreak == false)
        {
            anim.SetBool("IsBroken", false);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
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
