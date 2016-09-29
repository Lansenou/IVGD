using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Destroyable))]
public class DestroyableEditor : Editor 
{
    public override void OnInspectorGUI()
    {
        Destroyable myTarget = (Destroyable)target;
        DrawDefaultInspector ();

        if (myTarget.SortObject == Destroyable.ObjectSort.Residence)
        {
            myTarget.BuildingType = (Destroyable.Building)EditorGUILayout.EnumPopup ("Residence", myTarget.BuildingType);
        } else if (myTarget.SortObject == Destroyable.ObjectSort.Shop)
        {
            myTarget.ShopType = (Destroyable.Shop)EditorGUILayout.EnumPopup ("Shop", myTarget.ShopType);
        } else if (myTarget.SortObject == Destroyable.ObjectSort.Vehicle)
        {
            myTarget.CarType = (Destroyable.Car)EditorGUILayout.EnumPopup ("Vehicle", myTarget.CarType);
        }


    }
}

