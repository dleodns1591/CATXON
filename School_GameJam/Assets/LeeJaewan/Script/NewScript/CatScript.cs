using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatScript : MonoBehaviour
{
    [SerializeField] public bool IsDrag = false;
    [SerializeField] public bool IsSit = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Chier"))
        {
            DragAndDrop.Instance.CollisionChier = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Chier")) 
        {
            DragAndDrop.Instance.CollisionChier = null;
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
