using UnityEngine;

namespace HelicopterPhysics.Cameras
{

    public class HeliFollowCamera : BaseHeliCamera
    {

        [Header("Follow Camera Properties")]
        public float Height = 2f;
        public float Distance = 2f;
        public float CameraSpeed = 2f;

        public override void HandleCamera()
        {

            Vector3 flatforward = -Rb.transform.forward;
            flatforward.y = 0f;
            flatforward = flatforward.normalized;

            TargetPos = Rb.position + (flatforward * Distance) + (Vector3.up * Height);

            transform.position = Vector3.SmoothDamp(transform.position, TargetPos,
                ref CurrentVelocity, CameraSpeed);

            transform.LookAt(LookatTarget);

        }
    }
}
