using UnityEngine;

public class ShipController : MonoBehaviour
{
    public float maxSpeedY;
    public float maxSpeedX;
    public float drag;
    public float rotationalDrag;
    public float rotSpeed = 250;
    [Range(1, 20)] public float damping;

    [HideInInspector] public Rigidbody2D thisRigidbody2D;
    [HideInInspector] public Transform position;

    GameObject texture;

    float horizontalInput;
    float verticalInput = -1;

    void Start()
    {
        thisRigidbody2D = GetComponent<Rigidbody2D>();
        texture = GameObject.Find("Texture");
    }

    // Update is called once per frame
    void Update()
    {
        thisRigidbody2D.drag = drag;

        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    void FixedUpdate()
    {
        Vector2 horizontal = transform.up * (verticalInput * maxSpeedY);
        thisRigidbody2D.AddForce(horizontal);

        Vector2 vertical = transform.right * (horizontalInput * maxSpeedX);
        thisRigidbody2D.AddForce(vertical);

        if (Input.GetMouseButton(1))
        {
            Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
            difference.Normalize();
            float z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
            var desiredRot = Quaternion.Euler(0f, 0f, z - 90);
            transform.rotation = Quaternion.Lerp(transform.rotation, desiredRot, Time.deltaTime * damping);
        }

        texture.transform.localScale = new Vector3(1, 1 + verticalInput * 0.15f, 1);

        var animTurn = Quaternion.Euler(0f, 0f, -horizontalInput * 20);
        texture.transform.localRotation = Quaternion.Lerp(texture.transform.localRotation, animTurn, Time.deltaTime * damping * 2);
    }
}
