using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.IMGUI.Controls;
using UnityEngine;

//https://github.com/marijnz/unity-autocomplete-search-field
[Serializable]
public class AutoCompleteSearchField
{
    public Action<string> onInputChanged;
    public Action<KeyValuePair<string, object>> onConfirm;
    public string searchString = "";
    public int maxResults = 15;

    [SerializeField]
    List<KeyValuePair<string, object>> results = new List<KeyValuePair<string, object>>();

    [SerializeField]
    public int selectedIndex = -1;

    public SearchField searchField;

    public Vector2 previousMousePosition;
    public bool selectedIndexByMouse;

    public bool showResults;


    public void AddResult(string result, object resultObj = null)
    {
        results.Add(new KeyValuePair<string, object>(result, resultObj));
    }

    public void ClearResults()
    {
        results.Clear();
    }

    public void OnToolbarGUI()
    {
        Draw(asToolbar: true);
    }

    public void OnGUI()
    {
        Draw(asToolbar: false);
    }

    void Draw(bool asToolbar)
    {
        var rect = GUILayoutUtility.GetRect(1, 1, 18, 18, GUILayout.ExpandWidth(true));
        GUILayout.BeginHorizontal();
        DoSearchField(rect, asToolbar);
        GUILayout.EndHorizontal();
        rect.y += 18;
        DoResults(rect);
    }

    void DoSearchField(Rect rect, bool asToolbar)
    {

        if (searchField == null)
        {
            searchField = new SearchField();
            searchField.downOrUpArrowKeyPressed += OnDownOrUpArrowKeyPressed;
        }

        var result = asToolbar
            ? searchField.OnToolbarGUI(rect, searchString)
            : searchField.OnGUI(rect, searchString);


        if (result != searchString && onInputChanged != null)
        {
            onInputChanged(result);
            selectedIndex = -1;
            showResults = true;
        }

        searchString = result;

        if (HasSearchbarFocused())
        {
            RepaintFocusedWindow();
        }
    }

    void OnDownOrUpArrowKeyPressed()
    {
        var current = Event.current;

        if (current.keyCode == KeyCode.UpArrow)
        {
            current.Use();
            selectedIndex--;
            selectedIndexByMouse = false;
        }
        else
        {
            current.Use();
            selectedIndex++;
            selectedIndexByMouse = false;
        }

        if (selectedIndex >= results.Count) selectedIndex = results.Count - 1;
        else if (selectedIndex < 0) selectedIndex = -1;
    }

    void DoResults(Rect rect)
    {
        if (results.Count <= 0 || !showResults) return;

        var current = Event.current;
        rect.height = AutoCompleteSearchFieldStyles.resultHeight * Mathf.Min(maxResults, results.Count);
        rect.width -= AutoCompleteSearchFieldStyles.resultsMargin * 2;

        var elementRect = rect;



        rect.height += AutoCompleteSearchFieldStyles.resultsBorderWidth;
        GUI.Label(rect, "", AutoCompleteSearchFieldStyles.resultsBorderStyle);

        var mouseIsInResultsRect = rect.Contains(current.mousePosition);

        if (mouseIsInResultsRect)
        {
            RepaintFocusedWindow();
        }

        var movedMouseInRect = previousMousePosition != current.mousePosition;

        elementRect.x += AutoCompleteSearchFieldStyles.resultsBorderWidth;
        elementRect.width -= AutoCompleteSearchFieldStyles.resultsBorderWidth * 2;
        elementRect.height = AutoCompleteSearchFieldStyles.resultHeight;

        var didJustSelectIndex = false;

        for (var i = 0; i < results.Count && i < maxResults; i++)
        {
            if (current.type == EventType.Repaint)
            {
                var style = i % 2 == 0 ? AutoCompleteSearchFieldStyles.entryOdd : AutoCompleteSearchFieldStyles.entryEven;

                style.Draw(elementRect, false, false, i == selectedIndex, false);

                var labelRect = elementRect;
                labelRect.x += AutoCompleteSearchFieldStyles.resultsLabelOffset;
                GUI.Label(labelRect, results[i].Key, AutoCompleteSearchFieldStyles.labelStyle);
            }
            if (elementRect.Contains(current.mousePosition))
            {
                if (movedMouseInRect)
                {
                    selectedIndex = i;
                    selectedIndexByMouse = true;
                    didJustSelectIndex = true;
                }
                if (current.type == EventType.MouseDown)
                {
                    OnConfirm(results[i]);
                    current.Use();
                }
            }
            elementRect.y += AutoCompleteSearchFieldStyles.resultHeight;
        }

        if (current.type == EventType.Repaint && !didJustSelectIndex && !mouseIsInResultsRect && selectedIndexByMouse)
        {
            selectedIndex = -1;
        }

        if ((GUIUtility.hotControl != searchField.searchFieldControlID && GUIUtility.hotControl > 0)
            || (current.rawType == EventType.MouseDown && !mouseIsInResultsRect))
        {
            showResults = false;
        }

        if (current.type == EventType.KeyUp && current.keyCode == KeyCode.Return && selectedIndex >= 0)
        {
            OnConfirm(results[selectedIndex]);
            current.Use();
        }

        if (current.type == EventType.Repaint)
        {
            previousMousePosition = current.mousePosition;
        }
    }

    void OnConfirm(KeyValuePair<string, object> result)
    {
        searchString = result.Key;
        showResults = false;
        if (onConfirm != null) onConfirm(result);
        if (onInputChanged != null) onInputChanged(result.Key);
        RepaintFocusedWindow();
        GUIUtility.keyboardControl = 0; // To avoid Unity sometimes not updating the search field text
    }

    bool HasSearchbarFocused()
    {
        return GUIUtility.keyboardControl == searchField.searchFieldControlID;
    }

    static void RepaintFocusedWindow()
    {
        if (EditorWindow.focusedWindow != null)
        {
            EditorWindow.focusedWindow.Repaint();
        }
    }
}


public class PopupFieldsConent : PopupWindowContent
{
    public override Vector2 GetWindowSize()
    {
        return new Vector2(200, 150);
    }

    public override void OnGUI(Rect rect)
    {

    }


}