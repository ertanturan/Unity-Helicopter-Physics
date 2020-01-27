using UnityEngine;

namespace HelicopterPhysics.Physics
{
    public class HeliEngine : MonoBehaviour
    {
        public float MaxHP = 140f;
        public float MaxRPM = 2700f;
        public float PowerDelay = 2f;
        public AnimationCurve PowerCurve =
            new AnimationCurve(new Keyframe(0f, 0f), new Keyframe(1f, 1f));

        private float _currentHp;
        public float CurrentHP
        {
            get { return _currentHp; }
        }

        private float _currentRPM;
        public float CurrentRPM
        {
            get { return _currentRPM; }
        }

        private float _normalizedRPM;
        public float NormalizedRPM
        {
            get { return _normalizedRPM; }
        }

        private void Start()
        {

        }

        public void UpdateEngine(float throttleInput)
        {
            //Calculate horsepower 
            float targetHP = PowerCurve.Evaluate(throttleInput) * MaxHP;
            _currentHp = Mathf.Lerp(_currentHp, targetHP, Time.deltaTime * PowerDelay);
            //calculate RPM
            float targetRPM = PowerCurve.Evaluate(throttleInput) * MaxRPM;
            _currentRPM = Mathf.Lerp(_currentRPM, targetRPM, Time.deltaTime * PowerDelay);
            _normalizedRPM = Mathf.InverseLerp(0f, MaxRPM, _currentRPM);
            //Debug.Log(_currenRPM);
        }


    }
}

