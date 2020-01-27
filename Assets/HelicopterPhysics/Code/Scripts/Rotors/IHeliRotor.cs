using HelicopterPhysics.Inputs;
namespace HelicopterPhysics.Mechanics.Rotors
{
    public interface IHeliRotor
    {
        void UpdateRotor(float dps, InputController inputController);
    }

}