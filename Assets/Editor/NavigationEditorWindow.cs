using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

public class NavigationEditorWindow : EditorWindow
{
    private float gizmoRadius = 1f;
    private State editorState = State.Building;
    private Waypoint startWaypoint = null;
    private Color gizmoColor = Color.red;
    private NavigationEditor navigationEditor = new NavigationEditor(); 
    
    private enum State
    {
        Building, 
        Connecting
    }

    [MenuItem("Window/Navigation Editor")]
    static void InitWindow()
    {
        NavigationEditorWindow window = (NavigationEditorWindow) GetWindow(typeof(NavigationEditorWindow));
        window.Show();
    }

    void OnGUI()
    {
        DrawNavigationSection();
        DrawDebugSection();
    }
    
    private void DrawNavigationSection()
    {
        DrawHeadline("Navigation");

        // If no or an invalid object is selected display some information
        if (Selection.activeGameObject == null)
        {
            GUILayout.Label("Select a waypoint or road to begin");
        }
        // If a waypoint is selected display the actions available for working with waypoints
        else if (Selection.activeGameObject.GetComponent<Waypoint>() != null)
        {
            Waypoint selectedWaypoint = Selection.activeGameObject.GetComponent<Waypoint>();
            
            // If we are in build mode show relevant actions
            if (editorState == State.Building)
            {
                if (GUILayout.Button("Create next waypoint"))
                {
                    Waypoint newWaypoint = navigationEditor.CreateNextWaypoint(selectedWaypoint);
                    Selection.objects = new Object[] { newWaypoint.gameObject };
                }

                // If the user clicks connect waypoint we switch to connecting mode and switch actions
                if (GUILayout.Button("Connect waypoint"))
                {
                    startWaypoint = selectedWaypoint;
                    editorState = State.Connecting;
                }
            }
            // When in connecting mode we display the relevant options
            else if (editorState == State.Connecting)
            {
                // Terminate connecting mode and switch back to build mode
                if (GUILayout.Button("Stop connecting"))
                {
                    editorState = State.Building;
                }
                // If the user selects a new waypoint and this waypoint is not the same we
                // create a connection between the two waypoints and switch back to build mode
                if (!startWaypoint.Equals(selectedWaypoint))
                {
                    if (GUILayout.Button("Create connection"))
                    {
                        startWaypoint.AddNext(selectedWaypoint);
                        startWaypoint = null;
                        editorState = State.Building;
                    }
                }
                else
                {
                    GUILayout.Label("Select a different waypoint to connect");
                }
            }

        }
        // If a road is selected assume the user wants to create a new waypoint to start from
        else if (Selection.activeGameObject.GetComponent<Road>() != null)
        {
            if (GUILayout.Button("Create new waypoint"))
            {
                Road road = Selection.activeGameObject.GetComponent<Road>();
                Waypoint waypoint = navigationEditor.CreateWaypoint(road);
                Selection.objects = new Object[] {waypoint.gameObject};
            }
        }
    }

    private void DrawDebugSection()
    {
        DrawHeadline("Gizmos");
        
        gizmoRadius = EditorGUILayout.Slider("Gizmo Radius", gizmoRadius, 0f, 6f);
        gizmoColor = EditorGUILayout.ColorField("Gizmo Color", gizmoColor);

        if (GUILayout.Button("Update gizmos"))
        {
            navigationEditor.UpdateGizmos(gizmoRadius, gizmoColor);
        }
    }

    private void DrawNewWaypointSection()
    {
        GUILayout.Label("test new");
    }

    private void DrawNextWaypointSection()
    {
        GUILayout.Label("test next");
    }

    /*****************************************
     *  Utility functions
     */

    private void DrawHeadline(string headline)
    {
        DrawSpace();
        SetHeadlineFontSize();
        GUILayout.Label(headline);
        ResetFontSize();
    }

    private void DrawSpace()
    {
        GUILayout.Space(8);
    }

    private void SetHeadlineFontSize()
    {
        GUI.skin.label.fontSize = 16;
    }

    private void ResetFontSize()
    {
        GUI.skin.label.fontSize = 10;
    }

    private void ShowErrorDialog(string message)
    {
        EditorUtility.DisplayDialog("Error", message, "ok");
    }

    private void ShowMessageDialog(string message)
    {
        EditorUtility.DisplayDialog("Success", message, "ok");
    }
}