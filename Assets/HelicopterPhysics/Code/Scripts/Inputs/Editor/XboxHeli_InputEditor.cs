using UnityEditor;
namespace HelicopterPhysics.Inputs.Editorial
{
    [CustomEditor(typeof(XboxHeliInput))]
    public class XboxHeli_InputEditor : Editor
    {

        private XboxHeliInput _target;

        private void OnEnable()
        {
            _target = (XboxHeliInput)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            DrawDebugLayout();
            Repaint();
        }

        private void DrawDebugLayout()
        {

            EditorGUILayout.BeginVertical(EditorStyles.helpBox);
            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Inputs", EditorStyles.boldLabel);
            EditorGUILayout.Space();
            EditorGUI.indentLevel++;
            EditorGUILayout.LabelField("Raw Throttle : " + _target.ThrottleInput.ToString("0.00"));
            EditorGUILayout.LabelField("Sticky Throttle : " + _target.StickyThrottle.ToString("0.00"));
            EditorGUILayout.LabelField("Collective : " + _target.CollectiveInput.ToString("0.00"));
            EditorGUILayout.LabelField("Sticky Collective : " + _target.StickyCollective.ToString("0.00"));
            EditorGUILayout.LabelField("Cyclic : " + _target.CyclicInput.ToString("0.00"));
            EditorGUILayout.LabelField("Pedal : " + _target.PedalInput.ToString("0.00"));

            EditorGUILayout.EndVertical();
        }
    }


}

