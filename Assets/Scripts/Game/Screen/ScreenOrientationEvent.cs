using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ModifiedLocation.Scripts.Game
{
    /// <summary>
    /// Event that is called whenever a screen orientation has been changed.
    /// </summary>
    [CreateAssetMenu(fileName = "Screen Orientation Event", menuName = "Events/Screen Orientation Event")]
    public class ScreenOrientationEvent : Utils.GenericEvent<ScreenOrientation> { }
}
