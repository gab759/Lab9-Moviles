using UnityEngine;

public class CameraVerticalFollow : MonoBehaviour
{
    [Header("Seguimiento")]
    [SerializeField] private float upwardSpeed = 2f;
    [SerializeField] private float speedIncreaseAmount = 0.5f;
    [SerializeField] private float increaseInterval = 5f;
    [SerializeField] private float followSmooth = 0.1f;

    private bool isFollowing = true;
    private float timer = 0f;
    private Vector3 velocity = Vector3.zero;

    public void OnEnable()
    {
        ShipMovement.Muerte += StopCamera;
    }
    public void OnDisable()
    {
        ShipMovement.Muerte -= StopCamera;

    }
    void Update()
    {
        if (!isFollowing) return;

        Vector3 newPos = transform.position + Vector3.up * upwardSpeed * Time.deltaTime;

        transform.position = new Vector3(transform.position.x, newPos.y, transform.position.z);

        timer += Time.deltaTime;
        if (timer >= increaseInterval)
        {
            upwardSpeed += speedIncreaseAmount;
            timer = 0f;
        }
    }

    public void StopCamera()
    {
        isFollowing = false;
    }
}
