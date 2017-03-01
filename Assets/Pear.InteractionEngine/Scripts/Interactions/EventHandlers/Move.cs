﻿using Pear.InteractionEngine.Properties;
using Pear.InteractionEngine.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace Pear.InteractionEngine.Interactions.EventHandlers
{
    /// <summary>
    /// Move based on the change in property value
    /// </summary>
    public class Move : MonoBehaviour, IGameObjectPropertyEventHandler<Vector3>
    {
        [Tooltip("Move speed")]
        public float MoveSpeed = 1f;

        List<GameObjectProperty<Vector3>> _properties = new List<GameObjectProperty<Vector3>>();

		/// <summary>
		/// Move each property's owner based on the property's value
		/// </summary>
        void Update()
        {
            _properties.ForEach(p =>
            {
				Vector3 moveVector = p.Value;
				moveVector.y *= -1; // y is reversed because of the object is in front of us

                p.Owner.transform.GetOrAddComponent<ObjectWithAnchor>()
                    .AnchorElement
                    .transform
                    .position += moveVector * MoveSpeed * Time.deltaTime;
            });
        }

        public void RegisterProperty(GameObjectProperty<Vector3> property)
        {
            _properties.Add(property);
        }

        public void UnregisterProperty(GameObjectProperty<Vector3> property)
        {
            _properties.Remove(property);
        }
    }
}