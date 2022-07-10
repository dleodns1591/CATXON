using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cat_Manager : MonoBehaviour
{
    public static Cat_Manager Inst { get; private set; }
    void Awake() => Inst = this;

    [Header("고양이 소환")]
    public List<GameObject> Cat_List = new List<GameObject>();
    public List<GameObject> Area = new List<GameObject>();
    public List<GameObject> D_Area = new List<GameObject>();
    public List<GameObject> Cat_Num = new List<GameObject>();
    private int Cat_Random;

    [Header("위치")]
    public GameObject Cat_Area;
    private int Area_Random;

    public Text Gold_Text;
    public int Gold;

    int Click = 0;

    void Start()
    {

    }

    void Update()
    {
        Gold_Text.text = "" + Gold;
        switch (D_Area.Count)
        {
            case 0:
                Gold = 50;
                break;
            case 1:
                Gold = 60;
                break;
            case 2:
                Gold = 72;
                break;
            case 3:
                Gold = 86;
                break;
            case 4:
                Gold = 111;
                break;
            case 5:
                Gold = 144;
                break;
            case 6:
                Gold = 187;
                break;
            case 7:
                Gold = 243;
                break;
            case 8:
                Gold = 315;
                break;
            case 9:
                Gold = 409;
                break;
            case 10:
                Gold = 531;
                break;
            case 11:
                Gold = 796;
                break;
            case 12:
                Gold = 1194;
                break;
            case 13:
                Gold = 1791;
                break;
            case 14:
                Gold = 2686;
                break;
            case 15:
                Gold = 4029;
                break;
            case 16:
                Gold = 6043;
                break;
            case 17:
                Gold = 9064;
                break;

        }
    }

    public void Cat_Instantitate_Click()
    {
        
        GameManager.gold -= Gold;
        if (Area.Count > 0)
        {
            Cat_Random = Random.Range(0, 3);
            Area_Random = Random.Range(0, Area.Count);

            Instantiate(Cat_List[Cat_Random], Area[Area_Random].transform.position, Quaternion.identity, GameObject.Find("Cat_Canvas").transform);

            Cat_Area = Area[Area_Random];
            D_Area.Add(Area[Area_Random]);
            Area.RemoveAt(Area_Random);

        }
    }
}
