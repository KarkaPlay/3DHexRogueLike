using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GridManager manager;

    public Scene scene;

    public GameObject winImage, looseImage, finaleImage;

    public void LoadNextScene()
    {
        SceneManager.LoadScene(scene.buildIndex + 1);
    }
    
    public void Reload()
    {
        SceneManager.LoadScene(scene.buildIndex);
    }

    private void Start()
    {
        scene = SceneManager.GetActiveScene();
        winImage = GameObject.FindGameObjectWithTag("WinImage");
        looseImage = GameObject.FindGameObjectWithTag("LooseImage");
        finaleImage = GameObject.FindGameObjectWithTag("FinaleImage");

        winImage.GetComponent<Image>().enabled = false;
        looseImage.GetComponent<Image>().enabled = false;
        finaleImage.GetComponent<Image>().enabled = false;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (winImage.GetComponent<Image>().isActiveAndEnabled)
            {
                LoadNextScene();
            }

            else if (looseImage.GetComponent<Image>().isActiveAndEnabled)
            {
                Reload();
            }
        }
    }
}
