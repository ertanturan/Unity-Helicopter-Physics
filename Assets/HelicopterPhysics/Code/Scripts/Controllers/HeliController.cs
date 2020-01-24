using HelicopterPhysics.Inputs;
using HelicopterPhysics.Physics;
using System.Collections.Generic;
using UnityEngine;

namespace HelicopterPhysics.Gameplay
{
    [RequireComponent(typeof(InputController))]
    public class HeliController : HelicopterBaseRigidbodyController
    {
        [Header("Helicopter Properties")]
        private InputController _inputController;
        public List<HeliEngine> Engines = new List<HeliEngine>();

        protected override void Awake()
        {
            base.Awake();
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
