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

    [Header("��")]
    [SerializeField] Text goldText;

    [Header("�ð�")]
    [SerializeField] int min;
    [SerializeField] float sec;
    [SerializeField] Text timerText;

    [Header("�����")]
    [SerializeField] Text catCountText;

    [Header("����� ���")]
    [SerializeField] Text employmentGoldText;
    [SerializeField] Button employmentBtn;

    [Header("���ӿ���")]
    [SerializeField] GameObject gameoverWindow;
    [SerializeField] Text overGold;
    [SerializeField] Text overTime;
    [SerializeField] Button rePlayBtn;
    [SerializeField] Button mainBtn;

    [Header("��")]
    [SerializeField] GameObject floor2;
    [SerializeField] GameObject floor3;
    [SerializeField] GameObject roof;
    [SerializeField] Button limit800Btn;
    [SerializeField] Button limit3000Btn;

    void Start()
    {
        Btns();
    }

    void Update()
    {
        Timer();
        UIText();
    }

    void UIText()
    {
        employmentGoldText.text = Cat_Manager.instance.employmentGold.ToString();
        goldText.text = string.Format("{0:#,0}$", GameManager.instance.currentGold);
        catCountText.text = string.Format("{0:#,0}����", Cat_Manager.instance.summonList.Count);
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

    public void GameOver()
    {
        // ���� ��尡 0������ ��� ���ӿ����� �ȴ�.
        if (GameManager.instance.currentGold <= 0)
        {
            Time.timeScale = 0;
            gameoverWindow.SetActive(true);

            overGold.text = GameManager.instance.currentGold + "$";
            overTime.text = string.Format("{0:D2}:{1:D2}", min, (int)sec);
        }
    }

    void Btns()
    {
        // �ٽ� �ϱ� ��ư�� ������ ���
        rePlayBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Ingame");
        });

        // ����ȭ�� ��ư�� ������ ���
        mainBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Title");
        });

        // ��� ��ư�� ������ ���
        employmentBtn.onClick.AddListener(() =>
        {
            Cat_Manager.instance.CatSummon();
        });

        // 800���� ��ư�� ������ ���
        limit800Btn.onClick.AddListener(() =>
        {
            if(GameManager.instance.currentGold >= 800)
            {
                GameManager.instance.currentGold -= 800;

                Cat_Manager.instance.floorIndex++;

                floor2.SetActive(true);
                limit800Btn.gameObject.SetActive(false);
            }
            else
                SoundManager.instance.PlaySoundClip("SFX_Shortage", SoundType.SFX);
        });

        // 1300���� ��ư�� ������ ���
        limit3000Btn.onClick.AddListener(() =>
        {
            if (GameManager.instance.currentGold >= 3000)
            {
                GameManager.instance.currentGold -= 3000;

                Cat_Manager.instance.floorIndex++;

                floor3.SetActive(true);
                roof.SetActive(true);
                limit3000Btn.gameObject.SetActive(false);
            }

            else
                SoundManager.instance.PlaySoundClip("SFX_Shortage", SoundType.SFX);
        });
    }
}
