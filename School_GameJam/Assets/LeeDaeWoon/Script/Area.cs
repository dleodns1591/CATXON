using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    public static Area Inst { get; private set; }
    void Awake() => Inst = this;

    public enum Area_Name
    {
        Area_01,
        Area_02,
        Area_03,
        Area_04,
        Area_05,
        Area_06
    }
    public Area_Name area_Name;
    public GameObject Area_Obj;


    void Start()
    {
        switch (area_Name)
        {
            case Area_Name.Area_01:
                Area_Obj = this.gameObject;
                break;

            case Area_Name.Area_02:
                Area_Obj = this.gameObject;
                break;

            case Area_Name.Area_03:
                Area_Obj = this.gameObject;
                break;

            case Area_Name.Area_04:
                Area_Obj = this.gameObject;
                break;

            case Area_Name.Area_05:
                Area_Obj = this.gameObject;
                break;

            case Area_Name.Area_06:
                Area_Obj = this.gameObject;
                break;

        }
    }

    void Update()
    {

    }

}
