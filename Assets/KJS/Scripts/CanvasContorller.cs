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
        // Canvas�� �ִ� Image ������Ʈ�� �����ɴϴ�.
        canvasImage = GetComponent<Image>();

        if (canvasImage != null)
        {
            originalColor = canvasImage.color;
        }
        else
        {
            Debug.LogError("Canvas�� Image ������Ʈ�� �����ϴ�. Canvas�� Image ������Ʈ�� �߰��ϼ���.");
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
                // r:62, g:85, b:202, a:0 ������ ������ �����մϴ�.
                canvasImage.color = new Color(62f / 255f, 85f / 255f, 202f / 255f, 0.1f);
            }
            else
            {
                canvasImage.color = originalColor;
            }
        }
    }
}