using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    [SerializeField] private Transform transformTouchMarker;
    [SerializeField] private GameObject camera;
    [SerializeField] private float deltaOffset = 0.01f;
    private Vector2 targetVector;
    private Touch touch;
    private float distance;
    
    
    void Update()
    {
        ObjectsManager.Instance.transformBall.rotation = camera.transform.rotation;
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            targetVector =  touch.position - (Vector2)transform.position;
            distance = targetVector.magnitude;
            if (distance <= 250)
            {
                transformTouchMarker.position = touch.position;
                targetVector /= 250;
            }
            else if (distance <= 400)
            {
                
                
                transformTouchMarker.position = transform.position + (Vector3)targetVector * 250 / distance;
                targetVector = targetVector/distance;
            }
            else
            {
                targetVector = Vector2.zero;
                transformTouchMarker.position = transform.position;
            }
        }
        else
        {
            targetVector = Vector2.zero;
            transformTouchMarker.position = transform.position;
        }

        transformDirection(targetVector);
    }
    
    private void transformDirection(Vector3 direction)
    {
        ObjectsManager.Instance.transformBall.position +=  ObjectsManager.Instance.transformBall.rotation * direction * deltaOffset * Time.deltaTime;
        ObjectsManager.Instance.startPosition = ObjectsManager.Instance.transformBall.position;
    }
}
