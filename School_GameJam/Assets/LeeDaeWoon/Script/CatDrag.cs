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

    [Header("����� ����")]
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

    [Header("��������")]
    bool isTrash = false;

    [Header("��ǻ��")]
    public GameObject computer;
    public bool isComputer = false;

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
        {
            isDrag = true;
            transform.GetChild(1).gameObject.SetActive(true);
            transform.GetChild(2).gameObject.SetActive(false);
            currentArea.transform.position = transform.position;
        }
    }

    // �巡�� ���� �� �� �� ȣ���Ѵ�.
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
                // ���� ��ް� ���� ���� �� ��
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

                // ���� ��ġ�� ����̰� ��ġ�Ǿ��� ��� ��ü�� ����̿� �巡�׸� �� ������� ��ġ�� ���� ��ü�Ѵ�.
                else
                {
                    GameObject curArea = currentArea; // �ű� ������� ��ġ�� curArea ������Ʈ�� �־��ش�.

                    transform.position = targetCat.transform.position; // ���� ����� ��ġ�� ��ü�� ����� ��ġ�� �̵��Ѵ�.
                    targetCat.transform.position = curArea.transform.position; // ��ü ���� ����̴� ���� ����� �� ��ġ�� �̵��Ѵ�.

                    currentArea = targetCat.GetComponent<CatDrag>().currentArea; // ���� ����� ��ġ�� ��ü�� ����� ��ġ ������Ʈ�� �־��ش�.
                    targetCat.GetComponent<CatDrag>().currentArea = curArea; // ��ü ���� ����̴� ���� ����� �� ��ġ ������Ʈ�� �־��ش�.
                }
            }

            // ��ǻ�� ��ġ�� �ƴϰų� �ر��� �ȉ��� ���
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

        // ��ȯ�� ����� �� ��ŭ for���� �����ش�.
        for (int i = 0; i < Cat_Manager.instance.summonList.Count; i++)
        {
            // ���� ���� ����̿� ���ٸ�
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
