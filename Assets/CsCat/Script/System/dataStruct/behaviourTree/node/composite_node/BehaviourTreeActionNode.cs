namespace CsCat
{
  public class BehaviourTreeActionNode : BehaviourTreeNode
  {
    #region override method

    public override BehaviourTreeNodeStatus Update()
    {
      if (status == BehaviourTreeNodeStatus.WaitingToRun)
        status = Enter();
      else
        status = Execute();

      if (status != BehaviourTreeNodeStatus.Running)
        Exit();
      return status;
    }

    #endregion

    #region virtual method

    public virtual BehaviourTreeNodeStatus Enter()
    {
      status = BehaviourTreeNodeStatus.Running;
      return status;
    }

    public virtual BehaviourTreeNodeStatus Execute()
    {
      return status;
    }

    public virtual void Exit()
    {
    }

    public virtual void OnInterrupt()
    {
    }

    public override void Interrupt()
    {
      OnInterrupt();
      base.Interrupt();
    }

    #endregion
  }
}