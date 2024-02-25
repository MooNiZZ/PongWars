using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class PlayFieldEditor : EditorWindow
    {
        private static Vector2 minMaxX = new Vector2(-7, 6.5f);
        private static Vector2 minMaxY = new Vector2(-3.5f, 4.0f);
        private static Vector2 squareSize = new Vector2(0.5f, 0.5f);
        private static GameObject leftSquarePrefab;
        private static GameObject rightSquarePrefab;
        
        [MenuItem("Window/PlayFieldEditor")]
        public static void ShowWindow()
        {
            GetWindow<PlayFieldEditor>("PlayFieldEditor");
        }

        private void OnGUI()
        {
            GUILayout.Label("This is a label", EditorStyles.boldLabel);

            minMaxX = EditorGUILayout.Vector2Field("MinMax X Size", minMaxX);
            minMaxY = EditorGUILayout.Vector2Field("Min Max Y Size", minMaxY);
            squareSize = EditorGUILayout.Vector2Field("Square Size", squareSize);
            leftSquarePrefab = EditorGUILayout.ObjectField("Left Square Prefab", leftSquarePrefab, typeof(GameObject), false) as GameObject;
            rightSquarePrefab = EditorGUILayout.ObjectField("Right Square Prefab", rightSquarePrefab, typeof(GameObject), false) as GameObject;
            
            if (GUILayout.Button("Press me"))
            {
                Debug.Log("Button pressed");
                CreatePlayField();
            }
            
        }

        private void CreatePlayField()
        {
            var playField =  GameObject.Find("PlayField");
            if (playField != null)
            {
                DestroyImmediate(playField);
            }
            
            playField = new GameObject("PlayField");

            float minXValue = minMaxX.x;
            float maxXValue = minMaxX.y;
            
            var totalX = Mathf.Abs(minXValue) + Mathf.Abs(maxXValue);
            var columns = (totalX / squareSize.x) + 1;
            
            var totalY = Mathf.Abs(minMaxY.x) + Mathf.Abs(minMaxY.y);
            var rows = (totalY / squareSize.y) + 1;
            for (int y = 0; y < rows; y++)
            {
                for (int i = 0; i < columns; i++)
                {
                    bool isLeft = i < columns / 2;
                    var prefab = isLeft ? leftSquarePrefab : rightSquarePrefab;
                    var square = PrefabUtility.InstantiatePrefab(prefab) as GameObject;
                    square.transform.position = new Vector3(minXValue + (i * squareSize.x), minMaxY.y - (y * squareSize.y), 0);
                    square.transform.parent = playField.transform;
                    square.layer = isLeft ? LayerMask.NameToLayer("RedSide") : LayerMask.NameToLayer("BlueSide");
                }
            }
            
        }
    }
}
