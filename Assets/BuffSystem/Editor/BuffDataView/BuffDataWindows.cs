using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System;
using System.Collections.Generic;
using PlasticGui.WorkspaceWindow.Items;
using System.Linq;
using System.ComponentModel;

public class BuffDataWindows : EditorWindow
{
    ListView listView;
    List<BuffData> buffDataList;
    IMGUIContainer buffInspector;
    [MenuItem("Window/BuffSystem/BuffDataWindows")]
    public static void ShowExample()
    {
        BuffDataWindows wnd = GetWindow<BuffDataWindows>();
        wnd.titleContent = new GUIContent("BuffDataWindows");
    }

    public void CreateGUI()
    {
        // Each editor window contains a root VisualElement object
        VisualElement root = rootVisualElement;

        // Import UXML
        var visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/BuffSystem/Editor/BuffDataView/BuffDataWindows.uxml");
        VisualElement labelFromUXML = visualTree.Instantiate();
        root.Add(labelFromUXML);

        //var buffDataBtnPrefab = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/BuffSystem/Editor/BuffDataView/BuffDataBtn.uxml");
        listView = root.Q<ListView>();

        buffInspector = root.Q<IMGUIContainer>();

        FindAllBuffData();


        Func<VisualElement> makeItem = () => new Label(); //buffDataBtnPrefab.Instantiate();
        //Func<VisualElement> makeItem = () => buffDataBtnPrefab.Instantiate();
        Action<VisualElement, int> bindItem = (e, i) =>
        {
            (e as Label).text = buffDataList[i].buffName;
        };

        listView.fixedItemHeight = 20;

        listView.makeItem = makeItem;
        listView.bindItem = bindItem;
        listView.itemsSource = buffDataList;
        listView.selectionType = SelectionType.Single;


        listView.onSelectionChange += OnListSelectionChanged;
    }

    private void OnListSelectionChanged(IEnumerable<object> enumerable)
    {
        BuffData buffData = enumerable.First() as BuffData;
        if (buffData == null)
            return;

        buffInspector.Clear();

        Editor editor = Editor.CreateEditor(buffData);
        buffInspector.onGUIHandler = () =>
        {
            if (buffData != null)
                editor.OnInspectorGUI();
        };
    }

    private void FindAllBuffData()
    {
        string[] allObjGuids =  AssetDatabase.FindAssets("t:BuffData", new string[] { "Assets/BuffSystem/Runtime/Data/BuffDataSO" });
        buffDataList = new List<BuffData>();
        foreach (string guid in allObjGuids) 
        {
            buffDataList.Add(AssetDatabase.LoadAssetAtPath<BuffData>(AssetDatabase.GUIDToAssetPath(guid)));
        }
        Debug.Log(buffDataList.Count);
    }
}