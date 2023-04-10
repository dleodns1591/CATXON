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

    // �巡�� �ϴ� ���콺 Ŀ���� ������ ������ ȣ���Ѵ�.
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    // �巡�� �� ó�� �� �� ȣ���Ѵ�.
    public void OnBeginDrag(PointerEventData eventData)
    {
        currentPos = transform.position;
    }

    // �巡�� ���� �� �� �� ȣ���Ѵ�.
    public void OnEndDrag(PointerEventData eventData)
    {
        if (save != null)
        {
            // ���� ��ް� ������ ���� ����� �� �ܰ� �÷��ش�.
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

            // ���� ��ġ�� ����̰� ��ġ�Ǿ��� ��� ��ü�� ����̿� �巡�׸� �� ������� ��ġ�� ���� ��ü�Ѵ�.
            GameObject Save_Area = catArea;

            transform.position = save.transform.position;
            catArea = save.GetComponent<CatDrag>().catArea;

            save.transform.position = currentPos;
            save.GetComponent<CatDrag>().catArea = Save_Area;
        }

        // ��ǻ�� ��ġ�� �ƴϰų� �ر��� �ȉ��� ���
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
