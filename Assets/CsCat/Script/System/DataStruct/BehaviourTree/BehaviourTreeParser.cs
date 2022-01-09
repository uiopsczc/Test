using System.Xml;

namespace CsCat
{
	public class BehaviourTreeParser
	{
		public static BehaviourTreeNode Parse(string path, object o = null)
		{
			var xml = new XmlDocument();
			xml.Load(path);
			var rootXMLNode = xml.FirstChild;
			var rootNode = ParseNode(rootXMLNode, o);
			return rootNode;
		}

		public static BehaviourTreeNode GetRefNode(string value, object o = null)
		{
			var xml = new XmlDocument();
			xml.Load("Assets/cscat/dataStruct/behaviourTree/config/base.xml");
			XmlNode xmlNode = null;
			foreach (XmlNode child in xml.FirstChild.ChildNodes)
			{
				var childValue = XMLUtil.GetNodeAttrValue(child, "name", "");
				if (childValue == value)
					xmlNode = child;
			}

			if (xmlNode != null)
				return ParseNode(xmlNode, o);
			return null;
		}

		public static BehaviourTreeNode ParseNode(XmlNode parentXMLNode, object o = null)
		{
			BehaviourTreeNode parent = null;
			if (parentXMLNode.Name == "RefNode")
				parent = GetRefNode(XMLUtil.GetNodeAttrValue(parentXMLNode, "ref", ""), o);
			if (parentXMLNode.Name == "SelectorNode")
				parent = new SelectorNode();
			if (parentXMLNode.Name == "ConditionNode")
				parent = new ConditionActionNode(XMLUtil.GetNodeAttrValue(parentXMLNode, "condition", ""));
			if (parentXMLNode.Name == "DecortorNode")
			{
				var untilStatusString = XMLUtil.GetNodeAttrValue(parentXMLNode, "untilStatus", "").ToLower();
				var untilStatus = untilStatusString == "success" ? BehaviourTreeNodeStatus.Success : BehaviourTreeNodeStatus.Fail;
				parent = new DecortorNode(untilStatus);
			}

			if (parentXMLNode.Name == "ParallelNode")
				parent = new ParallelNode();
			if (parentXMLNode.Name == "ParallelNode2")
				parent = new ParallelNode2();
			if (parentXMLNode.Name == "RandomSelectorNode")
				parent = new RandomSelectorNode();
			if (parentXMLNode.Name == "SequenceNode")
				parent = new SequenceNode();


			for (var i = 0; i < parentXMLNode.ChildNodes.Count; i++)
			{
				XmlNode childXMLNode = parentXMLNode.ChildNodes[i];
				var child = ParseNode(childXMLNode, o);
				((BehaviourTreeCompositeNode) parent).AddChild(child);
			}

			return parent;
		}
	}
}