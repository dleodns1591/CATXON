using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor3System : MonoBehaviour
{
    Animator Anim;
    public static bool ClickButton2 = false;
    public GameObject Button;
    void Start()
    {
        ClickButton2 = false;
        Anim = GetComponent<Animator>();
        Anim.SetBool("IsOpen3", false);
    }
    void Update()
    {
      
    }
    public void OpenFloor3()
    {
        //if (GameManager.gold >= 3000 && Cat_Manager.instance.BuyFloor_Idx == 1)
        //{
        //    GameManager.gold -= 3000;
        //    Anim.SetBool("IsOpen3", true);
        //    ++Cat_Manager.instance.BuyFloor_Idx;
        //    Button.SetActive(false);
        //}

    }
}
