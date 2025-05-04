using UnityEngine;
using UnityEngine.Events;

// AI ������ �ൿ�� �����ϴ� ��ũ��Ʈ
// AI ������ ���� ����, Ȯ��, Ŀ�� �������� �����Ѵ�.
// ���� �ۼ��� : �̻�
// ������: �̻�
// ���� ������: 2025-05-04

namespace Pitcher {
    public class PitcherAI : MonoBehaviour
    {
        ///////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////
        // Components - about event Action
        ///////////////////////////////////////////////////////////////
        public UnityAction<Vector2> PitchSelectActions;
        public UnityAction          PitchConfirmActions;
        public UnityAction<Vector2> PitchCursorActions;

        // management AI statement
        enum State { IDLE, PICKING, THROWING, CLOSING };

        private State m_CurrentState = State.IDLE;
        [SerializeField] private float m_WaitTime = 5.0f;
        private float m_Time = 0.0f;
        private bool isReady;

        // Update is called once per frame
        void Update()
        {
            /*
             ���� ���� ���
            1. �������κ��� �غ� ��ȣ ���
            2. �������� ���� ���� (����ġ ����)
            3. ���� ��ġ ����
             */
            switch (m_CurrentState)
            {
                case State.IDLE:
                    if (m_Time > m_WaitTime)
                    {
                        m_CurrentState = State.PICKING;
                        m_Time = 0.0f;
                    }
                    else
                    {
                        m_Time += Time.deltaTime;
                    }
                    break;
                case State.PICKING:
                    // ���� ���� �� Ȯ��
                    SelectNConfirm();
                    m_CurrentState = State.THROWING;
                    break;
                case State.THROWING:
                    // Ŀ�� �̵�
                    CursorMove();
                    break;
                case State.CLOSING:
                    //wait for the response from the other part of the game for pitch to start to throw

                    //TEMP - just back to idle for now
                    m_CurrentState = State.IDLE;
                    break;
            }
        }

        ///////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////
        // call the function when finished the pitch
        ///////////////////////////////////////////////////////////////
        public void Throwed()
        {
            m_CurrentState = State.CLOSING;
        }

        ///////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////
        // select the breaking ball Function
        ///////////////////////////////////////////////////////////////
        private void SelectNConfirm()
        {
            //Randomly select the pitch
            Vector2 selected = new Vector2(Mathf.RoundToInt(Random.value * 2 - 1), Mathf.RoundToInt(Random.value * 2 - 1));
            //Invoke actions
            if(PitchConfirmActions != null && PitchSelectActions != null)
            {
                PitchSelectActions.Invoke(selected);
                PitchConfirmActions.Invoke();
            }
        }

        ///////////////////////////////////////////////////////////////
        ///////////////////////////////////////////////////////////////
        // move the cursor function
        ///////////////////////////////////////////////////////////////
        private void CursorMove()
        {
            if(PitchCursorActions != null)
            {
                Vector2 dir = new Vector2(Random.value* 2 - 1, Random.value * 2 - 1 );
                PitchCursorActions.Invoke(dir);
            }
        }
    }

}