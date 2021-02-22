/**
 * This script catches the touch movements and rotates the
 * inspected item or the character in the inventory / menu view.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateInspectedItem : MonoBehaviour
{
    public bool inGame = false;
    public bool inspecting = false;

    private bool rotating = false;
    float rotationSpeed = 250;

    public Transform itemContainer;
    public Transform playerModel;

    void Update() {

        // Rotate character in Inventory menu
        if (Input.touchCount > 0) {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Moved) {
                if (rotating && !inGame && !inspecting) {
                    playerModel.Rotate(new Vector3(0, -(touch.deltaPosition.x / Screen.width) * rotationSpeed, 0));
                } else if(rotating && inspecting) {
                    itemContainer.Rotate(new Vector3(0, -(touch.deltaPosition.x / Screen.width) * rotationSpeed, 0));
                }
            }


            float xPositionPercentage = touch.position.x / Screen.width;
            float yPositionPercentage = touch.position.y / Screen.height;

            if (touch.phase == TouchPhase.Ended) rotating = false;
            else if (touch.phase == TouchPhase.Began) {
                if (!inGame && !inspecting && xPositionPercentage > .11f && xPositionPercentage < .44f) rotating = true;
                if (inspecting && yPositionPercentage > .3f && yPositionPercentage < .85f) rotating = true;
            }
        }
    }
}
