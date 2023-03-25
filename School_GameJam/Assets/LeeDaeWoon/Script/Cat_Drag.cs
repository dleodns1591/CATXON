using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Cat_Drag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    public enum Cat_Information
    {
        Tricolored_Cat,
        White_Cat,
        RussianBlue_Cat
    }
    public Cat_Information cat_Information;

    int Cat_Star;
    bool Recycle = false;
    bool Drage_Check = false;
    public GameObject Area_obj;

    public int _Cat_Star
    {
        get { return Cat_Star; }
        set
        {
            transform.GetChild(Cat_Star).gameObject.SetActive(false);
            transform.GetChild(Cat_Star + 1).gameObject.SetActive(true);
            Cat_Star = value;
        }
    }
    GameObject Save;

    public static Vector2 defultposition;
    GameObject catArea;

    void Start()
    {
        catArea = Cat_Manager.instance.catArea;
    }

    void Update()
    {

    }

    public void OnDrag(PointerEventData eventData)
    {
        transform.position = eventData.position;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        defultposition = transform.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // ���� ��ް� ������ ���� ����� �� �ܰ� �÷��ش�.
        if (Save != null && Save.GetComponent<Cat_Drag>().cat_Information == cat_Information && _Cat_Star == Save.GetComponent<Cat_Drag>()._Cat_Star)
        {
            for (int i = 0; i < Cat_Manager.instance.D_Area.Count; i++)
            {
                if (catArea == Cat_Manager.instance.D_Area[i])
                {
                    switch (Cat_Manager.instance.D_Area[i].GetComponent<Area>().floor)
                    {
                        case 0:
                            Cat_Manager.instance.area.floor01.Add(Cat_Manager.instance.D_Area[i]);
                            break;

                        case 1:
                            Cat_Manager.instance.area.floor02.Add(Cat_Manager.instance.D_Area[i]);
                            break;

                        case 2:
                            Cat_Manager.instance.area.floor03.Add(Cat_Manager.instance.D_Area[i]);
                            break;
                    }
                    Cat_Manager.instance.D_Area.RemoveAt(i);
                }
            }
            Save.GetComponent<Cat_Drag>()._Cat_Star++;
            Destroy(gameObject);

        }

        // ���� ��ġ�� ����̰� ��ġ�Ǿ��� ��� ��ü�� ����̿� �巡�׸� �� ������� ��ġ�� ���� ��ü�Ѵ�.
        else if (Save != null)
        {
            GameObject Save_Area = catArea;

            transform.position = Save.transform.position;
            catArea = Save.GetComponent<Cat_Drag>().catArea;

            Save.transform.position = defultposition;
            Save.GetComponent<Cat_Drag>().catArea = Save_Area;

        }

        // ��ǻ�� ��ġ�� �ƴϰų� �ر��� �ȉ��� ���
        else
            transform.position = defultposition;

        if (Recycle)
        {
            for (int i = 0; i < Cat_Manager.instance.D_Area.Count; i++)
            {
                if (catArea == Cat_Manager.instance.D_Area[i])
                {
                    switch (Cat_Manager.instance.D_Area[i].GetComponent<Area>().floor)
                    {
                        case 0:
                            Cat_Manager.instance.area.floor01.Add(Cat_Manager.instance.D_Area[i]);
                            break;
                        case 1:
                            Cat_Manager.instance.area.floor02.Add(Cat_Manager.instance.D_Area[i]);
                            break;
                        case 2:
                            Cat_Manager.instance.area.floor03.Add(Cat_Manager.instance.D_Area[i]);
                            break;
                    }
                    Cat_Manager.instance.D_Area.RemoveAt(i);
                }
            }
            Destroy(gameObject);
        }
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cat"))
            Save = collision.gameObject;

        if (collision.CompareTag("Recycle_Bin"))
            Recycle = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Cat"))
            Save = null;

        if (collision.CompareTag("Recycle_Bin"))
            Recycle = false;

        if (collision.CompareTag("Area"))
            Drage_Check = false;

    }
}
