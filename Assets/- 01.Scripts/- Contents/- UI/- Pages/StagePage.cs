using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StagePage : BasePage
{
    [SerializeField] private Button _backButton;
    [SerializeField] private Button _prevStageButton;
    [SerializeField] private Button _nextStageButton;
    [SerializeField] private Transform _contents;
    [SerializeField] private Image _fadeImage;

    [SerializeField] private int _width = 850;
    private int _currentStep = 0;
    private int _totalCount;

    
    private void Awake()
    {
        foreach(Transform stage in _contents)
        {
            stage.GetComponent<Button>().onClick.AddListener(OnClickStageButton);
        }

        _prevStageButton?.onClick.AddListener(OnClickPrevStageButton);
        _nextStageButton?.onClick.AddListener(OnClickNextStageButton);
        _backButton?.onClick.AddListener(OnClickBackButton);
    }

    private void Start()
    {
        _fadeImage.color = new Color(0,0,0,0);
        _totalCount = _contents.childCount-1;
        ChecjCurrentStep();
    }

    private void OnClickBackButton()
    {
        PagingSystem.Instance.ShowPreviousPage();
    }

    private void ChecjCurrentStep()
    {
        _prevStageButton.gameObject.SetActive((_currentStep > 0));
        _nextStageButton.gameObject.SetActive(_currentStep < _totalCount);
    }

    private void OnClickPrevStageButton()
    {
        _currentStep--;
        ChecjCurrentStep();
        _contents.DOLocalMoveX(_contents.localPosition.x + _width, 0.1f);
    }

    private void OnClickNextStageButton()
    {
        _currentStep++;
        ChecjCurrentStep();
        _contents.DOLocalMoveX(_contents.localPosition.x - _width, 0.1f);
    }

    private void OnClickStageButton()
    {
        _fadeImage.DOFade(1, 0.5f).OnComplete(() =>
        {
            SceneLauncher.Instance.SceneChange("01.InfiniteMap");
        });

    }
}
