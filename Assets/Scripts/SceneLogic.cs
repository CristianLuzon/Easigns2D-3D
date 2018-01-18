using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLogic : MonoBehaviour
{
    [Header("Scene state")]
    public int backScene;
    public int currentScene;
    public int nextScene;
    private int managerScene = 0; //realmente sobra
    private int sceneCountInBuildSettings;

    [Header("Load parameters")]
    private AsyncOperation asynLoad = null;
    private AsyncOperation asynUnLoad = null;
    private bool loading = false;
    private int sceneToLoad;
    public Image blackScreen;
    public Image loadingBar;
    public float fadeTime = 0.20f;
    bool optionsSceneLoad = false;

    void Start()
    {
        blackScreen.color = Color.black;
        if(SceneManager.sceneCount >= 2) SceneManager.SetActiveScene(SceneManager.GetSceneAt(1));

        UpdateSceneState();

        if(currentScene == managerScene) StartLoad(nextScene);

        Screen.SetResolution(1280, 720, false);
    }

    void Update()
    { // Input manager
        if(Input.GetKeyDown(KeyCode.N)) StartLoad(nextScene);
        if(Input.GetKeyDown(KeyCode.B)) StartLoad(backScene);
    }

    void UpdateSceneState()
    {
        sceneCountInBuildSettings = SceneManager.sceneCountInBuildSettings;
        currentScene = SceneManager.GetActiveScene().buildIndex;

        if(currentScene + 1 >= sceneCountInBuildSettings) nextScene = managerScene + 1; // nextScene = 1 tambien serviría
        else nextScene = currentScene + 1;

        if(currentScene - 1 <= managerScene) backScene = SceneManager.sceneCountInBuildSettings - 1;
        else backScene = currentScene - 1;
    }

    public void StartLoad(int index)
    {
        if(loading)
        {
            return;
        }

        if(index == 3 && optionsSceneLoad != true) optionsSceneLoad = true;

        loading = true;
        sceneToLoad = index;
        FadeOut();
        StartCoroutine(WaitingLoad());

    }

    void Load()
    {
        if(currentScene != managerScene) asynUnLoad = SceneManager.UnloadSceneAsync(currentScene);
        asynLoad = SceneManager.LoadSceneAsync(sceneToLoad, LoadSceneMode.Additive);
        StartCoroutine(Loading());
        FadeIn();
    }

    void FadeIn()
    {
        blackScreen.CrossFadeAlpha(0, fadeTime, true);
        loadingBar.CrossFadeAlpha(0, fadeTime, true);
    }

    void FadeOut()
    {
        blackScreen.CrossFadeAlpha(1, fadeTime, true);
        loadingBar.CrossFadeAlpha(1, fadeTime, true);
    }

    IEnumerator Loading()
    {
        while(loading)
        {
            loadingBar.rectTransform.Rotate(0, 0, 10);

            if((asynUnLoad == null || asynUnLoad.isDone) && asynLoad.isDone)
            {

                SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(sceneToLoad));
                UpdateSceneState();
                loading = false;
            }
            yield return null;
        }
    }

    IEnumerator WaitingLoad()
    {
        yield return new WaitForSeconds(fadeTime);
        Load();
    }
}