using System.IO;
using UnityEditor;
using UnityEngine;
using System.Collections.Generic;

public class PanelTool : EditorWindow
{
    private static Dictionary<string, string> uiTypeDic = new Dictionary<string, string>()
    {
        {"bt", "Button"},
        {"img", "Image"},
        {"go", "GameObject"},
        {"tmp", "TextMeshProUGUI"},
        {"t", "Text"},
        {"tmpInput", "TMP_InputField"},
        {"slider", "Slider"},
        {"scroll", "ScrollRect"},
        {"dropdown","Dropdown"},
        {"tmpDropdown","TMP_Dropdown"}
    };

    [MenuItem("Tools/Generate UIPanel Scripts")]
    public static void GenerateUIPanelScripts()
    {
        string prefabFolder = "Assets/AssetPackage/Prefab/UIPanel";
        string scriptFolder = "Assets/Scripts/System/UI/Panels";

        if (!Directory.Exists(scriptFolder))
        {
            Directory.CreateDirectory(scriptFolder);
        }

        foreach (string prefabPath in Directory.GetFiles(prefabFolder, "*.prefab"))
        {
            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(prefabPath);
            if (prefab != null)
            {
                string className = prefab.name + "UI";
                string scriptPath = Path.Combine(scriptFolder, className + ".cs");

                // 检查脚本是否已经存在
                if (!File.Exists(scriptPath))
                {
                    GenerateScriptForPrefab(prefab, scriptPath);
                }
                else
                {
                    Debug.Log($"Script for {className} already exists, skipping generation.");
                }
            }
        }

        AssetDatabase.Refresh();
    }



    private static void GenerateScriptForPrefab(GameObject prefab, string scriptFolder)
    {
        
        string className = prefab.name + "UI";



        using (StreamWriter writer = new StreamWriter(scriptFolder))
        {
            writer.WriteLine("using UnityEngine;");
            writer.WriteLine("using UnityEngine.UI;");
            writer.WriteLine("using TMPro;");
            writer.WriteLine("using MY_Framework.UI;");
            writer.WriteLine();
            writer.WriteLine($"public class {className} : BasePanel");
            writer.WriteLine("{");

            // 声明字段
            writer.WriteLine("\tconst string panelName=\"" + prefab.name + "\";");
            Transform[] allChildren = prefab.GetComponentsInChildren<Transform>(true);
            foreach (Transform child in allChildren)
            {
                string[] nameParts = child.name.Split('_');
                if (nameParts.Length > 1 && uiTypeDic.TryGetValue(nameParts[0], out string uiType))
                {
                    writer.WriteLine($"\t[SerializeField] private {uiType} {child.name};");
                }
            }

            writer.WriteLine();
            writer.WriteLine("\tpublic void Awake()");
            writer.WriteLine("\t{");
            writer.WriteLine();
            // 生成控件的赋值语句
            foreach (Transform child in allChildren)
            {
                string[] nameParts = child.name.Split('_');
                if (nameParts.Length > 1 && uiTypeDic.TryGetValue(nameParts[0], out string uiType))
                {
                    writer.WriteLine($"\t\t{child.name} = transform.Find(\"{GetTransformPath(prefab.transform, child)}\").GetComponent<{uiType}>();");
                }
            }

            writer.WriteLine("\t}");
            writer.WriteLine("}");
        }
    }
    private static string GetTransformPath(Transform root, Transform target)
    {
        string path = target.name;
        while (target.parent != null && target.parent != root)
        {
            target = target.parent;
            path = target.name + "/" + path;
        }
        return path;
    }
}
