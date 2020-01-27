using HelicopterPhysics.Inputs;
using HelicopterPhysics.Mechanics.Rotors;
using HelicopterPhysics.Physics;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HelicopterPhysics.Gameplay
{
    [RequireComponent(typeof(InputController))]
    public class HeliController : HelicopterBaseRigidbodyController
    {
        [Header("Helicopter Properties")]
        private InputController _inputController;
        private List<HeliEngine> _engines = new List<HeliEngine>();
        [Header("Helicopter Rotors")]
        private HeliRotorController _rotorController;

        protected override void Awake()
        {
            base.Awake();
            _inputController = GetComponent<InputController>();
            _rotorController = GetComponentInChildren<HeliRotorController>();
            _engines = GetComponentsInChildren<HeliEngine>().ToList();
        }

        protected override void HandlePhysics()
        {
            base.HandlePhysics();
            if (_inputController)
            {
                HandleEngines();
                HandleRotors();
                HandleChracteristics();
            }
        }

        protected virtual void HandleEngines()
        {
            for (int i = 0; i < _engines.Count; i++)
            {
                _engines[i].UpdateEngine(_inputController.CurrentInput.StickyThrottle);
                float finalPower = _engines[i].CurrentHP;
            }
        }

        protected virtual void HandleChracteristics()
        {

        }


        protected virtual void HandleRotors()
        {
            if (_rotorController && _engines.Count > 0)
            {
                _rotorController.UpdateRotors(_inputController, _engines[0].CurrentRPM);
            }
        }
    }

}
