using UnityEngine;

namespace HelicopterPhysics.Cameras
{

    public class HeliFollowCamera : MonoBehaviour, IHeliCamera
    {

        [Header("Camera Properties")]
        public Rigidbody Rb;
        public float Height = 2f;
        public float Distance = 2f;


        public void FixedUpdate()
        {
            if (Rb)
            {
                HandleCamera();
            }
        }

        public void HandleCamera()
        {
            Debug.Log("Handling camera ..");
        }
    }
}
