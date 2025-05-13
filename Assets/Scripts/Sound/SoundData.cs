using System.Collections.Generic;
using UnityEngine;

// BGM, SFX�� ���õ� �����Ϳ� ���õ� ��ũ���ͺ� ������Ʈ
// ���� �ۼ��� : �̻�
// ������: �̻�
// ���� ������: 2025-05-13

[CreateAssetMenu(fileName = "SoundData", menuName = "Sound/SoundData")]


public class SoundData : ScriptableObject
{
    ///////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
    // Components
    ///////////////////////////////////////////////////////////////
    
    [System.Serializable]
    public class Sound
    {
        public string soundName;
        public AudioClip audioClip;
    }

    public List<Sound> sound = new List<Sound>();
}
