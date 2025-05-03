using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// baseEvent -> void Event �Ļ��Ǵ� ��ũ��Ʈ
// void�� ���õ� �̺�Ʈ���� ����
// void.cs�ʹ� ������ Ÿ�� ���ǿ� �̺�Ʈ ������ ���ǿ����� ���� ����
// ���� �ۼ��� : �̻�
// ������: �̻�
// ���� ������: 2025-05-03

[CreateAssetMenu(fileName="New Void Event", menuName ="Event/void")]
public class VoidEvent : BaseEvent<Void>
{
    public void Raise()
    {
        Raise(new Void());
    }
}
