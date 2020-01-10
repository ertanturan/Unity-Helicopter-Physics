using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace IndiePixel
{
    public static class IP_Sequence_Menu 
    {
        [MenuItem("Indie Pixel/Sequence/Create Sequence")]
        [MenuItem("GameObject/Indie-Pixel/Create Sequence", false, 11)]
        public static void CreateGameSequence()
        {
            GameObject sequenceGO = new GameObject("New Sequence", typeof(IP_Game_Sequence));
            sequenceGO.transform.position = Vector3.zero;
            Selection.activeGameObject = sequenceGO;
        }
    }
}
