using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropitem : MonoBehaviour
{
    public GameObject[] normalItems; // Liste des objets normaux
    public GameObject[] rareItems;   // Liste des objets rares

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DropItem()
    {

        float itemDrop = Random.value;

        // Générer un nombre aléatoire entre 0 et 1
        float rand = Random.value;
        if (itemDrop < 0.2f)
        {
            // Choix aléatoire entre les objets normaux et rares en fonction de la probabilité
            GameObject itemToDrop;
            if (rand < 0.7f) // 70% de chance pour un objet normal
            {
                int randIndex = Random.Range(0, normalItems.Length);
                itemToDrop = normalItems[randIndex];
            }
            else // 30% de chance pour un objet rare
            {
                int randIndex = Random.Range(0, rareItems.Length);
                itemToDrop = rareItems[randIndex];
            }
            // Instanciation de l'objet choisi
            Instantiate(itemToDrop, transform.position, Quaternion.identity);
        }
        
    }
}
