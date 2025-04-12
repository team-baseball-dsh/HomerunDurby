using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Result
{
    public enum ResultState { StrikeOut, Ground, Foul, HR };
    
    public class ResultManager : MonoBehaviour
    {
        public Text ScoreText;
        public Text TempDistanceText;
        public Text HRCountText;

        private uint m_HRCount = 0;
        private int m_Score = 0;
        private int m_Distance = 0;
        private ResultState m_CurrentResult = ResultState.Ground;

        public void Start()
        {
            ScoreText.text = "" + m_Score;
            HRCountText.text = "" + m_HRCount;
        }

        public void UpdateHR(int result)
        {
            m_CurrentResult = (ResultState)result;

            if (m_CurrentResult == ResultState.HR)
            {
                HRCountText.text = "" + ++m_HRCount;

                m_Score += m_Distance * 2;
            }
            else if (m_CurrentResult == ResultState.Ground)
            {
                m_Score += m_Distance;
            }

            ScoreText.text = "" + m_Score;
        }

        public void UpdateMaxDistance()
        {
            m_CurrentResult = ResultState.Ground;
        }

        public void UpdateDistance(float distance)
        {
            m_Distance = Mathf.RoundToInt(distance);
            TempDistanceText.text = m_Distance + "m";
        }

        public void Reset()
        {
            m_HRCount = 0;
            m_Score = 0;
            ScoreText.text = "" + m_Score;
        }
    }
}
