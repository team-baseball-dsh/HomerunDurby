using UnityEngine;

// Ÿ���� �ִϸ��̼��� �����ϴ� ��ũ��Ʈ
// ���� �ۼ��� : �̻�
// ������: �̻�
// ���� ������: 2025-04-28

public class BatterAnimation : MonoBehaviour
{
    ////////////////////////////////////////////
    // Component
    ////////////////////////////////////////////

    // Animator 
    public Animator BatterAnimator;

    // animation for end swing
    public VoidEvent SwingFinishedEvent;

    // boolean swing 
    private bool isSwing = false;


    ///////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
    // Animation Function
    ///////////////////////////////////////////////////////////////

    // set up swing 
    public void EnableSwing()
    {
        isSwing = false;
    }

    // connect swing animation
    public void Swing()
    {
        if (!isSwing)
        {
            BatterAnimator.SetTrigger("swing");
            isSwing = true;
        }
    }

    // end swing animation
    public void SwingFinished()
    {
        SwingFinishedEvent.Raise();
    }
}
