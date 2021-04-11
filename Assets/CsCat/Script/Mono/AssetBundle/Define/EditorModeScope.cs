using System;

namespace CsCat
{
  public class EditorModeScope : IDisposable
  {
    private bool org_is_editor_mode;

    public EditorModeScope(bool is_editor_mode)
    {
      org_is_editor_mode = EditorModeConst.Is_Editor_Mode;
      EditorModeConst.Is_Editor_Mode = is_editor_mode;
    }

    public void Dispose()
    {
      EditorModeConst.Is_Editor_Mode = org_is_editor_mode;
    }
  }
}