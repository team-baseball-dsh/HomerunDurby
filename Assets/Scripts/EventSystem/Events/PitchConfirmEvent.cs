using UnityEngine;

// baseEvent -> pitch type Event �Ļ��Ǵ� ��ũ��Ʈ
// ��ġ Ÿ�Ժ� �Ͼ�� event
// ���� �ۼ��� : �̻�
// ������: �̻�
// ���� ������: 2025-05-03

[CreateAssetMenu(fileName = "New PitchType Event", menuName = "Event/PitchType")]
public class PitchConfirmEvent : BaseEvent<Pitcher.PitchTypeSO>{}