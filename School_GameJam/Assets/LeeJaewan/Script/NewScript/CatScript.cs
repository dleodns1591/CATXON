using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatScript : MonoBehaviour
{
    [SerializeField] public bool IsDrag = false;
    [SerializeField] bool CanSit = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Chier"))
        {
            CanSit = true;
            Debug.Log("CanSit :" + CanSit);
            DragAndDrop.Instance.ReturnCanSit(CanSit);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Chier")) 
        {
            CanSit = false;
            Debug.Log("CanSit :" + CanSit);
            DragAndDrop.Instance.ReturnCanSit(CanSit);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
