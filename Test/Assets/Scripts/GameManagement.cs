using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour
{
    public class levelData
    {
        public string levelName { get; set; }
        public int levelsLeft { get; set; }
        public int countOfRows { get; set; }
    }

    public static void WrongAnswerBounce(GameObject gameObject)
    {
        if (gameObject != null)
        {
            gameObject.transform.DOShakePosition(2.0f, strength: new Vector3(2, 2, 0), vibrato: 5, randomness: 1, snapping: false, fadeOut: true);
        }        
    }

    public static void easeInBounce(GameObject gameObject)
    {
        if (gameObject != null)
        {
            gameObject.transform.DOShakePosition(4.0f, strength: new Vector3(0, 2, 0), vibrato: 5, randomness: 0, snapping: false, fadeOut: true).SetEase(Ease.InBounce);
        }        
    }

    public static void BounceEffect(GameObject gameObject)
    {
        if (gameObject != null)
        {
            float endValue = 0f;
            float duration = 1.0f;

            gameObject.transform
                .DOScale(new Vector3(endValue, endValue, 0), duration)
                .SetDelay(endValue / 2)
                .SetEase(Ease.InOutBack)
                .OnComplete(() =>
                {
                    gameObject.transform
                        .DOScale(new Vector3(1, 1, 0), duration/2)
                        .SetEase(Ease.InOutBack);

                });
        }
    }

    public static void HalfBounceEffect(GameObject gameObject)
    {
        if (gameObject != null)
        {
            float endValue = 0.6f;
            float duration = 0.4f;

            gameObject.transform
                .DOScale(new Vector3(endValue, endValue, 0), duration)
                .SetDelay(endValue / 2)
                .SetEase(Ease.InOutBack)
                .OnComplete(() =>
                {
                    gameObject.transform
                        .DOScale(new Vector3(1, 1, 0), duration / 2)
                        .SetEase(Ease.InOutBack);

                });
        }
    }

    public static void RestartCurrentScene() 
    { 
        int currentScene = SceneManager.GetActiveScene().buildIndex; 
        SceneManager.LoadScene(currentScene); 
    }
}
