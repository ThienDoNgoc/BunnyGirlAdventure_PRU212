using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static bool isFirstTime = true;
}
public class MenuController : MonoBehaviour
{
    public void Play()
    {
        if (GameManager.isFirstTime)
        {
            GameManager.isFirstTime = false;
            SceneManager.LoadScene(1);
        }
        else
        {
            PlayAgain();
        }
    }
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
    Application.Quit();
#endif
    }
    public void BackMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }
    public void PlayAgain()
    {
        Time.timeScale = 1f;
        CarrotManager.instance.Reset();
        TimeManage.instance.Reset();
        SceneManager.LoadScene(1);
    }
}
