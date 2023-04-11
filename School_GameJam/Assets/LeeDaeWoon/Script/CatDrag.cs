using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CatDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public enum EType
    {
        GrayCat,
        WhiteCat,
        OrnageCat,
    }
    public EType eType;

    [Header("고양이 설정")]
    [SerializeField] GameObject targetCat;
    public GameObject currentArea;
    public bool isSit = false;
    public bool isDrag = false;
    int catStar = 0;

    public int _CatStar
    {
        get { return catStar; }
        set
        {
            transform.GetChild(catStar).gameObject.SetActive(false);
            transform.GetChild(catStar + 1).gameObject.SetActive(true);
            catStar = value;
        }
    }

    [Header("쓰레기통")]
    bool isTrash = false;

    [Header("컴퓨터")]
    GameObject computer;
    bool isComputer = false;

    void Start()
    {
        isSit = true;
        currentArea = Cat_Manager.instance.catArea;
    }

    void Update()
    {

    }

    // 드래그 하는 마우스 커서가 움직일 때마다 호출한다.
    public void OnDrag(PointerEventData eventData)
    {
        if (!EventManager.instance.isDragLimit)
            transform.position = eventData.position;
    }

    // 드래그 시 처음 한 번 호출한다.
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!EventManager.instance.isDragLimit)
        {
            isSit = false;
            currentArea.transform.position = transform.position;
        }
    }

    // 드래그 종료 시 한 번 호출한다.
    public void OnEndDrag(PointerEventData eventData)
    {
        isSit = true;

        Trash();
        CatAdd();
    }

    void CatAdd()
    {
        if (targetCat != null && !EventManager.instance.isDragLimit)
        {
            // 같은 등급과 같은 종류 일 때
            if (targetCat.GetComponent<CatDrag>().eType == eType && _CatStar == targetCat.GetComponent<CatDrag>()._CatStar)
            {
                for (int i = 0; i < Cat_Manager.instance.summonList.Count; i++)
                {
                    if (currentArea == Cat_Manager.instance.summonList[i])
                    {
                        int floor = Cat_Manager.instance.summonList[i].GetComponent<Area>().floor;
                        Cat_Manager.instance.area[floor].areaList.Add(Cat_Manager.instance.summonList[i]);
                        Cat_Manager.instance.summonList.RemoveAt(i);
                    }
                }

                targetCat.GetComponent<CatDrag>()._CatStar++;
                Destroy(gameObject);
            }

            // 현재 위치에 고양이가 배치되었을 경우 교체할 고양이와 드래그를 할 고양이의 위치를 서로 교체한다.
            else
            {
                GameObject curArea = currentArea; // 옮길 고양이의 위치를 curArea 오브젝트에 넣어준다.

                transform.position = targetCat.transform.position; // 현재 고양이 위치를 교체할 고양이 위치로 이동한다.
                targetCat.transform.position = currentArea.transform.position; // 교체 당한 고양이는 현재 고양이 전 위치로 이동한다.

                currentArea = targetCat.GetComponent<CatDrag>().currentArea; // 현재 고양이 위치를 교체할 고양이 위치 오브젝트를 넣어준다.
                targetCat.GetComponent<CatDrag>().currentArea = curArea; // 교체 당한 고양이는 현재 고양이 전 위치 오브젝트를 넣어준다.
            }
        }

        // 컴퓨터 위치가 아니거나 해금이 안됬을 경우
        else
        {
            if (!isComputer)
                transform.position = currentArea.transform.position;
            else
                ComPuter();
        }
    }

    void Trash()
    {
        if (isTrash)
        {
            for (int i = 0; i < Cat_Manager.instance.summonList.Count; i++)
            {
                if (currentArea == Cat_Manager.instance.summonList[i])
                {
                    isSit = false;

                    int floor = Cat_Manager.instance.summonList[i].GetComponent<Area>().floor;
                    Cat_Manager.instance.area[floor].areaList.Add(Cat_Manager.instance.summonList[i]);
                    Destroy(gameObject);
                    Cat_Manager.instance.summonList.RemoveAt(i);
                }
            }
        }
    }

    void ComPuter()
    {
        var computerArea = computer.GetComponent<Computer>();

        // 소환된 고양이 수 만큼 for문을 돌려준다.
        for (int i = 0; i < Cat_Manager.instance.summonList.Count; i++)
        {
            // 만약 현재 고양이와 같다면
            if (currentArea == Cat_Manager.instance.summonList[i])
            {
                int floor = Cat_Manager.instance.summonList[i].GetComponent<Area>().floor;

                // areaList에 summonList[i]를 넣어준다,
                Cat_Manager.instance.area[floor].areaList.Add(Cat_Manager.instance.summonList[i]);
                currentArea = computerArea.currentArea.gameObject;
                Cat_Manager.instance.summonList.RemoveAt(i);


                //for (int p = 0; p < 3; p++)
                //{
                //    for (int c = 0; c < 6; c++)
                //    {
                //        if (currentArea == Cat_Manager.instance.area[p].areaList[c])
                //        {
                //            Cat_Manager.instance.summonList.Add(Cat_Manager.instance.area[p].areaList[c]);
                //            Cat_Manager.instance.area[p].areaList.RemoveAt(c);
                //            break;
                //        }
                //    }
                //}
            }
        }


        transform.position = currentArea.transform.position;
        isComputer = false;
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Cat"))
            targetCat = collision.gameObject;

        if (collision.CompareTag("Trash"))
            isTrash = true;

        if (collision.CompareTag("Computer"))
        {
            isComputer = true;
            computer = collision.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Cat"))
            targetCat = null;

        if (collision.CompareTag("Computer"))
        {
            if (currentArea == collision.GetComponent<Computer>().currentArea.gameObject)
            {
                isSit = false;
                isComputer = false;
            }
        }
    }
}
