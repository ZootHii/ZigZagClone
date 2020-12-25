using System.Collections;
using UnityEngine;

public class ScorePopUp : MonoBehaviour
{
    private void Update()
    {
        if (Time.frameCount % 15 == 0)
        {
            StartCoroutine(DestroyScorePopUp());
        }
    }

    private IEnumerator DestroyScorePopUp()
    {
        yield return new WaitForSeconds(1.2f);
        Destroy(gameObject);
    }
}
