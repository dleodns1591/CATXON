using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] Text goldText;
    [SerializeField] Text timerText;
    [SerializeField] Text catCountText;

    void Start()
    {
        
    }

    void Update()
    {
        UIText();
    }

    void UIText()
    {
        goldText.text = GameManager.instance.currentGold.ToString();
        timerText.text = GameManager.instance.currentTime.ToString();
        catCountText.text = Cat_Manager.Inst.D_Area.Count + "¸¶¸®";
    }
}
