using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChierScript : MonoBehaviour
{
    [SerializeField] public GameObject Sit_Obj; //자리에 앉아있는 오브젝트 입니다.
    public void _ObjectSit(GameObject obj) 
    {
        Sit_Obj = obj;
        obj.transform.position = transform.position;
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
