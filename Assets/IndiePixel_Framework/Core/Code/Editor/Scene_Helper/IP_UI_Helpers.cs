using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace IndiePixel.Core
{
    public static class IP_UI_Helpers 
    {
        public static void CreateUIGroup()
        {
            GameObject selectedGO = IP_Editor_Utils.GetSelectedObject();
            GameObject uiGrp = (GameObject)AssetDatabase.LoadAssetAtPath("Assets/IndiePixel_Framework/UI/Prefabs/UI_GRP.prefab", typeof(GameObject));
            if(uiGrp)
            {
                GameObject curUIGrp = GameObject.Instantiate(uiGrp);
                curUIGrp.name = "UI_GRP";

                if(selectedGO)
                {
                    curUIGrp.transform.SetParent(selectedGO.transform);
                }
            }
            else
            {
                IP_Editor_Utils.DisplayDialogBox("Unable to Find the UI_GRP Prefab!");
            }
        }
    }
}
