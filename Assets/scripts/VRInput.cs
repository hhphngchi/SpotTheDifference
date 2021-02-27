using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class VRInput : BaseInput
{
    public Camera eventCamera = null; //dummy camera
    public OVRInput.Button clickButton = OVRInput.Button.PrimaryIndexTrigger;
    public OVRInput.Controller controller = OVRInput.Controller.All;

    protected override void Awake()
    {
        GetComponent<BaseInputModule>().inputOverride = this;
    }
    
    //get a button of the mouse
    public override bool GetMouseButton(int button)
    {
        return OVRInput.Get(clickButton, controller);
    }

    // get down button of the mouse
    public override bool GetMouseButtonDown(int button)
    {
        return OVRInput.GetDown(clickButton, controller);
    }

    // get up button of the mouse
    public override bool GetMouseButtonUp(int button)
    {
        return OVRInput.GetUp(clickButton, controller);
    }

    // cursor position is in the middle of the event camera
    public override Vector2 mousePosition
    {
        get
        {
            return new Vector2(eventCamera.pixelWidth / 2, eventCamera.pixelHeight / 2);
        }
    }

}
