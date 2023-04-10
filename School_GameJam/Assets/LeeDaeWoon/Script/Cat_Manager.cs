using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Floor
{
    public List<GameObject> area01 = new List<GameObject>();
    public List<GameObject> area02 = new List<GameObject>();
    public List<GameObject> area03 = new List<GameObject>();
}

public class Cat_Manager : MonoBehaviour
{
    public static Cat_Manager instance;
    void Awake() => instance = this;

    [Header("고양이 소환")]
    public Floor area = new Floor();
    public List<GameObject> summonList = new List<GameObject>();

    [SerializeField] GameObject summonCat;
    [SerializeField] GameObject[] catType = new GameObject[3];

    [Header("위치")]
    public GameObject catArea;
    int areaRandom = 0;

    public int employmentGold;
    public int buyFloorIndex = 0;

    void Start()
    {

    }

    void Update()
    {
        EmployMnetGold();
    }

    // 고용 소비금액
    void EmployMnetGold()
    {
        switch (summonList.Count)
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
        if (GameManager.instance.currentGold >= employmentGold && summonList.Count < 6 * (buyFloorIndex + 1))
        {
            GameManager.instance.currentGold -= employmentGold;

            bool cheak = true;
            int whileCount = 0;
            int randomFloor = 0;

            while (cheak)
            {
                whileCount++;
                randomFloor = Random.Range(0, buyFloorIndex + 1);
                switch (randomFloor)
                {
                    case 0:
                        if (area.area01.Count != 0)
                            cheak = false;
                        break;

                    case 1:
                        if (area.area02.Count != 0)
                            cheak = false;
                        break;

                    case 2:
                        if (area.area03.Count != 0)
                            cheak = false;
                        break;
                }

                if (whileCount > 50)
                    break;
            }

            int catRandom = Random.Range(0, catType.Length);
            switch (randomFloor)
            {
                case 0:
                    {
                        areaRandom = Random.Range(0, area.area01.Count);

                        GameObject cat = Instantiate(catType[catRandom], area.area01[areaRandom].transform.position, Quaternion.identity, summonCat.transform);

                        catArea = area.area01[areaRandom];
                        summonList.Add(catArea);
                        area.area01.RemoveAt(areaRandom);
                        break;
                    }

                case 1:
                    {
                        areaRandom = Random.Range(0, area.area02.Count);

                        Instantiate(catType[catRandom], area.area02[areaRandom].transform.position, Quaternion.identity, GameObject.Find("CatCanvas").transform);
                        catArea = area.area02[areaRandom];
                        summonList.Add(catArea);
                        area.area02.RemoveAt(areaRandom);
                        break;
                    }

                case 2:
                    {
                        areaRandom = Random.Range(0, area.area03.Count);
                        Instantiate(catType[catRandom], area.area03[areaRandom].transform.position, Quaternion.identity, GameObject.Find("CatCanvas").transform);
                        catArea = area.area03[areaRandom];
                        summonList.Add(catArea);
                        area.area03.RemoveAt(areaRandom);
                        break;
                    }

                default:
                    break;
            }
        }
    }
}
