using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : GoldSystem
{
    // ��带 �� �ʸ��� �����ϴ��� �ʸ� �����ִ� �����Դϴ�.
    public float GoldGetTime = 3;

    // ��带 �󸶳� �������� ���� �����Դϴ�.
    public float GoldValue = 10;



    // ��ǻ�Ͱ� ���峪�� �ð� �����Դϴ�.
    [SerializeField]
    float BreakTime = 5;

    // ����̰� �ɾ� �ֳ� �� ���ϴ� �����Դϴ�.
    public bool IsSit = false;

    // ��ǻ�Ͱ� �μ����� �� ���ϴ� ����
    public bool IsBreak = false;
    void Start()
    {
        
    }
    void Update()
    {

        if (IsSit == true && IsBreak == false)
        {
            StartCoroutine("GoldGet");
            StartCoroutine("ComputerBreak");
        }

        else if (IsSit == false)
        {
            StopCoroutine("GoldGet");
            StartCoroutine("ComputerBreak");
        }

        if (IsBreak == true)
            StopCoroutine("GoldGet");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cat"))
        {
            IsSit = true;
            Debug.Log("���� �۵����Դϴ�.");
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
    IEnumerator ComputerBreak() 
    {
        IsBreak = true;
        yield return new WaitForSeconds(BreakTime);
        IsBreak = false;
    }
    IEnumerator GoldGet() 
    {
        this.PlayerGold = this.PlayerGold + GoldValue;
        yield return new WaitForSeconds(GoldGetTime);    
    }
}
