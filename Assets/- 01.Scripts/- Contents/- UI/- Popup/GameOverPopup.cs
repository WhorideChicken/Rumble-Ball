using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPopup : BasePopup
{
    [SerializeField] Button ExitButton;

    private void Start()
    {
        ExitButton.onClick.AddListener(OnClickExitButton);
    }
    private void OnClickExitButton()
    {
        Time.timeScale = 1;
        SceneLauncher.Instance.SceneChange("Intro");
    }
}
