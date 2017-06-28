using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InfoController : MonoBehaviour
{

    public GameController gameController;
    public Image soundBtn;
    public Image InfoBtn;
    public Sprite soundOn, soundOff, infoOn, infoOff;
    public GameObject infoWindow;

    private void Start()
    {
        if (AudioListener.volume!=0)
        {
            soundBtn.sprite = soundOn;
        }
        else
        {
            soundBtn.sprite = soundOff;
        }
    }

    public void toggleSound()
    {
        gameController.toggleGlobalVolume();
        if (gameController.hasSound)
        {
            soundBtn.sprite = soundOn;
        }
        else
        {
            soundBtn.sprite = soundOff;
        }
    }

    public void toggleInfo()
    {
        infoWindow.SetActive(!infoWindow.activeSelf);
        gameController.HideSplash();
        if (infoWindow.activeSelf)
        {
            InfoBtn.sprite = infoOn;
        }
        else
        {
            InfoBtn.sprite = infoOff;
        }
    }
}
