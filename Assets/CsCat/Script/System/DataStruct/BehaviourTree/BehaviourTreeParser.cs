using System.Xml;

namespace CsCat
{
	public class BehaviourTreeParser
	{
		public static BehaviourTreeNode Parse(string path, object o = null)
		{
			var xml = new XmlDocument();
			xml.Load(path);
			var root_xmlNode = xml.FirstChild;
			var root_node = ParseNode(root_xmlNode, o);
			return root_node;
		}

		public static BehaviourTreeNode GetRefNode(string value, object o = null)
		{
			var xml = new XmlDocument();
			xml.Load("Assets/cscat/dataStruct/behaviourTree/config/base.xml");
			XmlNode xmlNode = null;
			foreach (XmlNode child in xml.FirstChild.ChildNodes)
			{
				var child_value = XMLUtil.GetNodeAttrValue(child, "name", "");
				if (child_value == value)
					xmlNode = child;
			}

			if (xmlNode != null)
				return ParseNode(xmlNode, o);
			return null;
		}

		public static BehaviourTreeNode ParseNode(XmlNode parent_xmlNode, object o = null)
		{
			BehaviourTreeNode parent = null;
			if (parent_xmlNode.Name == "RefNode")
				parent = GetRefNode(XMLUtil.GetNodeAttrValue(parent_xmlNode, "ref", ""), o);
			if (parent_xmlNode.Name == "SelectorNode")
				parent = new SelectorNode();
			if (parent_xmlNode.Name == "ConditionNode")
				parent = new ConditionActionNode(XMLUtil.GetNodeAttrValue(parent_xmlNode, "condition", ""));
			if (parent_xmlNode.Name == "DecortorNode")
			{
				var untilStatus_s = XMLUtil.GetNodeAttrValue(parent_xmlNode, "untilStatus", "").ToLower();
				var untilStatus = untilStatus_s == "success" ? BehaviourTreeNodeStatus.Success : BehaviourTreeNodeStatus.Fail;
				parent = new DecortorNode(untilStatus);
			}

			if (parent_xmlNode.Name == "ParallelNode")
				parent = new ParallelNode();
			if (parent_xmlNode.Name == "ParallelNode2")
				parent = new ParallelNode2();
			if (parent_xmlNode.Name == "RandomSelectorNode")
				parent = new RandomSelectorNode();
			if (parent_xmlNode.Name == "SequenceNode")
				parent = new SequenceNode();


			foreach (XmlNode child_xmlNode in parent_xmlNode.ChildNodes)
			{
				var child = ParseNode(child_xmlNode, o);
				((BehaviourTreeCompositeNode)parent).AddChild(child);
			}

			return parent;
		}
	}
}