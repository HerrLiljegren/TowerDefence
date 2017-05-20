using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[Tiled2Unity.CustomTiledImporter]
public class MyCustomImporter : Tiled2Unity.ICustomTiledImporter
{

    public void HandleCustomProperties(GameObject gameObject,
            IDictionary<string, string> keyValuePairs)
    {
        Debug.Log("Handle custom properties from Tiled map" + keyValuePairs);

    }

    public void CustomizePrefab(GameObject prefab)
    {
        var polylines = prefab.GetComponentsInChildren<EdgeCollider2D>();
        ImportWaypoints(prefab, polylines);

        //var parent = prefab.transform.Find("BuildNodes");
        //if(parent != null)
        //{
        //    for(var i = 0; i < parent.childCount; i++)
        //    {
        //        parent.GetChild(i).name = "BuildNode";
        //    }            
        //}        
    }

    private void ImportWaypoints(GameObject prefab, EdgeCollider2D[] polylines)
    {
        if (polylines == null) return;

        var parent = prefab.transform.Find("Waypoints");

        foreach (var polyline in polylines)
        {
            for (var i = 0; i < polyline.points.Length; i++)
            {
                var point = polyline.points[i];
                var waypoint = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/Waypoint.prefab", typeof(GameObject));
                if (waypoint != null)
                {
                    var spawnInstance = (GameObject)GameObject.Instantiate(waypoint);
                    var name = waypoint.name + " (" + i + ")";

                    if (i == 0) name = waypoint.name + " - START";
                    if (i == polyline.points.Length - 1) name = waypoint.name + " - END";
                    spawnInstance.name = name;

                    // Use the position of the game object we're attached to

                    spawnInstance.transform.parent = parent;
                    spawnInstance.transform.localPosition = Vector3.zero;
                    spawnInstance.transform.position = new Vector3(polyline.bounds.min.x + point.x, polyline.bounds.min.y + point.y, -200f);
                }
            }
        }

        var path = parent.transform.Find("Path");
        if (path != null)
        {
            GameObject.DestroyImmediate(path.gameObject);
        }
    }

    private void ImportTurrentSpots(GameObject prefab)
    {

    }
}
