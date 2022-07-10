using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[System.Serializable]
public class Floor
{
    public List<GameObject> floor_1 = new List<GameObject>();
    public List<GameObject> floor_2 = new List<GameObject>();
    public List<GameObject> floor_3 = new List<GameObject>();
}

public class Cat_Manager : MonoBehaviour
{
    public static Cat_Manager Inst { get; private set; }
    void Awake() => Inst = this;

    [Header("고양이 소환")]
    public List<GameObject> Cat_List = new List<GameObject>();
    public Floor Area = new Floor();
    public List<GameObject> D_Area = new List<GameObject>();
    public List<GameObject> Cat_Num = new List<GameObject>();
    private int Cat_Random;

    [Header("위치")]
    public GameObject Cat_Area;
    private int Area_Random;

    public Text Gold_Text;
    public int Gold;

    int Click = 0;

    public int BuyFloor_Idx = 0;
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
        if (GameManager.gold >= Gold && D_Area.Count < 6 * (BuyFloor_Idx + 1))
        {
            GameManager.gold -= Gold;
            Cat_Random = Random.Range(0, 3);
            int Random_Floor = 0;
            bool cheak = true;
            int whileCount = 0;
            while (cheak)
            {
                whileCount++;
                Random_Floor = Random.Range(0, BuyFloor_Idx + 1);
                switch (Random_Floor)
                {
                    case 0:
                        if (Area.floor_1.Count != 0)
                            cheak = false;
                        break;
                    case 1:
                        if (Area.floor_2.Count != 0)
                            cheak = false;
                        break;
                    case 2:
                        if (Area.floor_3.Count != 0)
                            cheak = false;
                        break;
                }
                if (whileCount > 50)
                {
                    Debug.Log("와이 돈 유 다이");
                    break;
                }
            }
            Debug.Log(Area.floor_1.Count);
            Debug.Log(Random_Floor);
            switch (Random_Floor)
            {
                case 0:
                    {
                        Area_Random = Random.Range(0, Area.floor_1.Count);
                        Instantiate(Cat_List[Cat_Random], Area.floor_1[Area_Random].transform.position, Quaternion.identity, GameObject.Find("Cat_Canvas").transform);
                        Cat_Area = Area.floor_1[Area_Random];
                        D_Area.Add(Area.floor_1[Area_Random]);
                        Area.floor_1.RemoveAt(Area_Random);
                        break;
                    }
                case 1:
                    {
                        Area_Random = Random.Range(0, Area.floor_2.Count);
                        Instantiate(Cat_List[Cat_Random], Area.floor_2[Area_Random].transform.position, Quaternion.identity, GameObject.Find("Cat_Canvas").transform);
                        Cat_Area = Area.floor_2[Area_Random];
                        D_Area.Add(Area.floor_2[Area_Random]);
                        Area.floor_2.RemoveAt(Area_Random);
                        break;
                    }
                case 2:
                    {
                        Area_Random = Random.Range(0, Area.floor_3.Count);
                        Instantiate(Cat_List[Cat_Random], Area.floor_3[Area_Random].transform.position, Quaternion.identity, GameObject.Find("Cat_Canvas").transform);
                        Cat_Area = Area.floor_3[Area_Random];
                        D_Area.Add(Area.floor_3[Area_Random]);
                        Area.floor_3.RemoveAt(Area_Random);
                        break;
                    }
                default:
                    Debug.Log("니가 왜 떠 ㅅㅂ");
                    Debug.Log(Random_Floor);
                    break;
            }
        }
    }
}
