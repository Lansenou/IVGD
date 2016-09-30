using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Destroyable)), CanEditMultipleObjects]
public class DestroyableEditor : Editor 
{
    private SerializedProperty buildingType;
    private SerializedProperty shopType;
    private SerializedProperty carType;

    private void OnEnable()
    {
        buildingType = serializedObject.FindProperty ("BuildingType");
        shopType = serializedObject.FindProperty ("ShopType");
        carType = serializedObject.FindProperty ("CarType");
    }
    public override void OnInspectorGUI()
    {
        serializedObject.Update ();
        Destroyable myTarget = (Destroyable)target;
        DrawDefaultInspector ();

        if (myTarget.SortObject == Destroyable.ObjectSort.Residence)
        {
           EditorGUILayout.PropertyField (buildingType);
        } else if (myTarget.SortObject == Destroyable.ObjectSort.Shop)
        {
            EditorGUILayout.PropertyField (shopType);
        } else if (myTarget.SortObject == Destroyable.ObjectSort.Vehicle)
        {
            EditorGUILayout.PropertyField (carType);
        }

        serializedObject.ApplyModifiedProperties ();
    }
}

