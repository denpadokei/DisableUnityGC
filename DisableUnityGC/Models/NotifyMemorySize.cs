using BeatSaberMarkupLanguage;
using DisableUnityGC.Utilites;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using TMPro;
using UnityEngine;

namespace DisableUnityGC.Models
{
    public class NotifyMemorySize : MonoBehaviour
    {
        private static Timer _memoryCheckTimer;
        private long _memorySize;
        private TextMeshProUGUI memorySizeText;
        private Canvas memorySizeCanvas;
        #region Unity message
        private void Awake()
        {
            this.memorySizeCanvas = this.gameObject.AddComponent<Canvas>();
            this.memorySizeCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            this.memorySizeText = this.memorySizeCanvas.gameObject.AddComponent<TextMeshProUGUI>();
            
        }

        private IEnumerator Start()
        {
            yield return new WaitWhile(() => !FontManager.IsInitialized);
            if (FontManager.TryGetTMPFontByFamily("Segoe UI", out var font)) {
                this.memorySizeText.font = font;
            }
            this.memorySizeText.alignment = TextAlignmentOptions.BottomRight;
            this.memorySizeText.rectTransform.anchoredPosition = new Vector2(1, 0);
            this.memorySizeText.rectTransform.pivot = new Vector2(1, 0);
            this.memorySizeText.transform.position = Vector2.zero;
            this.memorySizeText.fontSize = 40;
            this.memorySizeText.ForceMeshUpdate();
            _memoryCheckTimer = new Timer(1000);
            _memoryCheckTimer.Elapsed += this._memoryCheckTimer_Elapsed;
            _memoryCheckTimer.Start();
        }

        private void OnDestroy()
        {
            _memoryCheckTimer.Dispose();
        }
        #endregion

        private void _memoryCheckTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            this._memorySize = NativeMethods.GetWorkingSet();
            this.memorySizeText.text = $"WorkingSet : {this._memorySize} byte ({this._memorySize / 1024 / 1024} MB)";
        }
    }
}
