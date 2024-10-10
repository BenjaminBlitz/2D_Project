using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using JetBrains.Annotations;
public class DamagePopUp : MonoBehaviour
{
    public TextMeshPro textMesh;
    [SerializeField] private Transform damagePopUpPrefab;
    [SerializeField] private Transform damagePopUpCritPrefab;
    private float disappearTimer;
    private Color textColor;
    private GameObject parent;


    public DamagePopUp Create(Transform position, float damageAmount, bool crit)
    {
        if(crit)
        {

            Transform damagePopUpTransform = Instantiate(damagePopUpCritPrefab, position.position, Quaternion.identity);
            parent = GameObject.Find("Temporary");
            damagePopUpTransform.parent = parent.transform;
            DamagePopUp damagePopUp = damagePopUpTransform.GetComponent<DamagePopUp>();
            damagePopUp.Setup(damageAmount);
            print(crit);
            return damagePopUp;
        }
        else
        {
            Transform damagePopUpTransform = Instantiate(damagePopUpPrefab, position.position, Quaternion.identity);
            parent = GameObject.Find("Temporary");
            damagePopUpTransform.parent = parent.transform;
            DamagePopUp damagePopUp = damagePopUpTransform.GetComponent<DamagePopUp>();
            damagePopUp.Setup(damageAmount);
            print(crit);
            return damagePopUp;
        }
        
    }


    private void Awake()
    {
        textMesh=transform.GetComponent<TextMeshPro>();
        
    }
    public void Setup(float damageAmount)
    {

            textMesh.SetText(damageAmount.ToString());
            textColor = textMesh.color;
            disappearTimer = 1f;
        
        
    }
    
    private void Update()
    {
        float speed=2f;
        transform.position += new Vector3(0, speed) * Time.deltaTime;

        disappearTimer -= Time.deltaTime;
        if(disappearTimer < 0)
        {
            float disappearSpeed = 3f;
            textColor.a -=disappearSpeed * Time.deltaTime;
            textMesh.color = textColor;
            
            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }

    }
}
