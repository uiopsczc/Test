using UnityEditor;
using UnityEngine;

namespace CsCat
{
	/// <summary>
	///   CZM工具菜单
	/// </summary>
	public partial class CZMToolMenu
	{
		static UnityEngine.Component[] copiedComponents;

		[MenuItem(CZMToolConst.Menu_Root + "Component/Copy All Components #C")]
		//crtl+alt+shift+c
		static void CopyAllComponents()
		{
			if (Selection.activeGameObject == null)
				return;

			copiedComponents = Selection.activeGameObject.GetComponents<UnityEngine.Component>();
			Debug.LogWarning("Copyed");
		}

		[MenuItem(CZMToolConst.Menu_Root + "Component/Paste All Components #P")]
		//crtl+alt+shift+P
		static void PasteAllComponents()
		{
			if (copiedComponents == null)
			{
				Debug.LogError("Nothing is copied!");
				return;
			}

			foreach (var targetGameObject in Selection.gameObjects)
			{
				if (!targetGameObject)
					continue;

				Undo.RegisterCompleteObjectUndo(targetGameObject, targetGameObject.name + ": Paste All Components");

				foreach (var copiedComponent in copiedComponents)
				{
					if (!copiedComponent)
						continue;

					UnityEditorInternal.ComponentUtility.CopyComponent(copiedComponent);

					var targetComponent = targetGameObject.GetComponent(copiedComponent.GetType());

					if (targetComponent) // if gameObject already contains the component
					{
						if (!UnityEditorInternal.ComponentUtility.PasteComponentValues(targetComponent))
							Debug.LogError("Failed to copy: " + copiedComponent.GetType());
					}
					else // if gameObject does not contain the component
					{
						if (!UnityEditorInternal.ComponentUtility.PasteComponentAsNew(targetGameObject))
							Debug.LogError("Failed to copy: " + copiedComponent.GetType());
					}
				}
			}

			copiedComponents = null; // to prevent wrong pastes in future
			Debug.LogWarning("Paseted");
		}
	}
}