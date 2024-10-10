using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseBehavior : MonoBehaviour
{

    public Transform crosshair;
    //Vector3 worldPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // Obtenir la position de la souris en pixels
        Vector3 mousePosition = Input.mousePosition;

        // Convertir la position de la souris en coordonnées du monde
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);

        // Mettre à jour la position du viseur
        transform.position = new Vector3(worldPosition.x, worldPosition.y, 0);
        /*
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = Camera.main.transform.position.z + Camera.main.nearClipPlane;
        crosshair.position = mousePosition;
        
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        worldPosition = Camera.main.ScreenToWorldPoint(mousePos);
        */

        //Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        //print(worldPosition);
    }
}
