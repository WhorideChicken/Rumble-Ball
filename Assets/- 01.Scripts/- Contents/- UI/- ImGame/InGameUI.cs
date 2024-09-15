using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;

public class InGameUI : Singletone<InGameUI>
{
    [SerializeField] private Button PauseButton;
    [SerializeField] GameObject PausePopup;
    [SerializeField] GameObject GameOverPopup;
    [SerializeField] Volume _volume;

    private Coroutine _hitUICor = null;
    //0 -> normal, 1 -> hit on, 2-> hit off
    [SerializeField] Color[] statusColor;

    private Vignette _vigne;
    private int killLog = 0;


    void Start()
    {
        _volume.profile.TryGet(out _vigne);
        PauseButton.onClick.AddListener(OnClickPauseButton);
        _vigne.color.value = statusColor[0];
    }

    public void GetWeapon(BaseWeapon weapon)
    {

    }

    public void HitVolume()
    {
        HitUICorStart();
    }

    private void HitUICorStart()
    {
        if (_hitUICor != null)
        {
            StopCoroutine(_hitUICor);
            _hitUICor = null;
        }

        _hitUICor = StartCoroutine(HitCoroutine());
    }

    IEnumerator HitCoroutine()
    {
        int blinkCount = 3;
        float blinkInterval = 0.1f;

        for (int i = 0; i < blinkCount; i++)
        {
            _vigne.color.value = statusColor[1];
            yield return new WaitForSeconds(blinkInterval);
            _vigne.color.value = statusColor[0];
            yield return new WaitForSeconds(blinkInterval);
        }
    }


    public void KillEnemy()
    {
        killLog++;
    }


    private void OnClickPauseButton()
    {
        if (Time.timeScale > 0)
        {
            Time.timeScale = 0;
            PausePopup.SetActive(true);
        }
        else
        {
            PausePopup.SetActive(false);
            Time.timeScale = 1;
        }
    }


    public void GameOver()
    {
        Time.timeScale = 0;
        GameOverPopup.SetActive(true);
    }
}
