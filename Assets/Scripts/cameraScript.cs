using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraScript : MonoBehaviour
{

    private Vector3 mouseStartPos;
    private Quaternion initialCameraAngle;
    private float onclickCameraAngleX;
    private float onclickCameraAngleY;
    private float mouseSensitivity = 100f;

    // Start is called before the first frame update
    void Start()
    {
        initialCameraAngle = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        RotateCamera();
    }

    void RotateCamera()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mouseStartPos = Input.mousePosition;

            onclickCameraAngleX = transform.eulerAngles.x;
            onclickCameraAngleY = transform.eulerAngles.y;
        }

        if (Input.GetMouseButton(0))
        {
            float diffAngleX = (mouseStartPos.x - Input.mousePosition.x)/Screen.width;
            float diffAngleY = (mouseStartPos.y - Input.mousePosition.y)/Screen.height;

            //Vector3 angle = new Vector3(onclickCameraAngleX + diffAngleY, onclickCameraAngleY + diffAngleX, 0) * mouseSensitivity;

            float eulerX = onclickCameraAngleX - diffAngleY * mouseSensitivity;
            float eulerY = onclickCameraAngleY + diffAngleX * mouseSensitivity;

            transform.rotation = Quaternion.Euler(eulerX,eulerY,0);
        }
    }
}
