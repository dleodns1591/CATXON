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
    public GameObject Cat_Area;

    void Start()
    {
        Cat_Area = Cat_Manager.Inst.Cat_Area;
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
        defultposition = this.transform.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        // ���� ��ް� ������ ���� ����� �� �ܰ� �÷��ش�.
        if (Save != null && Save.GetComponent<Cat_Drag>().cat_Information == cat_Information && _Cat_Star == Save.GetComponent<Cat_Drag>()._Cat_Star)
        {
            for (int i = 0; i < Cat_Manager.Inst.D_Area.Count; i++)
            {
                if (Cat_Area == Cat_Manager.Inst.D_Area[i])
                {
                    Cat_Manager.Inst.Area.Add(Cat_Manager.Inst.D_Area[i]);
                    Cat_Manager.Inst.D_Area.RemoveAt(i);
                }
            }
            Save.GetComponent<Cat_Drag>()._Cat_Star++;
            Destroy(this.gameObject);

        }

        // ���� ��ġ�� ����̰� ��ġ�Ǿ��� ��� ��ü�� ����̿� �巡�׸� �� ������� ��ġ�� ���� ��ü�Ѵ�.
        else if (Save != null)
        {
            GameObject Save_Area = Cat_Area;

            transform.position = Save.transform.position;
            Cat_Area = Save.GetComponent<Cat_Drag>().Cat_Area;

            Save.transform.position = defultposition;
            Save.GetComponent<Cat_Drag>().Cat_Area = Save_Area;

        }

        //else if (Save == null && Save.GetComponent<Cat_Drag>().cat_Information != cat_Information)
        //{

        //    for (int i = 0; i < Cat_Manager.Inst.D_Area.Count; i++)
        //    {
        //        if (Cat_Area == Cat_Manager.Inst.D_Area[i])
        //        {
        //            Cat_Manager.Inst.Area.Add(Cat_Manager.Inst.D_Area[i]);
        //            Cat_Manager.Inst.D_Area.RemoveAt(i);
        //        }
        //    }
        //    transform.position = Area_obj.transform.position;
        //}

        // ��ǻ�� ��ġ�� �ƴϰų� �ر��� �ȉ��� ���
        else
            this.transform.position = defultposition;

        if (Recycle == true)
        {
            for (int i = 0; i < Cat_Manager.Inst.D_Area.Count; i++)
            {
                if (Cat_Area == Cat_Manager.Inst.D_Area[i])
                {
                    Cat_Manager.Inst.Area.Add(Cat_Manager.Inst.D_Area[i]);
                    Cat_Manager.Inst.D_Area.RemoveAt(i);
                }
            }
            Destroy(this.gameObject);
        }
    }


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cat"))
            Save = collision.gameObject;

        if (collision.CompareTag("Recycle_Bin"))
            Recycle = true;

        if (collision.CompareTag("Area"))
            Area_obj = collision.GetComponent<Area>().Area_Obj;
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
