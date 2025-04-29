using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// Ȩ�� ���� ����� ���õ� ��ũ��Ʈ
// ���� �ۼ��� : �̻�
// ������: �̻�
// ���� ������: 2025-04-29

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
}
