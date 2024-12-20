using UnityEngine;

public class ObstacleMovement : MonoBehaviour
{
    [SerializeField] Transform PointA;
    [SerializeField] Transform PointB;
    [SerializeField] float speed = 2f;
    private bool movingToPointB = true;

    void Update()
    {
        if (movingToPointB)
        {
            transform.position = Vector3.MoveTowards(transform.position, PointB.position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, PointB.position) < 0.1f)
                movingToPointB = false;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, PointA.position, speed * Time.deltaTime);
            if (Vector3.Distance(transform.position, PointA.position) < 0.1f)
                movingToPointB = true;
        }
    }
}
