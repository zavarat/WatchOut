using UnityEngine;
using System.Collections;

public class TestJoyStick : MonoBehaviour {

    [SerializeField]
    private Rigidbody playerRb;
    [SerializeField]
    private GameObject playerObject;
    [SerializeField]
    private GameObject joyStick_Back;
    [SerializeField]
    private GameObject joyStick_Front;
    [SerializeField]
    private Camera ui_Camera;

    private Vector3 originJoyStickPos;

    private Ray screenToTouch;
    private RaycastHit rayCastHit;

    private float moveSpeed = 10.0f;

    private Vector2 backJoyStick_center;
    private Vector2 endTouchPos;

    [SerializeField]
    private UILabel lbl_debug;

	void Start () 
    {
        originJoyStickPos = joyStick_Front.transform.position;
        backJoyStick_center = ui_Camera.WorldToScreenPoint(joyStick_Back.transform.position);
	}


	void FixedUpdate () 
    {
        if (Input.touchCount == 0) return;

        screenToTouch = ui_Camera.ScreenPointToRay(Input.GetTouch(0).position);
        TouchPhase touchPhase = Input.GetTouch(0).phase;
        switch (touchPhase)
        {
            case TouchPhase.Began:
                {
                    // to do
                }
                break;
            case TouchPhase.Moved:
                {
                    endTouchPos = Input.GetTouch(0).position;
                    TrackingTouchPoint(endTouchPos);
                    MoveChacracter();
                    RotationCharacter();
                }
                break;
            case TouchPhase.Stationary:
                {
                    MoveChacracter();
                    RotationCharacter();
                }
                break;

            case TouchPhase.Ended:
                {
                    endTouchPos = Input.GetTouch(0).position;
                    joyStick_Front.transform.position = originJoyStickPos;
                }
                break;
            case TouchPhase.Canceled:
                {
                    joyStick_Front.transform.position = originJoyStickPos;
                }
                break;
        }
	}

    /// <summary>
    ///  joystick tracking touched point
    /// </summary>
    /// <param name="_touchPosition"></param>
    private void TrackingTouchPoint(Vector2 _touchPosition)
    {
        if ((Physics.Raycast(screenToTouch, out rayCastHit)) &&
            (rayCastHit.collider.gameObject.tag == "JoyStick_Back"))
        {
            Vector3 endPos = ui_Camera.ScreenToWorldPoint(_touchPosition);
            joyStick_Front.transform.position = Vector3.Lerp(joyStick_Front.transform.position, endPos, 0.5f);
        }
    }

    
    private void MoveChacracter()
    {
        if ((Physics.Raycast(screenToTouch, out rayCastHit)) &&
            (rayCastHit.collider.gameObject.tag == "JoyStick_Back"))
        {
            Vector3 dirVector = Vector3.zero;
            float angle = GetBetweenAngle();
            lbl_debug.text = angle.ToString();
            if(((angle >= 0.0f) && (angle < 15.0f)) || ((angle <= 0.0f) && (angle >= -15.0f)))
            {
                dirVector = playerObject.transform.right;
            }
            else if(((angle >= 180.0f) && (angle < 195.0f)) || ((angle <= 180.0f) && (angle >= 165.0f)))
            {
                dirVector = -playerObject.transform.right;
            } 

            playerRb.MovePosition(playerObject.transform.position + dirVector *Time.deltaTime * moveSpeed);
        }
    }

    private void RotationCharacter()
    {
        float touchX = Input.GetTouch(0).deltaPosition.x;
        Quaternion xQuaternion = Quaternion.AngleAxis(touchX, Vector3.up);
        playerObject.transform.rotation *= xQuaternion;
    }
    
    private float GetBetweenAngle()
    {
        float angle = Mathf.Atan((endTouchPos.y - backJoyStick_center.y) / (endTouchPos.x - backJoyStick_center.x)) * 57.2958f;

        if ((endTouchPos.y > backJoyStick_center.y) && (endTouchPos.x > backJoyStick_center.x))
        {
            return angle;
        }
        else if (((endTouchPos.y < backJoyStick_center.y) && (endTouchPos.x < backJoyStick_center.x)) ||
           ((endTouchPos.y > backJoyStick_center.y) && (endTouchPos.x < backJoyStick_center.x)))
        {
            return angle += 180.0f;
        }
        else
            return angle += 360.0f;
    }

}


