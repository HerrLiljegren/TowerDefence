using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager  : MonoBehaviour {

    #region Singelton
    public static BuildManager Instance;
    private void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("More than one BuildManager in scene!");
            return;
        }
        Instance = this;
    }
    #endregion

    public GameObject standardTurretPrefab;

    private GameObject turretToBuild;

    private void Start()
    {
        turretToBuild = standardTurretPrefab;
    }

    public GameObject GetTurretToBuild()
    {
        return turretToBuild;
    }
}
