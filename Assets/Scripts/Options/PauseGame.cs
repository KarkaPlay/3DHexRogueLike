using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    [SerializeField]
    private GameObject Interface;
    [SerializeField]
    private GameObject OptionMenu;
    [SerializeField]
    private GameObject PauseMenu;
    [SerializeField]
    private GameObject VideoSettings;
    [SerializeField]
    private GameObject AudioSettings;
    public bool isPaused = false;



    private void Start()
    {
        ShowVideoSettings();
        PauseMenu.SetActive(false);
        OptionMenu.SetActive(false);
 
    }
    void Update()
    {

        if (Input.GetKeyDown("escape"))
        {
            PauseSwitch();                
        }
    }
    private void PauseSwitch()
    {
        isPaused = !isPaused;
        if (isPaused)
        {
            Pause();
        }
        else
        {
            unPause();
        }
    }
     public void OptionsMenu()
    {
        PauseMenu.SetActive(false);
        OptionMenu.SetActive(true);
     
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    private void Pause()
    {
        Time.timeScale = 0f;
        Interface.SetActive(false);
        PauseMenu.SetActive(true);
       
    }
    private void unPause()
    {
        Time.timeScale = 1;
        Interface.SetActive(true);
        PauseMenu.SetActive(false);
        OptionMenu.SetActive(false);
   
    }
    public void BackToGame()
    {
        unPause();
        isPaused = false;
        
    }
    public void MainMenu()
    {
        
        unPause();
        isPaused = false;
        SceneManager.LoadScene("Menu");
    }
    public void Back()
    {
        PauseMenu.SetActive(true);
        OptionMenu.SetActive(false);
    }
  
    public void ShowVideoSettings()
    {
        VideoSettings.SetActive(true);

        AudioSettings.SetActive(false);
    }
    public void ShowAudioSettings()
    {
        VideoSettings.SetActive(false);

        AudioSettings.SetActive(true);
    }
}
