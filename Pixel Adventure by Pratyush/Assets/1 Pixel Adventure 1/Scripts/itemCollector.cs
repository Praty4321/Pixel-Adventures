using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class itemCollector : MonoBehaviour
{
    private int pineCount = 0;
    [SerializeField] private Text pineText;
    [SerializeField] private AudioSource collectionSoundEffect;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("pineapple"))
        {
            collectionSoundEffect.Play();
            Destroy(collision.gameObject);
            pineCount++;
            pineText.text = "Pines : " + pineCount;
        }
    }
}
