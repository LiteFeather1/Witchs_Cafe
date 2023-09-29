using UnityEngine;

[System.Serializable]
public class TranslatedString
{
    [SerializeField, TextArea(1, 6)] private string en_string;
    [SerializeField, TextArea(1, 6)] private string pt_string;

    public string EN_String => en_string;
    public string PT_String => pt_string;

    public string String(Languages lang)
    {
        return lang switch
        {
            Languages.English => en_string,
            Languages.Portuguese => pt_string,
            _ => "Not Supported Language",
        };
    }
}
