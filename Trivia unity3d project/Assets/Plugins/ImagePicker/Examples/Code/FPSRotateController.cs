using UnityEngine;
using System.Collections;

/// <summary>
/// FPS Rotate Controller, compatible with keyboard, joystick, mouse, touch, stylus
/// Attribution license, if you use any of the code from this file, make a thank you note to Elicit Ice in the credits
/// </summary>
public class FPSRotateController : SmartMonoBehaviour {
    public bool zoomInverted = false;
    public Vector2 touchMult = new Vector2(3, 3);
    
    public float rotateY = 1;
    public float rotateX = 1;

    public Vector2 xRotateMinMax = new Vector2(0,45);
    public Vector2 origin;
    public Vector3 setRotation;
    
    public Transform zoomControl;
    public Vector2 zoomClamp = new Vector2(-10, -20);
    float zoomLerp = 0.5f;
    public  float zoomMult = 0.03f;
    Vector3 zoompos;

    public Timer moveDelay = new Timer(0.2f);
    public bool touchBlocker = false;

    public override void Start()
    {
        base.Start();

        zoompos = zoomControl.localPosition;
    }

    public int inputType = 0;

#if UNITY_ANDROID || UNITY_IPHONE
    public int tracking = -1;
#endif

    public void Update()
    { 
#if UNITY_ANDROID || UNITY_IPHONE
        if(Input.touchCount == 0 && inputType == 1)
        {
            touchBlocker = false;
            moveDelay.Stop();
            inputType = 0;
        }
        else if(inputType == 0 && Input.touchCount > 0)
        {
            inputType = 1;
        }
        if(inputType == 1)
        {
            if(Input.touchCount == 1)
            {
                inputType = 1;
                if(!moveDelay.IsRunning)
                    moveDelay.Start();
                if(moveDelay)
                {
                    touchBlocker = true;
                    PanUpdate();
                }
            }
            else if(Input.touchCount == 2)
            {
                if(!moveDelay.IsRunning)
                    moveDelay.Start();
                if(moveDelay)
                {
                    touchBlocker = true;

                    Vector3 touch1Dir = Input.touches[0].deltaPosition;
                    Vector3 touch2Dir = Input.touches[1].deltaPosition;

                    float dotProduct = Vector2.Dot(touch1Dir, touch2Dir);

                    if(dotProduct < -1) //pinch/zoom
                    {
                        ZoomUpdate();
                    }
                    else if(dotProduct > 1) //pan gesture
                    {
                        PanUpdate();
                    }
                }
            }
        }
#endif
        float y= Input.GetAxis("Horizontal") * -1;
        float x= Input.GetAxis("Vertical") * -1;
        if(x != 0 || y != 0)
        {
            setRotation.y += y * rotateY;
            setRotation.x = Mathf.Clamp((setRotation.x + x * rotateX) % 360, xRotateMinMax.x, xRotateMinMax.y);

            _transform.localEulerAngles = setRotation;
        }
        float s = Input.GetAxis("Mouse ScrollWheel");
        if (s != 0)
        {             
            _ZoomUpdate(s * 10);
        }

        if(inputType == 0 || inputType == 2)
        {
            if(Input.GetMouseButton(0))
            {
                if(!moveDelay.IsRunning)
                {
                    moveDelay.Start();
                    inputType = 2;
                    //prevent weirdness
                    oldpos = Input.mousePosition;
                }
                if(moveDelay)
                {
                    touchBlocker = true;
                    PanMouseUpdate();
                }
            }
            else
            {
                touchBlocker = false;
                moveDelay.Stop();
                inputType = 0;
            }
        }
    }
	
    public static Vector2 FixTouchDelta(Touch aT)
    {
	    float dt = Time.deltaTime / aT.deltaTime;
	    if (float.IsNaN(dt) || float.IsInfinity(dt))
	        dt = 1.0f;
	    return aT.deltaPosition * dt;
    }
	
    private void ZoomUpdate()
    {
        Touch touch = Input.GetTouch(0);
        Touch touch1 = Input.GetTouch(1);

        Vector2 curDist = touch.position - touch1.position;
        Vector2 prevDist = (touch.position - FixTouchDelta(touch)) - (touch1.position - FixTouchDelta(touch1));
        float delta = curDist.magnitude - prevDist.magnitude;
        if(zoomInverted)
            delta *= -1;
        _ZoomUpdate(delta);
    }

    private void _ZoomUpdate(float delta)
    {
        zoomLerp = Mathf.Clamp(zoomLerp + delta * zoomMult, 0, 1);
        zoompos.z = Mathf.Lerp(zoomClamp.x, zoomClamp.y, zoomLerp);
        zoomControl.localPosition = zoompos;
        
        //Vector3 change = transform.localPosition;
        //change.y = 0;
        //transform.localPosition = change;
    }

    private void PanUpdate()
    {
        Touch touch = Input.touches[0];
        if(touch.phase != TouchPhase.Moved)
            return;

        setRotation.y += (touch.deltaPosition.x / Screen.width) * touchMult.x * rotateY;
        setRotation.x = Mathf.Clamp((setRotation.x + (touch.deltaPosition.y / Screen.height) * touchMult.y * rotateX) % 360, xRotateMinMax.x, xRotateMinMax.y);

        _transform.localEulerAngles = setRotation;
    }

    public void SetY()
    {
        _ZoomUpdate(0);
    }

    Vector2 oldpos = Vector2.zero;
    private void PanMouseUpdate()
    {
        Vector2 pos = Input.mousePosition;
        pos = pos - oldpos;

        setRotation.y += (pos.x/ Screen.width) * touchMult.x * rotateY;
        setRotation.x = Mathf.Clamp((setRotation.x + (pos.y / Screen.height) * touchMult.y * rotateX) % 360, xRotateMinMax.x, xRotateMinMax.y);

        _transform.localEulerAngles = setRotation;
        
        oldpos = Input.mousePosition;
    }

}
