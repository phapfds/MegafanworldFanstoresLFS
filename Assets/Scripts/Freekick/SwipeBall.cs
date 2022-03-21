using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
public class SwipeBall : MonoBehaviour
{
    public static SwipeBall Ins { get; private set; }
    Vector3 startPos, endPos, direction; // touch start position, touch end position, swipe direction
    float touchTimeStart, touchTimeFinish, timeInterval; // to calculate swipe time to sontrol throw force in Z direction
    [SerializeField]
    float throwForceInXandY; // to control throw force in X and Y directions
    [SerializeField]
    float throwForceInZ; // to control throw force in Z direction
    Rigidbody rb;
    bool canAddForce;
    public bool isKicked;
    public bool canKick;
    //[SerializeField] Material material;
    private void Awake()
    {
        Ins = this;
        rb = GetComponent<Rigidbody>();
    }

    //void Update()
    //{
    //    if (Input.GetMouseButtonUp(0) && canAddForce)
    //    {
    //        touchTimeFinish = Time.time;
    //        timeInterval = touchTimeFinish - touchTimeStart;
    //        endPos = Input.mousePosition;
    //        direction = startPos - endPos;
    //        rb.isKinematic = false;
    //        Destroy(gameObject, 3);
    //    }
    //}
    private void OnMouseDown()
    {
        if (!EventSystem.current.IsPointerOverGameObject() && !canAddForce && canKick)
        {
            touchTimeStart = Time.time;
            startPos = Input.mousePosition;
            canAddForce = true;
        }
    }

    private void OnMouseUp()
    {
        if (canAddForce&& canKick)
        {
            touchTimeFinish = Time.time;
            timeInterval = touchTimeFinish - touchTimeStart;
            endPos = Input.mousePosition;
            direction = startPos - endPos;
            rb.isKinematic = false;
            NewKickerController.ins.StartKickAnim();
            isKicked = true;
            canKick = false;
            Destroy(gameObject, 3);
        }
    }

    #region "Change color"
    //private void OnMouseEnter()
    //{
    //    material.color = Color.green;    
    //}
    //private void OnMouseExit()
    //{
    //    material.color = Color.white;
    //}
    #endregion
    public void ApplyForce()
    {
        float force = 0.1f;
        rb.AddForce(-direction.x * throwForceInXandY, direction.y * throwForceInXandY, 2.2f * throwForceInZ / force); //timeInterval
        FKAudioManage.Ins.PlaySound(FKAudioType.kick);
    }

    #region "Mobile"
    //public void Replay()
    //{
    //    //yield return new WaitForSeconds(4);
    //    transform.position = pos;
    //}

    // Update is called once per frame
    //void Update () {

    //// if you touch the screen
    //if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Began) {

    //	// getting touch position and marking time when you touch the screen
    //	touchTimeStart = Time.time;
    //	startPos = Input.GetTouch (0).position;
    //}

    //// if you release your finger
    //if (Input.touchCount > 0 && Input.GetTouch (0).phase == TouchPhase.Ended) {

    //	// marking time when you release it
    //	touchTimeFinish = Time.time;

    //	// calculate swipe time interval 
    //	timeInterval = touchTimeFinish - touchTimeStart;

    //	// getting release finger position
    //	endPos = Input.GetTouch (0).position;

    //	// calculating swipe direction in 2D space
    //	direction = startPos - endPos;

    //	// add force to balls rigidbody in 3D space depending on swipe time, direction and throw forces
    //	rb.isKinematic = false;
    //	rb.AddForce (- direction.x * throwForceInXandY, - direction.y * throwForceInXandY, throwForceInZ / timeInterval);

    //	// Destroy ball in 4 seconds
    //	Destroy (gameObject, 3f);

    //}
    #endregion

}
