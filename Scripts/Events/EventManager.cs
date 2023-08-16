using System;
using System.Collections.Generic;

namespace CM.Events
{
	/// <summary>
	/// Contains listeners and triggers events.
	/// </summary>
	public static class EventManager
	{
        private static readonly Dictionary<Type, List<Action<object>>> _listeners = new();

		/// <summary>
		/// Adds a listener that will listen for an event being called.
		/// </summary>
		/// <typeparam name="T">The event Type to listen to.</typeparam>
		/// <param name="listener">The callback method.</param>
		public static void AddListener<T>(Action<object> listener) where T : class
		{
			if (!_listeners.ContainsKey(typeof(T)))
				_listeners.Add(typeof(T), new List<Action<object>>());

			_listeners[typeof(T)].Add(listener);
		}

		/// <summary>
		/// Remove a listener.
		/// </summary>
		/// <typeparam name="T">The event Type to listen to.</typeparam>
		/// <param name="listener">The callback method.</param>
		public static void RemoveListener<T>(Action<object> listener) where T : class
		{
			if (!_listeners.ContainsKey(typeof(T)))
				return;

			_listeners[typeof(T)].Remove(listener);
		}

		/// <summary>
		/// Triggers an event.
		/// </summary>
		/// <typeparam name="T">The event Type to trigger.</typeparam>
		/// <param name="eventData">The data of the event.</param>
		public static void Trigger<T>(T eventData) where T : class
		{
			if (!_listeners.ContainsKey(typeof(T)))
				return;

			TriggerEvent(eventData);
		}

		private static void TriggerEvent<T>(T eventData) where T : class
		{
			foreach (var action in _listeners[typeof(T)])
				action.Invoke(eventData);
		}
	}
}