using System.Collections.Generic;
using System.Linq;
using UnityEngine;


// 공의 움직임을 제어하는 스크립트
// 최초 작성자 : 이상도
// 수정자: 이상도
// 최종 수정일: 2025-04-28

public class Ball : MonoBehaviour
{

    ///////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
    // Components
    ///////////////////////////////////////////////////////////////
    public Vector3Event BallTravelEvent;
    public IntEvent BallFinishEvent;
    public TransformEvent BallHitEvent;
    public FloatEvent DistanceTrackerEvent;

    public float SPEED_CONSTANT = 100000f;
    public int PointCounts = 100;

    private Rigidbody m_RB; 
    private Bezier m_BallPath;

    private Vector3 m_HitPos;

    private float m_Time  = 0f;
    private int   m_Index = 0;
    private float m_Speed = 0.0f; 
    private bool  m_Start = false;
    private bool  m_IsHit = false;

    private const int NUM_CTRL_PTS = 4;
    private const int EXTRA_PTS = 4; 


    private float m_BallSpeed = 0f;

  
    private const float VERT_MULTIPLIER = 0.0015f;
    private const float HORI_MULTIPLIER = 0.0015f;

    private Result.ResultState m_ResultState = Result.ResultState.Ground;


    LineRenderer line;


    ///////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
    // Unity Function
    ///////////////////////////////////////////////////////////////
    public void Awake()
    {
        m_Start = false;
        m_IsHit = false;
        m_RB = GetComponent<Rigidbody>();
    }

    public void Update()
    {
        if (!m_Start) return;

        // value of hit statement
        if (m_IsHit)
        {
            if (m_RB.velocity.magnitude < 0.55f)
            {
                m_ResultState = Result.ResultState.Ground;
                Destroy(gameObject);
            }

            DistanceTrackerEvent.Raise((transform.position - m_HitPos).magnitude);
            return;
        }

        m_Time += Time.deltaTime;

        if (m_Index < m_BallPath.PathPoints.Count && m_Time > m_Speed)
        {
            transform.position = m_BallPath.PathPoints[m_Index++];
            BallTravelEvent.Raise(transform.position);
            m_Time = 0.0f; 
        }
        else if(m_Index >= m_BallPath.PathPoints.Count)
        {

            m_ResultState = Result.ResultState.StrikeOut;
            Destroy(gameObject);
        }
    }

    ///////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
    // Physical and Position of Ball
    ///////////////////////////////////////////////////////////////
    public void StartMove()
    {
        m_Start = true;
    }


    // init ball path
    public Vector3 InstantiateBallPath(Pitcher.PitchTypeSO selectedType, Vector3 releasePt, Vector3 targetPt)
    {
        // create bezier curve path
        m_BallPath = CreatePath(selectedType, releasePt, targetPt);
     
       // making path points (feat.breaking ball)
        PointCounts = m_BallPath.PathPoints.Count;

        // set up ball max speed
        m_BallSpeed = selectedType.MaxSpeed;

        // making ball speed
        m_Speed = selectedType.MaxSpeed * ((targetPt - releasePt).magnitude / m_BallPath.PathPoints.Count) * SPEED_CONSTANT;

        // return last ball path
        return m_BallPath.PathPoints.Last();
    }

    // init bezier path
    public static Bezier CreatePath(Pitcher.PitchTypeSO type, Vector3 releasePt, Vector3 targetPt)
    {

        Vector2 delta = new Vector2(-type.CurveOffset * HORI_MULTIPLIER, -type.DropOffset * VERT_MULTIPLIER);

       // vector between start pt to end pt
        Vector3 dir = targetPt - releasePt;

        // reset control points
        List<Vector3> ctrlPts = new List<Vector3> { releasePt };

        // cal dis ctrl points
        // 전체 거리를 3등분하여 4개의 컨트롤 포인트로 배치
        float interval = dir.magnitude / (NUM_CTRL_PTS - 1);

        // create ctrl points roof
        for (int i = 1; i < NUM_CTRL_PTS; ++i)
        {
            Vector3 previousPts = ctrlPts[i - 1];

            Vector3 point = previousPts + (dir.normalized * interval);
            point.y += delta.y * i;
            if (i > NUM_CTRL_PTS - 2)
                point.x += delta.x * i;
            if (i == NUM_CTRL_PTS - 1)
            {
                point.x += delta.x * 10;
            }
            ctrlPts.Add(point);
        }

        return new Bezier(ctrlPts);
    }

    ///////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
    // Collision And Hitting Function
    ///////////////////////////////////////////////////////////////
    public void OnHit(Vector3 vel)
    {
        m_IsHit = true;
        if (m_RB == null)
        {
            Debug.Log("m_RB is undefined for some reason");
        }
        else
        {

            m_HitPos = transform.position;

            m_RB.useGravity = true;
            m_RB.velocity = vel;
            BallHitEvent.Raise(transform);
        }
    }


    public void OnTriggerEnter(Collider col)
    {
        if (col.CompareTag("OutOfBound"))
        {

            m_ResultState = Result.ResultState.Foul;
        }
        else if (col.CompareTag("HomeRunBound"))
        {
            m_ResultState = Result.ResultState.HR;
        }
        else
        {
            m_ResultState = Result.ResultState.Ground;
            return;
        }
        Destroy(gameObject);
    }

    public void OnDestroy()
    {
        BallFinishEvent.Raise((int)m_ResultState);
    }


    public float GetBallSpeed()
    {
        return m_BallSpeed;
    }
}

