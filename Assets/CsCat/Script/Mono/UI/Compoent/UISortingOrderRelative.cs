using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
  //canvas有被设置，用canvas的sortingOrder，否则用gameObject的parent中的Canvas
  public class UISortingOrderRelative : MonoBehaviour
  {
    public Canvas canvas;
    private readonly List<Renderer> children_renderer_list = new List<Renderer>();
    public int last_sorting_order = -1;

    public int relative_sorting_order = 0;

    public void Start()
    {
      if (canvas == null)
      {
        canvas = gameObject.GetComponentInParent<Canvas>();
        if (canvas == null)
          return;
      }


      gameObject.GetComponentsInChildren(true, children_renderer_list);

      var sorting_order = canvas.sortingOrder + relative_sorting_order;
      UpdateChildrenSortingOrder(sorting_order);
    }


    private void UpdateChildrenSortingOrder(int sorting_order)
    {
      for (var i = 0; i < children_renderer_list.Count; ++i)
        if (children_renderer_list[i] != null)
          children_renderer_list[i].sortingOrder = sorting_order;
      last_sorting_order = sorting_order;
    }


    private void Update()
    {
      if (canvas == null)
        return;

      var sorting_order = canvas.sortingOrder + relative_sorting_order;
      if (last_sorting_order == sorting_order)
        return;

      UpdateChildrenSortingOrder(sorting_order);
    }
  }
}