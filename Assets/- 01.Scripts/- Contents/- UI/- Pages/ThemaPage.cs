using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class ThemaPage : BasePage
{
    [SerializeField] private Text _touchText;

    private void Start()
    {
        if (_touchText != null)
        {
            _touchText.DOFade(0f, 0.65f).SetLoops(-1, LoopType.Yoyo);
        }
        else
        {
            Debug.LogWarning("TouchText reference is missing.");
        }
    }

    public void OnClickThemaScreen()
    {
        gameObject.SetActive(false);
        PagingSystem.Instance.PushAndShowPage(Define.PageType.Menu);
    }
    
}
