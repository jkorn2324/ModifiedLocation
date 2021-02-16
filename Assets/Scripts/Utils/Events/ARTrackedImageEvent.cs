using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.ARFoundation;

namespace ModifiedLocation.Scripts.Utils
{
    [CreateAssetMenu(fileName = "AR Tracked Image Event", menuName = "Events/Game Event (AR Tracked Image)")]
    public class ARTrackedImageEvent : GenericEvent<ARTrackedImage> { }
}


