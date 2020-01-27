using HelicopterPhysics.Inputs;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace HelicopterPhysics.Mechanics.Rotors
{
    public class HeliRotorController : MonoBehaviour
    {
        private List<IHeliRotor> _rotors;

        private void Awake()
        {
            _rotors = GetComponentsInChildren<IHeliRotor>().ToList();
        }

        public void UpdateRotors(InputController inputController, float currentRPMs)
        {
            //Debug.Log(currentRPMs);
            float dps = (currentRPMs * 360f) / 60f;
            for (int i = 0; i < _rotors.Count; i++)
            {
                _rotors[i].UpdateRotor(dps, inputController);
            }
        }
    }

}
