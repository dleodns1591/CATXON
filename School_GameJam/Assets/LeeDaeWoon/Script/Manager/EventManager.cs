using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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

    [Header("이벤트")]
    [SerializeField] int maxCoolTime = 0;
    [SerializeField] float currentCoolTime = 0;
    [SerializeField] GameObject eventPanel;
    [SerializeField] GameObject eventParent;
    [SerializeField] Image eventSlider;

    public bool isDragLimit = false;
    bool isEvent = false;
    bool isEventCool = false;

    void Start()
    {
        currentCoolTime = maxCoolTime;
    }

    void Update()
    {
        StartCoroutine(EventCard());
    }

    IEnumerator EventCard()
    {
        eventSlider.fillAmount = currentCoolTime / maxCoolTime;

        if (!isEventCool)
        {
            isEventCool = true;
            eventSlider.fillAmount = 1;
            currentCoolTime = maxCoolTime;
            StopCoroutine("EventCoolTime");
            StartCoroutine("EventCoolTime");
        }

        if (currentCoolTime <= 0)
        {
            isEventCool = false;
            isEvent = true;

            int randomEvent = Random.Range(0, eventSpawn.Count);
            Vector3 summonPos = new Vector3(0, 960, 0);
            EventType(randomEvent);

            GameObject eventType = Instantiate(eventPanel, summonPos, Quaternion.identity, eventParent.transform);
            eventType.transform.localPosition = summonPos;

            eventType.transform.GetChild(0).GetComponent<Image>().sprite = eventSpawn[randomEvent].icon;
            eventType.transform.GetChild(1).GetComponent<Text>().text = eventSpawn[randomEvent].name;
            eventType.transform.GetChild(2).GetComponent<Text>().text = eventSpawn[randomEvent].exPlanation;

            eventType.transform.DOLocalMoveY(0, 0.5f).SetEase(Ease.OutBack);

            yield return new WaitForSeconds(1.5f);
            eventType.transform.DOLocalMoveX(-1500, 1).SetEase(Ease.InBack).OnComplete(() =>
            {
                Destroy(eventType);
            });
        }
    }

    void EventType(int randomEvent)
    {
        switch (eventSpawn[randomEvent].eEvent)
        {
            case Event.EEvent.Lotto: // 로또
                GameManager.instance.currentGold += GameManager.instance.currentGold / 2;
                break;

            case Event.EEvent.Repair: // 수리
                if (Cat_Manager.instance.summonList.Count != 0)
                {
                    for (int i = 0; i < GameManager.instance.computers.Count; i++)
                        GameManager.instance.computers[i].isBreak = false;
                }
                break;

            case Event.EEvent.WalkOut: // 파업
                StartCoroutine(WalkOutEvent());
                break;

            case Event.EEvent.Stray: // 도둑
                float result = GameManager.instance.currentGold * 0.2f;
                GameManager.instance.currentGold += (int)result;
                break;
        }
    }

    IEnumerator WalkOutEvent()
    {
        isDragLimit = true;
        yield return new WaitForSeconds(10);
        isDragLimit = false;
    }

    IEnumerator EventCoolTime()
    {
        while (currentCoolTime >= 0)
        {
            currentCoolTime -= 0.01f;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
