using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    void Awake() => instance = this;

    [Header("��")]
    [SerializeField] Text goldText;

    [Header("�ð�")]
    [SerializeField] Text timerText;
    [SerializeField] int min;
    [SerializeField] float sec;

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
    public bool isGameOver = false;

    [Header("��")]
    [SerializeField] GameObject floor2;
    [SerializeField] GameObject floor3;
    [SerializeField] GameObject roof;
    [SerializeField] Button limit800;
    [SerializeField] Button limit3000;

    void Start()
    {
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
        goldText.text = GameManager.instance.currentGold + "$";
        catCountText.text = Cat_Manager.instance.D_Area.Count + "����";
    }

    void Timer()
    {
        sec += Time.deltaTime;
        timerText.text = string.Format("{0:D2}:{1:D2}", min, (int)sec);

        if ((int)sec > 59)
        {
            sec = 0;
            min++;
        }
    }

    void GameOver()
    {
        // ���� ��尡 0�� �Ǿ��� �� ���ӿ����� �ȴ�.
        if (GameManager.instance.currentGold <= 0)
        {
            Time.timeScale = 0;
            isGameOver = true;

            gameoverWindow.SetActive(true);

            overGold.text = GameManager.instance.currentGold + "$";
            overTime.text = timerText.text;
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
        limit800.onClick.AddListener(() =>
        {
            if(GameManager.instance.currentGold >= 800)
            {
                GameManager.instance.currentGold -= 800;
                limit800.gameObject.SetActive(false);
                floor2.SetActive(true);
            }
        });

        // 1300���� ��ư�� ������ ���
        limit3000.onClick.AddListener(() =>
        {
            if (GameManager.instance.currentGold >= 3000)
            {
                GameManager.instance.currentGold -= 3000;
                limit3000.gameObject.SetActive(false);
                floor3.SetActive(true);
                roof.SetActive(true);
            }
        });
    }
}
