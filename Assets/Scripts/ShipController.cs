using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float maxSpeed;
    public float drag;
    public float rotationalDrag;
    public float rotSpeed = 250;
    [Range(1, 20)] public float damping;

    private Rigidbody2D rb2D;

    [HideInInspector] public Transform position;

    float horizontalInput;
    float verticalInput = -1;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        rb2D.drag = drag;

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        Vector2 horizontal = transform.up * (verticalInput * maxSpeed);
        rb2D.AddForce(horizontal);

        Vector2 vertical = transform.right * (horizontalInput * maxSpeed);
        rb2D.AddForce(vertical);

        if (Input.GetKey(KeyCode.Space))
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            difference.Normalize();
            float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            var desiredRotQ = Quaternion.Euler(0f, 0f, rotation_z - 90);
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotQ, Time.deltaTime * damping);
        }
    }
}
