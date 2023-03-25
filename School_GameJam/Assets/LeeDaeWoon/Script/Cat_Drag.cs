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
        // 같은 등급과 종류를 합쳐 등급을 한 단계 올려준다.
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

        // 현재 위치에 고양이가 배치되었을 경우 교체할 고양이와 드래그를 할 고양이의 위치를 서로 교체한다.
        else if (Save != null)
        {
            GameObject Save_Area = catArea;

            transform.position = Save.transform.position;
            catArea = Save.GetComponent<Cat_Drag>().catArea;

            Save.transform.position = defultposition;
            Save.GetComponent<Cat_Drag>().catArea = Save_Area;

        }

        // 컴퓨터 위치가 아니거나 해금이 안됬을 경우
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
