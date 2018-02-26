using System;
using System.Collections;
using System.Collections.Generic;

namespace GPP
{
	public abstract class GameEvent
	{
		public delegate void Handler(GameEvent e);
	}

	public class EventManager 
	{
		private static EventManager _instance;
		public static EventManager Instance
		{
			get
			{
				if (_instance == null)
				{
					_instance=new EventManager();
				}

				return _instance;
			}
		}
		private readonly Dictionary<System.Type,GameEvent.Handler> _eventTypeToHandlersMap=new Dictionary<Type, GameEvent.Handler>();

		public void Register<EventType>(GameEvent.Handler handler) where EventType : GameEvent
		{
			System.Type type = typeof(EventType);
			GameEvent.Handler handlers;
			if (_eventTypeToHandlersMap.ContainsKey(type))
			{
				_eventTypeToHandlersMap[type] += handler;
			}
			else
			{
				_eventTypeToHandlersMap.Add(type,handler);
			}
		}

		public void UnRegister<EventType>(GameEvent.Handler handler) where EventType : GameEvent
		{
			System.Type type = typeof(EventType);
			GameEvent.Handler handlers;
			if (_eventTypeToHandlersMap.TryGetValue(type, out handlers))
			{
				handlers -= handler;
				if (handlers == null)
				{
					_eventTypeToHandlersMap.Remove(type);
				}
				else
				{
					_eventTypeToHandlersMap[type] = handlers;
				}
			}
		}

		public void Fire(GameEvent e)
		{
			System.Type type = e.GetType();
			GameEvent.Handler handlers;
			if (_eventTypeToHandlersMap.TryGetValue(type, out handlers))
			{
				handlers(e);
			}
			//GameEvent.Handler handlers;
			//_eventTypeToHandlersMap[type].
			
		}
	}
}