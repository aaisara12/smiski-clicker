
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class UiConverters
{
    [InitializeOnLoadMethod]
    public static void InitConverters()
    {
        // Create a converter group
        var group = new ConverterGroup("TimeleftToColorConverter");

        // Add converters to the converter group
        group.AddConverter((ref int input) => TimeLeftToColor(input));
        
        // Register the converter with a unique name
        ConverterGroups.RegisterConverterGroup(group);
    }

    private static StyleColor TimeLeftToColor(int timeLeft)
    {
        Color.RGBToHSV(Color.red, out float h1, out float _, out float _);
        Color.RGBToHSV(Color.green, out float h2, out float _, out float _);
        var newColor = Color.HSVToRGB(Mathf.Lerp(h1, h2, timeLeft/10f), 1f, 1f);
        
        return new StyleColor(newColor);
    }
}
