using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class DraggingActions : MonoBehaviour {
	
	public abstract void OnStartDrag();

	public abstract void OnEndDrag();

	public abstract void OnDraggingInUpdate();

	public abstract void OnCancelDrag();

	public virtual bool CanDrag{
		get{
			return GlobalSettings.Instance.CanControlThisPlayer (playerOwner);
		}
	}

	protected virtual Player playerOwner{
		get{
			if (tag.Contains ("Blue"))
				return GlobalSettings.Instance.playerBlue;
			else if (tag.Contains ("Red"))
				return GlobalSettings.Instance.playerRed;
			else
				return null;
		}
	}

	protected abstract bool DragSuccessful();
}
