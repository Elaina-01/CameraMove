using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MyCamera : MonoBehaviour
{
    public Transform targetObject;
    private Vector3 origonPos;//ԭʼ��λ��
    public float rotateSpeed;
    private float yaw;//���ø�������
    private float pitch;//����ƫת����
    public List<Button> viewButton = new List<Button>();
    private float mouseUp;
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
        GetViewUp();
       // viewButton[0].onClick.AddListener(UseViewButtonRight);
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
        origonPos = targetObject.position;//ʵʱ���������λ�ã�ʹ������ܹ�����Ŀ������ƶ�
        Quaternion rotation = Quaternion.Euler(pitch, yaw, 0);//������Դŷ���ǵ���ֵ
        float distance = Vector3.Distance(origonPos, transform.position);//�������λ�õľ���
        transform.position = origonPos + rotation * Vector3.forward * distance;//������Դŷ���Ǽ������λ�ò����������λ��
        transform.LookAt(origonPos);//���ó���
        //transform.rotation = rotation;  
        
    }
    private void GetViewUp()
    {
        mouseUp = Input.GetAxis("Mouse ScrollWheel");
        if (mouseUp != 0)
        {
            if (mouseUp > 0)
            {
                float zoom = mouseUp * 10;//�趨���ŵ��ٶ�
                Camera.main.fieldOfView += zoom;//���ݹ��ֵĻ������������������Ұ
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
