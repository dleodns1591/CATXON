using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellingSystem : MonoBehaviour
{
    Animator Anim;
    public static bool OnClickButton3 = false;
    void Start()
    {
        OnClickButton3 = false;
        Anim = GetComponent<Animator>();
        Anim.SetBool("IsOpenCelling", false);
    }
    void Update()
    {
        if (OnClickButton3 == true && GameManager.gold >= 3000) 
        {
            
            OpenCelling();
        }
    }
    public void OpenCelling() 
    {
        Anim.SetBool("IsOpenCelling", true);
    }
}
