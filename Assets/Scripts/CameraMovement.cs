using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    private Vector3 velocity = Vector3.zero;
    public Vector3 offset;
    [Range(0, 1)] public float viewSize = 5;
    [Range(0, 1)] public float movementDamping = 0.15f;
    [Range(1, 20)] public float rotationDamping = 5;

    GameObject player;
    void Start()
    {

        player = FindObjectOfType<ShipController>().gameObject;
    }

    void FixedUpdate()
    {
        var distance = player.GetComponent<Rigidbody2D>().velocity.magnitude * 0.1f;
        Camera.main.orthographicSize = viewSize + distance;

        Vector3 point = GetComponent<Camera>().WorldToViewportPoint(player.transform.position);
        Vector3 delta = player.transform.position - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
        Vector3 destination = new Vector3(player.transform.position.x + offset.x, player.transform.position.y + offset.y, offset.z - 10) + delta;
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, movementDamping);

        Vector3 difference = Camera.main.ScreenToWorldPoint(player.transform.position) - transform.position;
        difference.Normalize();
        float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        var desiredRotQ = player.transform.rotation;
        transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotQ, Time.deltaTime * rotationDamping);
    }
}
