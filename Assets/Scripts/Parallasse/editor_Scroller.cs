using UnityEngine;
using UnityEditor;
using UnityEngine.UI;

[CustomEditor(typeof(Scroller))]
public class editor_Scroller : Editor
{
    public override void OnInspectorGUI()
    {
        // Ottieni il riferimento alla classe Scroller
        Scroller scroller = (Scroller)target;

        // Titolo personalizzato
        EditorGUILayout.LabelField("Scroller Settings", EditorStyles.boldLabel);

        // Dropdown per selezionare il metodo di scrolling
        scroller.scrollMethod = (Scroller.ScrollMethod)EditorGUILayout.EnumPopup("Scroll Method", scroller.scrollMethod);

        // Mostra i campi per RawImage
        scroller._img = (RawImage)EditorGUILayout.ObjectField("Raw Image", scroller._img, typeof(RawImage), true);

        // Mostra i campi per X e Y
        scroller._x = EditorGUILayout.FloatField("Scroll Speed X", scroller._x);
        scroller._y = EditorGUILayout.FloatField("Scroll Speed Y", scroller._y);

        // Mostra il riferimento al giocatore e il moltiplicatore solo se il metodo è PlayerMovement
        if (scroller.scrollMethod == Scroller.ScrollMethod.PlayerMovement)
        {
            scroller.player = (GameObject)EditorGUILayout.ObjectField("Player", scroller.player, typeof(GameObject), true);
            scroller.scrollMultiplier = EditorGUILayout.FloatField("Scroll Multiplier", scroller.scrollMultiplier);
        }

        // Applica le modifiche
        if (GUI.changed)
        {
            EditorUtility.SetDirty(scroller);
        }
    }
}
