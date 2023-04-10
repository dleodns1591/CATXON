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

    [SerializeField] GameObject targetCat;
    [SerializeField] GameObject currentArea;

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

    bool isDrag = false;
    bool isRecycle = false;

    void Start()
    {
        currentArea = Cat_Manager.instance.catArea;
    }

    void Update()
    {

    }

    // �巡�� �ϴ� ���콺 Ŀ���� ������ ������ ȣ���Ѵ�.
    public void OnDrag(PointerEventData eventData)
    {
        if (!EventManager.instance.isDragLimit)
            transform.position = eventData.position;
    }

    // �巡�� �� ó�� �� �� ȣ���Ѵ�.
    public void OnBeginDrag(PointerEventData eventData)
    {
        if (!EventManager.instance.isDragLimit)
            currentArea.transform.position = transform.position;
    }

    // �巡�� ���� �� �� �� ȣ���Ѵ�.
    public void OnEndDrag(PointerEventData eventData)
    {
        if (targetCat != null && !EventManager.instance.isDragLimit)
        {
            // ���� ��ް� ������ ���� ����� �� �ܰ� �÷��ش�.
            if (targetCat.GetComponent<CatDrag>().eType == eType && _CatStar == targetCat.GetComponent<CatDrag>()._CatStar)
            {
                for (int i = 0; i < Cat_Manager.instance.summonList.Count; i++)
                {
                    if (currentArea == Cat_Manager.instance.summonList[i])
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
                targetCat.GetComponent<CatDrag>()._CatStar++;
                Destroy(gameObject);
            }

            // ���� ��ġ�� ����̰� ��ġ�Ǿ��� ��� ��ü�� ����̿� �巡�׸� �� ������� ��ġ�� ���� ��ü�Ѵ�.
            GameObject curArea = currentArea;

            transform.position = targetCat.transform.position;
            currentArea = targetCat.GetComponent<CatDrag>().currentArea;

            targetCat.transform.position = curArea.transform.position;
            targetCat.GetComponent<CatDrag>().currentArea = curArea;
        }

        // ��ǻ�� ��ġ�� �ƴϰų� �ر��� �ȉ��� ���
        else
            transform.position = currentArea.transform.position;


        if (isRecycle)
        {
            for (int i = 0; i < Cat_Manager.instance.summonList.Count; i++)
            {
                if (currentArea == Cat_Manager.instance.summonList[i])
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
            targetCat = collision.gameObject;

        if (collision.CompareTag("Recycle_Bin"))
            isRecycle = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Cat"))
            targetCat = null;

        if (collision.CompareTag("Recycle_Bin"))
            isRecycle = false;

        if (collision.CompareTag("Area"))
            isDrag = false;

    }
}
