namespace CsCat
{
  public class SequenceNode : BehaviourTreeCompositeNode
  {
    #region override method

    public override BehaviourTreeNodeStatus Update()
    {
      if (child_list == null || child_list.Count == 0)
      {
        status = BehaviourTreeNodeStatus.Success;
        return status;
      }

      foreach (var child in child_list)
      {
        var child_status = child.Update();
        if (child_status == BehaviourTreeNodeStatus.Running || child_status == BehaviourTreeNodeStatus.Fail)
        {
          status = child_status;
          return status;
        }
      }

      status = BehaviourTreeNodeStatus.Success;
      return status;
    }

    #endregion
  }
}