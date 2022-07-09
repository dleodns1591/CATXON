using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor2System : MonoBehaviour
{
    Animator Anim;
    public static bool IsOpenFloor2 = false;
    public static bool ClickButton = false;
    void Start()
    {
        ClickButton = false;
        Anim = GetComponent<Animator>();
        Anim.SetBool("IsOpen2", false);
    }
    void Update()
    {
        if (ClickButton == true && GameManager.gold >= 801)
        {
            OpenFloor2();

            Debug.Log(IsOpenFloor2);
        }
    }
    public void OpenFloor2()
    {
        Anim.SetBool("IsOpen2", true);
        IsOpenFloor2 = true;
        
    }
}


