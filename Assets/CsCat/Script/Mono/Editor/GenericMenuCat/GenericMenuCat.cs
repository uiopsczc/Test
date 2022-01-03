using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEditor;

namespace CsCat
{
	public class GenericMenuCat
	{
		public GenericMenuItemInfo root;
		public string rootName;

		private Dictionary<string, (object sourceObject, object userData)> _argDict =
			new Dictionary<string, (object sourceObject, object userData)>();

		GenericMenu GetGenericMenu()
		{
			root.Sort(); //通过priority进行排序
			GenericMenu genericMenu = new GenericMenu();
			foreach (var e in root.GetLeafList())
			{
				string path = e.GetNamePath(rootName);
				if (e.isValidate && e.methodInfoValidate != null &&
				    !(bool) (e.methodInfoValidate.Invoke(e.methodInfoValidate.DeclaringType, new object[0])))
					genericMenu.AddDisabledItem(path.ToGUIContent());
				else
				{
					Delegate del = null;
					Type menuFunctionType = e.methodInfo.GetParameters().Length == 0
						? typeof(GenericMenu.MenuFunction)
						: typeof(GenericMenu.MenuFunction2);
					del = Delegate.CreateDelegate(menuFunctionType,
						e.methodInfo.GetParameters().Length == 0
							? null
							: _argDict[e.itemName].sourceObject, e.methodInfo);
					if (e.methodInfo.GetParameters().Length == 0)
					{
						GenericMenu.MenuFunction myMethod = del as GenericMenu.MenuFunction;
						genericMenu.AddItem(path.ToGUIContent(), false, myMethod);
					}
					else
					{
						GenericMenu.MenuFunction2 myMethod = del as GenericMenu.MenuFunction2;
						genericMenu.AddItem(path.ToGUIContent(), false, myMethod,
							_argDict[e.itemName].userData);
					}
				}
			}

			return genericMenu;
		}


		public void InitOrUpdateRoot(GenericMenuItemAttribute menuItemAttribute, MethodInfo methodInfo)
		{
			if (root == null)
				InitRoot(menuItemAttribute, methodInfo);
			else
				UpdateRoot(menuItemAttribute, methodInfo);
		}

		public void InitRoot(GenericMenuItemAttribute genericMenuItemAttribute, MethodInfo methodInfo)
		{
			this.rootName = genericMenuItemAttribute.rootName;
			genericMenuItemAttribute.currentNameIndex = 0;
			root = new GenericMenuItemInfo(null, genericMenuItemAttribute, methodInfo);
		}

		public void UpdateRoot(GenericMenuItemAttribute genericMenuItemAttribute, MethodInfo methodInfo)
		{
			this.rootName = genericMenuItemAttribute.rootName;
			genericMenuItemAttribute.currentNameIndex = 0;
			root.Update(genericMenuItemAttribute, methodInfo);
		}

		private void GenArgDict((string itemName, object sourceObject, object userData)[] args)
		{
			_argDict.Clear();
			if (!args.IsNullOrEmpty())
			{
				foreach (var arg in args)
					_argDict[arg.itemName] = (arg.sourceObject, arg.userData);
			}
		}

		public void Show(params (string itemName, object sourceObject, object userData)[] args)
		{
			GenArgDict(args);
			GetGenericMenu().ShowAsContext();
		}

		public void Show(Rect position,
			params (string itemName, object sourceObject, object userData)[] args)
		{
			GenArgDict(args);
			GetGenericMenu().DropDown(position);
		}
	}
}