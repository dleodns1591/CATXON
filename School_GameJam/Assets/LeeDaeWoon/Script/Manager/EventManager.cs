using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Event
{
    public enum EEvent
    {
        Lotto,
        Repair,
        WalkOut,
        Stray,
    }

    public Sprite icon;
    public string name;
    public EEvent eEvent;

    [TextArea(7, 3)]
    public string exPlanation;
}

public class EventManager : MonoBehaviour
{
    public static EventManager instance;
    void Awake() => instance = this;

    public List<Event> eventSpawn = new List<Event>();

    [Header("ÄðÅ¸ÀÓ")]
    [SerializeField] float currentCoolTime = 0;
    [SerializeField] int maxCoolTime = 0;
    bool isEvent = false;

    void Start()
    {
    }

    void Update()
    {
        EventSummon();
    }

    void EventSummon()
    {
        //if (!isEvent)
        //{
        //    isEvent = true;
        //}
    }

    IEnumerator CoolTime()
    {
        while(currentCoolTime >= 0)
        {
            currentCoolTime -= 1;
            yield return new WaitForSeconds(1);
        }
    }
}
