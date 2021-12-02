using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
//using Valve.VR;

public class InputManagerForMenu : MonoBehaviour
{
    [Header("Actions")]
    public InputActionReference touch = null;
    public InputActionReference touchPosition = null;
    public InputActionReference click = null;
    //public SteamVR_Action_Boolean select = null;
    // Start is called before the first frame update
    private void Awake()
    {
        this.touch.action.started += ShowMenu;
        this.touchPosition.action.performed += EmitPos;
        this.click.action.started += ChooseMode;
    }


    // Update is called once per frame
    void ShowMenu(InputAction.CallbackContext context)
    {
        Debug.Log("Touched");
    }

    void EmitPos(InputAction.CallbackContext context)
    {
        //Debug.Log("Pos selected" + context.ReadValue<Vector2>());
    }

    void ChooseMode(InputAction.CallbackContext context)
    {
        Debug.Log("Clicked");
    }
}
