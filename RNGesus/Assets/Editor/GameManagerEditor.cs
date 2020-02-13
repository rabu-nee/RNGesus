using UnityEditor;
using UnityEditorInternal;

[CustomEditor(typeof(GameManager))]
[CanEditMultipleObjects]
public class GameManagerEditor : Editor {
    private ReorderableList emotions;

    private void OnEnable() {
        var property = this.serializedObject.FindProperty("emotions");

        this.emotions = ReorderableListUtility.CreateAutoLayout(property);
    }

    public override void OnInspectorGUI() {
        this.serializedObject.Update();

        ReorderableListUtility.DoLayoutListWithFoldout(this.emotions);

        this.serializedObject.ApplyModifiedProperties();
    }
}