using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleManager : MonoBehaviour
{
    [Header("Ÿ��Ʋ ��ư")]
    [SerializeField] Button startBtn;

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
            SoundManager.instance.PlaySoundClip("SFX_Click", SoundType.SFX);
            SceneManager.LoadScene("Ingame");
        });
    }
}
