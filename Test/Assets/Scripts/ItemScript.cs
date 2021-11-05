using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ItemScript : MonoBehaviour
{
    private bool IsInteractable = true;
    public ParticleSystem particle;
    private bool IsTrue;
    private ItemsData data;

    public void Init(ItemsData _data)
    {
        data = _data;
        GetComponent<Image>().sprite = data.Sprite;
        GetComponent<RectTransform>().sizeDelta = new Vector2(data.Width, data.Height);
    }

    public string GetName()
    {
        return data.Name;
    }

    public void SetRightAnswer()
    {
        if (!IsTrue)
        {
            IsTrue = true;
        }
        
        else
        {
            IsTrue = false;
        }
    }

    private void OnMouseUp()
    {
        if (IsInteractable)
        {
            if (IsTrue)
            {
                GameManagement.easeInBounce(this.gameObject);
                Particles.TurnOn();
                StartCoroutine(Particles.TurnOff());
                StartCoroutine(waitTillAnimationEnd());
                IsInteractable = false;
            }

            else
            {
                GameManagement.WrongAnswerBounce(this.gameObject);
            }
        }
    }

    private IEnumerator waitTillAnimationEnd()
    {
        yield return new WaitForSeconds(4.0f);
        DOTween.KillAll();
        ItemsSpawn.NewLevelIsActive();
        SetRightAnswer();
    }
}
