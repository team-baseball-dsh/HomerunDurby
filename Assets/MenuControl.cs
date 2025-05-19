using UnityEngine;
using UnityEngine.SceneManagement;

// �޴� ��ư �Է��� ó���ϴ� ��ũ��Ʈ
// ���� �ۼ��� : �̻�
// ������: �̻�
// ���� ������: 2025-05-19

public class MenuControl : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
    // Components
    ///////////////////////////////////////////////////////////////
    public MenuInputReader InputReader;

    ///////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
    // Unity Function
    ///////////////////////////////////////////////////////////////
    private void OnEnable()
    {
        InputReader.StartActions += OnStartGame;
    }

    private void OnDisable()
    {
        InputReader.StartActions -= OnStartGame;
    }

    ///////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
    // Game Start Scene Function
    ///////////////////////////////////////////////////////////////
    ///
    private void OnStartGame()
    {
        Debug.Log("OnStartGame ȣ���");

        if (SoundManager.Instance != null)
        {
            Debug.Log("SoundManager.Instance ������");
            SoundManager.Instance.PlaySound("playball", false, 1.0f);
        }
        else
        {
            Debug.LogError("SoundManager.Instance�� null�Դϴ�!");
        }

        Invoke("StartGame", 0.1f);
    }

    private void StartGame()
    {
        InputReader.StartActions -= OnStartGame; //remove the function from the action incase of double tap to avoid calling multiple time
        SceneManager.LoadScene("DerbyScene",LoadSceneMode.Single);
    }
}
