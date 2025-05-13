using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Ȩ�� ���� ����� ���õ� ��ũ��Ʈ
// ���� �ۼ��� : �̻�
// ������: �̻�
// ���� ������: 2025-05-07

public class DerbyManager : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
    // Components
    ///////////////////////////////////////////////////////////////
    
    public Text RemainCountText;
    public VoidEvent GameOverEvent;
    public MenuInputReader InputReader;

    // life count
    private int m_Count = 10;

    // Debug Mode
    [Header("����� ����")]
    [Tooltip("Ȱ��ȭ�ϸ� ��� Ÿ���� Ȩ���� �ȴ�.")]
    public bool DebugHR = false;

    [Tooltip("����� ��忡�� Ȩ���� ��Ÿ� (����)")]
    public int DebugHomeRunDistance = 120;

    [Tooltip("����� ��忡�� ����� ���� �ӵ�")]
    public float DebugBallSpeed = 40f;

    // set the debug bool button
    public static bool IsDebugHomeRunMode = false;
    public static int DebugDistance = 120;
    public static float DebugBallSpeedValue = 25f;

    ///////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
    // Unity Function
    ///////////////////////////////////////////////////////////////
    public void Awake()
    {
        IsDebugHomeRunMode = DebugHR;
        DebugDistance = DebugHomeRunDistance;
        DebugBallSpeedValue = DebugBallSpeed;
    }

    ///////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
    // text and game over and restart logic function
    ///////////////////////////////////////////////////////////////
    public void DecrementCount()
    {
        --m_Count;
        RemainCountText.text = "" + m_Count;
        if (m_Count == 0)
        {
            //GameOver
            //Display Result
            GameOverEvent.Raise();
            InputReader.StartActions += Restart;
        }
    }

    private void Restart()
    {
        InputReader.StartActions -= Restart;
        SceneManager.LoadScene("DerbyScene",LoadSceneMode.Single); // change scene
    }

    ///////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
    // Debug toggle fundtion
    ///////////////////////////////////////////////////////////////
    public void ToggleDebugHomeRun()
    {
        DebugHR = !DebugHR;
        IsDebugHomeRunMode = DebugHR;
        Debug.Log("Ȩ�� ����� ���: " + (DebugHR ? "Ȱ��ȭ" : "��Ȱ��ȭ"));
    }
}
