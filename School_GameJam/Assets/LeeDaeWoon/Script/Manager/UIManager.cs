using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("��")]
    [SerializeField] Text goldText;

    [Header("�ð�")]
    [SerializeField] Text timerText;
    [SerializeField] int min;
    [SerializeField] float sec;

    [Header("�����")]
    [SerializeField] Text catCountText;

    [Header("����� ���")]
    [SerializeField] int employmentGold;
    [SerializeField] Text employmentGoldText;
    [SerializeField] Button employmentBtn;


    [Header("���ӿ���")]
    [SerializeField] GameObject gameoverWindow;
    [SerializeField] Text overGold;
    [SerializeField] Text overTime;
    [SerializeField] Button rePlayBtn;
    [SerializeField] Button mainBtn;

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
        employmentGoldText.text = employmentGold.ToString();
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

        });

        // ����ȭ�� ��ư�� ������ ���
        mainBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Title");
        });

        // ��� ��ư�� ������ ���
        employmentBtn.onClick.AddListener(() =>
        {

        });
    }
}
