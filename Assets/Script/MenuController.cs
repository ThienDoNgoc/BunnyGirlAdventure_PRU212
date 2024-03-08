using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public void Play()
    {
        SceneManager.LoadScene(1);
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

        SceneManager.LoadScene(0);
    }
    public void PlayAgain()
    {
        CarrotManager.instance.Reset();
        TimeManage.instance.Reset();
        SceneManager.LoadScene(1);
    }
}
