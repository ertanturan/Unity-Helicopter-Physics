using HelicopterPhysics.Gameplay;
using UnityEditor;
using UnityEngine;

namespace HelicopterPhysics.Editorial
{
    public class HelicopterMenu
    {
        [MenuItem("Helicopter Physics/Vehicles/Setup New Helicopter")]
        public static void BuildNewHelicopter()
        {
            //Create a new helicopter setup
            GameObject currentHeli = new GameObject("New Helicopter", typeof(HeliController));
            Selection.activeGameObject = currentHeli;


            //COG : Center of Gravity
            //Create the COG object for the helicopter
            GameObject curCOG = new GameObject("COG");
            curCOG.transform.SetParent(currentHeli.transform);

            HeliController currentController = currentHeli.GetComponent<HeliController>();
            //Assign the COG to the current helicopter
            currentController.COG = curCOG.transform;

            //Create Groups
            GameObject audioGroup = new GameObject("Audio Group");
            GameObject graphicsGroup = new GameObject("Graphics Group");
            GameObject collisionGroup = new GameObject("Collision Group");
            GameObject rotorsGroup = new GameObject("Rotors Group");

            audioGroup.transform.SetParent(currentHeli.transform);
            graphicsGroup.transform.SetParent(currentHeli.transform);
            collisionGroup.transform.SetParent(currentHeli.transform);
            rotorsGroup.transform.SetParent(currentHeli.transform);


        }

    }

}

