using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RestartButton : MonoBehaviour
{
    private bool isInteractable = true;

    public void ButtonClick()
    {
        if (isInteractable)
        {
            isInteractable = false;
            this.GetComponent<Button>().interactable = false;
            GameManagement.easeInBounce(this.gameObject);
            StartCoroutine(waitForLEvelReset());
        }
    }

    private IEnumerator waitForLEvelReset()
    {
        yield return new WaitForSeconds(3);
        GameManagement.RestartCurrentScene();
    }
}
