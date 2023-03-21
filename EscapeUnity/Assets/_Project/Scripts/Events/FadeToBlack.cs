using System.Collections;
using System.Threading;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeToBlack : MonoBehaviour
{
    [SerializeField] private Image blackImage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StartCoroutine(Fade());
    }

    private IEnumerator Fade()
    {
        float alpha = 0;
        var tempColor = blackImage.color;
        
        while(alpha < 1f)
        {
            alpha += Time.deltaTime * .5f;
            tempColor.a = alpha;
            blackImage.color = tempColor;
            yield return null;
        }

        SceneManager.LoadScene("Menu");
    }
}
