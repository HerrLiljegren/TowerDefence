using System.Linq;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Tiled2Unity;

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

        var parent = prefab.transform.Find("BuildNodes");
        if (parent != null)
        {
            //var childCount = parent.childCount;
            //for(var i = 0; i < childCount; i++)
            //{
            //    var towerLocation = (Transform)parent.GetChild(i);
            //    if(towerLocation != null)
            //    {
            //        var buildNodePrefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/BuildNode.prefab", typeof(GameObject));
            //        if(buildNodePrefab != null)
            //        {
            //            var instance = (GameObject)GameObject.Instantiate(buildNodePrefab);
            //            instance.transform.position = towerLocation.position;
            //            instance.transform.parent = parent;
            //        }
            //    }
            //}

            var rectangles = parent.GetComponentsInChildren<RectangleObject>();
            foreach (var rect in rectangles)
            {
                var buildNodePrefab = AssetDatabase.LoadAssetAtPath("Assets/Prefabs/BuildNode.prefab", typeof(GameObject));
                if (buildNodePrefab != null)
                {
                    var instance = (GameObject)GameObject.Instantiate(buildNodePrefab);                    
                    instance.transform.position = rect.gameObject.transform.position - new Vector3(-32.0f, 32.0f, 0);
                    instance.transform.parent = rect.gameObject.transform.parent;
                    instance.name = rect.TmxName;
                }

                GameObject.DestroyImmediate(rect.gameObject);
            }

            

            //var node = parent.transform.GetComponent<Transform>();
            //GameObject.DestroyImmediate(node.gameObject);
                       

            //for (var i = 0; i < parent.childCount; i++)
            //{
                
            //    GameObject.DestroyImmediate(parent.GetChild(i).gameObject);
            //    Debug.Log("Destroy");
            //}
        }
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
