using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor3System : MonoBehaviour
{
    Animator Anim;
    public static bool IsOpenFloor3 = false;
    public static bool ClickButton2 = false;
    void Start()
    {
        ClickButton2 = false;
        Anim = GetComponent<Animator>();
        Anim.SetBool("IsOpen3", false);
    }
    void Update()
    {
        if (ClickButton2 == true && Floor2System.IsOpenFloor2 == true)
        {
            OpenFloor3();
            Debug.Log(IsOpenFloor3);
        }
    }
    void OpenFloor3()
    {
        Anim.SetBool("IsOpen3", true);
        IsOpenFloor3 = true;
        
    }
}
