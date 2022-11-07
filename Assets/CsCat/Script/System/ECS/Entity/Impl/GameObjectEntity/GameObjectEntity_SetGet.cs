using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CsCat
{
	public partial class GameObjectEntity
	{
		public TransformInfoComponent GetTransformComponent()
		{
			return this.TransformInfoComponent;
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
			this.TransformInfoComponent.SetLocalPosition(localPosition);
			this.graphicComponent.SetLocalPosition(localPosition);
		}

		public Vector3 GetLocalPosition()
		{
			return this.TransformInfoComponent.GetLocalPosition();
		}

		public void SetLocalEulerAngles(Vector3 localEulerAngles)
		{
			this.TransformInfoComponent.SetLocalEulerAngles(localEulerAngles);
			this.graphicComponent.SetLocalEulerAngles(localEulerAngles);
		}

		public Vector3 GetLocalEulerAngles()
		{
			return this.TransformInfoComponent.GetLocalEulerAngles();
		}

		public void SetLocalRotation(Quaternion localRotation)
		{
			this.TransformInfoComponent.SetLocalRotation(localRotation);
			this.graphicComponent.SetLocalRotation(localRotation);
		}


		public Quaternion GetLocalRotation()
		{
			return this.TransformInfoComponent.GetLocalRotation();
		}

		public void SetLocalScale(Vector3 localScale)
		{
			this.TransformInfoComponent.SetLocalScale(localScale);
			this.graphicComponent.SetLocalScale(localScale);
		}

		public Vector3 GetLocalScale()
		{
			return this.TransformInfoComponent.GetLocalScale();
		}

		public void SetPosition(Vector3 position)
		{
			this.TransformInfoComponent.SetPosition(position);
			this.graphicComponent.SetPosition(position);
		}

		public Vector3 GetPosition()
		{
			return this.TransformInfoComponent.GetPosition();
		}

		public void SetEulerAngles(Vector3 eulerAngles)
		{
			this.TransformInfoComponent.SetEulerAngles(eulerAngles);
			this.graphicComponent.SetEulerAngles(eulerAngles);
		}

		public Vector3 GetEulerAngles()
		{
			return this.TransformInfoComponent.GetEulerAngles();
		}

		public void SetRotation(Quaternion rotation)
		{
			this.TransformInfoComponent.SetRotation(rotation);
			this.graphicComponent.SetRotation(rotation);
		}

		public Quaternion GetRotation()
		{
			return this.TransformInfoComponent.GetRotation();
		}

		public void SetScale(Vector3 scale)
		{
			this.TransformInfoComponent.SetScale(scale);
			this.graphicComponent.SetScale(scale);
		}

		public Vector3 GetScale()
		{
			return this.TransformInfoComponent.GetScale();
		}
	}
}