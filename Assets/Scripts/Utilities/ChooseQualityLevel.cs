using UnityEngine;

public class ChooseQualityLevel : MonoBehaviour
{
    void OnGUI()
    {
        string[] names = QualitySettings.names;

        GUILayout.BeginVertical();
        for (int i = 0; i < names.Length; i++)
        {
            if (GUILayout.Button(names[i]))
            {
                QualitySettings.SetQualityLevel(i, true);
                PlayerPrefs.SetInt("Quality", i);
                Debug.Log(i);
                this.enabled = false;
            }
        }
        GUILayout.EndVertical();
    }
}