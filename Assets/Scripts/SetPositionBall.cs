using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SetPositionBall : MonoBehaviour
{
    [SerializeField] private GameObject camera;
    [SerializeField] private GameObject ball;
    [SerializeField] 
    void Update()
    {
        ball.transform.localRotation = camera.transform.rotation;
    }

    public void Up()
    {
        //position = Vector3(transform.localPosition.x , transform.localPosition.y + 0.1f, transform.localPosition.z);
        ball.transform.position +=  transform.rotation* Vector3.up  * 0.1f;
    }
    
    public void Left()
    {
        //position = Vector3(transform.localPosition.x , transform.localPosition.y + 0.1f, transform.localPosition.z);
        ball.transform.position +=  transform.rotation* Vector3.left  * 0.1f;
    }
    
    public void Right()
    {
        //position = Vector3(transform.localPosition.x , transform.localPosition.y + 0.1f, transform.localPosition.z);
        ball.transform.position +=  transform.rotation* Vector3.right  * 0.1f;
    }
    
    public void Down()
    {
        //position = Vector3(transform.localPosition.x , transform.localPosition.y + 0.1f, transform.localPosition.z);
        ball.transform.position +=  transform.rotation* Vector3.down  * 0.1f;
    }
}
