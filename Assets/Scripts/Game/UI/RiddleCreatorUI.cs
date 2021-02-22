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
        private Utils.GameEvent cancelEvent;
        [SerializeField]
        private ImageLoadEvent loadEvent;
        [SerializeField]
        private ScreenOrientationSections sections;

        private RiddleDataHolder _riddleDataHolder;

        protected override void HookEvents()
        {
            this.sections.HookEvents();
            this.loadEvent?.AddListener(this.OnImageLoad);
        }

        protected override void UnHookEvents()
        {
            this.sections.UnHookEvents();
            this.cancelEvent?.AddListener(this.OnCancelled);
        }

        private void OnImageLoad(ImageLoadedData data)
        {
            this._riddleDataHolder.texture = data.imageTexture;

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
            float rectX = 0.0f, rectY = 0.0f;
            float imageWidth = outImageWidth, imageHeight = outImageHeight;
            outputRect = new Rect(rectX, rectY, imageWidth, imageHeight);
        }

        private void OnDescriptionUpdated(string text)
        {
            this._riddleDataHolder.description = text;
        }

        private void OnTitleUpdated(string title)
        {
            this._riddleDataHolder.title = title;
        }

        private void OnCancelled()
        {
            Debug.Log("Cancelled loading the image...");
        }
    }
}

