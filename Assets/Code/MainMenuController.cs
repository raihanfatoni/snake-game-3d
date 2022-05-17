using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{

    public GameObject CreditPanel;
    public GameObject GuidePanel;
    public GameObject MenuPanel;
    // Start is called before the first frame update
    void Start()
    {
        MenuPanel.SetActive(true);
        CreditPanel.SetActive(false);
        GuidePanel.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GoToCreditPanel()
    {
        MenuPanel.SetActive(false);
        CreditPanel.SetActive(true);
        GuidePanel.SetActive(false);
    }

    public void GoToGuidePanel()
    {
        MenuPanel.SetActive(false);
        CreditPanel.SetActive(false);
        GuidePanel.SetActive(true);
    }

    public void Back()
    {
        MenuPanel.SetActive(true);
        CreditPanel.SetActive(false);
        GuidePanel.SetActive(false);
    }

    public void PlayGame(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("Quit Game");
    }
}
