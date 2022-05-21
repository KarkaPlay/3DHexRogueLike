using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Menu : MonoBehaviour
{
    [SerializeField]
    private GameObject Main;
    [SerializeField]
    private GameObject OptionMenu;

    private void Start()
    {
        Main.SetActive(true);
        OptionMenu.SetActive(false);
    }
    private void Update()
    {
 
        Cursor.lockState = CursorLockMode.Confined;
    }
    public void OptionsMenu()
    {
        Main.SetActive(false);
        OptionMenu.SetActive(true) ;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void Back()
    {
        Main.SetActive(true);
        OptionMenu.SetActive(false) ;
    }
    public void Play()
    {
        SceneManager.LoadScene("SampleScene");
    }
}

