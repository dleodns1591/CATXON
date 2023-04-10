using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Event
{
    public Sprite icon;
    public string name;

    [TextArea(7,3)]
    public string exPlanation;
}

public class EventManager : MonoBehaviour
{
    public static EventManager instance;
    void Awake() => instance = this;

    public List<Event> eventSpawn = new List<Event>();

    void Start()
    {

    }

    void Update()
    {

    }
}
