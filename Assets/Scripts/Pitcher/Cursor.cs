using System.Linq;
using UnityEngine;
using UnityEngine.UI;

// ������ Ŀ�� �� ���� ���� ��ũ��Ʈ
// ���� �ۼ��� : �̻�
// ������: �̻�
// ���� ������: 2025-05-04

namespace Pitcher
{

    ///////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
    // Components - UI �� �̺�Ʈ ���� ����
    ///////////////////////////////////////////////////////////////
    public class Cursor : BaseCursor
    {
        public ResultUI resultUI;

        public FloatEvent BallSpeedEvent;

        public PitcherAI AIReader;

        public GameObject OffsetCursorObject;

        public BoolEvent IsStrikeEvent;

        public GameObject BallObj; //ball object use to instantiate
        public Transform  ReleasePoint; //The position to instantiate the ball


        ///////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////
        // Components - Ŀ�� ���� ������
        ///////////////////////////////////////////////////////////////
        private Image m_CursorImage;

        private Vector3 m_OffsetInterval = Vector3.zero;

        private RectTransform m_OffsetCursorRectT;
        private RectTransform m_CanvasRectT;
        private Ball m_Ball;

        private PitchTypeSO m_SelectedType;


        ///////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////
        // Unity Function
        ///////////////////////////////////////////////////////////////
        private new void Awake()
        {
            base.Awake();

            m_CursorImage = GetComponent<Image>();
            m_CanvasRectT = m_Cursor.GetComponent<Image>().canvas.GetComponent<RectTransform>();
            m_OffsetCursorRectT = OffsetCursorObject.GetComponent<RectTransform>();

            gameObject.SetActive(false);
            m_CursorImage.enabled = false;
        }

        private void OnEnable()
        {
            AIReader.PitchCursorActions += MoveCursor;
            m_Cursor.localPosition = m_CenterPosition;

            //if there's drop or curve, enable the offset cursor
            if (m_SelectedType.DropOffset != 0 || m_SelectedType.CurveOffset != 0)
            {
                //Change the parent of the m_OffsetCursorRectT to canvas instead of cursor so the position won't be messed
                m_OffsetCursorRectT.SetParent(m_CanvasRectT);
                //calculate the offset position
                m_OffsetCursorRectT.localPosition = CalculateOffsetCursor();
                //change back the parent to cursor
                m_OffsetCursorRectT.SetParent(m_Cursor);
            }
            m_CursorImage.enabled = false;
        }

        ///////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////
        // show the cursor functon - call when selecting ball type
        ///////////////////////////////////////////////////////////////
        /*
         * Called by Listener when the select cursor is selected
         *  modify the cursor according to the type selected and calculate the interval
         */
        public void ShowCursor(PitchTypeSO selectedType)
        {
            m_SelectedType = selectedType;

            //if there's drop or curve, enable the offset cursor
            if (m_SelectedType.DropOffset != 0 || m_SelectedType.CurveOffset != 0)
            {
                OffsetCursorObject.SetActive(true);

                //Change the parent of the _offset to canvas instead of cursor so the position won't be messed on assigned
                m_OffsetCursorRectT.SetParent(m_CanvasRectT);
                m_OffsetCursorRectT.localPosition = CalculateOffsetCursor();
                //change back the parent to cursor
                m_OffsetCursorRectT.SetParent(m_Cursor);
            }
            else
            {
                m_OffsetCursorRectT.localPosition = Vector3.zero;
                OffsetCursorObject.SetActive(false);
            }

            gameObject.SetActive(true);
            m_CursorImage.enabled = false;
            enabled = true;
        }

        ///////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////
        // Updating next cursor function
        ///////////////////////////////////////////////////////////////
        public void NextCursorPos()
        {
            m_Cursor.position += m_OffsetInterval;
        }

        ///////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////
        // Throw function
        ///////////////////////////////////////////////////////////////
        public void Throw()
        {
            //Conver the screen position to world position
            Vector3 realWorldCursorPos = Util.CameraTranform.ScreenToWorldPointCamera(Camera.main, m_Cursor);

            //Instantiate the ball
            m_Ball = Instantiate(BallObj, ReleasePoint.position, new Quaternion()).GetComponent<Ball>();

            //Create the path of ball travelling
            Vector3 Goal = m_Ball.InstantiateBallPath(m_SelectedType, ReleasePoint.position, realWorldCursorPos);

            if (resultUI != null)
            {
                resultUI.SetBallSpeed(m_SelectedType.MaxSpeed);
                Debug.Log("������ ResultUI�� ���� ���� : " + m_SelectedType.MaxSpeed);
            }
            else
            {
                Debug.LogError("resultUI ������ �����ϴ�!");
            }

            if (BallSpeedEvent != null)
            {
                BallSpeedEvent.Raise(m_Ball.GetBallSpeed());
            }

            m_OffsetInterval = (m_OffsetCursorRectT.position - m_Cursor.position) / m_Ball.PointCounts;

            //Remove the offset cursor and let the main cursor display the changes 
            OffsetCursorObject.SetActive(false);

            enabled = false;
            m_CursorImage.enabled = true;

            //start the ball moving
            m_Ball.StartMove();

            Debug.Log("���� ����: " + m_Ball.GetBallSpeed() + " km/h");
        }

        ///////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////
        // calcurate offset cursor function
        ///////////////////////////////////////////////////////////////
        private Vector3 CalculateOffsetCursor()
        {
            //Conver the screen position to world position
            Vector3 releasePt = Util.CameraTranform.ScreenToWorldPointCamera(Camera.main, m_Cursor);
            Vector3 offsetPos = Ball.CreatePath(m_SelectedType, ReleasePoint.position, releasePt).PathPoints.Last();
            Vector3 screenPos = Util.CameraTranform.WorldToScreenSpaceCamera(Camera.main, Camera.main, m_CanvasRectT, offsetPos);
            return screenPos;
        }

        ///////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////
        // show the strike UI fucntion
        ///////////////////////////////////////////////////////////////
        private bool IsStrike()
        {
            return InsideTheBound();
        }

        ///////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////
        // finish ball move function
        ///////////////////////////////////////////////////////////////
        public void OnBallFinish()
        {
            bool isInStrikeZone = InsideTheBound();

            var derbyManager = FindObjectOfType<DerbyManager>();
            if(derbyManager != null)
            {
                derbyManager.DecrementCount(isInStrikeZone);
            }

            IsStrikeEvent.Raise(InsideTheBound());
        }
    }
}
