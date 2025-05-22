using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
//using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class Camera : MonoBehaviour
{

    public Transform targetObject;
    public Vector3 origonPos;//原始的位置
    public float rotateSpeed;
    private float yaw;//设置俯仰变量
    private float pitch;//设置偏转变量
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
        Quaternion rotation = Quaternion.Euler(pitch ,yaw, 0);//计算四源欧拉角的数值
        float distance = Vector3.Distance(origonPos, transform.position);//计算相对位置的距离
        transform.position = origonPos + rotation * Vector3.forward *distance;//根据四源欧拉角计算相对位置并设置相机的位置
        transform.LookAt(origonPos);//设置朝向
        //transform.rotation = rotation;  
    }
    private float NormliezdAngle(float angle)
    {
        angle %= 360;
        if (angle > 180)
            angle -= 360;
        return angle;
    }
   
}
