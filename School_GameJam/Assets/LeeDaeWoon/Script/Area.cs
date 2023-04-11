using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    public static Area instance;
    void Awake() => instance = this;

    public int area;
    public int floor;

    void Start()
    {
       
    }

    void Update()
    {

    }

}
