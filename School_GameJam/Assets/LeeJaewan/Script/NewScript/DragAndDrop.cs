using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : Singleton<DragAndDrop>
{
    [SerializeField] CatScript SelectCat;
    [SerializeField] public GameObject CollisionChier;
    ChierScript chier;
    [SerializeField] public bool IsChier;
    [SerializeField] public Vector3 prevPos;

    /*
        이 함수는 호출되었을때 touchPos에 마우스 클릭 위치를 받아와 초기화해준 후,
        hitInfo에 해당 위치에 있는 오브젝트의 정보를 받아온다.
        만약에 hitInfo에 collider가 null이 아니라면
        touchObj 지역변수에 hitInfo에 해당하는 gameObject를 초기화 해준다.
        그 후 touchObj가 가지고 있는 CatScript를 cat 지역변수에 초기화 해주고,
        cat 객체가 가진 IsDrag 변수를 true로 초기화 해준다.
        그 후에 만약 cat 지역변수에 해당된 값이 null이 아니라면 touchObj를 반환하고,
        그 외에는 null값을 반환한다.
     */
    CatScript _ClickObjectReturn() 
    {
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hitInfo = Physics2D.Raycast(touchPos, Camera.main.transform.forward);
            if (hitInfo.collider != null)
            {
               
            }
             
    }
    /*
        이 함수는 SelectObject의 CatScript를 참조한 후, CatScript를 가진 해당 객체의 IsDrag 변수를 false로 초기화 한 후
        만약에 IsCheir가 false라면 ReturnPrevious 함수를 호출해 드래그 하기 전 위치로 돌려보내고
        그 후에 null값을 리턴하는 함수입니다.
     */
    CatScript _DropObj() 
    {
        
        return null;
    }
    /* 이 함수는 마우스 좌클릭을 입력 받았을 때 ClickObject 변수를 _ClickObjectReturn 함수로 초기화 해준 후,
       SelectObject가 null이고, ClickObject가 null이 아닐때 SelectObject를 ClickObject로 초기화해서
       SelectObject가 클릭된 오브젝트의 값을 가질 수 있게 해주는 함수이다.
       만약에 SelectObject 가 null이 아닐 때, 마우스를 클릭한다면
       SelectObject를 _DropObject로 초기화 시켜서 마우스 포인터를 더 이상 따라오지 않게 해주는 함수이다.
     */
    void _SetSelectObject() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            CatScript ClickObject = _ClickObjectReturn();
            if(ClickObject != null)
            CollisionChier = ClickObject.CollisionObj;
            if (SelectCat == null && ClickObject != null)
            {
                SelectCat = ClickObject;
                prevPos = SelectCat.transform.position;
            }
            else if (SelectCat != null)
                SelectCat = _DropObj();
        }
    }
    /*   
        _SetSelectObject 함수를 호출해서 SelectObject 변수를 지속적으로 초기화 해주고,
          SelectObject가 null이 아닐때, SelectObject.transform.localPosition을 mousePos로 초기화해
          SelectObject가 마우스 포인터를 따라다닐 수 있도록 해주는 함수
     */
    void _ClickObjectSetTransform() 
    {
        _SetSelectObject();
        if (SelectCat != null)
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);;
            float x = mousePos.x, y = mousePos.y, z = SelectCat.transform.position.z;
            SelectCat.transform.localPosition = new Vector3(x,y,z);
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
