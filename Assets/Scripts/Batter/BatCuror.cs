using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Batter
{
    public class BatCuror : BaseCursor
    {
        private enum CursorState { FOCUS, MEAT, BUNT };

        public BatterInputReader BatterInput;

        public VoidEvent SwingEvent;
        public Vector3Event BallExitVelEvent;

        public GameObject MeatCursor;
        public GameObject BuntArrow;

        public RectTransform PivotRectT;

        private CursorState m_CurrentState;
        private float m_Distance;

        [SerializeField] private bool m_CheckCollision = false;
        [SerializeField] private bool m_AnimationFinisehd = true;
        private float m_SwingDelay = 0.01f;
        private float m_SwingDone = 0.5f;
        private float m_Timer = 0.0f;

        private CapsuleCollider m_Collision;

        private Vector3 BAT_CONTACT = Vector3.one;

        private Camera m_MainCamera;
        private bool m_MouseControlEnabled = true;

        public new void Awake()
        {
            base.Awake();
            m_MainCamera = Camera.main;
            m_Collision = GetComponent<CapsuleCollider>();
            m_Collision.enabled = false;
            m_Distance = m_Cursor.localPosition.x - PivotRectT.localPosition.x;
            //m_Collision = GetComponent<CapsuleCollider>();

            // ё¶їмЅє ГК±вИ­
            if (BatterInput != null)
            {
                var canvasRT = m_Cursor.GetComponentInParent<Canvas>().GetComponent<RectTransform>();
                BatterInput.Initialize(m_MainCamera, canvasRT);
            }
        }

        public void OnEnable()
        {
            BatterInput.SwingActions += OnSwing;
            BatterInput.BuntActions += OnBunt;
            BatterInput.MoveActions += MoveCursor;

            //m_Collision.enabled = false;
        }

        public void OnDisable()
        {
            BatterInput.SwingActions -= OnSwing;
            BatterInput.BuntActions -= OnBunt;
            BatterInput.MoveActions -= MoveCursor;

        }

        protected new void MoveCursor(Vector2 dir)
        {
            /*
            // ё¶їмЅє їтБчАУїЎ ґлЗС °Ёµµ Б¶Б¤
            float sensitivity = 0.01f;

            // ё¶їмЅє АФ·ВАП °жїм ґх іфАє °Ёµµ Аыїл
            if (dir.magnitude <= 1.01f)
            {
                // ±вБё Е°єёµе АФ·В Гіё®
                base.MoveCursor(dir);
            }
            else
            {
                // ё¶їмЅє АФ·Вїл °Ёµµ Б¶Б¤
                base.MoveCursor(dir * sensitivity);
            }
        }

        // ё¶їмЅє АМµї Гіё®ё¦ А§ЗС »х ёЮј­µе
        private void OnMouseMove(Vector2 delta)
        {
            if (!m_MouseControlEnabled) return;

            // ё¶їмЅє їтБчАУїЎ µы¶у Дїј­ АМµї (°Ёµµ Б¶Аэ °ЎґЙ)
            float sensitivity = 1.0f;
            MoveCursor(delta * sensitivity);
            */

            if (dir.magnitude <= 1.01f)
            {
                base.MoveCursor(dir);
            }
            else
            {
                Vector3 targetPosition = new Vector3(dir.x, dir.y, m_Cursor.localPosition.z);

                float lerpFactor = 0.1f;

                Vector3 newPosition = Vector3.Lerp(m_Cursor.localPosition, targetPosition, lerpFactor);
                m_Cursor.localPosition = newPosition;

                m_Dir = Vector2.zero;
            }
        }

        public new void Update()
        {

            if (m_Dir.magnitude > 0)
            {
                m_Cursor.localPosition += new Vector3(m_Dir.x, m_Dir.y) * Speed * Time.deltaTime;
            }

            base.Update();


            //PivotRectT.localPosition = new Vector3(m_Cursor.localPosition.x - m_Distance, PivotRectT.localPosition.y);
            float dx = m_Cursor.localPosition.x - PivotRectT.localPosition.x;
            float dy = m_Cursor.localPosition.y - PivotRectT.localPosition.y;
            float angle = Mathf.Rad2Deg * Mathf.Atan(dy / dx);
            m_Cursor.localRotation = new Quaternion
            {
                eulerAngles = new Vector3(0, 0, angle)
            };


            if (m_CheckCollision)
            {
                m_Timer += Time.deltaTime;
                if (m_Timer > m_SwingDelay)
                {
                    m_Collision.enabled = true;
                }
                if (m_Timer > m_SwingDone)
                {
                    m_Collision.enabled = false;
                    m_CheckCollision = false;
                }
            }
        }

        public void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Ball"))
            {
                //hit!
                //calculate the exiting velocity
                Vector3 ballPos = other.transform.position;


                //Where the ball is heading based on the two
                //x - direction of ball is heading
                //y - z launch angle of the vectoy
                Vector3 delta = ballPos - transform.position;

                //using inverse equation to penalize the contact between the cursor is offcenter
                //delta.x = delta.x == 0 ? (delta.x > 0 ? (1 / delta.x) : (1 / -delta.x))*BAT_CONTACT.x : BAT_CONTACT.x;

                //z also, calculate the power of the ball exit (depends on when the bat hit the ball)

                BallExitVelEvent.Raise(delta * 150);
            }
        }

        public void OnSwingFinished()
        {
            m_AnimationFinisehd = true;
        }

        private void OnSwing()
        {
            if (!m_CheckCollision && m_AnimationFinisehd)
            {
                SwingEvent.Raise();

                m_CheckCollision = true;
                m_AnimationFinisehd = false;
                m_Timer = 0.0f;
            }
        }


        private void OnBunt(bool isPressed)
        {

        }
    }
}