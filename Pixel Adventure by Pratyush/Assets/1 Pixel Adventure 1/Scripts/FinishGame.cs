using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishGame : MonoBehaviour
{
    private AudioSource finishSoundEffect;
    private bool levelcompleted = false;

    void Start()
    {
        finishSoundEffect = GetComponent<AudioSource>();    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Player" && !levelcompleted)
        {
            finishSoundEffect.Play();
            levelcompleted = true;
            Invoke("completeLevel", 2f);
            
        }
    }

    private void completeLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

}
