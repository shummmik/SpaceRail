using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class SetPositionBall : MonoBehaviour
{
    [SerializeField] private float deltaOffset = 0.01f;
    [SerializeField] private GameObject camera;
    [SerializeField] private GameObject ball;
    [SerializeField] 
    void Update()
    {
        ball.transform.rotation = camera.transform.rotation;
    }

    public void Up()
    {
        transformDirection(Vector3.up);
    }
    
    public void Left()
    {
        transformDirection(Vector3.left);
    }
    
    public void Right()
    {
        transformDirection( Vector3.right);
    }
    
    public void Down()
    {
        transformDirection(Vector3.down);
    }

    private void transformDirection(Vector3 direction)
    {
        ball.transform.position +=  ball.transform.rotation * direction * deltaOffset;
        ObjectsManager.Instance.startPosition = ball.transform.position;
    }
}
