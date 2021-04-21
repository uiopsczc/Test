#if UNITY_EDITOR
using UnityEditorInternal;
using System.Collections;

namespace CsCat
{
  public class ReorderableListInfo : ICopyable
  {
    public IList to_reorder_list;
    private GUIToggleTween toggleTween = new GUIToggleTween();
    public ReorderableList _reorderableList;
    public ReorderableList reorderableList
    {
      get
      {
        to_reorder_list.ToReorderableList(ref _reorderableList);
        return _reorderableList;
      }
    }

    public ReorderableListInfo(IList to_reorder_list)
    {
      this.to_reorder_list = to_reorder_list;
    }

    public void SetElementHeight(float element_height)
    {
      this.reorderableList.SetElementHeight(element_height);
    }

    public void DrawGUI(string title)
    {
      this.reorderableList.DrawGUI(toggleTween, title);
    }

    public void CopyTo(object dest)
    {
      var _dest = dest as ReorderableListInfo;
      _dest.to_reorder_list.Clear();
      foreach (var to_reorder_element in to_reorder_list)
      {
        if (to_reorder_element is ICopyable)
        {
          var _to_reorder_element = (ICopyable)to_reorder_element;
          var _dest_clone_elemnt = _to_reorder_element.GetType().CreateInstance<ICopyable>();
          _to_reorder_element.CopyTo(_dest_clone_elemnt);
          _dest.to_reorder_list.Add(_dest_clone_elemnt);
        }
        else
          _dest.to_reorder_list.Add(to_reorder_element);
      }

      _dest.to_reorder_list.ToReorderableList(ref _dest._reorderableList);
    }

    public void CopyFrom(object source)
    {
      var _source = source as ReorderableListInfo;
      to_reorder_list.Clear();
      foreach (var source_to_reorder_element in _source.to_reorder_list)
      {
        if (source_to_reorder_element is ICopyable _source_to_reorder_element)
        {
          var clone_elemnt = _source_to_reorder_element.GetType().CreateInstance<ICopyable>();
          clone_elemnt.CopyFrom(_source_to_reorder_element);
          to_reorder_list.Add(clone_elemnt);
        }
        else
          to_reorder_list.Add(source_to_reorder_element);
      }
      to_reorder_list.ToReorderableList(ref _reorderableList);
    }
  }
}
#endif