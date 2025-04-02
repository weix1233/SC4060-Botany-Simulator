using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    [Header("Levels to load")]
    public string _newGameLevel;
    private string levelToLoad;
    [SerializeField] private GameObject noSavedGameDialog = null;

    public void NewGameDialogYes()
    {
        SceneManager.LoadScene(_newGameLevel);

    }

    public void LoadGameDialogYes()
    {
        if (PlayerPrefs.HasKey("Water"))
        {
            SceneManager.LoadScene(_newGameLevel);
        }
        else
        {
            noSavedGameDialog.SetActive(true);
        }
    }
 
    public void ExitButton()
    {
        Application.Quit();
    }
}
