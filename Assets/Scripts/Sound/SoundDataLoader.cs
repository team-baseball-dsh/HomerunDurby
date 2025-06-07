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
    public SoundData sfxData;
    public SoundData bgmData;

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
            if (sfxData != null)
            {
                SoundManager.Instance.LoadSoundData(sfxData);
            }

            if (bgmData != null)
            {
                SoundManager.Instance.LoadSoundData(bgmData);
            }

            // StrikeData �ε� (���� �ε���� �ʴ� ���)
            if (strikeData != null)
            {
                SoundManager.Instance.LoadSoundData(strikeData);
            }
        }

    }
}
