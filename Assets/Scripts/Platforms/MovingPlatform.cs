using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour {
	/// <summary>
	/// Position List
	/// The script will interpolate the object's position from P(i)->P(i+1),
	/// coming back to the start if it hits the end if Mode==Circle.
	/// </summary>
	[SerializeField]
	private List<Vector3> _positions;

	/// <summary>
	/// Speed in seconds that the platform will take to move between any 2 points in the Positions List.
	/// </summary>
	[SerializeField]
	private float _speedInSeconds = 2f;

	private enum LoopModes {
		Circular,
		Reverse
	}

	/// <summary>
	/// Loop Mode
	/// Circular: the script will cycle through the list and go back to the start when it hits the end.
	/// Reverse: the script will go till the end of the list and come back in the reverse order.
	/// </summary>
	[SerializeField]
	private LoopModes _loopMode = LoopModes.Circular;

	/// <summary>
	/// Whether to use the local transform or the global transform values.
	/// </summary>
	[SerializeField]
	private bool _localTransform = false;

	/// <summary>
	/// Interval (in seconds) between arriving at a target position and starting to move to the next target position.
	/// </summary>
	[SerializeField]
	private float _movingInterval = 1f;

	/// <summary>
	/// Used to control the current and next positions.
	/// </summary>
	private int _currentPosition, _targetPosition;

	/// <summary>
	/// Used to control move time and interval time.
	/// </summary>
	private float _timer, _intervalTimer;

	public MovingPlatform(List<Vector3> positions) {
		_positions = positions;
	}

	// Use this for initialization
	private void Start() {
		// If the positions list is initialized and contains elements
		if (_positions != null && _positions.Count > 0) {
			// Initialize the indexes
			_currentPosition = 0;
			_targetPosition = 1;
			// If the position count is only 1, we interpret that as
			// (starting position) -> (provided position)
			// So we insert the current position at index=0
			// Otherwise, set this object's position to be the first provided position.
			if (_positions.Count == 1)
				_positions.Insert(0, (_localTransform ? transform.localPosition : transform.position));
			else {
				if (_localTransform)
					transform.localPosition = _positions[0];
				else
					transform.position = _positions[0];
			}
			// Start the interval timer.
			_intervalTimer = _movingInterval;
		} else
			Debug.LogError("Position list is null or empty.");
	}

	// Update is called once per frame
	private void Update() {
		if (_intervalTimer > 0) {
			_intervalTimer -= Time.deltaTime;
			return;
		}
		// Update the platform position
		if (_localTransform)
			transform.localPosition = Vector3.Lerp(_positions[_currentPosition], _positions[_targetPosition], (_timer += Time.deltaTime) / _speedInSeconds);
		else
			transform.position = Vector3.Lerp(_positions[_currentPosition], _positions[_targetPosition], (_timer += Time.deltaTime) / _speedInSeconds);
		// If we're approximately in the final position, change the indexes and restart the timer.
		if (Vector3Util.Approx((_localTransform ? transform.localPosition : transform.position), _positions[_targetPosition], 0.01f)) {
			switch (_loopMode) {
				case LoopModes.Circular:
					// Circular mode is simple, keep incrementing and user modulus to go back to start of the list when you hit the end.
					_currentPosition = (_currentPosition + 1) % _positions.Count;
					_targetPosition = (_targetPosition + 1) % _positions.Count;
					break;

				case LoopModes.Reverse:
					// Reverse mode needs to check if it's going (targetPosition > currentPosition) or coming back (currentPosition > targetPosition)
					// Then increment/decrement accordingly or change the direction if it hits the end/start of the list.
					if (_targetPosition > _currentPosition) {
						if (_targetPosition + 1 < _positions.Count) {
							_currentPosition++;
							_targetPosition++;
						} else {
							_currentPosition = _positions.Count - 1;
							_targetPosition = _positions.Count - 2;
						}
					} else {
						if (_targetPosition - 1 > -1) {
							_currentPosition--;
							_targetPosition--;
						} else {
							_currentPosition = 0;
							_targetPosition = 1;
						}
					}
					break;

				default:
					Debug.LogError("Invalid platform mode.");
					break;
			}
			_timer = 0;
			_intervalTimer = _movingInterval;
		}
	}
}