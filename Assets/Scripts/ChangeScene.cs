
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
   public void ChangeToScene (string sceneToChangeTo)
    {

        //Use once more scenes develop
        SceneManager.LoadScene(sceneToChangeTo);


        //testing purposes.
        //print("I WORK!");
    }

    public void QuitGame()
    {
        //TESTING
        Debug.Log("QUIT!");
        Application.Quit();
    }
}
