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
        Area_06,
        Area_07,
        Area_08,
        Area_09,
        Area_10,
        Area_11,
        Area_12,
        Area_13,
        Area_14,
        Area_15,
        Area_16,
        Area_17,
        Area_18
    }
    public Area_Name area_Name;
    public GameObject Area_Obj;
    public int floor;

    void Start()
    {
        switch (area_Name)
        {
            case Area_Name.Area_01:
                Area_Obj = this.gameObject;
                floor = 0;
                break;

            case Area_Name.Area_02:
                Area_Obj = this.gameObject;
                floor = 0;
                break;

            case Area_Name.Area_03:
                Area_Obj = this.gameObject;
                floor = 0;
                break;

            case Area_Name.Area_04:
                Area_Obj = this.gameObject;
                floor = 0;
                break;

            case Area_Name.Area_05:
                Area_Obj = this.gameObject;
                floor = 0;
                break;

            case Area_Name.Area_06:
                floor = 0;
                Area_Obj = this.gameObject;
                break;

            case Area_Name.Area_07:
                Area_Obj = this.gameObject;
                floor = 1;
                break;

            case Area_Name.Area_08:
                Area_Obj = this.gameObject;
                floor = 1;
                break;

            case Area_Name.Area_09:
                Area_Obj = this.gameObject;
                floor = 1;
                break;

            case Area_Name.Area_10:
                Area_Obj = this.gameObject;
                floor = 1;
                break;

            case Area_Name.Area_11:
                Area_Obj = this.gameObject;
                floor = 1;
                break;

            case Area_Name.Area_12:
                Area_Obj = this.gameObject;
                floor = 1;
                break;

            case Area_Name.Area_13:
                Area_Obj = this.gameObject;
                floor = 2;
                break;

            case Area_Name.Area_14:
                Area_Obj = this.gameObject;
                floor = 2;
                break;

            case Area_Name.Area_15:
                Area_Obj = this.gameObject;
                floor = 2;
                break;

            case Area_Name.Area_16:
                Area_Obj = this.gameObject;
                floor = 2;
                break;

            case Area_Name.Area_17:
                Area_Obj = this.gameObject;
                floor = 2;
                break;

            case Area_Name.Area_18:
                Area_Obj = this.gameObject;
                floor = 2;
                break;


        }
    }

    void Update()
    {

    }

}
