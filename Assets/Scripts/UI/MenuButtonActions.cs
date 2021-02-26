using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonActions : MonoBehaviour
{
    [SerializeField] private Button PlayButton;
    [SerializeField] private Button OptionsButton;
    [SerializeField] private Button ExitButton;

    void Start()
    {
        PlayButton.onClick.AddListener(() => { SceneLoader.instance.LoadScene(1); });
        ExitButton.onClick.AddListener(() => { Application.Quit(); });
    }
}
