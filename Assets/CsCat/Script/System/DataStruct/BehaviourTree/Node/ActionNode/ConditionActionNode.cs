namespace CsCat
{
	public class ConditionActionNode : BehaviourTreeActionNode
	{
		#region field

		public string condition;

		#endregion

		#region ctor

		public ConditionActionNode(string condition)
		{
			this.condition = condition;
		}

		#endregion

		#region public method

		#region virtual method

		public virtual bool ParseCondition()
		{
			//{
			//    //根据Condition解析，返回true或false;
			//    string tmp = GameParser.Parse(condition, this);
			//    RPN rpn = new RPN();
			//    bool result = Convert.ToBoolean(rpn.Evaluate(tmp));
			//    return result;
			//}
			return true;
		}

		#endregion

		#region override method

		public override BehaviourTreeNodeStatus Update()
		{
			var match = ParseCondition();
			if (match)
				status = BehaviourTreeNodeStatus.Success;
			else
				status = BehaviourTreeNodeStatus.Fail;
			return status;
		}

		#endregion

		#endregion
	}
}