using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    [SerializeField] private string sceneToLoad;
    [SerializeField] private string areaTransitionName;

    private float waitToLoadTime = 1f;
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<PlayerController>())
        {
            UIFade.Instance.FadeToBlack();
            SceneManagement.Instance.SetTransitionName(areaTransitionName);
            StartCoroutine(LoadSceneRoutine());
        }
    }
    
    private IEnumerator LoadSceneRoutine()
    {
        yield return new WaitForSeconds(waitToLoadTime);
        
        SceneManager.LoadScene(sceneToLoad);
    }
}
