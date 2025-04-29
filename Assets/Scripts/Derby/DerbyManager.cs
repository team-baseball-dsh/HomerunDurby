using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

// 홈런 더비 제어와 관련된 스크립트
// 최초 작성자 : 이상도
// 수정자: 이상도
// 최종 수정일: 2025-04-29

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
