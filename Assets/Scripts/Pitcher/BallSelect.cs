using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Pitcher
{
    public class BallSelect : MonoBehaviour
    {
        private bool m_IsAI = true;

        public PitcherInputReader InputReader;
        public PitcherAI AIReader;

        public PitcherDataSO PitcherData;

        private Dictionary<BallDir, GameObject>  m_PitchArrowUIDict = new Dictionary<BallDir, GameObject>();
        private Dictionary<BallDir, PitchTypeSO> m_PitchTypeDict    = new Dictionary<BallDir, PitchTypeSO>();

        private BallDir m_SelectedBallDir;

        public PitchConfirmEvent _OnConfirmPitchEvent;

        public void OnEnable()
        {
            m_IsAI = true;
            if (m_IsAI)
            {
                AIReader.PitchSelectActions += Select;
                AIReader.PitchConfirmActions += Confirm;
            }
            else
            {
                InputReader.SelectActions += Select;
                InputReader.ConfirmActions += Confirm;
            } 
        }

        public void OnDisable()
        {
            InputReader.SelectActions  -= Select;
            InputReader.ConfirmActions -= Confirm;
            AIReader.PitchSelectActions -= Select;
            AIReader.PitchConfirmActions -= Confirm;
        }

        public void Override(bool isAI) { m_IsAI = isAI; }

        public void Start()
        {
            for (int i = 0; i < transform.childCount; ++i)
            {
                GameObject arrowUI = transform.GetChild(i).gameObject;
                ArrowDirection arrowDir = arrowUI.GetComponent<ArrowDirection>();

                arrowUI.SetActive(false); 
   

                foreach (PitchTypeSO pitchType in PitcherData.PitchTypes)
                {

                    if (arrowDir.Dir == pitchType.Dir)
                    {

                        arrowUI.SetActive(true);

                        if(!m_PitchTypeDict.ContainsKey(arrowDir.Dir))     m_PitchTypeDict.Add(arrowDir.Dir, pitchType);
                        if(!m_PitchArrowUIDict.ContainsKey(arrowDir.Dir)) m_PitchArrowUIDict.Add(arrowDir.Dir, arrowUI);
                        break;
                    }
                }
            }
            m_SelectedBallDir = BallDir.SLOW;
            SelectBall(BallDir.SLOW);
        }

        public void SelectBall(BallDir dir)
        {
            if (m_PitchArrowUIDict.ContainsKey(dir))
            {
                m_SelectedBallDir = dir;
            }  
            else
            {
                m_SelectedBallDir = BallDir.SLOW;
            }
        }
        private void Select(Vector2 dir)
        {
            SelectBall(PitchTypeSO.WhatBallType(dir));
        }

        private void Confirm()
        {
            PitchTypeSO selectedType = m_PitchTypeDict[m_SelectedBallDir];

            _OnConfirmPitchEvent.Raise(selectedType);

            gameObject.SetActive(false);
        }
    }
}