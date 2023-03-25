using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floor2System : MonoBehaviour
{
    Animator Anim;
    public static bool ClickButton = false;
    public GameObject Button;
    void Start()
    {
        ClickButton = false;
        Anim = GetComponent<Animator>();
        Anim.SetBool("IsOpen2", false);
    }
    void Update()
    {
        
    }
    public void OpenFloor2()
    {
        if(GameManager.gold >= 800 && Cat_Manager.instance.BuyFloor_Idx == 0)
        {
            GameManager.gold -= 800;
            Anim.SetBool("IsOpen2", true);
            ++Cat_Manager.instance.BuyFloor_Idx;
            Button.SetActive(false);
        }
        
    }
}


