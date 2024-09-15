using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PausePopup : MonoBehaviour
{
    [SerializeField] Button ReturnButton;
    [SerializeField] Button ExitButton;

    private void Start()
    {
        ReturnButton.onClick.AddListener(OnClickReturnButton);
        ExitButton.onClick.AddListener(OnClickExitButton);
    }

    private void OnClickReturnButton()
    {
        Time.timeScale = 1;
        this.gameObject.SetActive(false);
    }

    private void OnClickExitButton()
    {
        Time.timeScale = 1;
        SceneLauncher.Instance.SceneChange("Intro");
    }
}
