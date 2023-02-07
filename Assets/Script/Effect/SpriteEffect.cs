using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SpriteEffect : Effect
{
    public override void Run()
    {
        StartCoroutine(SpriteDestroy());
    }

    private IEnumerator SpriteDestroy()
    {

        if (gameObject.TryGetComponent<Image>(out Image image))
        {
            image.sprite = sprite;

            Color color = image.color;

            while (true)
            {
                color.a -= 0.1f;

                image.color=color;

                yield return new WaitForSeconds(0.1f);

                if (image.color.a <= 0)
                {
                    Destroy(this.gameObject);
                    yield break;
                }
            }
        }
        else if(gameObject.TryGetComponent<SpriteRenderer>(out SpriteRenderer renderer))
        {
            renderer.sprite = sprite;

            Color color = renderer.color;

            while (true)
            {
                color.a -= 0.1f;

                renderer.color = color;

                yield return new WaitForSeconds(0.1f);

                if (renderer.color.a <= 0)
                {
                    Destroy(this.gameObject);
                    yield break;
                }
            }
        }
           
        else
            Debug.LogError("OMG NOT Component");
    }


   



}
