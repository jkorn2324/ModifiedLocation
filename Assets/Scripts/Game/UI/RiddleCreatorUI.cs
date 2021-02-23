using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ModifiedLocation.Scripts.Game
{

    /// <summary>
    /// The riddle data holder.
    /// </summary>
    public struct RiddleDataHolder
    {
        public string title;
        public string description;
        public Texture2D texture;
    }

    [System.Serializable]
    public class RiddleSectionUIElements
    {
        [SerializeField]
        public GameObject section;
        [SerializeField]
        private Text riddleTitle;
        [SerializeField]
        private Text riddleDescription;
        [SerializeField]
        private Image riddleImage;

        public string Title
            => this.riddleTitle.text;

        public string Description
            => this.riddleDescription.text;

        public Image RiddleImage
            => this.riddleImage;
    }

    [System.Serializable]
    public struct ScreenOrientationSections
    {
        [SerializeField]
        private RiddleSectionUIElements portraitSection;
        [SerializeField]
        private RiddleSectionUIElements landscapeSection;
        [SerializeField]
        private ScreenOrientationEvent screenOrientationEvent;

        public RiddleSectionUIElements ActiveSection
            => this.portraitSection.section.activeSelf ? this.portraitSection : this.landscapeSection;

        public void HookEvents()
        {
            this.screenOrientationEvent?.AddListener(this.OnOrientationChanged);
        }

        public void UnHookEvents()
        {
            this.screenOrientationEvent?.RemoveListener(this.OnOrientationChanged);
        }

        private void OnOrientationChanged(ScreenOrientation orientation)
        {
            Debug.Log("Orientation Changed");
            if(orientation == ScreenOrientation.Landscape
                && orientation == ScreenOrientation.LandscapeRight)
            {
                this.landscapeSection.section.SetActive(true);
                this.portraitSection.section.SetActive(false);
                return;
            }

            this.landscapeSection.section.SetActive(false);
            this.portraitSection.section.SetActive(true);
        }
    }

    /// <summary>
    /// Riddle creator ui.
    /// </summary>
    public class RiddleCreatorUI : Utils.EventsListener
    {
        [SerializeField]
        private RiddleScannedComponent prefab;
        [SerializeField]
        private GameRiddleSetReference editedRiddleSet;
        [SerializeField]
        private Utils.GameEvent cancelEvent;
        [SerializeField]
        private Utils.GameEvent addRiddleEvent;
        [SerializeField]
        private ImageLoadEvent loadEvent;
        [SerializeField]
        private ScreenOrientationSections sections;

        private RiddleDataHolder? _riddleDataHolder = null;

        protected override void HookEvents()
        {
            this.sections.HookEvents();
            this.addRiddleEvent?.AddListener(this.OnAddRiddle);
            this.loadEvent?.AddListener(this.OnImageLoad);
            this.cancelEvent?.AddListener(this.OnImageCancelled);
        }

        protected override void UnHookEvents()
        {
            this.sections.UnHookEvents();
            this.addRiddleEvent?.RemoveListener(this.OnAddRiddle);
            this.loadEvent?.RemoveListener(this.OnImageLoad);
            this.cancelEvent?.RemoveListener(this.OnImageCancelled);
        }

        private void OnImageLoad(ImageLoadedData data)
        {
            if(!this._riddleDataHolder.HasValue)
            {
                this._riddleDataHolder = new RiddleDataHolder();
            }

            RiddleDataHolder valueOfData = this._riddleDataHolder.Value;
            valueOfData.texture = data.imageTexture;
            this._riddleDataHolder = valueOfData;

            Image activeImage = sections.ActiveSection.RiddleImage;
            RectTransform rectTransform = activeImage.rectTransform;
            Rect activeRect = rectTransform.rect;

            Rect outputRect;
            this.GetScalarData(out outputRect, activeRect,
                data.imageTexture.width, data.imageTexture.height);

            Sprite sprite = Sprite.Create(
                data.imageTexture, outputRect,
                Vector2.zero, 100.0f);
            activeImage.type = Image.Type.Simple;
            activeImage.sprite = sprite;
        }

        private void GetScalarData(out Rect outputRect, Rect rect, int outImageWidth, int outImageHeight)
        {
            float rectX = 0.0f;
            float rectY = 0.0f;
            float imageWidth = outImageWidth;
            float imageHeight = outImageHeight;
            outputRect = new Rect(rectX, rectY, imageWidth, imageHeight);
        }

        public void OnDescriptionUpdated(Text updatedTextElement)
        {
            if(!this._riddleDataHolder.HasValue)
            {
                this._riddleDataHolder = new RiddleDataHolder();
            }

            RiddleDataHolder dataHolderValue = this._riddleDataHolder.Value;
            dataHolderValue.description = updatedTextElement.text;
            this._riddleDataHolder = dataHolderValue;
        }

        public void OnTitleUpdated(Text updatedTextElement)
        {
            if(!this._riddleDataHolder.HasValue)
            {
                this._riddleDataHolder = new RiddleDataHolder();
            }
            RiddleDataHolder dataHolderValue = this._riddleDataHolder.Value;
            dataHolderValue.title = updatedTextElement.text;
            this._riddleDataHolder = dataHolderValue;
        }

        private void OnImageCancelled()
        {
            Debug.Log("Cancelled loading the image...");
        }

        private void OnAddRiddle()
        {
            if(!this._riddleDataHolder.HasValue)
            {
                Debug.Log("Riddle holder doesnt have anyting...");
                return;
            }

            Debug.Log("Riddle should be added...");
            RiddleDataHolder dataHolder = this._riddleDataHolder.Value;
            /* if(dataHolder.title.Length <= 0
                || dataHolder.description.Length <= 0
                || dataHolder.texture == null)
            {
                return;
            }

            if(!this.editedRiddleSet.HasRiddleSet)
            {
                return;
            } */

            GameRiddle riddle = GameRiddle.Create(
                dataHolder.title, dataHolder.description, dataHolder.texture, prefab);
            this.editedRiddleSet.AddRiddle(riddle);
            this._riddleDataHolder = null;
            // TODO: Hide creator ui
        }
    }
}

