using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RollingTimer : MonoBehaviour
{
    [SerializeField] private Text _timerText;
    private int _minutes;
    private float _seconds;
    private const float Interval = 1f; // 1초 간격

    private void Start()
    {
        StartCoroutine(TimerCoroutine());
    }

    private IEnumerator TimerCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(Interval);
            UpdateTimer();
        }
    }

    private void UpdateTimer()
    {
        _seconds += Interval;
        if (_seconds >= 60f)
        {
            _seconds = 0f;
            _minutes++;
        }

        _timerText.text = $"{_minutes:D2} : {(int)_seconds:D2}";
    }
}
