

using System.Collections.Generic;

namespace CsCat
{
  public class BlanceGroup
  {
    private int open_start_index;
    private int open_end_index;

    private int close_start_index;
    private int close_end_index;

    private List<BlanceGroup> children = new List<BlanceGroup>();

    public BlanceGroup()
    {
    }

    public void SetOpenIndexes(int open_start_index, int open_end_index)
    {
      this.open_start_index = open_start_index;
      this.open_end_index = open_end_index;
    }

    public void SetCloseIndexes(int close_start_index, int close_end_index)
    {
      this.close_start_index = close_start_index;
      this.close_end_index = close_end_index;
    }

    public string GetContent(string s)
    {
      return s.Substring(open_end_index + 1, close_start_index - open_end_index - 1);
    }


  }
}