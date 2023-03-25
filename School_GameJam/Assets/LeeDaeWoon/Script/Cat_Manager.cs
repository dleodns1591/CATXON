using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Floor
{
    public List<GameObject> floor01 = new List<GameObject>();
    public List<GameObject> floor02 = new List<GameObject>();
    public List<GameObject> floor03 = new List<GameObject>();
}

public class Cat_Manager : MonoBehaviour
{
    public static Cat_Manager instance;
    void Awake() => instance = this;

    [Header("고양이 소환")]
    public Floor area = new Floor();
    public List<GameObject> D_Area = new List<GameObject>();
    public List<GameObject> Cat_Num = new List<GameObject>();

    public GameObject[] catType = new GameObject[3];

    [Header("위치")]
    public GameObject catArea;
    int areaRandom = 0;

    public int employmentGold;
    public int BuyFloor_Idx = 0;

    void Start()
    {

    }

    void Update()
    {
        EmployMnetGold();
    }

    void EmployMnetGold()
    {
        switch (D_Area.Count)
        {
            case 0:
                employmentGold = 50;
                break;
            case 1:
                employmentGold = 60;
                break;
            case 2:
                employmentGold = 72;
                break;
            case 3:
                employmentGold = 86;
                break;
            case 4:
                employmentGold = 111;
                break;
            case 5:
                employmentGold = 144;
                break;
            case 6:
                employmentGold = 187;
                break;
            case 7:
                employmentGold = 243;
                break;
            case 8:
                employmentGold = 315;
                break;
            case 9:
                employmentGold = 409;
                break;
            case 10:
                employmentGold = 531;
                break;
            case 11:
                employmentGold = 796;
                break;
            case 12:
                employmentGold = 1194;
                break;
            case 13:
                employmentGold = 1791;
                break;
            case 14:
                employmentGold = 2686;
                break;
            case 15:
                employmentGold = 4029;
                break;
            case 16:
                employmentGold = 6043;
                break;
            case 17:
                employmentGold = 9064;
                break;
        }
    }

    public void CatSummon()
    {
        if (GameManager.instance.currentGold >= employmentGold && D_Area.Count < 6 * (BuyFloor_Idx + 1))
        {
            GameManager.instance.currentGold -= employmentGold;
            int catRandom = Random.Range(0, 3);

            bool cheak = true;
            int whileCount = 0;
            int randomFloor = 0;

            while (cheak)
            {
                whileCount++;
                randomFloor = Random.Range(0, BuyFloor_Idx + 1);
                switch (randomFloor)
                {
                    case 0:
                        if (area.floor01.Count != 0)
                            cheak = false;
                        break;

                    case 1:
                        if (area.floor02.Count != 0)
                            cheak = false;
                        break;

                    case 2:
                        if (area.floor03.Count != 0)
                            cheak = false;
                        break;
                }

                if (whileCount > 50)
                    break;
            }

            switch (randomFloor)
            {
                case 0:
                    {
                        areaRandom = Random.Range(0, area.floor01.Count);

                        Instantiate(catType[catRandom], area.floor01[areaRandom].transform.position, Quaternion.identity, GameObject.Find("CatCanvas").transform);
                        catArea = area.floor01[areaRandom];
                        D_Area.Add(area.floor01[areaRandom]);
                        area.floor01.RemoveAt(areaRandom);
                        break;
                    }

                case 1:
                    {
                        areaRandom = Random.Range(0, area.floor02.Count);
                        Instantiate(catType[catRandom], area.floor02[areaRandom].transform.position, Quaternion.identity, GameObject.Find("CatCanvas").transform);
                        catArea = area.floor02[areaRandom];
                        D_Area.Add(area.floor02[areaRandom]);
                        area.floor02.RemoveAt(areaRandom);
                        break;
                    }

                case 2:
                    {
                        areaRandom = Random.Range(0, area.floor03.Count);
                        Instantiate(catType[catRandom], area.floor03[areaRandom].transform.position, Quaternion.identity, GameObject.Find("CatCanvas").transform);
                        catArea = area.floor03[areaRandom];
                        D_Area.Add(area.floor03[areaRandom]);
                        area.floor03.RemoveAt(areaRandom);
                        break;
                    }

                default:
                    break;
            }
        }
    }
}
