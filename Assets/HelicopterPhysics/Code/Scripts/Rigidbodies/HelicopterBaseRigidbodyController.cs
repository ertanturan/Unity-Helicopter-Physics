using UnityEngine;

namespace HelicopterPhysics.Gameplay
{
    public class HelicopterBaseRigidbodyController : MonoBehaviour
    {
        protected Rigidbody Rb;

        private void Awake()
        {
            Rb = GetComponent<Rigidbody>();
        }

        private void FixedUpdate()
        {
            if (Rb)
            {
                HandlePhysics();
            }
        }

        protected virtual void HandlePhysics()
        {

        }
    }

}
