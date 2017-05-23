using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildNode : MonoBehaviour {

    private SpriteRenderer sprite;
    private GameObject turret;
    

	// Use this for initialization
	void Start () {
        sprite = GetComponent<SpriteRenderer>();
        sprite.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnMouseUp()
    {
        if(turret != null)
        {
            Debug.Log("Can't build here...");
            return;
        }

        var turretToBuild = BuildManager.Instance.GetTurretToBuild();
        turret = (GameObject)Instantiate(turretToBuild, transform.position, transform.rotation);

    }

    private void OnMouseEnter()
    {
        sprite.enabled = true;
    }

    private void OnMouseExit()
    {
        sprite.enabled = false;
    }
}
