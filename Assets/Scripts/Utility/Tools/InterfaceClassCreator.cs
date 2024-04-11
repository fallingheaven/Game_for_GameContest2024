using UnityEngine;
using UnityEditor;

public class InterfaceClassCreator : EditorWindow
{
    private static string _path;
    private static string _interfaceName = "IEventMessage";
    private string _className;

    [MenuItem("Tools/Create Interface Class")]
    public static void ShowWindow()
    {
        GetWindow<InterfaceClassCreator>("Create Interface Class");
    }

    private void OnGUI()
    {
        GUILayout.Label("输入接口名字：");
        _interfaceName = EditorGUILayout.TextField(_interfaceName);

        GUILayout.Label("输入类的名字：");
        _className = EditorGUILayout.TextField(_className);
        
        if (GUILayout.Button("创建"))
        {
            CreateClass();
        }
    }

    private void CreateClass()
    {
        if (string.IsNullOrEmpty(_interfaceName))
        {
            Debug.LogError("请输入接口名");
            return;
        }

        if (string.IsNullOrEmpty(_className))
        {
            Debug.LogError("请输入类名");
        }

        var template = 
$@"using UnityEngine;
using System;
using Utility.Interface;

public class {_className} : {_interfaceName}
{{
    
}}
";

        var path = EditorUtility.SaveFilePanel("保存脚本", "Assets/Scripts", _className, "cs");
        if (!string.IsNullOrEmpty(path))
        {
            System.IO.File.WriteAllText(path, template);
            AssetDatabase.Refresh();
            Debug.Log($"{path}创建成功");
        }
    }
}