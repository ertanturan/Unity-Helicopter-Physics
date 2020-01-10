using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace IndiePixel.Core
{
    #if UNITY_EDITOR
    public static class IP_Camera_Helpers
    {
        
        public static void CreateCameraRig(int aType) 
        {
            switch(aType)
            {
                case 0:
                    InstantiateCamera("FreeFly");
                    break;

                case 1:
                    InstantiateCamera("FPS");
                    break;

                case 2:
                    InstantiateCamera("ThirdPerson");
                    break;

                case 3:
                    InstantiateCamera("TopDown");
                    break;

                default:
                    break;
            }
        }



        static void InstantiateCamera(string aType)
        {

            GameObject cameraGO = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/Emortal_Framework/Cameras/Prefabs/" + aType + "_Camera.prefab", typeof(GameObject));
            if(cameraGO)
            {
                GameObject camera = GameObject.Instantiate(cameraGO);
                camera.name = aType + "_Camera";
            }
            else
            {
                EditorUtility.DisplayDialog("Creation Warning", "Unable to find the " + aType + "_Camera, in any folder!", "OK");
            }
        }

    }
    #endif
}
