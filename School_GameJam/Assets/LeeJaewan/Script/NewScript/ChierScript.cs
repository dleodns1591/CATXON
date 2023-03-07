using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChierScript : MonoBehaviour
{
    [SerializeField] GameObject Sit_Obj; //�ڸ��� �ɾ��ִ� ������Ʈ �Դϴ�.
    void _ChierCheck(GameObject obj) 
    {
        
            CatScript cat;
        if (obj != null)
            cat = obj.GetComponent<CatScript>();
        else
            cat = null;

        if (Sit_Obj == null && cat.IsDrag)
            _SitCheir(obj);
        else if (Sit_Obj != null)
            DragAndDrop.Instance.ReturnPreviousPos();
    }
    void _SitCheir(GameObject obj) 
    {
        Sit_Obj = obj;
        Sit_Obj.transform.position = transform.position;
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cat")) 
        {
            _ChierCheck(collision.gameObject);
        }
    }
    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.CompareTag("Cat"))
        {
            DragAndDrop.Instance.prevPos = transform.position;
            _ChierCheck(null);
        }
    }

    void Update()
    {
        
    }
}
