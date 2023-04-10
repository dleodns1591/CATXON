using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CatDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public enum EInformation
    {
        GrayCat,
        WhiteCat,
        OrnageCat,
    }
    public EInformation eInformation;

    int catStar = 0;
    bool isDrag = false;
    bool isRecycle = false;
    public GameObject Area_obj;

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
    [SerializeField] GameObject save;

    public static Vector2 currentPos;
    GameObject catArea;

    void Start()
    {
        catArea = Cat_Manager.instance.catArea;
    }

    void Update()
    {

    }

    // 드래그 하는 마우스 커서가 움직일 때마다 호출한다.
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    // 드래그 시 처음 한 번 호출한다.
    public void OnBeginDrag(PointerEventData eventData)
    {
        currentPos = transform.position;
    }

    // 드래그 종료 시 한 번 호출한다.
    public void OnEndDrag(PointerEventData eventData)
    {
        if (save != null)
        {
            // 같은 등급과 종류를 합쳐 등급을 한 단계 올려준다.
            if (save.GetComponent<CatDrag>().eInformation == eInformation && _CatStar == save.GetComponent<CatDrag>()._CatStar)
            {
                for (int i = 0; i < Cat_Manager.instance.summonList.Count; i++)
                {
                    if (catArea == Cat_Manager.instance.summonList[i])
                    {
                        switch (Cat_Manager.instance.summonList[i].GetComponent<Area>().floor)
                        {
                            case 0:
                                Cat_Manager.instance.area.area01.Add(Cat_Manager.instance.summonList[i]);
                                break;

                            case 1:
                                Cat_Manager.instance.area.area02.Add(Cat_Manager.instance.summonList[i]);
                                break;

                            case 2:
                                Cat_Manager.instance.area.area03.Add(Cat_Manager.instance.summonList[i]);
                                break;
                        }
                        Cat_Manager.instance.summonList.RemoveAt(i);
                    }
                }
                save.GetComponent<CatDrag>()._CatStar++;
                Destroy(gameObject);
            }

            // 현재 위치에 고양이가 배치되었을 경우 교체할 고양이와 드래그를 할 고양이의 위치를 서로 교체한다.
            GameObject Save_Area = catArea;

            transform.position = save.transform.position;
            catArea = save.GetComponent<CatDrag>().catArea;

            save.transform.position = currentPos;
            save.GetComponent<CatDrag>().catArea = Save_Area;
        }

        // 컴퓨터 위치가 아니거나 해금이 안됬을 경우
        else
            transform.position = currentPos;

        if (isRecycle)
        {
            for (int i = 0; i < Cat_Manager.instance.summonList.Count; i++)
            {
                if (catArea == Cat_Manager.instance.summonList[i])
                {
                    switch (Cat_Manager.instance.summonList[i].GetComponent<Area>().floor)
                    {
                        case 0:
                            Cat_Manager.instance.area.area01.Add(Cat_Manager.instance.summonList[i]);
                            break;
                        case 1:
                            Cat_Manager.instance.area.area02.Add(Cat_Manager.instance.summonList[i]);
                            break;
                        case 2:
                            Cat_Manager.instance.area.area03.Add(Cat_Manager.instance.summonList[i]);
                            break;
                    }
                    Cat_Manager.instance.summonList.RemoveAt(i);
                }
            }
            Destroy(gameObject);
        }
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cat"))
            save = collision.gameObject;

        if (collision.CompareTag("Recycle_Bin"))
            isRecycle = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Cat"))
            save = null;

        if (collision.CompareTag("Recycle_Bin"))
            isRecycle = false;

        if (collision.CompareTag("Area"))
            isDrag = false;

    }
}
