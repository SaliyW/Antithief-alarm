using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 5;
    [SerializeField] private float _rotationSpeed = 200;

    private const string Horizontal = "Horizontal";
    private const string Vertical = "Vertical";

    private void Update()
    {
        Rotate();
        Move();
    }

    private void Move()
    {
        float distance = Input.GetAxis(Vertical) * _moveSpeed;

        transform.Translate(distance * Time.deltaTime * Vector3.forward);
    }

    private void Rotate()
    {
        float distance = Input.GetAxis(Horizontal) * _rotationSpeed;

        transform.Rotate(Vector3.up, distance * Time.deltaTime);
    }
}
