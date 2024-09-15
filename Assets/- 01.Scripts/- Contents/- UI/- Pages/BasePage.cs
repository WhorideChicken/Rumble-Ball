using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.Events;

public class BasePage : MonoBehaviour
{
    private CanvasGroup _canvas = null;

    private void Awake()
    {
        _canvas = this.GetComponent<CanvasGroup>();
    }

    public virtual void ShowPage(UnityAction action = null)
    {
        this.gameObject.SetActive(true);
        _canvas.DOFade(1.0f, 1.5f).OnComplete(() =>
        {
            if (action != null)
            {
                HidePage(action);
            }
        });
    }

    public virtual void HidePage(UnityAction action = null)
    {
        this.gameObject.SetActive(false);
        if (action != null)
        {
            action();
        }
    }
}
