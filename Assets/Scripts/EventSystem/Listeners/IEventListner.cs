using UnityEngine;

// �������� ������ ���õ� ��ũ��Ʈ
// ���� �ۼ��� : �̻�
// ������: �̻�
// ���� ������: 2025-05-03

public interface IEventListener<T>
{
    void OnEventRaised(T data);
}
