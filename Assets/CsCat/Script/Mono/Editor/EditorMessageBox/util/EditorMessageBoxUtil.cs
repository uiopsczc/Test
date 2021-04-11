using System;
using System.Collections;
using UnityEditor;
using UnityEngine;

namespace CsCat
{
  public static class EditorMessageBoxUtil
  {
    public static EditorMessageBox Show(string title, string content,string button1_text, Action on_button1_callback=null, string button2_text=null, Action on_button2_callback=null)
    {
      EditorMessageBox editorMessageBox= EditorWindow.CreateWindow<EditorMessageBox>();
      editorMessageBox.minSize = new Vector2(EditorMessageBoxConst.Width, EditorMessageBoxConst.Height);
      editorMessageBox.maxSize = editorMessageBox.minSize;
      editorMessageBox.message_title = title;
      editorMessageBox.content = content;

      editorMessageBox.button1_text = button1_text;
      editorMessageBox.on_button1_callback = on_button1_callback;

      editorMessageBox.button2_text = button2_text;
      editorMessageBox.on_button2_callback = on_button2_callback;


      editorMessageBox.Show();

      return editorMessageBox;
    }

    public static EditorMessageBox FadeShow(string title, string content, float duration=1f)
    {
      var editorMessageBox = Show(title, content,null);
      editorMessageBox.minSize = new Vector2(EditorMessageBoxConst.Width, EditorMessageBoxConst.Height/2);
      editorMessageBox.maxSize = editorMessageBox.minSize;
      PausableCoroutineManager.instance.StartCoroutine(IEFade(editorMessageBox, duration), editorMessageBox);
      return editorMessageBox;
    }

    private static IEnumerator IEFade(EditorMessageBox editorMessageBox, float duration)
    {
      yield return new WaitForSeconds(duration);
      editorMessageBox.Close();
    }
  }
}