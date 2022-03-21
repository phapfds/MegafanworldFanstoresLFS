using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class FirstPersonLook : MonoBehaviour
{
    [SerializeField]
    Transform character;
    public float sensitivity = 2;
    public float smoothing = 1.5f;

    Vector2 velocity;
    public Vector2 frameVelocity;


    void Reset()
    {
        // Get the character from the FirstPersonMovement in parents.
        character = GetComponentInParent<FirstPersonMovement>().transform;
    }

    public void Start()
    {
        // Lock the mouse cursor to the game screen.
        //Cursor.lockState = CursorLockMode.Locked;
        //Vector2 mouseDelta = new Vector2(1920/2, 1080/2);
        //Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * sensitivity);
        //velocity += frameVelocity;
        //velocity.y = Mathf.Clamp(velocity.y, -90, 90);
        //transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.AngleAxis(-velocity.y, Vector3.right), 0.1f);
        //character.localRotation = Quaternion.Lerp(character.localRotation, Quaternion.AngleAxis(velocity.x, Vector3.up), 0.1f);
        //transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);
        //character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
    }


    

    void Update()
    {
        // Get smooth velocity.


        // Rotate camera up-down and controller left-right from velocity.
        if (Input.GetMouseButton(0) && !DragObject.isDrag && !EventSystem.current.IsPointerOverGameObject())
        {
            Vector2 mouseDelta = new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y"));
            Vector2 rawFrameVelocity = Vector2.Scale(mouseDelta, Vector2.one * sensitivity);
            frameVelocity = Vector2.Lerp(frameVelocity, rawFrameVelocity, 1 / smoothing);
            velocity += frameVelocity;
            velocity.y = Mathf.Clamp(velocity.y, -90, 90);
            //transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.AngleAxis(-velocity.y, Vector3.right), 0.1f);
            //character.localRotation = Quaternion.Lerp(character.localRotation, Quaternion.AngleAxis(velocity.x, Vector3.up), 0.1f);
            transform.localRotation = Quaternion.AngleAxis(-velocity.y, Vector3.right);
            character.localRotation = Quaternion.AngleAxis(velocity.x, Vector3.up);
        }

    }
}
