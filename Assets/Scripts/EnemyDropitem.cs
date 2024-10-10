using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropitem : MonoBehaviour
{
    public GameObject[] normalItems; // Liste des objets normaux
    public GameObject[] rareItems;   // Liste des objets rares
    public GameObject[] reallyRareItems;   // Liste des objets très rares
    public GameObject[] legendaryItems;   // Liste des objets légendaires
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void DropItem(float doubleDrop)
    {
        while (doubleDrop >= 0)
        {
            float itemDrop = Random.value;

            // Générer un nombre aléatoire entre 0 et 1
            float rand = Random.value;
            if (itemDrop < 0.2f)
            {
                // Choix aléatoire entre les objets normaux et rares en fonction de la probabilité
                GameObject itemToDrop;
                if (rand < 0.03f) // 3% de chance pour un objet legendaire
                {
                    int randIndex = Random.Range(0, legendaryItems.Length);
                    itemToDrop = legendaryItems[randIndex];
                }
                else if (rand < 0.07f) // 7% de chance pour un objet très rare
                {
                    int randIndex = Random.Range(0, reallyRareItems.Length);
                    itemToDrop = reallyRareItems[randIndex]; ;
                }

                else if (rand < 0.2f) // 20% de chance pour un objet rare
                {
                    int randIndex = Random.Range(0, rareItems.Length);
                    itemToDrop = rareItems[randIndex];
                }
                else    // 70% de chance pour un objet normal si on a aucun des autres
                {
                    int randIndex = Random.Range(0, normalItems.Length);
                    itemToDrop = normalItems[randIndex];
                }

                // Instanciation de l'objet choisi
                Instantiate(itemToDrop, transform.position, Quaternion.identity);
            }
            doubleDrop--;
        }
        
    }
}
