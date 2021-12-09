using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    Vector3 playerPosition;
    Transform player;
    public Vector3 offset;
    private Vector3 velocity = Vector3.zero;
    public float movementDamping = 0.15f;
    [Range(1, 20)] public float rotationDamping = 5f;

    void Update()
    {
        playerPosition = FindObjectOfType<ShipController>().transform.position;
        player = FindObjectOfType<ShipController>().transform;
    }

    void FixedUpdate()
    {
        Vector3 point = GetComponent<Camera>().WorldToViewportPoint(playerPosition);
        Vector3 delta = playerPosition - GetComponent<Camera>().ViewportToWorldPoint(new Vector3(0.5f, 0.5f, point.z));
        Vector3 destination = new Vector3(playerPosition.x + offset.x, playerPosition.y + offset.y, offset.z) + delta;
        transform.position = Vector3.SmoothDamp(transform.position, destination, ref velocity, movementDamping);

        // Vector3 difference = Camera.main.ScreenToWorldPoint(playerPosition) - transform.position;
        // difference.Normalize();
        // float rotation_z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        // var desiredRotQ = player.transform.rotation;
        // transform.rotation = Quaternion.Lerp(transform.rotation, desiredRotQ, Time.deltaTime * rotationDamping);
    }
}
