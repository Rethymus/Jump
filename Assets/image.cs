using UnityEngine;
using UnityEngine.UI;

public class ImageFillMethodSetter : MonoBehaviour
{
    public Image targetImage;
    public Image.FillMethod fillMethod = Image.FillMethod.Horizontal;
    public Image.OriginHorizontal horizontalOrigin = Image.OriginHorizontal.Left;
    public Image.OriginVertical verticalOrigin = Image.OriginVertical.Bottom;

    void Start()
    {
        // 如果没有在Inspector中指定，尝试获取当前GameObject的Image组件
        if (targetImage == null)
        {
            targetImage = GetComponent<Image>();
        }

        // 设置Fill Method
        if (targetImage != null)
        {
            targetImage.type = Image.Type.Filled;
            targetImage.fillMethod = fillMethod;

            // 根据选择的填充方法设置起始位置
            switch (fillMethod)
            {
                case Image.FillMethod.Horizontal:
                    targetImage.fillOrigin = (int)horizontalOrigin;
                    break;
                case Image.FillMethod.Vertical:
                    targetImage.fillOrigin = (int)verticalOrigin;
                    break;
                case Image.FillMethod.Radial90:
                    targetImage.fillOrigin = 0; // 可以根据需要调整
                    break;
                case Image.FillMethod.Radial180:
                    targetImage.fillOrigin = 0; // 可以根据需要调整
                    break;
                case Image.FillMethod.Radial360:
                    targetImage.fillOrigin = 0; // 可以根据需要调整
                    break;
            }

            // 初始填充amount为0
            targetImage.fillAmount = 0f;
        }
        else
        {
            Debug.LogError("No Image component found!");
        }
    }

    // 可选：提供一个动态更新Fill Method的方法
    public void SetFillMethod(Image.FillMethod method)
    {
        if (targetImage != null)
        {
            targetImage.fillMethod = method;
        }
    }
}