using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class MainMenuPage : BasePage
{
    [SerializeField] private Image _fadeImage;
    [SerializeField] private Button _playButton;
    [SerializeField] private Button _InfinityplayButton;

    void Start()
    {
        _playButton.onClick.AddListener(OnClickPlayButton);
        _InfinityplayButton.onClick.AddListener(OnClickInfinityPlayButton);
    }

    private void OnClickInfinityPlayButton()
    {
        _fadeImage.DOFade(1, 0.5f).OnComplete(() =>
        {
            SceneLauncher.Instance.SceneChange("01.InfiniteMap");
        });
    }

    private void OnClickPlayButton()
    {
        this.GetComponent<BasePage>().HidePage();
        PagingSystem.Instance.PushAndShowPage(Define.PageType.Stage);
    }

}
