using UnityEngine;
using UnityEngine.InputSystem;
//using Valve.VR;

public class InputManager : MonoBehaviour
{
    [Header("Actions")]
    public InputActionReference touch = null;
    public InputActionReference touchPosition = null;
    public InputActionReference click = null;
    //public SteamVR_Action_Boolean select = null;
    // Start is called before the first frame update
    public InputManager otherMenu;
    private bool canShow = true;

    [Header("Objects")]
    public RadialMenu radialMenu = null;
    private void Awake()
    {
        this.touch.action.started += ShowMenu;
        this.touch.action.canceled += HideMenu;
        this.touchPosition.action.performed += EmitPos;
        this.click.action.performed += ChooseMode;
    }
    private void OnDestroy()
    {
        this.touch.action.started -= ShowMenu;
        this.touchPosition.action.performed -= EmitPos;
        this.click.action.started -= ChooseMode;
    }

    // Update is called once per frame
    void ShowMenu(InputAction.CallbackContext context)
    {
        //Debug.Log("Touched"+ context.ReadValue<float>());
        /*
        if (!otherMenu.activeSelf)
        {
            radialMenu.Show(true);
        }
        */
        if (canShow)
        {
            radialMenu.Show(true);
            otherMenu.canShow = false;
        }
    }

    void HideMenu(InputAction.CallbackContext context)
    {
        //Debug.Log("Touched" + context.ReadValue<float>());
        radialMenu.Show(false);
        otherMenu.canShow = true;
    }

    void EmitPos(InputAction.CallbackContext context)
    {
        //Debug.Log("Pos selected" + context.ReadValue<Vector2>());
        radialMenu.SetTouchPosition(context.ReadValue<Vector2>());
    }

    void ChooseMode(InputAction.CallbackContext context)
    {
        //Debug.Log("Clicked");
        if (gameObject.activeSelf)
        {
            radialMenu.HightlightSection();
        }
    }
}
