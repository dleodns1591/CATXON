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
    public bool isDrag = false;

    int catStar = 0;

    public int _CatStar
    {
        get { return catStar; }
        set
        {
            transform.GetChild(0).GetChild(catStar).gameObject.SetActive(false);
            transform.GetChild(0).GetChild(catStar + 1).gameObject.SetActive(true);
            catStar = value;
        }
    }

    [Header("쓰레기통")]
    bool isTrash = false;

    [Header("컴퓨터")]
    public GameObject computer;
    public bool isComputer = false;

    void Start()
    {
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
            isDrag = true;
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(2).gameObject.SetActive(false);
            currentArea.transform.position = transform.position;
        }
    }

    // 드래그 종료 시 한 번 호출한다.
    public void OnEndDrag(PointerEventData eventData)
    {
        isDrag = false;
        transform.GetChild(1).gameObject.SetActive(false);
        transform.GetChild(2).gameObject.SetActive(true);

        Trash();
        CatAdd();
    }

    void CatAdd()
    {
        if (!EventManager.instance.isDragLimit)
        {
            if (targetCat != null)
            {
                // 같은 등급과 같은 종류 일 때
                if (eType == targetCat.GetComponent<CatDrag>().eType && _CatStar == targetCat.GetComponent<CatDrag>()._CatStar)
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
                    targetCat.transform.position = curArea.transform.position; // 교체 당한 고양이는 현재 고양이 전 위치로 이동한다.

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
    }

    void Trash()
    {
        if (isTrash)
        {
            for (int i = 0; i < Cat_Manager.instance.summonList.Count; i++)
            {
                if (currentArea == Cat_Manager.instance.summonList[i])
                {

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
                currentArea = computerArea.currentArea.gameObject;

                int floor = Cat_Manager.instance.summonList[i].GetComponent<Area>().floor;

                Cat_Manager.instance.area[floor].areaList.Add(Cat_Manager.instance.summonList[i]);
                Cat_Manager.instance.summonList.RemoveAt(i);
                Cat_Manager.instance.summonList.Add(currentArea);

                for (int j = 0; j < Cat_Manager.instance.area[floor].areaList.Count; j++)
                {
                    if (Cat_Manager.instance.area[floor].areaList[j] == Cat_Manager.instance.summonList[i])
                        Cat_Manager.instance.area[floor].areaList.RemoveAt(j);
                }

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
            if (!collision.GetComponent<Computer>().isSit)
            {
                isComputer = true;
                computer = collision.gameObject;
            }
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Cat"))
            targetCat = null;

        if (collision.CompareTag("Trash"))
            isTrash = false;

        if (collision.CompareTag("Computer"))
        {
            if (currentArea == collision.GetComponent<Computer>().currentArea.gameObject)
            {
                isComputer = false;
            }
        }
    }
}
