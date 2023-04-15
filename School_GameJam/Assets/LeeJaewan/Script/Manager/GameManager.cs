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
        if (!isDie)
        {
            for (int floor = 0; floor < 3; floor++)
            {
                for (int area = 0; area < 6; area++)
                {
                    if (computer[2].computerList[5].isBreak)
                    {
                        isDie = true;
                        UIManager.instance.GameOver();
                    }
                    else
                        break;

                }
            }

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