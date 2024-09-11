using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private float radius;
    [SerializeField] private float height;
    [SerializeField] private float speed;

    private void Update()
    {
        var positionFactor = Time.time * speed;
        transform.position = new Vector3
        {
            x = Mathf.Cos(positionFactor) * radius,
            y = height,
            z = Mathf.Sin(positionFactor) * radius
        };
        transform.LookAt(Vector3.zero, Vector3.up);
    }
}