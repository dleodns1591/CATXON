using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cat_Manager : MonoBehaviour
{
    public static Cat_Manager Inst { get; private set; }
    void Awake() => Inst = this;

    [Header("고양이 소환")]
    public List<GameObject> Cat_List = new List<GameObject>();
    public List<GameObject> Cat_Instantiate = new List<GameObject>();
    private int Cat_Random;

    [Header("위치")]
    public GameObject Cat_Area;
    private int Area_Random;
    

    void Start()
    {

    }

    void Update()
    {

    }

    public void Cat_Instantitate_Click()
    {
        Cat_Random = Random.Range(0, 3);
        Area_Random = Random.Range(0, 6);

        Instantiate(Cat_List[Cat_Random], Cat_Area.transform.GetChild(Area_Random).position, Quaternion.identity, GameObject.Find("Cat_Area_Canvas").transform);

    }
}
