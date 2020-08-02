using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    #region ingame menu variables

    public void OpenInGameUI()
    {
        //reset in game variables
        LevelCountText.text = (GameManager.instance.LevelId + 1).ToString();
        //CoinCountText.text = GameManager.instance.Player.CollectedCoin.ToString();

        InGamePanel.SetActive(true);
    }

    public GameObject InGamePanel;
    public Text LevelCountText;
    //public Text CoinCountText;
    #endregion

    #region settings variables
    public GameObject SettingsPanel;
/*
    public Toggle MusicToggle;
    public Toggle SfxToggle;
    public Toggle HapticToggle;

    public void ChangeMusic()
    {
        //GameManager.instance.Sfx.PlaySfx(0);

        //GameManager.instance.Music.IsPlaying = MusicToggle.isOn;
        if (MusicToggle.isOn)
        {
            // gamemanager.music true
            //Text text = MusicToggle.GetComponentInChildren<Text>();
            //text.text = "Music On";
            PlayerPrefs.SetInt("Music", 1);
        }
        else if (!MusicToggle.isOn)
        {
            // gamemanager.music false
            //Text text = MusicToggle.GetComponentInChildren<Text>();
            //text.text = "Music Off";
            PlayerPrefs.SetInt("Music", 0);
        }
    }

    public void ChangeSfx()
    {
        //GameManager.instance.Sfx.PlaySfx(0);

        //GameManager.instance.Sfx.IsPlaying = SfxToggle.isOn;
        if (SfxToggle.isOn)
        {
            // gamemanager.music true
            //Text text = SfxToggle.GetComponentInChildren<Text>();
            //text.text = "Sfx On";
            PlayerPrefs.SetInt("Sfx", 1);
        }
        else if (!SfxToggle.isOn)
        {
            // gamemanager.music false
            //Text text = SfxToggle.GetComponentInChildren<Text>();
            //text.text = "Sfx Off";
            PlayerPrefs.SetInt("Sfx", 0);
        }
    }

    public void ChangeHaptic()
    {
        //GameManager.instance.Sfx.PlaySfx(0);

        //GameManager.instance.IsHapticEnabled = HapticToggle.isOn;
        if (HapticToggle.isOn)
        {
            // gamemanager.music true
            //Text text = HapticToggle.GetComponentInChildren<Text>();
            //text.text = "Haptic On";
            PlayerPrefs.SetInt("Haptic", 1);
        }
        else if (!HapticToggle.isOn)
        {
            // gamemanager.music false
            //Text text = HapticToggle.GetComponentInChildren<Text>();
            //text.text = "Haptic Off";
            PlayerPrefs.SetInt("Haptic", 0);
        }
    }

    public void ResetGameData()
    {
        
        GameManager.instance.Sfx.PlaySfx(0);

        GameManager.instance.LevelId = 0;
        GameManager.instance.IsHapticEnabled = false;
        GameManager.instance.Music.IsPlaying = false;
        GameManager.instance.Sfx.IsPlaying = false;

        GameManager.instance.SetPlayerData();
        
    }
*/
    public void CloseSettings()
    {
        //GameManager.instance.Sfx.PlaySfx(0);
        //GameManager.instance.Player.Resume();
        SettingsPanel.SetActive(false);
    }

    #endregion


    #region main_crash_panels

    public GameObject MainMenuPanel;
    public GameObject FailPanel;
    public GameObject NextLevelPanel;

    public void OpenSettings()
    {
        //GameManager.instance.Sfx.PlaySfx(0);

        //set settings variables;
        //MusicToggle.isOn = GameManager.instance.Music.IsPlaying;
        //SfxToggle.isOn = GameManager.instance.Sfx.IsPlaying;
        //HapticToggle.isOn = GameManager.instance.IsHapticEnabled;

        SettingsPanel.SetActive(true);
    }

    public void StartNewGame()
    {
        //GameManager.instance.Music.Mute();
        //GameManager.instance.Sfx.PlaySfx(0);

        MainMenuPanel.SetActive(false);
        GameManager.instance.StartGame();
    }

    public void RestartGame()
    {
        //GameManager.instance.Music.Mute();
        //GameManager.instance.Sfx.PlaySfx(0);

        FailPanel.SetActive(false);
        //GameManager.instance.Player.CollectedCoin = 0;
        GameManager.instance.StartGame();
    }

    public void GoToNextLevel()
    {
        //GameManager.instance.Music.Mute();
        //GameManager.instance.Sfx.PlaySfx(0);

        NextLevelPanel.SetActive(false);
        GameManager.instance.StartGame();
    }

    public void OpenCrashMenu()
    {
        //GameManager.instance.Music.UnMute();
        FailPanel.SetActive(true);
    }

    public void OpenMainMenu()
    {
        //GameManager.instance.Music.UnMute();
        MainMenuPanel.SetActive(true);
    }

    public void OpenNextLevel()
    {
        //GameManager.instance.Music.UnMute();
        NextLevelPanel.SetActive(true);
    }

    #endregion
}
