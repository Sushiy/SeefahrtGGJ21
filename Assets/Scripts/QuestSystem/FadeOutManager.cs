using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeOutManager : MonoBehaviour
{
    public CanvasGroup m_screenTexture;
    public float m_fadeDuration;
    public String m_menuName = "Menu";
    public String m_levelName = "MainMap_v1";
    public void StartFade()
    {
        m_screenTexture.alpha = 0;
        m_screenTexture.gameObject.SetActive(true);
        StartCoroutine(AnimateFadeAndReturnToMenu());
        
        
    }

    private IEnumerator AnimateFadeAndReturnToMenu()
    {
        while (m_screenTexture.alpha < 1)
        {
            m_screenTexture.alpha += Time.deltaTime / m_fadeDuration;
            yield return null;
        }
        SceneManager.UnloadSceneAsync(m_levelName);
        SceneManager.LoadScene(m_menuName,LoadSceneMode.Single);
        
    }
}
