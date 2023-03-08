using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChierScript : MonoBehaviour
{
    [SerializeField] public GameObject Sit_Obj; //�ڸ��� �ɾ��ִ� ������Ʈ �Դϴ�.
    public void _ObjectSit(GameObject obj) 
    {
        Sit_Obj = obj;
        CatScript cat = obj.GetComponent<CatScript>();
        cat.IsSit = true;
        obj.transform.position = transform.position;
    }
    public void ReturnPrevSeat(CatScript cat)
    {
        _ObjectSit(cat.SitChier.gameObject);
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cat")) 
        {
            Sit_Obj = null;
        }
    }


    void Update()
    {
        
    }
}
