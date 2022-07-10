using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonClick : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void InputButtonClick() 
    {
        SoundManager.Instance.audioSources[0].Play();
    }
}
