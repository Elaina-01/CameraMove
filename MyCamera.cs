using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyCamera : MonoBehaviour
{
    public Transform targetObject;
    private Vector3 origonPos;//原始的位置
    public float rotateSpeed;
    private float yaw;//设置俯仰变量
    private float pitch;//设置偏转变量
    public List<Button> viewButton = new List<Button>();
    private float mouseUp;
    // Start is called before the first frame update
    void Start()
    {
        if (targetObject != null)
        {
            origonPos = targetObject.position;
        }
        Vector3 relativePos = transform.position - origonPos;//计算相对位置
        Quaternion rotation = Quaternion.LookRotation(relativePos);//初始化相机的朝向位置
        yaw = rotation.eulerAngles.y;//获取俯仰角度
        pitch = rotation.eulerAngles.x;//获取偏转角度
        Vector3 euler = rotation.eulerAngles;
        yaw = euler.y;
        pitch = NormliezdAngle(euler.x);
    }

    // Update is called once per frame
    void Update()
    {
        GetMouseAndRotate();
        GetViewUp();
       // viewButton[0].onClick.AddListener(UseViewButtonRight);
    }

    private void GetMouseAndRotate()
    {
        if (Input.GetMouseButton(1))
        {
            float mouseX = Input.GetAxis("Mouse X");//获取鼠标的偏移量
            float mouseY = Input.GetAxis("Mouse Y");

            yaw += mouseX * rotateSpeed;//根据鼠标的偏移量设置角度
            pitch -= mouseY * rotateSpeed;

            pitch = Mathf.Clamp(pitch, -85, 85);//限制角度的范围

        }
        origonPos = targetObject.position;//实时更新相机的位置，使得相机能够跟随目标进行移动
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);//计算四源欧拉角的数值
        float distance = Vector3.Distance(origonPos, transform.position);//计算相对位置的距离
        transform.position = origonPos + rotation * Vector3.forward * distance;//根据四源欧拉角计算相对位置并设置相机的位置
        transform.LookAt(origonPos);//设置朝向
        //transform.rotation = rotation;  
        
    }
    private void GetViewUp()
    {
        mouseUp = Input.GetAxis("Mouse ScrollWheel");
        if (mouseUp != 0)
        {
            if (mouseUp > 0)
            {
                float zoom = mouseUp * 10;//设定缩放的速度
                Camera.main.fieldOfView += zoom;//根据滚轮的滑动方向设置相机的视野
                if (Camera.main.fieldOfView >= 60)
                {
                    Camera.main.fieldOfView = 60;
                }
            }
            if(mouseUp < 0)
            {
                float zoom = mouseUp * 10;
                Camera.main.fieldOfView += zoom;
                if (Camera.main.fieldOfView <=27)
                {
                    Camera.main.fieldOfView = 27;
                }
            }
            
        }
    }
   /* private void UseViewButtonRight()
    {
        if (targetObject != null)
        {
            origonPos = targetObject.position;
            transform.position = targetObject.position + new Vector3(-22.5f, 0, 0);
            transform.LookAt(targetObject.position);
        }
        
    }*/
    private float NormliezdAngle(float angle)
    {
        angle %= 360;
        if (angle > 180)
            angle -= 360;
        return angle;
    }
}
