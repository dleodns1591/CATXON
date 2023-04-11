using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class Floor
{
    public List<GameObject> areaList = new List<GameObject>();
}

public class Cat_Manager : MonoBehaviour
{
    public static Cat_Manager instance;
    void Awake() => instance = this;

    [Header("����� ��ȯ")]
    public Floor[] area = new Floor[3];
    public List<GameObject> summonList = new List<GameObject>();

    [SerializeField] GameObject summonCat;
    [SerializeField] GameObject[] catType = new GameObject[3];

    [Header("��ġ")]
    int areaRandom = 0;
    public int employmentGold;
    public int floorIndex = 0;
    public GameObject catArea;

    void Start()
    {

    }

    void Update()
    {
        EmployMnetGold();
    }

    // ��� �Һ�ݾ�
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
        // �ر� �� ���� ����� ���� ��ȯ�� ����� ������ �޶�����.
        if (GameManager.instance.currentGold >= employmentGold && summonList.Count < 6 * (floorIndex + 1))
        {
            GameManager.instance.currentGold -= employmentGold;
            CatRandom();
        }
    }

    void CatRandom()
    {
        int catRandom = Random.Range(0, catType.Length);
        int randomFloor = Random.Range(0, floorIndex + 1);

        if (area[randomFloor].areaList.Count != 0)
        {
            areaRandom = Random.Range(0, area[randomFloor].areaList.Count);
            Instantiate(catType[catRandom], area[randomFloor].areaList[areaRandom].transform.position, Quaternion.identity, summonCat.transform);

            catArea = area[randomFloor].areaList[areaRandom]; // ������ ��Ҹ� catArea ������ �־��ش�.
            summonList.Add(catArea); // ��ȯ�ȸ���Ʈ �ȿ� catArea�� �־��ش�.
            area[randomFloor].areaList.RemoveAt(areaRandom); // ����� ��Ҹ� �����ش�.
        }

        else
            CatRandom();
    }
}
