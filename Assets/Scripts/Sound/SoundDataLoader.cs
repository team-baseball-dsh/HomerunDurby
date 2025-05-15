using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ���� �����Ϳ� ���õ� ��ũ��Ʈ
// ���� �ۼ��� : �̻�
// ������: �̻�
// ���� ������: 2025-05-15

public class SoundDataLoader : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
    // Components
    ///////////////////////////////////////////////////////////////

    public SoundData strikeData;
    public SoundData soundData;

    ///////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
    // Unity Function
    ///////////////////////////////////////////////////////////////

    private void Start()
    {

        /*
        if (SoundManager.Instance != null && soundData != null)
        {
            SoundManager.Instance.LoadSoundData(soundData);
        }
        */

        if (SoundManager.Instance != null)
        {
            if (soundData != null)
            {
                SoundManager.Instance.LoadSoundData(soundData);
            }

            // StrikeData �ε� (���� �ε���� �ʴ� ���)
            if (strikeData != null)
            {
                SoundManager.Instance.LoadSoundData(strikeData);
            }
        }

    }
}
