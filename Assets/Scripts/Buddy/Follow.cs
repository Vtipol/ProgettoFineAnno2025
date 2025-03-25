using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;
    public float speed = 5f;
    public float stopDistance;

    void Update()
    {
       stopDistance = Vector2.Distance(transform.position, target.transform.position);

        if (stopDistance >= 1) 
        {
            transform.position = Vector3.MoveTowards(transform.position, target.position, speed * Time.deltaTime);
        }
    }
}
