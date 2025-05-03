using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

// ���� �޴� �Է¿� ���õ� ��ũ��Ʈ
// ��ũ���ͺ� ������Ʈ �� input action���� ȿ���� �
// ���� �ۼ��� : �̻�
// ������: �̻�
// ���� ������: 2025-05-03


[CreateAssetMenu(fileName = "MenuInputReader", menuName = "InputReader/Menu", order = 1)]
public class MenuInputReader : ScriptableObject, MenuInput.IMenuActions
{
    ///////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
    // Components
    ///////////////////////////////////////////////////////////////
    public event UnityAction StartActions;  

    private MenuInput m_MenuInput;

    ///////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
    // Unity Function
    ///////////////////////////////////////////////////////////////
    void OnEnable()
    {
        if (m_MenuInput == null)
        {
            m_MenuInput = new MenuInput();
            m_MenuInput.Menu.SetCallbacks(this);
        }
        m_MenuInput.Menu.Enable();
    }

    void OnDisable()
    {
        m_MenuInput.Menu.Disable();
    }

    ///////////////////////////////////////////////////////////////
    ///////////////////////////////////////////////////////////////
    // starting menu function
    ///////////////////////////////////////////////////////////////
    public void OnStartGame(InputAction.CallbackContext context)
    {
        if(StartActions != null && context.canceled) 
        {
            StartActions.Invoke();    
        }
    }
}
