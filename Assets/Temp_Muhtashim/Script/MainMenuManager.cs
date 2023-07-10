using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

namespace Game.MainMenu
{
    public class MainMenuManager : MonoBehaviour
    {
        [SerializeField] private string startGameButtonName;
        [SerializeField] private string quitGameButtonName;
        [SerializeField] private int GameSceneIndex;

        [SerializeField] private UIDocument mainMenuDocument;

        private Button startGameButton;
        private Button quitGameButton;

        private void OnEnable()
        {
            VisualElement rootElement = mainMenuDocument.rootVisualElement;

            startGameButton = rootElement.Q<Button>(startGameButtonName);
            quitGameButton = rootElement.Q<Button>(quitGameButtonName);

            startGameButton.clicked += onStartGame;
            quitGameButton.clicked += onQuitGame;
        }

        private void OnDisable()
        {
            startGameButton.clicked -= onStartGame;
            quitGameButton.clicked -= onQuitGame;
        }

        private void onStartGame() => SceneManager.LoadSceneAsync(GameSceneIndex);
        private void onQuitGame() => Application.Quit();
    }

}