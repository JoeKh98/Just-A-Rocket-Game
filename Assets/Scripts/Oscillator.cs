using UnityEngine;

public class Oscillator : MonoBehaviour
{

    [SerializeField] Vector3 movementVector;
    [SerializeField] float speed;
    Vector3 startPoint;
    Vector3 endPoint;
    float movementFactor;

    void Start()
    {
        startPoint = transform.position;
        endPoint = startPoint + movementVector; 
    }

    void Update()
    {
        movementFactor = Mathf.PingPong(Time.time * speed, 1f);
        transform.position = Vector3.Lerp(startPoint, endPoint, movementFactor);    
    }
}
