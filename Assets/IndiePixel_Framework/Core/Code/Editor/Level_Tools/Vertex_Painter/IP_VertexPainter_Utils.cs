using UnityEngine;
using UnityEditor;
using System.Collections;

namespace IndiePixel.Core
{
    public class IP_VertexPainter_Utils 
    {

        #region Selection Methods
        public static GameObject GetSelection()
        {
            //Lets see if we have anything Selected
            GameObject curSelected = Selection.activeGameObject;
            if(!curSelected)
            {
                EditorUtility.DisplayDialog("Mesh Painter", "Please select an object in the scene!", "OK");
                return null;
            }
            return curSelected;
        }
        #endregion




        #region Mesh Methods
        public static Mesh GetMesh(GameObject aGO)
        {
            Mesh curMesh = null;
            if(aGO)
            {
                MeshFilter curFilter = aGO.GetComponent<MeshFilter>();
                SkinnedMeshRenderer curSkinned = aGO.GetComponent<SkinnedMeshRenderer>();
                if(curFilter && !curSkinned)
                {
                    curMesh = curFilter.sharedMesh;
                }

                if(!curFilter && curSkinned)
                {
                    curMesh = curSkinned.sharedMesh;
                }
            }
            return curMesh;
        }



        public static void SaveMesh(GameObject aGO)
        {
            //Check the GO first
            if(!aGO)
            {
                return;
            }

            //Lets see if there is a mesh on this object
            Mesh curMesh = GetMesh(aGO);
            if(!curMesh)
            {
                EditorUtility.DisplayDialog("Mesh Painter", "Please select an object with a Mesh Attached!", "OK");
                return;
            }

            //Let the user select a path and name for this new mesh
            string filePath = EditorUtility.SaveFilePanel("Save Mesh", Application.dataPath, curMesh.name + "_VersionNumber", "asset");
            string finalPath = "";

            //If we have the path then lets start Saving
            if(!string.IsNullOrEmpty(filePath))
            {
                //Get the relative Path to the Unity Project
                finalPath = filePath.Substring(0, filePath.LastIndexOf(".")).Trim() + ".asset";
                finalPath = "Assets" + finalPath.Remove(0, Application.dataPath.Length);
                //Debug.Log(finalPath);

                Mesh FinalMesh = CloneMesh(curMesh, finalPath);
                SetMesh(aGO, FinalMesh);
            }
        }



