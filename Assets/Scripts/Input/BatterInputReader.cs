using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "New Battter Input", menuName = "InputReader/Batter")]
public class BatterInputReader : ScriptableObject, GameInput.IBatterNRunnerActions
{
    public event UnityAction<Vector2> MoveActions;
    public event UnityAction SwingActions;
    public event UnityAction<bool> BuntActions;

    private GameInput m_GameInput;

    private Camera m_MainCamera;
    private RectTransform m_CanvasRectTransform;

    // ё¶їмЅє А§ДЎ ГЯАы єЇјц
    private Vector2 m_PreviousMousePosition;

    public void Initialize(Camera camera, RectTransform canvasRect)
    {
        m_MainCamera = camera;
        m_CanvasRectTransform = canvasRect;
        m_PreviousMousePosition = Mouse.current.position.ReadValue();
    }

    public void OnEnable()
    {
        if (m_GameInput == null)
        {
            m_GameInput = new GameInput();
            m_GameInput.BatterNRunner.SetCallbacks(this);
        }
        m_GameInput.BatterNRunner.Enable();
    }

    public void OnDisable()
    {
        m_GameInput.BatterNRunner.Disable();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (MoveActions != null)
        {
            MoveActions.Invoke(context.ReadValue<Vector2>());
        }
    }

    public void OnMousePosition(InputAction.CallbackContext context)
    {
        if (MoveActions == null || m_MainCamera == null) return;

        Vector2 mousePosition = context.ReadValue<Vector2>();

        if (m_CanvasRectTransform != null)
        {
            // ё¶їмЅє А§ДЎё¦ UI БВЗҐ°и·О єЇИЇ
            Vector2 localPoint;
            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                m_CanvasRectTransform,
                mousePosition,
                m_MainCamera,
                out localPoint))
            {
                // БчБўАыАО А§ДЎё¦ АьґЮ (delta°Ў ѕЖґС)
                MoveActions.Invoke(localPoint);
            }
        }
        m_PreviousMousePosition = mousePosition;
    }

    public void OnMouseClick(InputAction.CallbackContext context)
    {
        if (context.performed && SwingActions != null)
        {
            SwingActions.Invoke();
        }
    }

    public void OnSwing(InputAction.CallbackContext context)
    {
        if (SwingActions != null && context.performed)
        {
            SwingActions.Invoke();
        }
    }

    public void OnBunt(InputAction.CallbackContext context)
    {
        if (BuntActions != null)  // MoveActionsїЎј­ BuntActions·О јцБ¤
        {
            if (context.performed)
            {
                BuntActions.Invoke(true);
            }
            else if (context.canceled)
            {
                BuntActions.Invoke(false);
            }
        }
    }
}