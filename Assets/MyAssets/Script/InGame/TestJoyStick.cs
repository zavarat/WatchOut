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

    //player move Speed
    private float moveSpeed = 4.5f;

    private Vector2 backJoyStick_center;
    private Vector2 endTouchPos;

    private Animator playerAnimator;

    [SerializeField]
    private UILabel lbl_debugForAngle;

    private bool isPlayerGround;

	void Start () 
    {
        originJoyStickPos = joyStick_Front.transform.position;
        backJoyStick_center = ui_Camera.WorldToScreenPoint(joyStick_Back.transform.position);

        playerAnimator = gameObject.GetComponent<Animator>();

        isPlayerGround = true;
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
                    playerAnimator.SetBool("playerRunStop", false);
                    endTouchPos = Input.GetTouch(0).position;
                    TrackingTouchPoint(endTouchPos);
                    MoveChacracter();
                }
                break;
            case TouchPhase.Stationary:
                {
                    playerAnimator.SetBool("playerRunStop", false);
                    MoveChacracter();
                }
                break;

            case TouchPhase.Ended:
                {
                    endTouchPos = Input.GetTouch(0).position;
                    joyStick_Front.transform.position = originJoyStickPos;
                    playerAnimator.SetBool("playerRunStop", true);
                }
                break;
            case TouchPhase.Canceled:
                {
                    joyStick_Front.transform.position = originJoyStickPos;
                    playerAnimator.SetBool("playerRunStop", true);
                }
                break;
        }

	}

    private float jumpSpeed = 300.0f;
    public void CharacterJump()
    {
        if(isPlayerGround == true)
        {
            playerAnimator.SetBool("playerJumping", true);
            playerRb.AddForce(playerObject.transform.up * jumpSpeed);
            isPlayerGround = false;
        }
    }
    // player 지면보다 위에 있는지 확인한다.
    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.CompareTag("Planet"))
        {
            isPlayerGround = true;
            playerAnimator.SetBool("playerJumping", false);
            
        }
    }

    /// <summary> 
    ///  joystick tracking touched point
    /// </summary>
    /// <param name="_touchPosition"></param>
    private void TrackingTouchPoint(Vector2 _touchPosition)
    {
        if ((Physics.Raycast(screenToTouch, out rayCastHit)) &&
            (rayCastHit.collider.gameObject.CompareTag("JoyStick_Back")))
        {
            Vector3 endPos = ui_Camera.ScreenToWorldPoint(_touchPosition);
            joyStick_Front.transform.position = Vector3.Lerp(joyStick_Front.transform.position, endPos, 0.5f);
        }
    }

    private void TestRotation(Vector3 _dirVector)
    {
        Quaternion rotationDir = Quaternion.LookRotation(_dirVector, transform.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationDir, 3.0f * Time.deltaTime);
    }

    
    private void MoveChacracter()
    {
        if ((Physics.Raycast(screenToTouch, out rayCastHit)) &&
            (rayCastHit.collider.gameObject.CompareTag("JoyStick_Back")))
        {
            Vector3 dirVector = Vector3.zero;
            float angle = GetBetweenAngle();
            //debug-test code
            lbl_debugForAngle.text = angle.ToString();
            if(((angle >= 0.0f) && (angle < 15.0f)) || ((angle <= 0.0f) && (angle > 345.0f)))
            {
                //EAST direction
                //dirVector = playerObject.transform.right;
                dirVector = playerObject.transform.right;
                TestRotation(dirVector);
            }
            else if ((angle >= 15.0f) && (angle < 75.0f))
            {
                // North East direction
                dirVector = playerObject.transform.right + playerObject.transform.forward;
                TestRotation(dirVector);
            }
            else if ((angle >= 75.0f) && (angle < 105.0f))
            {
                // North direction
                dirVector = playerObject.transform.forward;
                TestRotation(dirVector);
            }
            else if ((angle >= 105.0f) && (angle < 165.0f))
            {
                // North West direction
                dirVector = (-playerObject.transform.right) + playerObject.transform.forward;
                TestRotation(dirVector);
            }
            else if(((angle >= 180.0f) && (angle < 195.0f)) || ((angle <= 180.0f) && (angle > 165.0f)))
            {
                // West direction
                dirVector = -playerObject.transform.right;
                TestRotation(dirVector);
            }
            else if ((angle >= 195.0f) && (angle < 265.0f))
            {
                // South West direction
                dirVector = (-playerObject.transform.right) + (-playerObject.transform.forward);
                TestRotation(dirVector);
            }
            else if ((angle >= 265.0f) && (angle < 295.0f))
            {
                //South direction
                dirVector = -playerObject.transform.forward;
                TestRotation(dirVector);
            }
            else if ((angle >= 295.0f) && (angle < 360.0f))
            {
                //South East direction
                dirVector = playerObject.transform.right + (-playerObject.transform.forward);
                TestRotation(dirVector);
            }
            if (isPlayerGround == true) playerAnimator.Play("Running@loop", 0);
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


