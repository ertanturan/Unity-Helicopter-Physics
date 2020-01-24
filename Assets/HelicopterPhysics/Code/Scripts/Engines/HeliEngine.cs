using UnityEngine;

namespace HelicopterPhysics.Physics
{
    public class HeliEngine : MonoBehaviour
    {
        public float MaxHP = 140f;
        public float MaxRPM = 2700f;
        public float PowerDelay = 2f;


        private float _currentHp;
        public float CurrentHP
        {
            get { return _currentHp; }
        }

        private float _currenRPM;
        public float CurrentRPM
        {
            get { return _currenRPM; }
        }

        private void Start()
        {

        }

        public void UpdateEngine(float throttleInput)
        {



        }

    }
}

