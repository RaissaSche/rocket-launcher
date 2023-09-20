using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {

    public void RestartScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void CloseGame() {
        Application.Quit();
    }
}