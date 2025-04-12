using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultUI : MonoBehaviour
{
    public VoidEvent ResetEvent; //TODO - will display speed/ball type later so have to move this later

    // text component that show velocity
    public Text m_SpeedText;

    private Text m_DisplayText;
    private float m_DisplayTime = 1.5f;

    // current ball velocity
    private float m_CurrentBallSpeed = 0f;

    // Start is called before the first frame update
    public void Start()
    {
        m_DisplayText = GetComponent<Text>();
        m_DisplayText.enabled = false;

        // unabled velocity text
        if (m_SpeedText != null)
        {
            m_SpeedText.enabled = false;
        }
    }

    public void SetBallSpeed(float speed)
    {
        m_CurrentBallSpeed = speed;

        Debug.Log("구속 설정: " + speed + " km/h");

        if (m_SpeedText == null)
        {
            Debug.LogError("SpeedText가 null입니다! UI 요소를 할당해주세요.");
            return;
        }

        m_SpeedText.text = Mathf.RoundToInt(speed) + " km/h";
        m_SpeedText.enabled = true;
        Debug.Log("구속 UI 설정 완료: " + m_SpeedText.text);
    }

    public void ShowResult(bool isStrike)
    {
        Debug.Log("ShowResult 호출됨: " + isStrike + ", 저장된 구속: " + m_CurrentBallSpeed);

        if (isStrike)
        {
            m_DisplayText.color = Color.red;
            m_DisplayText.text = "STRIKE";
        }
        else
        {
            m_DisplayText.color = Color.cyan;
            m_DisplayText.text = "BALL";
            
        }
        m_DisplayText.enabled = true;

        if (m_SpeedText != null)
        {
            m_SpeedText.text = Mathf.RoundToInt(m_CurrentBallSpeed) + " km/h";
            m_SpeedText.enabled = true;
        }

        Debug.Log("결과 표시 구속: " + m_CurrentBallSpeed + " km/h");

        DisplayBallSpeed();

        StartCoroutine("Show");
    }

    //Wait for certain time until hide the text and invoke next event
    private IEnumerator Show()
    {
        yield return new WaitForSeconds(m_DisplayTime);
        m_DisplayText.enabled = false;

        if (m_SpeedText != null)
        {
            m_SpeedText.enabled = false;
        }

        ResetEvent.Raise();
    }

    private void DisplayBallSpeed()
    {
        if (m_SpeedText == null)
        {
            Debug.LogError("SpeedText가 null입니다! ShowResult에서 구속을 표시할 수 없습니다.");
            return;
        }

        m_SpeedText.text = Mathf.RoundToInt(m_CurrentBallSpeed) + " km/h";
        m_SpeedText.enabled = true;
        Debug.Log("구속 표시됨: " + m_SpeedText.text);
    }

}
