using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;


public enum HandType
{
    Left,
    Right
}

public class XRHandController : MonoBehaviour
{
    public HandType handType;
    public float thumbMoveSpeed = 0.1f;

    private Animator animator;
    private InputDevice inputDevice;

    private float threeFingersValue;

    public static bool collided = false;

    private bool xButton = false;

    float speed = 1f;
    float delta = 3f;

    Color32 objColor;

    public GameObject obj;

    public Material glow;
    //public Material nonGlow;


    private IEnumerator coroutine;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        inputDevice = GetInputDevice();
        
        //objColor = gameObject.GetComponent<MeshRenderer>().material.color;
    }

    // Update is called once per frame
    void Update()
    {
        AnimateHand();
        if (collided)
        {
            if (inputDevice.TryGetFeatureValue(CommonUsages.primaryButton, out xButton) && xButton)
            {                
                Debug.Log("Primary button is pressed.");
                //visual cue for selected
                // coroutine = Blink();
                // StartCoroutine(coroutine);
                obj.GetComponent<MeshRenderer>().material = glow;

                //bool selected = true;
            }
        }
       //IEnumerator Blink()
       //{
       //    for (int i = 0; i < 5; i++)
       //    {
       //        obj.GetComponent<MeshRenderer>().material = glow;
       //        yield return new WaitForSeconds(2f);
       //        obj.GetComponent<Renderer>().material = objMaterial;
       //        yield return new WaitForSeconds(2f);
       //    }
       //}
    }

    
   
    InputDevice GetInputDevice()
    {
        InputDeviceCharacteristics controllerCharacteristic = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller;

        if (handType == HandType.Left)
        {
            controllerCharacteristic = controllerCharacteristic | InputDeviceCharacteristics.Left;
        }
        else
        {
            controllerCharacteristic = controllerCharacteristic | InputDeviceCharacteristics.Right;
        }

        List<InputDevice> inputDevices = new List<InputDevice>();
        InputDevices.GetDevicesWithCharacteristics(controllerCharacteristic, inputDevices);
        Debug.Log(inputDevices.Count);
        return inputDevices[0];
    }

    void AnimateHand()
    {
        inputDevice.TryGetFeatureValue(CommonUsages.grip, out threeFingersValue);
        animator.SetFloat("ThreeFingers", threeFingersValue);
    }

    void OnCollisionEnter(Collision other)
    {

        Debug.Log("Collider entered : " + other.gameObject.name);
        if (other.gameObject.CompareTag("object"))
        {
            collided = true;
            Rigidbody rbdy = other.gameObject.GetComponent<Rigidbody>();

            //Stop Moving/Translating
            rbdy.velocity = Vector3.zero;

            //Stop rotating
            rbdy.angularVelocity = Vector3.zero;
            //visual cue
            Debug.Log("collided" + collided);
            Vector3 scale = other.transform.localScale;
            scale *= 1.5f; // or scale.x = Time.deltaTime
            other.transform.localScale = scale;

           // objColor = other.gameObject.GetComponent<MeshRenderer>().material.color;
            obj = other.gameObject;
           // nonGlow = other.gameObject.GetComponent<MeshRenderer>().material;
        }
    }

    void OnCollisionExit(Collision col)
    {
        Debug.Log("Collider exited : " + col.gameObject.name);
        if (col.gameObject.CompareTag("object"))
        {
            collided = false;
        }
        //visual cue
        Debug.Log(collided);
        Vector3 smaller = col.transform.localScale;
        smaller = smaller / 1.5f;
        col.transform.localScale = smaller;

       // obj.GetComponent<Renderer>().material = nonGlow;
    }
}
