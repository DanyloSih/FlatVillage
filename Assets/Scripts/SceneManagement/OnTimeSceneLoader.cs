using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace FlatVillage.SceneManagement
{
    public class OnTimeSceneLoader : MonoBehaviour
    {
        [SerializeField] private string _sceneName;  // имя сцены для загрузки
        [SerializeField] private float _delay;  // задержка перед загрузкой сцены

        private void Start()
        {
            StartCoroutine(LoadScene());
        }

        private IEnumerator LoadScene()
        {
            yield return new WaitForSeconds(_delay); 
            SceneManager.LoadSceneAsync(_sceneName);
        }
    }
}
