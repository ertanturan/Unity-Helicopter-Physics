using UnityEngine;

namespace HelicopterPhysics.Cameras
{
    public class AdvancedHeliCamera : BaseHeliCamera
    {
        [Header("Advanced Camera Properties")]
        public float Height = 2f;
        public float MinDistance = 4f;
        public float MaxDistance = 8f;
        public float CatchUpModifier = 5f;


        public override void HandleCamera()
        {
            Debug.Log("Handling camera");
            base.HandleCamera();
            Vector3 dirToTarget = transform.position - Rb.position;
            dirToTarget.y = 0f;
            Vector3 normalizedDir = dirToTarget.normalized;

            float currentMagnitued = dirToTarget.magnitude;

            if (dirToTarget.magnitude < MinDistance)
            {
                float delta = currentMagnitued - MaxDistance;
                TargetPos += normalizedDir * delta * Time.fixedDeltaTime * CatchUpModifier;
            }
            else if (dirToTarget.magnitude > MaxDistance)
            {
                float delta = currentMagnitued - MaxDistance;
                TargetPos -= normalizedDir * delta * Time.fixedDeltaTime * CatchUpModifier;
            }

            TargetPos = Rb.position + dirToTarget + (Vector3.up * Height);
            transform.LookAt(LookatTarget);
        }
    }




}

