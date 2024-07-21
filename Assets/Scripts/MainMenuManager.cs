using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour {


    [SerializeField] private Button startButton;
    [SerializeField] private Button creditsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private string gameScene;
    [SerializeField] private GameObject creditsMenu;


    private void Awake() {
        startButton.onClick.AddListener(() => {
            SceneManager.LoadScene(gameScene);
        });
        creditsButton.onClick.AddListener(() => {
            ToggleCredits();
        });
        quitButton.onClick.AddListener(() => {
            Application.Quit();
        });
    }

    public void ToggleCredits() {
        creditsMenu.SetActive(!creditsMenu.activeSelf);
        gameObject.SetActive(!creditsMenu.activeSelf);
    }

}
