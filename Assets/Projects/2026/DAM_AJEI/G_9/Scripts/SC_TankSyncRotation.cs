using UnityEngine;

public class SC_TankSyncRotation : MonoBehaviour
{
    public Transform tank_transform;
    public float rotation_speed = 5;

    void Update()
    {
        transform.rotation = Quaternion.Slerp(transform.rotation, tank_transform.rotation, Time.deltaTime * rotation_speed);
    }
}