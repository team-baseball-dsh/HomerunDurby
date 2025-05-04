using UnityEngine;

// ī�޶� ���� ��ǥ ��ȯ ��ƿ��Ƽ Ŭ����
// ���� �ۼ��� : �̻�
// ������: �̻�
// ���� ������: 2025-05-04

namespace Util
{
    public static class CameraTranform 
    {
        ///////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////
        // ���� ��ǥ�� UI ĵ������ ���� ��ǥ�� ��ȯ�ϴ� �Լ�
        ///////////////////////////////////////////////////////////////
        public static Vector3 WorldToScreenSpaceCamera(Camera worldCamera, Camera canvasCamera, RectTransform canvasRectTransform, Vector3 worldPosition)
        {
            // ���� ��ǥ�� ��ũ�� ��ǥ�� ��ȯ
            var screenPoint = RectTransformUtility.WorldToScreenPoint(cam: worldCamera, worldPoint: worldPosition);
            // ��ũ�� ��ǥ�� ĵ���� RectTransform�� ���� ��ǥ�� ��ȯ
            RectTransformUtility.ScreenPointToLocalPointInRectangle(rect: canvasRectTransform, screenPoint: screenPoint, cam: canvasCamera, localPoint: out var localPoint);

            return localPoint;
        }

        ///////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////
        // UI ���(RectTransform)�� ��ġ�� ���� ��ǥ�� ��ȯ�ϴ� �Լ�
        ///////////////////////////////////////////////////////////////
        public static Vector3 ScreenToWorldPointCamera(Camera canvasCamera, RectTransform rect)
        {
            // RectTransform�� ��ġ�� ��ũ�� ��ǥ�� ��ȯ
            Vector2 screenPos = RectTransformUtility.WorldToScreenPoint(canvasCamera, rect.position);

            // ��ũ�� ��ǥ�� ���� ��ǥ�� ��ȯ
            RectTransformUtility.ScreenPointToWorldPointInRectangle(rect, screenPos, canvasCamera, out var result);

            return result;
        }
    }
}
