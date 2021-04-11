using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace CsCat
{
  public class BlanceGroupManager
  {
    private BlanceGroupDefinition[] blanceGroupDefinitions;

    public BlanceGroupManager(params BlanceGroupDefinition[] blanceGroupDefinitions)
    {
      this.blanceGroupDefinitions = blanceGroupDefinitions;
    }

    public void Parse(string content)
    {
      int start_index = 0;
      int index;
      Regex regex;
      Match match;
      Stack<BlanceGroup> blanceGroup_stack = new Stack<BlanceGroup>();
      while (start_index != content.Length - 1)
      {
        foreach (var blanceGroupDefinition in blanceGroupDefinitions)
        {
          regex = new Regex(blanceGroupDefinition.open_regex_pattern);
          match = regex.Match(content, start_index);
          if (match.Index == start_index)
          {
            BlanceGroup blanceGroup = new BlanceGroup();
            blanceGroup.SetOpenIndexes(start_index, start_index + match.Length);
            blanceGroup_stack.Push(blanceGroup);
            start_index = start_index + match.Length;
            break;
          }

          regex = new Regex(blanceGroupDefinition.close_regex_pattern);
          match = regex.Match(content, start_index);
          if (match.Index == start_index)
          {
            BlanceGroup blanceGroup = blanceGroup_stack.Pop();
            blanceGroup.SetCloseIndexes(start_index, start_index + match.Length);
            start_index = start_index + match.Length;
            break;
          }
        }
      }
    }
  }
}