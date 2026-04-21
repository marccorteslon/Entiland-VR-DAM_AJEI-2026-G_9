using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace EntilandVR.DosSeis.DAM_VIOD.G_Nueve
{
    public class SC_Tank : MonoBehaviour
    {

        [Header("Stats")]
        public float speed;
        public float turn_speed;
        public Vector3 right_wheel_offset, left_wheel_offset;
        [Range(0, 1)]public float damping = 0.99f;
        public float shoot_force = 10;

        public float right_input = 0, left_input = 0;
        private Vector2 input;
        private Rigidbody rb;

        [Header("Camera")]
        public Transform targetCamera;
        public float distance_traveled = 0.5f;
        private Coroutine routine_camera;
        private Vector3 pos_camera_start;

        [Header("Particles")]
        public ParticleSystem parts_shoot;

        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            rb = GetComponent<Rigidbody>();

            pos_camera_start = targetCamera.transform.localPosition;

            right_wheel_offset += transform.position;
            left_wheel_offset += transform.position;
        }

        // Update is called once per frame
        void FixedUpdate()
        {
            TankVelocity();
        }

        private void TankVelocity()
        {
            Vector3 right_vel = transform.forward * speed * right_input;
            Vector3 left_vel = transform.forward * speed * left_input;

            Vector3 apply_vel = new Vector3(right_vel.x + left_vel.x, 0, right_vel.z + right_vel.z);

            float turn = right_input - left_input;


            rb.AddTorque(Vector3.up * turn * turn_speed, ForceMode.Acceleration);

            rb.AddForce(apply_vel * speed, ForceMode.Acceleration);


            // Clamp the velocity
            //rb.linearVelocity += Physics.gravity * 0.1f;
            rb.linearVelocity = ClampSpeed(rb.linearVelocity);
            rb.angularVelocity = ClampSpeed(rb.angularVelocity);

            rb.linearVelocity = new Vector3(rb.linearVelocity.x * damping, rb.linearVelocity.y, rb.linearVelocity.z * damping);
            rb.angularVelocity *= damping;
        }

        private Vector3 ClampSpeed(Vector3 vel)
        {
            vel.x = Mathf.Clamp(vel.x, -10, 10);
            vel.y = Mathf.Clamp(vel.y, -20, 5);
            vel.z = Mathf.Clamp(vel.z, -10, 10);

            return vel;
        }
        
        public void ProcessInput(InputAction.CallbackContext con)
        {
            if (con.performed)
            {
                input = con.ReadValue<Vector2>();

                if (input.x == 0)
                {
                    right_input = input.y;
                    left_input = input.y;
                }
                else if (input.x > 0)
                {
                    right_input = 1;
                    left_input = 0;
                }
                else if (input.x < 0)
                {
                    right_input = 0;
                    left_input = 1;
                }
                
            }
            else if (con.canceled)
            {
                right_input = 0;
                left_input = 0;
            }
        }

        public void Shoot()
        {
            // Si la rutina se está ejecutando no la ejecutará otra vez
            if (routine_camera != null) return;
            routine_camera = StartCoroutine(ShootCameraRoutine());

            parts_shoot.Play();
        }
        private IEnumerator ShootCameraRoutine()
        {
            float duration = 1;
            float timer = 0;

            Vector3 pos_end = targetCamera.transform.localPosition - transform.InverseTransformDirection(transform.forward) * distance_traveled;

            while (timer < duration * 0.1f)
            {
                float t = timer / duration;

                targetCamera.transform.localPosition = Vector3.Lerp(pos_camera_start, pos_end, t);


                timer += Time.deltaTime;
                yield return null;
            }

            pos_end = targetCamera.localPosition;

            timer = 0;
            while (timer < duration)
            {
                float t = timer / duration;
                t = Mathf.SmoothStep(0, 1, t);
                targetCamera.transform.localPosition = Vector3.Lerp(pos_end, pos_camera_start, t);

                timer += Time.deltaTime;
                yield return null;
            }

            // Retaura la posición de la cámara
            targetCamera.transform.localPosition = pos_camera_start;

            // Permite que se vuelva a ejecutar la rutina
            routine_camera = null;
        }
    
        public void ProcessRightInput(InputAction.CallbackContext con)
        {
            if (con.performed)
            {
                input.x = con.ReadValue<float>();
            }
        }
        public void ProcessLeftInput(InputAction.CallbackContext con)
        {
            if (con.performed)
            {
                input.y = con.ReadValue<float>();
            }
        }
    }
}