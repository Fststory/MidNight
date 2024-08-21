using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine;
using UnityEngine.UI;

public class ChangeCanvasColor : MonoBehaviour
{
    private Image canvasImage;
    private Color originalColor;
    private bool isActive = false;

    void Start()
    {
        // Canvas에 있는 Image 컴포넌트를 가져옵니다.
        canvasImage = GetComponent<Image>();

        if (canvasImage != null)
        {
            originalColor = canvasImage.color;
        }
        else
        {
            Debug.LogError("Canvas에 Image 컴포넌트가 없습니다. Canvas에 Image 컴포넌트를 추가하세요.");
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            isActive = !isActive;
            ChangeColor();
        }
    }

    void ChangeColor()
    {
        if (canvasImage != null)
        {
            if (isActive)
            {
                // r:62, g:85, b:202, a:0 값으로 색상을 변경합니다.
                canvasImage.color = new Color(62f / 255f, 85f / 255f, 202f / 255f, 0.1f);
            }
            else
            {
                canvasImage.color = originalColor;
            }
        }
    }
}