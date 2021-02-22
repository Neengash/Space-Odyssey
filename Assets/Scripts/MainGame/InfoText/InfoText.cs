using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InfoText : MonoBehaviour
{
    TextMeshPro text;
    const float ALPHA_INCREMENT = 0.05f;
    const float TRANSPARENT = 0f;
    const float OPAQUE = 1f;
    const float TIME_VISIBLE = 1.5f;
    float r, g, b;

    void Start() {
        text = GetComponent<TextMeshPro>();

        r = text.color.r;
        g = text.color.g;
        b = text.color.b;

        updateColor(TRANSPARENT);
    }

    public void showText(string newText) {
        text.text = newText;
        StartCoroutine(inAndOut());
    }

    void updateColor(float alfa) {
        text.color = new Color(r, g, b, alfa);
    }

    IEnumerator inAndOut() {
        float alpha = text.color.a;

        while (alpha < OPAQUE) {
            alpha += ALPHA_INCREMENT;
            updateColor(alpha);
            yield return null;
        }

        yield return new WaitForSeconds(TIME_VISIBLE);

        while (alpha > TRANSPARENT) {
            alpha -= ALPHA_INCREMENT;
            updateColor(alpha);
            yield return null;

        }
    }
}
