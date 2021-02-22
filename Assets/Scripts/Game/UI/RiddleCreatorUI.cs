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
            float displayRectScalar = rect.height > rect.width ? rect.height : rect.width;
            float displayRectXScaled = rect.x / displayRectScalar;
            float displayRectYScaled = rect.y / displayRectScalar;

            float imageRectScalar = outImageHeight < outImageWidth ? outImageHeight : outImageWidth;
            float imageRectXScaled = outImageWidth / imageRectScalar;
            float imageRectYScaled = outImageHeight / imageRectScalar;

            float extraImageWidth = imageRectXScaled - displayRectXScaled;
            float extraImageHeight = imageRectYScaled - displayRectYScaled;

            float rectX = (extraImageWidth * 0.5f) * outImageWidth, rectY = (extraImageHeight * 0.5f) * outImageHeight;
            float imageWidth = outImageWidth * (displayRectXScaled / imageRectXScaled), imageHeight = outImageHeight *(displayRectYScaled / imageRectYScaled);
            outputRect = new Rect(rectX, rectY, imageWidth, imageHeight);
        }

        private void OnDescriptionUpdated(string text)
        {
            if(this._riddleDataHolder.HasValue)
            {
                this._riddleDataHolder = new RiddleDataHolder();
            }

            RiddleDataHolder dataHolderValue = this._riddleDataHolder.Value;
            dataHolderValue.description = text;
        }

        private void OnTitleUpdated(string title)
        {
            if(this._riddleDataHolder.HasValue)
            {
                this._riddleDataHolder = new RiddleDataHolder();
            }
            RiddleDataHolder dataHolderValue = this._riddleDataHolder.Value;
            dataHolderValue.title = title;
        }

        private void OnImageCancelled()
        {
            Debug.Log("Cancelled loading the image...");
        }

        private void OnAddRiddle()
        {
            // TODO: Close this and add the riddle to the set
        }
    }
}

