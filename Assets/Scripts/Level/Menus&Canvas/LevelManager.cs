using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    #region SINGLETON

    private static LevelManager instance;
    public static LevelManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindFirstObjectByType<LevelManager>(FindObjectsInactive.Include);
                if (instance != null)
                    return instance;

                LevelManager levelManager = Instantiate(Resources.Load<GameObject>("Prefab/Level/Menus/LevelManager"),
                    Vector3.zero,
                    Quaternion.identity)
                    .GetComponent<LevelManager>();
                return levelManager;
            }
            else 
                return instance;
        }
        set
        {
            instance = value;
        }
    }

    #endregion

    [Header("Load Screen")]
    [SerializeField] GameObject loaderCanvas;
    [SerializeField] Image progressBar;

    [Header("Fede Stats")]
    [SerializeField] Image fadePanel;
    [SerializeField] float fadeSpeed;

    [Header("Tips")]
    [SerializeField] List<string> userTips;
    [SerializeField] TextMeshProUGUI userTipText;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }

    #region Button Funtions

    public void ChangeScene(string sceneName)
    {
        StartCoroutine(PreLoadFadeIn(() => StartCoroutine(LoadSceneAsync(sceneName, LoadSceneMode.Single, 
            () => 
            {
                loaderCanvas.SetActive(true);
                progressBar.fillAmount = 0;
                string tip = userTips[UnityEngine.Random.Range(0, userTips.Count)];
                userTipText.text = tip;
            }, 
            () => 
            {
                loaderCanvas.SetActive(false);
                progressBar.fillAmount = 0;
                StartCoroutine(PostLoadFadeOut(null));
            }))));
    }

    public void AddScene(string sceneName)
    {
        StartCoroutine(LoadSceneAsync(sceneName, LoadSceneMode.Additive,
            () =>
            {
                loaderCanvas.SetActive(false);
                progressBar.fillAmount = 0;
            },
            null));
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    #endregion

    #region IEnumerator

    private IEnumerator PreLoadFadeIn(Action onPreLoadEnd)
    {
        fadePanel.gameObject.SetActive(true);
        fadePanel.color = new Color(fadePanel.color.r, fadePanel.color.g, fadePanel.color.b, 0);
        while (true)
        {
            yield return new WaitForEndOfFrame();
            fadePanel.color = new Color(
                fadePanel.color.r, 
                fadePanel.color.g, 
                fadePanel.color.b,
                Time.deltaTime * fadeSpeed + fadePanel.color.a);

            if (fadePanel.color.a >= 1)
            {
                fadePanel.color = new Color(
                    fadePanel.color.r,
                    fadePanel.color.g,
                    fadePanel.color.b,
                    1);

                break;
            }
        } 
        
        onPreLoadEnd?.Invoke();      
        fadePanel.gameObject.SetActive(false);
    }

    private IEnumerator LoadSceneAsync(string sceneName, LoadSceneMode loadMode, Action onLoadSceneStart, Action onLoadSceneEnd)
    {
        onLoadSceneStart?.Invoke();

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName, loadMode);

        // to decide when allow the scene attivation
        operation.allowSceneActivation = true;

        while (!operation.isDone)
        {
            progressBar.fillAmount = Mathf.Clamp01(operation.progress / 0.9f);

            yield return new WaitForEndOfFrame();

            if (progressBar.fillAmount == 1)
            {
                //Add delay to give a sensation "ritardo"
                yield return new WaitForSeconds(0.5f);

                operation.allowSceneActivation = true;
            }
        }
        
        onLoadSceneEnd?.Invoke();
    }

    private IEnumerator PostLoadFadeOut(Action onPostLoadEnd)
    {
        fadePanel.gameObject.SetActive(true);
        fadePanel.color = new Color(fadePanel.color.r, fadePanel.color.g, fadePanel.color.b, 1);
        while (true)
        {
            yield return new WaitForEndOfFrame();
            fadePanel.color = new Color(
                fadePanel.color.r,
                fadePanel.color.g,
                fadePanel.color.b,
                fadePanel.color.a - Time.deltaTime * fadeSpeed);

            if (fadePanel.color.a <= 0)
            {
                fadePanel.color = new Color(
                    fadePanel.color.r,
                    fadePanel.color.g,
                    fadePanel.color.b,
                    0);

                break;
            }
        }

        onPostLoadEnd?.Invoke();
        fadePanel.gameObject.SetActive(false);
    }

    #endregion
}
