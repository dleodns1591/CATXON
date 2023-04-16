using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ComputerFloor
{
    public List<Computer> computerList = new List<Computer>();
}

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    void Awake() => instance = this;

    [Header("µ·")]
    public int currentGold = 0;
    const int basicFund = 2000;

    [Header("½Ã°£")]
    public int currentTime = 0;

    [Header("ÄÄÇ»ÅÍ")]
    public ComputerFloor[] computer = new ComputerFloor[3];

    bool isDie = false;
    bool isDieCheck = false;

    void Start()
    {
        SoundManager.instance.PlaySoundClip("BGM_Ingame", SoundType.BGM);
        currentGold = basicFund;
    }

    void Update()
    {
        Die();
        Cheat();
    }

    void Die()
    {
        if (!isDie && !isDieCheck)
        {
            isDieCheck = true;

            if (currentGold <= 0)
            {
                isDie = true;
                UIManager.instance.GameOver();
            }
        }
    }

    void Cheat()
    {
        // µ· Áõ°¡
        if (Input.GetKeyDown(KeyCode.G))
            currentGold += 1000;

        // µ· Â÷°¨
        if (Input.GetKeyDown(KeyCode.F))
            currentGold -= 1000;
    }
}