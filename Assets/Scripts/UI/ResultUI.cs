using System.Collections;
using UnityEngine;
using UnityEngine.UI;


// ���� ����� ������ UI�� ǥ���ϴ� ��ũ��Ʈ
// ���� �ۼ��� : �̻�
// ������: �̻�
// ���� ������: 2025-05-04

public class ResultUI : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
    // Components
    ///////////////////////////////////////////////////////////////
    public VoidEvent ResetEvent; //TODO - will display speed/ball type later so have to move this later

    // text component that show velocity
    public Text m_SpeedText;

    private Text m_DisplayText;
    private float m_DisplayTime = 1.5f;

    // current ball velocity
    private float m_CurrentBallSpeed = 0f;

    ///////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
    // Unity Function
    ///////////////////////////////////////////////////////////////
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

    ///////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
    // velocity function
    ///////////////////////////////////////////////////////////////
    
    // set the velocity
    public void SetBallSpeed(float speed)
    {
        m_CurrentBallSpeed = speed;

        Debug.Log("���� ����: " + speed + " km/h");

        if (m_SpeedText == null)
        {
            Debug.LogError("SpeedText�� null�Դϴ�! UI ��Ҹ� �Ҵ����ּ���.");
            return;
        }

        m_SpeedText.text = Mathf.RoundToInt(speed) + " km/h";
        m_SpeedText.enabled = true;
        Debug.Log("���� UI ���� �Ϸ�: " + m_SpeedText.text);
    }

    // show the velocity function
    private void DisplayBallSpeed()
    {
        if (m_SpeedText == null)
        {
            Debug.LogError("SpeedText�� null�Դϴ�! ShowResult���� ������ ǥ���� �� �����ϴ�.");
            return;
        }

        m_SpeedText.text = Mathf.RoundToInt(m_CurrentBallSpeed) + " km/h";
        m_SpeedText.enabled = true;
        Debug.Log("���� ǥ�õ�: " + m_SpeedText.text);
    }


    ///////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
    // event couroutain function
    ///////////////////////////////////////////////////////////////
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



}
