using HelicopterPhysics.Inputs;
using HelicopterPhysics.Physics;
using UnityEngine;

namespace HelicopterPhysics.Gameplay
{
    [RequireComponent(typeof(InputController))]
    public class HeliController : HelicopterBaseRigidbodyController
    {
        private InputController _inputController;

        private void Awake()
        {
            _inputController = GetComponent<InputController>();
        }

        protected override void HandlePhysics()
        {
            base.HandlePhysics();
            if (_inputController)
            {
                HandleEngines();
                HandleChracteristics();
            }
        }

        protected virtual void HandleEngines()
        {

        }

        protected virtual void HandleChracteristics()
        {

        }

    }

}
