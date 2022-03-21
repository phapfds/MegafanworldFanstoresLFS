using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;
[RequireComponent(typeof(ThirdPersonCharacter))]
public class ThirdPersonUserControl : MonoBehaviour
{
    private ThirdPersonCharacter m_Character; // A reference to the ThirdPersonCharacter on the object
    private Transform m_Cam;                  // A reference to the main camera in the scenes transform
    private Vector3 m_CamForward;             // The current forward direction of the camera
    private Vector3 m_Move;
    private bool m_Jump;                    // the world-relative desired move direction, calculated from the camForward and user input.
    private GameObject securityPos;
    string sceneName;
    private void Awake()
    {

        sceneName = SceneManager.GetActiveScene().name;
        if (SceneManager.GetActiveScene().name != "FanRoom" && SceneManager.GetActiveScene().name != "FanStore")
        {
            if (InGameManager.Instance.IngameType == IngameType.OutsideStadium)
            {
                Security.catched += IsCatchedBySecurity;
            }
        }
    }
    public void IsCatchedBySecurity(Transform trans)
    {
        securityPos = trans.gameObject;
        RotateToAim(trans.position); ;
    }
    public void RotateToAim(Vector3 aim)
    {
        Vector3 dir = aim - transform.position;

        dir.Normalize();
        if (dir != Vector3.zero)
        {
            Quaternion q = Quaternion.LookRotation(dir, Vector3.up);
            transform.rotation = Quaternion.Euler(new Vector3(0, q.eulerAngles.y, 0));
        }
    }
    private void Start()
    {
        // get the transform of the main camera
        if (Camera.main != null)
        {
            m_Cam = Camera.main.transform;
        }
        else
        {
            Debug.LogWarning(
                "Warning: no main camera found. Third person character needs a Camera tagged \"MainCamera\", for camera-relative controls.", gameObject);
            // we use self-relative controls in this case, which probably isn't what the user wants, but hey, we warned them!
        }

        // get the third person character ( this should never be null due to require component )
        m_Character = GetComponent<ThirdPersonCharacter>();
        m_Character.Move(Vector3.forward, false, false);
    }


    private void Update()
    {
        if (!m_Jump && sceneName != "FanRoom" && sceneName != "FanStore")
        {
            m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
        }
        if (sceneName != "FanRoom" && sceneName != "FanStore")
        {
            if (InGameManager.Instance.IngameState == IngameState.CatchedBySecurity)
                RotateToAim(securityPos.transform.position);
        }
    }


    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        // read inputs
        float h = 0;
        float v = 0;
        if (!m_Character.m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Throw") && !m_Character.m_Animator.GetCurrentAnimatorStateInfo(0).IsName("Kick"))
        {
            h = CrossPlatformInputManager.GetAxis("Horizontal") + InputManager.variableJoystick.Horizontal;
            v = CrossPlatformInputManager.GetAxis("Vertical") + InputManager.variableJoystick.Vertical;
            //h = CrossPlatformInputManager.GetAxis("Horizontal");
            //v = CrossPlatformInputManager.GetAxis("Vertical");
            //h = InputManager.variableJoystick.Horizontal;
            //v = InputManager.variableJoystick.Vertical;
        }

        //float h =  InputManager.variableJoystick.Horizontal;
        //float v =  InputManager.variableJoystick.Vertical;
        //float h = CrossPlatformInputManager.GetAxis("Horizontal");
        //float v = CrossPlatformInputManager.GetAxis("Vertical");
        bool crouch = Input.GetKey(KeyCode.C);

        // calculate move direction to pass to character
        if (m_Cam != null)
        {
            // calculate camera relative direction to move:
            m_CamForward = Vector3.Scale(m_Cam.forward, new Vector3(1, 0, 1)).normalized;
            m_Move = v * m_CamForward + h * m_Cam.right;
        }
        else
        {
            // we use world-relative directions in the case of no main camera
            m_Move = v * Vector3.forward + h * Vector3.right;
        }
#if !MOBILE_INPUT
        // walk speed multiplier
        if (Input.GetKey(KeyCode.LeftShift)) m_Move *= 0.5f;
#endif

        // pass all parameters to the character control script
        m_Character.Move(m_Move, crouch, m_Jump);
        m_Jump = false;
    }

    private void OnApplicationQuit()
    {
        Security.catched -= IsCatchedBySecurity;

    }
    private void OnDestroy()
    {
        Security.catched -= IsCatchedBySecurity;
    }
}

