//一个返回成功则成功，一个返回失败则失败
namespace CsCat
{
  public class ParallelNode2 : BehaviourTreeCompositeNode
  {
    #region override method

    public override BehaviourTreeNodeStatus Update()
    {
      if (child_list == null || child_list.Count == 0)
      {
        status = BehaviourTreeNodeStatus.Success;
        return status;
      }

      BehaviourTreeNodeStatus child_status;
      foreach (var child in child_list)
      {
        child_status = child.Update();
        if (child_status == BehaviourTreeNodeStatus.Fail)
        {
          status = BehaviourTreeNodeStatus.Fail;
          return status;
        }

        if (child_status == BehaviourTreeNodeStatus.Success)
        {
          status = BehaviourTreeNodeStatus.Success;
          return status;
        }
      }

      status = BehaviourTreeNodeStatus.Running;
      return status;
    }

    #endregion
  }
}