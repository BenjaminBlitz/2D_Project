using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public PauseMenu pauseMenu;
    public Transform shootPosition;
    public float bulletSpeed;


    public GameObject bulletPrefab;
    public PlayerMovement player;
    public PlayerInventory playerStats;



    // Update is called once per frame
    void Update()
    {
        if (!pauseMenu.isPaused)
        {
            Vector3 worldMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector2 direction = (Vector2)((worldMousePos - shootPosition.position));
            direction.Normalize();
            if (Input.GetMouseButtonDown(1))
            {
                // Creates the bullet locally
                GameObject bullet = (GameObject)Instantiate(
                                        bulletPrefab,
                                        shootPosition.position + (Vector3)(direction * 0.5f),
                                        Quaternion.identity);

                //bullet.GetComponent<HittingEnemies>().playerStats = playerStats;

                // Adds velocity to the bullet
                bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
                Destroy(bullet, 1f);
            }
            if (Input.GetMouseButton(0))
            {
                player.isBasicAttack = true;
            }
            else { player.isBasicAttack = false; }
        }



        /*
        lookDirection = Camera.main.WorldToScreenPoint(Input.mousePosition);
        lookAngle=Mathf.Atan2(lookDirection.y, lookDirection.x)*Mathf.Rad2Deg;
        lookDirection = new Vector2(lookDirection.x - shootPosition.position.x, lookDirection.y - shootPosition.position.y);


        shootPosition.rotation = Quaternion.Euler(0, 0, lookAngle);

        if (Input.GetMouseButtonDown(0))
        {
            GameObject bulletClone = Instantiate(bulletPrefab);
            bulletClone.transform.position = shootPosition.position;
            bulletClone.transform.rotation = Quaternion.Euler(0, 0, lookAngle);

            bulletClone.GetComponent<Rigidbody2D>().velocity = shootPosition.right * bulletSpeed;
            Destroy(bulletClone, 1f);

        }*/



            /*
            if (Input.GetMouseButtonDown(0))
            {
                GameObject bullet = Instantiate(bulletPrefab, new Vector3(player.lookDirection.position.x, player.lookDirection.position.y, 0), Quaternion.identity);
                Destroy(bullet, 1f);
            }
            */

    }


}
