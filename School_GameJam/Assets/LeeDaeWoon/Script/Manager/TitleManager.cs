using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [Header("타이틀 버튼")]
    [SerializeField] Button startBtn;
    [SerializeField] Button creditBtn;
    [SerializeField] Button mainBtn;

    [Header("화면")]
    [SerializeField] GameObject creditWindow;

    void Start()
    {
        Btns();
    }

    void Update()
    {
        
    }

    void Btns()
    {
        startBtn.onClick.AddListener(() =>
        {
            SoundManager.Instance.audioSources[0].Play();
            SceneManager.LoadScene("Ingame");
        });

        creditBtn.onClick.AddListener(() =>
        {
            SoundManager.Instance.audioSources[0].Play();
            creditWindow.SetActive(true);
        });

        mainBtn.onClick.AddListener(() =>
        {
            SoundManager.Instance.audioSources[0].Play();
            creditWindow.SetActive(false);
        });
    }
}
