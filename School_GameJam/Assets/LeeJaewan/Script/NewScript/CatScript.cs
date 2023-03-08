using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CatScript : MonoBehaviour
{
    [SerializeField] public bool IsDrag = false;
    [SerializeField] public bool IsSit = false;
    [SerializeField] public ChierScript SitChier;
    [SerializeField] public GameObject CollisionObj;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Chier"))
        {
            CollisionObj = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Chier")) 
        {
            CollisionObj = null;
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
