using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



class RisingArrow : MonoBehaviour
{
    public float AllowedSeconds { get; set; }
    // [SerializeField] private float m_allowedSeconds;
    [SerializeField] private Vector3 m_startPos;
    [SerializeField] private Vector3 m_endPos;
    [SerializeField] private Text m_timerText;
    [SerializeField] private Cooker m_cooker;
    private float currentSecondsRemaining = 0.0f;

    void Start()
    {
        AllowedSeconds = 10;        
        RestartClock();
    }

    void Update()
    {
        currentSecondsRemaining -= Mathf.Clamp(Time.deltaTime, 0.0f, AllowedSeconds);
        m_timerText.text = "" + Mathf.Round(currentSecondsRemaining);

        float scaler = (m_endPos.y - m_startPos.y) * (currentSecondsRemaining/AllowedSeconds);
        Vector3 currentPos = m_endPos + new Vector3(0, 0-scaler ,0);
        gameObject.transform.localPosition = currentPos;

        if (transform.localPosition.y > m_endPos.y)
        {
            RestartClock();
        }
    }

    public void RestartClock()
    {
        gameObject.transform.localPosition = m_startPos;
        m_cooker.MakeElement();
        AllowedSeconds *= 0.9f;
        currentSecondsRemaining = AllowedSeconds;
    }
}
