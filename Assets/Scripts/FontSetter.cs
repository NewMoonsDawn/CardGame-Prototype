using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class FontSetter : MonoBehaviour
{
    public FontTypes fontType;
    private void OnEnable()
    {
        OptionsManager.FontUpdated += SetFont;
        SetFont();
    }
    private void OnDisable()
    {
        OptionsManager.FontUpdated -= SetFont;
    }

    private void SetFont()
    {
        TMP_Text textComponent = GetComponent<TMP_Text>();
        if (textComponent &&  GameManager.Instance.optionsManager !=null)
        {
            textComponent.font = GameManager.Instance.optionsManager.GetFontClass(fontType.ToString());
        }
    }
    public enum FontTypes
    {
        MenuText,
        CardTitle,
        CardBody,
        CardBodyBold,
        MenuTextBold
    }
}
