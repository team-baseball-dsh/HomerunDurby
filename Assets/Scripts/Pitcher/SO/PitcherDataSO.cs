using System.Collections.Generic;
using UnityEngine;

// ���� �����͸� �����ϴ� ��ũ���ͺ� ������Ʈ ��ũ��Ʈ
// ���� �ۼ��� : �̻�
// ������: �̻�
// ���� ������: 2025-05-04

namespace Pitcher
{
    ///////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
    // new asset
    ///////////////////////////////////////////////////////////////
    [CreateAssetMenu(fileName = "PitcherData", menuName = "ScriptableObjects/PitcherData", order = 1)]
    public class PitcherDataSO : ScriptableObject
    {
        ///////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////
        // components - pitcher character
        ///////////////////////////////////////////////////////////////
        [Range(50,100)] public float Stamina;
        [Range(50,100)] public int Control;
        public List<PitchTypeSO> PitchTypes = new List<PitchTypeSO>();
    }
}

