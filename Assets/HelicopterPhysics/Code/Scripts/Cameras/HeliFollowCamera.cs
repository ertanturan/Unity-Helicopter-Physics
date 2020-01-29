using UnityEngine;

namespace HelicopterPhysics.Cameras
{

    public class HeliFollowCamera : MonoBehaviour, IHeliCamera
    {

        [Header("Camera Properties")]
        public Rigidbody Rb;
        public float Height = 2f;
        public float Distance = 2f;
        public float CameraSpeed = 2f;
        private Vector3 _currentVelocity = Vector3.zero;

        public Transform LookatTarget;
        private Vector3 _targetPos;

        public void FixedUpdate()
        {
            if (Rb)
            {
                HandleCamera();
            }
        }

        public void HandleCamera()
        {
            //Debug.Log("Handling camera ..");

            Vector3 flatforward = -Rb.transform.forward;
            flatforward.y = 0f;
            flatforward = flatforward.normalized;

            _targetPos = Rb.position + (flatforward * Distance) + (Vector3.up * Height);

            transform.position = Vector3.SmoothDamp(transform.position, _targetPos,
                ref _currentVelocity, CameraSpeed);

            transform.LookAt(LookatTarget);

        }
    }
}
