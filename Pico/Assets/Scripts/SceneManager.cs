using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Audio;
using Photon.Pun;
public class SceneManager : MonoBehaviourPunCallbacks
{
    [System.Serializable]
    public class LoadingScreenComponenets
    {
        public GameObject LoadingScreen;
        public Slider LoadingSlider;
        public TextMeshProUGUI ProgressText;
    }
    [System.Serializable]
    public class SettingsComponenets
    {
        public AudioMixer audioMixer;
        public GameObject GraphicOptions;
        public bool isGraphicsSelected;
    }
    public LoadingScreenComponenets loadingComponents;
    public SettingsComponenets settingsComponenets;
    void Update()
    {
        if(settingsComponenets.GraphicOptions != null)
        {
            settingsComponenets.GraphicOptions.SetActive(settingsComponenets.isGraphicsSelected);
        }
    }
    public void SceneChange(string SceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(SceneName);
    }
    public void Exit()
    {
        Application.Quit();
    }
    public void CanvasHideAndShow(string CanvasName)
    {
        for(int i = 0; i < transform.childCount; i++)
        {
            if(transform.GetChild(i).name != CanvasName)
            {
                transform.GetChild(i).gameObject.SetActive(false);
            }
            else
            {
                transform.GetChild(i).gameObject.SetActive(true);
            }
        }
    }
    public void LoadLevel(int LevelNo)
    {
        StartCoroutine(LoadAsyncronously(LevelNo));
        CanvasHideAndShow("Loading Canvas");
    }
    IEnumerator LoadAsyncronously(int LevelNo)
    {
        AsyncOperation operation = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(LevelNo);
        loadingComponents.LoadingScreen.SetActive(true);
        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            loadingComponents.LoadingSlider.value = progress;
            loadingComponents.ProgressText.text = progress * 100f + "%";
            yield return null;
        }
    }
    public void SetVolume(float Volume)
    {
        settingsComponenets.audioMixer.SetFloat("Volume", Volume);
    }
    public void SetQuality(int QualityIndex)
    {
        QualitySettings.SetQualityLevel(QualityIndex);
    }
    public void SubCanvas(string SubCanvasName)
    {
        switch(SubCanvasName)
        {
            case "Graphics":
                settingsComponenets.isGraphicsSelected = true;
                break;
        }
    }
    public void ClosingUIWhenClickedOutside()
    {
        if (settingsComponenets.isGraphicsSelected)
        {
            settingsComponenets.isGraphicsSelected = false;
        }
    }
}