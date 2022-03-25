using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public partial class GameObjectEntity
	{
		public TransformComponent GetTransformComponent()
		{
			return this.transformComponent;
		}

		public ResLoadComponent GetResLoadComponent()
		{
			return this.resLoadComponent;
		}

		public GraphicComponent GetGraphicComponent()
		{
			return this.graphicComponent;
		}

		public GameObject GetGameObject()
		{
			return this.GetGraphicComponent().GetGameObject();
		}

		public Transform GetTransform()
		{
			return this.GetGraphicComponent().GetTransform();
		}

		public RectTransform GetRectTransform()
		{
			return this.GetGraphicComponent().GetRectTransform();
		}

		public void SetLocalPosition(Vector3 localPosition)
		{
			this.transformComponent.SetLocalPosition(localPosition);
			this.graphicComponent.SetLocalPosition(localPosition);
		}

		public Vector3 GetLocalPosition()
		{
			return this.transformComponent.GetLocalPosition();
		}

		public void SetLocalEulerAngles(Vector3 localEulerAngles)
		{
			this.transformComponent.SetLocalEulerAngles(localEulerAngles);
			this.graphicComponent.SetLocalEulerAngles(localEulerAngles);
		}

		public Vector3 GetLocalEulerAngles()
		{
			return this.transformComponent.GetLocalEulerAngles();
		}

		public void SetLocalRotation(Quaternion localRotation)
		{
			this.transformComponent.SetLocalRotation(localRotation);
			this.graphicComponent.SetLocalRotation(localRotation);
		}


		public Quaternion GetLocalRotation()
		{
			return this.transformComponent.GetLocalRotation();
		}

		public void SetLocalScale(Vector3 localScale)
		{
			this.transformComponent.SetLocalScale(localScale);
			this.graphicComponent.SetLocalScale(localScale);
		}

		public Vector3 GetLocalScale()
		{
			return this.transformComponent.GetLocalScale();
		}

		public void SetPosition(Vector3 position)
		{
			this.transformComponent.SetPosition(position);
			this.graphicComponent.SetPosition(position);
		}

		public Vector3 GetPosition()
		{
			return this.transformComponent.GetPosition();
		}

		public void SetEulerAngles(Vector3 eulerAngles)
		{
			this.transformComponent.SetEulerAngles(eulerAngles);
			this.graphicComponent.SetEulerAngles(eulerAngles);
		}

		public Vector3 GetEulerAngles()
		{
			return this.transformComponent.GetEulerAngles();
		}

		public void SetRotation(Quaternion rotation)
		{
			this.transformComponent.SetRotation(rotation);
			this.graphicComponent.SetRotation(rotation);
		}

		public Quaternion GetRotation()
		{
			return this.transformComponent.GetRotation();
		}

		public void SetScale(Vector3 scale)
		{
			this.transformComponent.SetScale(scale);
			this.graphicComponent.SetScale(scale);
		}

		public Vector3 GetScale()
		{
			return this.transformComponent.GetScale();
		}
	}
}