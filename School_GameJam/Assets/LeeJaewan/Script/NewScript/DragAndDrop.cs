using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : Singleton<DragAndDrop>
{
    [SerializeField] GameObject SelectObject;
    [SerializeField] public GameObject CollisionChier;
                     ChierScript chier;
    [SerializeField] public bool IsChier;
    [SerializeField] public Vector3 prevPos;

    /*
        �� �Լ��� ȣ��Ǿ����� touchPos�� ���콺 Ŭ�� ��ġ�� �޾ƿ� �ʱ�ȭ���� ��,
        hitInfo�� �ش� ��ġ�� �ִ� ������Ʈ�� ������ �޾ƿ´�.
        ���࿡ hitInfo�� collider�� null�� �ƴ϶��
        touchObj ���������� hitInfo�� �ش��ϴ� gameObject�� �ʱ�ȭ ���ش�.
        �� �� touchObj�� ������ �ִ� CatScript�� cat ���������� �ʱ�ȭ ���ְ�,
        cat ��ü�� ���� IsDrag ������ true�� �ʱ�ȭ ���ش�.
        �� �Ŀ� ���� cat ���������� �ش�� ���� null�� �ƴ϶�� touchObj�� ��ȯ�ϰ�,
        �� �ܿ��� null���� ��ȯ�Ѵ�.
     */
    GameObject _ClickObjectReturn() 
    {
            Vector2 touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hitInfo = Physics2D.Raycast(touchPos, Camera.main.transform.forward);
            if (hitInfo.collider != null)
            {
                GameObject touchObj = hitInfo.transform.gameObject;
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
    /*
        �� �Լ��� SelectObject�� CatScript�� ������ ��, CatScript�� ���� �ش� ��ü�� IsDrag ������ false�� �ʱ�ȭ �� ��
        ���࿡ IsCheir�� false��� ReturnPrevious �Լ��� ȣ���� �巡�� �ϱ� �� ��ġ�� ����������
        �� �Ŀ� null���� �����ϴ� �Լ��Դϴ�.
     */
    GameObject _DropObj() 
    {
        CatScript cat = SelectObject.GetComponent<CatScript>();
        GameObject obj = cat.gameObject;
        if(CollisionChier != null)
        chier = CollisionChier.GetComponent<ChierScript>();
        cat.IsDrag = false;

        if (CollisionChier == null) // ����ִ� ���ڰ� ����, ����ִ� ���ڿ� ���� obj�� ���� ��
            ReturnPreviousPos(prevPos);
        else if (CollisionChier != null && cat.IsSit == false) ;
        else if (chier.Sit_Obj == null)
            chier._ObjectSit(obj);
        return null;
    }
    public void ReturnPreviousPos(Vector3 prev) 
    {
        if(prev != null)
        SelectObject.transform.position = prev;
    }
    /* �� �Լ��� ���콺 ��Ŭ���� �Է� �޾��� �� ClickObject ������ _ClickObjectReturn �Լ��� �ʱ�ȭ ���� ��,
       SelectObject�� null�̰�, ClickObject�� null�� �ƴҶ� SelectObject�� ClickObject�� �ʱ�ȭ�ؼ�
       SelectObject�� Ŭ���� ������Ʈ�� ���� ���� �� �ְ� ���ִ� �Լ��̴�.
       ���࿡ SelectObject �� null�� �ƴ� ��, ���콺�� Ŭ���Ѵٸ�
       SelectObject�� _DropObject�� �ʱ�ȭ ���Ѽ� ���콺 �����͸� �� �̻� ������� �ʰ� ���ִ� �Լ��̴�.
     */
    void _SetSelectObject() 
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject ClickObject = _ClickObjectReturn();
            if (SelectObject == null && ClickObject != null)
            {
                SelectObject = ClickObject;
                prevPos = SelectObject.transform.position;
            }
            else if (SelectObject != null)
                SelectObject = _DropObj();
        }
    }
    /*   _SetSelectObject �Լ��� ȣ���ؼ� SelectObject ������ ���������� �ʱ�ȭ ���ְ�,
          SelectObject�� null�� �ƴҶ�, SelectObject.transform.localPosition�� mousePos�� �ʱ�ȭ��
          SelectObject�� ���콺 �����͸� ����ٴ� �� �ֵ��� ���ִ� �Լ�
     */
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
