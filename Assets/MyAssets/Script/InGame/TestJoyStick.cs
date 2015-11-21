using UnityEngine;
using System.Collections;

public class TestJoyStick : MonoBehaviour {

    [SerializeField]
    private Rigidbody playerRb;
    [SerializeField]
    private GameObject playerObject;
    //[SerializeField]
    //private GameObject joyStick_Back;
    //[SerializeField]
    //private GameObject joyStick_Front;
    [SerializeField]
    private Camera ui_Camera;

    private Vector3 originJoyStickPos;

    private Ray screenToTouch;
    private RaycastHit rayCastHit;

    //player move Speed
    private float moveSpeed = 4.5f;
    public void SetPlayerSpeed(float _speed) { moveSpeed = _speed; }

    private Vector2 backJoyStick_center;
    private Vector2 endTouchPos;

    private Animator playerAnimator;

    
    private bool isPlayerGround;

    private bool isGameStop = false;
    public void SetIsGameStop(bool _flag) { isGameStop = _flag; }
    

	void Start () 
    {
        //originJoyStickPos = joyStick_Front.transform.position;
       // backJoyStick_center = ui_Camera.WorldToScreenPoint(joyStick_Back.transform.position);

        playerAnimator = gameObject.GetComponent<Animator>();

        isPlayerGround = true;
    }


	void FixedUpdate () 
    {
        //키입력시 종료에 대한 부분은 update문으로 해야할듯싶은데, 따로 만들어서 update문을 실행시키는건 cpu낭비가..
        //이 부분은 임시코드 -- ( 사용자가 인게임중에 back-key 입력시 종료.)
        if ((Application.platform == RuntimePlatform.Android) &&  (Input.GetKey(KeyCode.Escape)))
            Application.Quit();

        if (isGameStop == true) return;
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
                    //endTouchPos = Input.GetTouch(0).position;
                    //TrackingTouchPoint(endTouchPos);
                    MoveChacracter();
                }
                break;
            case TouchPhase.Stationary:
                {
                    playerAnimator.SetBool("playerRunStop", false);
                    //endTouchPos = Input.GetTouch(0).position;
                    //TrackingTouchPoint(endTouchPos);
                    MoveChacracter();
                }
                break;

            case TouchPhase.Ended:
                {
                    //endTouchPos = Input.GetTouch(0).position;
                    //joyStick_Front.transform.position = originJoyStickPos;
                    playerAnimator.SetBool("playerRunStop", true);
                }
                break;
            case TouchPhase.Canceled:
                {
                    //joyStick_Front.transform.position = originJoyStickPos;
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
    //private void TrackingTouchPoint(Vector2 _touchPosition)
    //{
    //    if ((Physics.Raycast(screenToTouch, out rayCastHit)) &&
    //        (rayCastHit.collider.gameObject.CompareTag("JoyStick_Back")))
    //    {
    //        Vector3 endPos = ui_Camera.ScreenToWorldPoint(_touchPosition);
    //        joyStick_Front.transform.position = Vector3.Lerp(joyStick_Front.transform.position, endPos, 0.5f);
    //    }
    //}

    // test Rot
    private void TestRotation(Vector3 _dirVector)
    {
        Quaternion rotationDir = Quaternion.LookRotation(_dirVector, transform.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotationDir, 3.0f * Time.deltaTime);
    }

    
    private void MoveChacracter()
    {
        if ((Physics.Raycast(screenToTouch, out rayCastHit)) &&
            (rayCastHit.collider.gameObject.CompareTag("JumpButton"))) return;
           
        Vector3 dirVector = playerObject.transform.forward;
        RotationCharacter();

        if (isPlayerGround == true) playerAnimator.Play("Running@loop", 0);
        playerRb.MovePosition(playerObject.transform.position + dirVector * Time.deltaTime * moveSpeed);
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


