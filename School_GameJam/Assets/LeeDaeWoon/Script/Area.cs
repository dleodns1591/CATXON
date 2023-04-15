using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Area : MonoBehaviour
{
    public static Area instance;
    void Awake() => instance = this;

    public int area = 0;
    public int floor = 0;
}