        public static Mesh CloneMesh(Mesh aMesh, string aPath)
        {
            Mesh finalMesh = null;
            if(aMesh)
            {
                finalMesh = new Mesh();
                finalMesh.name = aMesh.name;

                finalMesh.vertices = aMesh.vertices;
                finalMesh.triangles = aMesh.triangles;
                finalMesh.colors = aMesh.colors;
                finalMesh.normals = aMesh.normals;
                finalMesh.tangents = aMesh.tangents;

                finalMesh.uv = aMesh.uv;
                finalMesh.uv2 = aMesh.uv2;
                finalMesh.uv3 = aMesh.uv3;
                finalMesh.uv4 = aMesh.uv4;

                finalMesh.boneWeights = aMesh.boneWeights;
                finalMesh.subMeshCount = aMesh.subMeshCount;
                finalMesh.bindposes = aMesh.bindposes;

                AssetDatabase.CreateAsset(finalMesh, aPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            return finalMesh;
        }



        public static void SetMesh(GameObject aGO, Mesh aMesh)
        {
            if(aGO && aMesh)
            {
                MeshFilter aFilter = aGO.GetComponent<MeshFilter>();
                SkinnedMeshRenderer aSkinnedFilter = aGO.GetComponent<SkinnedMeshRenderer>();
                if(aFilter && !aSkinnedFilter)
                {
                    aFilter.sharedMesh = aMesh;
                }

                if(!aFilter && aSkinnedFilter)
                {
                    aSkinnedFilter.sharedMesh = aMesh;
                }

                MeshCollider aCollider = aGO.GetComponent<MeshCollider>();
                if(aCollider)
                {
                    aCollider.sharedMesh = aMesh;
                }
            }
        }



        public static void AddMeshCollider(GameObject aGO)
        {
            if(aGO)
            {
                MeshCollider foundMCollider = aGO.GetComponent<MeshCollider>();
                if(foundMCollider)
                {
                    EditorUtility.DisplayDialog("Mesh Painter", "There is already a Mesh Collider attached to this object", "OK");
                    return;
                }

                //Add the Mesh Collider
                aGO.AddComponent<MeshCollider>();
            }
        }
        #endregion




        #region Main Actions Methods
        public static void HandleBrush(GameObject aGO, Mesh aMesh, Vector3 brushPos, IP_BrushData brushData, IP_VTXPainterState aState)
        {
            if(aGO && aMesh)
            {

                Vector3[] verts = aMesh.vertices;
                Vector3[] normals = aMesh.normals;


                //Check to see if the Object has colors
                Color[] colors = new Color[aMesh.vertexCount];
                if(aMesh.colors.Length > 0)
                {
                    colors = aMesh.colors;
                }
                else
                {
                    for(int i = 0; i < aMesh.vertexCount; i++)
                    {
                        colors[i] = Color.black;
                    }
                }


                //process the brush action
                for(int i = 0; i < verts.Length; i++)
                {
                    float mag = (aGO.transform.TransformPoint(verts[i]) - brushPos).magnitude;
                    if(mag > brushData.brushSize)
                    {
                        continue;
                    }

                    //Get Falloff
                    float curFalloff = 1f;
                    if(mag > brushData.falloffSize)
                    {
                        curFalloff = (brushData.brushSize / mag) * 0.1f;
                    }

                    switch(aState)
                    {
                        case IP_VTXPainterState.Sculpting:
                            verts[i] = SculptMesh(verts[i], normals[i], brushData, curFalloff);
                            break;

                        case IP_VTXPainterState.Painting:
                            colors[i] = PaintMesh(colors[i], brushData, curFalloff);
                            break;

                        default:
                            break;
                    }

                    //Draw Some Handles
                    Handles.color = Color.yellow;

                }


                //Handle any clean up we need
                switch(aState)
                {
                    case IP_VTXPainterState.Sculpting:
                        aMesh.vertices = verts;
                        aMesh.RecalculateBounds();
                        aMesh.RecalculateNormals();
                        break;

                    case IP_VTXPainterState.Painting:
                        aMesh.colors = colors;
                        break;

                    default:
                        break;

                }
            }
               
        }




        public static Vector3 SculptMesh(Vector3 aVert, Vector3 aNormal, IP_BrushData brushData, float aFalloff)
        {
            switch(brushData.sculptState)
            {
                case IP_SculptState.NormalDir:
                    //Normal direction displacement
                    aVert += (aNormal * (brushData.opacity * brushData.sculptIntensity) * 0.1f * aFalloff) * (float)brushData.sculptDir;
                    break;

                case IP_SculptState.WorldUpDir:
                    aVert += (Vector3.up * (brushData.opacity * brushData.sculptIntensity) * 0.1f * aFalloff) * (float)brushData.sculptDir;
                    break;

                case IP_SculptState.Flatten:
                    aVert = new Vector3(aVert.x, brushData.startHeight, aVert.z);
                    break;

                default:
                    break;
            }
            return aVert;
        }




        public static Color PaintMesh(Color aColor, IP_BrushData brushData, float aFalloff)
        {
            aColor = Color.Lerp(aColor, brushData.brushColor, (brushData.opacity * aFalloff) * 0.1f);
            return aColor;
        }




        public static void FillMeshWithColor(Mesh aMesh, Color aColor)
        {
            if(!aMesh)
            {
                EditorUtility.DisplayDialog("Mesh Painter", "Please select an object with a Mesh Attached!", "OK");
                return;
            }

            Color[] colors = new Color[aMesh.vertexCount];
            for(int i = 0; i < aMesh.vertexCount; i++)
            {
                colors[i] = aColor;
            }

            aMesh.colors = colors;

        }
        #endregion
    }
}
