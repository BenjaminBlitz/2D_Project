using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasterEggs : MonoBehaviour
{
    public GameObject kkPrefab;
    public Transform feetPos;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.K))
        {
            GameObject kk = Instantiate(kkPrefab, new Vector3(feetPos.position.x,feetPos.position.y,0),Quaternion.identity);
            Destroy(kk,1f);

        }

    }
}
