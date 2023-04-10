using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    void Awake() => instance = this;

    [Header("돈")]
    [SerializeField] Text goldText;

    [Header("시간")]
    [SerializeField] Text timerText;
    [SerializeField] int min;
    [SerializeField] float sec;

    [Header("고양이")]
    [SerializeField] Text catCountText;

    [Header("고양이 고용")]
    [SerializeField] Text employmentGoldText;
    [SerializeField] Button employmentBtn;

    [Header("이벤트")]
    public int maxCoolTime = 0;
    public float currentCoolTime = 0;
    [SerializeField] Image eventCard;
    [SerializeField] Image eventSlider;
    bool isEvent = false;

    [Header("게임오버")]
    [SerializeField] GameObject gameoverWindow;
    [SerializeField] Text overGold;
    [SerializeField] Text overTime;
    [SerializeField] Button rePlayBtn;
    [SerializeField] Button mainBtn;
    public bool isGameOver = false;

    [Header("층")]
    [SerializeField] GameObject floor2;
    [SerializeField] GameObject floor3;
    [SerializeField] GameObject roof;
    [SerializeField] Button limit800Btn;
    [SerializeField] Button limit3000Btn;

    void Start()
    {
        currentCoolTime = maxCoolTime;

        Btns();
    }

    void Update()
    {
        Timer();
        UIText();
        GameOver();
    }

    void UIText()
    {
        employmentGoldText.text = Cat_Manager.instance.employmentGold.ToString();
        goldText.text = string.Format("{0:#,0}$", GameManager.instance.currentGold);
        catCountText.text = string.Format("{0:#,0}마리", Cat_Manager.instance.summonList.Count);
    }

    void Timer()
    {
        sec += Time.deltaTime;

        if ((int)sec > 59)
        {
            sec = 0;
            min++;
        }
    }

    void GameOver()
    {
        // 현재 골드가 0이하일 경우 게임오버가 된다.
        if (GameManager.instance.currentGold <= 0)
        {
            Time.timeScale = 0;
            isGameOver = true;

            gameoverWindow.SetActive(true);

            overGold.text = GameManager.instance.currentGold + "$";
            overTime.text = string.Format("{0:D2}:{1:D2}", min, (int)sec);
        }
    }

    void Btns()
    {
        // 다시 하기 버튼을 눌렀을 경우
        rePlayBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Ingame");
        });

        // 메인화면 버튼을 눌렀을 경우
        mainBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Title");
        });

        // 고용 버튼을 눌렀을 경우
        employmentBtn.onClick.AddListener(() =>
        {
            Cat_Manager.instance.CatSummon();
        });

        // 800제한 버튼을 눌렀을 경우
        limit800Btn.onClick.AddListener(() =>
        {
            if(GameManager.instance.currentGold >= 800)
            {
                GameManager.instance.currentGold -= 800;
                limit800Btn.gameObject.SetActive(false);
                floor2.SetActive(true);
            }
        });

        // 1300제한 버튼을 눌렀을 경우
        limit3000Btn.onClick.AddListener(() =>
        {
            if (GameManager.instance.currentGold >= 3000)
            {
                GameManager.instance.currentGold -= 3000;
                limit3000Btn.gameObject.SetActive(false);
                floor3.SetActive(true);
                roof.SetActive(true);
            }
        });
    }
}
