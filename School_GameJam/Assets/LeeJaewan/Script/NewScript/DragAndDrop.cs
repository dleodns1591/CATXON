using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : Singleton<DragAndDrop>
{
    [SerializeField] GameObject SelectObject;
    [SerializeField] public Vector3 prevPos;
    bool IsChier = false;
    public void ReturnCanSit(bool cansit) 
    {
        IsChier = cansit;
    }
    GameObject _ClickObjectReturn() 
    {
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hitInfo = Physics2D.Raycast(touchPos, Camera.main.transform.forward);
            if (hitInfo.collider != null)
            {
                GameObject touchObj = hitInfo.transform.gameObject;
                Debug.Log(touchObj);
                CatScript cat = touchObj.GetComponent<CatScript>();
                cat.IsDrag = true;
                if (cat != null)
                    return touchObj;
                else
                    return null;
            }
            else
                return null;
    }
    GameObject _DropObj() 
    {
        CatScript cat = SelectObject.GetComponent<CatScript>();
        cat.IsDrag = false;
        if (IsChier == false) 
            ReturnPreviousPos(); 
        return null;
    }
    public void ReturnPreviousPos() 
    {
        SelectObject.transform.position = prevPos;
        Debug.Log("IsChier : " + IsChier);
    }
    void _SetSelectObject() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (SelectObject == null && IsChier == )
            {
                SelectObject = _ClickObjectReturn();
                prevPos = SelectObject.transform.position;
            }
            else if (SelectObject != null)
                SelectObject = _DropObj();
        }
    }
    void _ClickObjectSetTransform() 
    {
        _SetSelectObject();
        if (SelectObject != null)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);;
            float x = mousePos.x, y = mousePos.y, z = SelectObject.transform.position.z;
            SelectObject.transform.localPosition = new Vector3(x,y,z);
        }
    }
    void Start()
    {
        
    }

    void Update()
    {
        _ClickObjectSetTransform();
    }
}
