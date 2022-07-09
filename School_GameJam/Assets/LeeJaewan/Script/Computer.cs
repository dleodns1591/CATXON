using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Computer : GoldSystem
{
    // 골드를 몇 초마다 지급하는지 초를 정해주는 변수입니다.
    public float GoldGetTime = 3;

    // 골드를 얼마나 얻을지에 대한 변수입니다.
    public float GoldValue = 10;



    // 컴퓨터가 고장나는 시간 변수입니다.
    [SerializeField]
    float BreakTime = 5;

    // 고양이가 앉아 있냐 를 뜻하는 변수입니다.
    public bool IsSit = false;

    // 컴퓨터가 부셔졌나 를 뜻하는 변수
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
            Debug.Log("지금 작동중입니다.");
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
