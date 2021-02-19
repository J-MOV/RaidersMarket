using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCharacter : MonoBehaviour

{

    private bool rotating = false;
    float rotationSpeed = 170;

    void Update()
    {

        // Rotate character in Inventory menu
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved) {
                if (rotating) {
                    transform.Rotate(new Vector3(0, -(touch.deltaPosition.x / Screen.width) * rotationSpeed, 0));
                }
            }


            float xPositionPercentage = touch.position.x / Screen.width;

            if (touch.phase == TouchPhase.Began && xPositionPercentage > .11f && xPositionPercentage < .44f) rotating = true;
            else if (touch.phase == TouchPhase.Ended) rotating = false;



            Debug.Log(touch.deltaPosition);
        }
    }
}
