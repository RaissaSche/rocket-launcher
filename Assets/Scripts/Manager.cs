using UnityEngine;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour {
    public void RestartScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void CloseGame() {
        Application.Quit();
    }
}