using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlatformDrop : MonoBehaviour
{
    public PlayerMovement player;
    public float input;
    public GameObject grounds;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        input = UnityEngine.Input.GetAxisRaw("Vertical");

        if (input<0) 
        {
            foreach (PlatformEffector2D p in grounds.GetComponentsInChildren<PlatformEffector2D>())
            {
                p.useColliderMask = false;
            }

        }
        else
        {
            foreach (PlatformEffector2D p in grounds.GetComponentsInChildren<PlatformEffector2D>())
            {
                p.useColliderMask = true;
            }
        }

    }

    /*
    public void DisablePlatforms()
    {
        foreach (PlatformEffector2D p in grounds.GetComponentsInChildren<PlatformEffector2D>())
        {
            p.useColliderMask = false;
        }

        
    }
    IEnumerator EnablePlatforms()
    {


        foreach (PlatformEffector2D p in grounds.GetComponentsInChildren<PlatformEffector2D>())
        {
            p.useColliderMask = true;
        }

    }*/


    /*
       IEnumerator FallTimer()
       {
           PlatformEffector2D[] array = grounds.GetComponentsInChildren<PlatformEffector2D>();
           foreach (PlatformEffector2D p in array)
           {
               p.useColliderMask = false;
           }

           yield return new WaitForSeconds(0.2f);
           foreach (PlatformEffector2D p in grounds.GetComponentsInChildren<PlatformEffector2D>())
           {
               p.useColliderMask = true;
           }
       }*/
}
