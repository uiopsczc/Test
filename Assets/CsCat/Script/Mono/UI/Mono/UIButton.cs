using System;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using UnityEngine;
using UnityEngine.UI;

// Button that's meant to work with mouse or touch-based devices.
[AddComponentMenu("UI/UIButton", 31)]
public class UIButton : Selectable, IPointerClickHandler
{
	public static Vector3 buttonScale = new Vector3(0.95f, 0.95f, 0.95f);

	[Serializable]
	public class ButtonClickedEvent : UnityEvent
	{
	}

	// Event delegates triggered on click.
	[FormerlySerializedAs("onClick")] [SerializeField]
	private ButtonClickedEvent _onClick = new ButtonClickedEvent();

	protected UIButton()
	{
	}

	public ButtonClickedEvent onClick
	{
		get => _onClick;
		set => _onClick = value;
	}

	private void Press()
	{
		if (!IsActive() || !IsInteractable())
			return;

		_onClick.Invoke();
	}

	// Trigger all registered callbacks.
	public virtual void OnPointerClick(PointerEventData eventData)
	{
		if (eventData.button != PointerEventData.InputButton.Left)
			return;

		Press();
	}

	protected override void DoStateTransition(SelectionState state, bool instant)
	{
		base.DoStateTransition(state, instant);
		transform.localScale = state.Equals(SelectionState.Pressed) ? buttonScale : Vector3.one;
	}

	protected override void Awake()
	{
		base.Awake();
		transition = Transition.None;
	}
}