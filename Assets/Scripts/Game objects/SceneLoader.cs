using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader instance;

    // last scene index = levels amount потому что 0 - это menu
    [SerializeField] private int levelsAmount = 1;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this.gameObject);

        DontDestroyOnLoad(this.gameObject);

    }

    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }

    public void LoadNextLevel()
    {
        LoadScene(GetSceneIndex() + 1);
    }

    public int GetSceneIndex()
    {
        return SceneManager.GetActiveScene().buildIndex;
    }

    public bool IsAtFinalLevel()
    {
        return GetSceneIndex() == levelsAmount;
    }

    public void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
