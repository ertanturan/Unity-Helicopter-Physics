using UnityEngine;

namespace HelicopterPhysics.Inputs
{
    public class BaseHeliInput : MonoBehaviour
    {

        public float Vertical
        {
            get { return _Vertical; }
        }
        public float Horizontal
        {
            get { return _Horizontal; }
        }


        private float _Vertical = 0f;
        private float _Horizontal = 0f;



        private void Update()
        {
            HandleInput();
        }

        protected virtual void HandleInput()
        {
            _Vertical = Input.GetAxis("Vertical");
            _Horizontal = Input.GetAxis("Horizontal");
        }
    }
}

