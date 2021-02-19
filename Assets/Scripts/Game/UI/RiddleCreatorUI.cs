using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ModifiedLocation.Scripts.Game
{

    [System.Serializable]
    public class RiddleSectionUIElements
    {
        [SerializeField]
        private Text riddleTitle;
        [SerializeField]
        private Text riddleDescription;
        [SerializeField]
        private Image riddleImage;

        public GameRiddle CreateFromUIElements()
        {
            // TODO: 
            return null;
        }
    }


    /// <summary>
    /// Riddle creator ui.
    /// </summary>
    public class RiddleCreatorUI : MonoBehaviour
    {
        // TODO: We need:
        // -> selected image (converted as a texture)
        // -> riddle text
        // -> riddle name
        [SerializeField]
        private RiddleSectionUIElements uiElements;
    }
}

