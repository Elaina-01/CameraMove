using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
//using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;

public class Camera : MonoBehaviour
{

    public Transform targetObject;
    public Vector3 origonPos;//ԭʼ��λ��
    public float rotateSpeed;
    private float yaw;//���ø�������
    private float pitch;//����ƫת����
    // Start is called before the first frame update
    void Start()
    {
        if (targetObject != null)
        {
            origonPos = targetObject.position;
        }
        Vector3 relativePos = transform.position - origonPos;//�������λ��
        Quaternion rotation = Quaternion.LookRotation(relativePos);//��ʼ������ĳ���λ��
        yaw = rotation.eulerAngles.y;//��ȡ�����Ƕ�
        pitch = rotation.eulerAngles.x;//��ȡƫת�Ƕ�
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
            float mouseX = Input.GetAxis("Mouse X");//��ȡ����ƫ����
            float mouseY = Input.GetAxis("Mouse Y");

            yaw += mouseX * rotateSpeed;//��������ƫ�������ýǶ�
            pitch -= mouseY * rotateSpeed;

            pitch = Mathf.Clamp(pitch, -85, 85);//���ƽǶȵķ�Χ

        }
        Quaternion rotation = Quaternion.Euler(pitch ,yaw, 0);//������Դŷ���ǵ���ֵ
        float distance = Vector3.Distance(origonPos, transform.position);//�������λ�õľ���
        transform.position = origonPos + rotation * Vector3.forward *distance;//������Դŷ���Ǽ������λ�ò����������λ��
        transform.LookAt(origonPos);//���ó���
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
