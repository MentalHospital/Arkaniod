using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Animator anim;

    [SerializeField] private Button exitToMenuButton;
    [SerializeField] private Button nextLevelButton;

    SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();

        FindObjectOfType<GameStateController>().OnFinishLevel += ShowNextButton;
        exitToMenuButton.onClick.AddListener(() => { FindObjectOfType<SceneLoader>().LoadScene(0); });
        nextLevelButton.onClick.AddListener(
            () => { 
                sceneLoader.LoadNextLevel(); 
            }
            );
    }

    private void ShowNextButton()
    {
        //if not at final level then show button
        nextLevelButton.gameObject.SetActive(!sceneLoader.IsAtFinalLevel());
    }

    public void HideLevel()
    {
        anim.SetTrigger("closeGatesTrigger");
    }
}
