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
        // ���û����Inspector��ָ�������Ի�ȡ��ǰGameObject��Image���
        if (targetImage == null)
        {
            targetImage = GetComponent<Image>();
        }

        // ����Fill Method
        if (targetImage != null)
        {
            targetImage.type = Image.Type.Filled;
            targetImage.fillMethod = fillMethod;

            // ����ѡ�����䷽��������ʼλ��
            switch (fillMethod)
            {
                case Image.FillMethod.Horizontal:
                    targetImage.fillOrigin = (int)horizontalOrigin;
                    break;
                case Image.FillMethod.Vertical:
                    targetImage.fillOrigin = (int)verticalOrigin;
                    break;
                case Image.FillMethod.Radial90:
                    targetImage.fillOrigin = 0; // ���Ը�����Ҫ����
                    break;
                case Image.FillMethod.Radial180:
                    targetImage.fillOrigin = 0; // ���Ը�����Ҫ����
                    break;
                case Image.FillMethod.Radial360:
                    targetImage.fillOrigin = 0; // ���Ը�����Ҫ����
                    break;
            }

            // ��ʼ���amountΪ0
            targetImage.fillAmount = 0f;
        }
        else
        {
            Debug.LogError("No Image component found!");
        }
    }

    // ��ѡ���ṩһ����̬����Fill Method�ķ���
    public void SetFillMethod(Image.FillMethod method)
    {
        if (targetImage != null)
        {
            targetImage.fillMethod = method;
        }
    }
}