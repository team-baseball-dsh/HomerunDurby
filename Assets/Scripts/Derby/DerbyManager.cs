using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Result;

// Ȩ�� ���� ����� ���õ� ��ũ��Ʈ
// ���� �ۼ��� : �̻�
// ������: �̻�
// ���� ������: 2025-05-19

public class DerbyManager : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
    // Components
    ///////////////////////////////////////////////////////////////
    
    public Text RemainCountText;
    public VoidEvent GameOverEvent;
    public MenuInputReader InputReader;

    private static bool s_HasPlayedEntranceMusic = false;

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

    public void Start()
    {
        if (!s_HasPlayedEntranceMusic)
        {
            if (SoundManager.Instance != null)
            {
                SoundManager.Instance.PlaySound("appear", false, 1.0f);
                s_HasPlayedEntranceMusic = true;
            }
        }

        if (RemainCountText != null)
        {
            RemainCountText.text = "" + m_Count;
        }
    }

    ///////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
    // text sound and game over and restart logic function
    ///////////////////////////////////////////////////////////////
    public void DecrementCount(bool isStrike = true)
    {
        if (isStrike)
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
    }
    public void PlayStrikeSound()
    {
        if (SoundManager.Instance != null)
        {
            int randomSound = Random.Range(1, 4);
            string soundName = "s" + randomSound;
            SoundManager.Instance.PlaySound(soundName);
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
