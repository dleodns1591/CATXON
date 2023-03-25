using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    [Header("돈")]
    [SerializeField] Text goldText;

    [Header("시간")]
    [SerializeField] Text timerText;
    [SerializeField] int min;
    [SerializeField] float sec;

    [Header("고양이")]
    [SerializeField] Text catCountText;

    [Header("고양이 고용")]
    [SerializeField] int employmentGold;
    [SerializeField] Text employmentGoldText;
    [SerializeField] Button employmentBtn;


    [Header("게임오버")]
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
        catCountText.text = Cat_Manager.instance.D_Area.Count + "마리";
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
        // 현재 골드가 0이 되었을 때 게임오버가 된다.
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
        // 다시 하기 버튼을 눌렀을 경우
        rePlayBtn.onClick.AddListener(() =>
        {

        });

        // 메인화면 버튼을 눌렀을 경우
        mainBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Title");
        });

        // 고용 버튼을 눌렀을 경우
        employmentBtn.onClick.AddListener(() =>
        {

        });
    }
}
