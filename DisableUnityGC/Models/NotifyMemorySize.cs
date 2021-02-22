using BeatSaberMarkupLanguage;
using DisableUnityGC.Configuration;
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
        private Timer _memoryCheckTimer;
        private ulong _memorySize;
        private TextMeshProUGUI memorySizeText;
        private Canvas memorySizeCanvas;
        #region Unity message
        private void Awake()
        {
            this.memorySizeCanvas = this.gameObject.AddComponent<Canvas>();
            this.memorySizeCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            this.memorySizeText = this.memorySizeCanvas.gameObject.AddComponent<TextMeshProUGUI>();
            this._memoryCheckTimer = new Timer(1000);
            this._memoryCheckTimer.Enabled = PluginConfig.Instance.MemorySize;
            PluginConfig.Instance.OnConfigChanged += this.Instance_OnConfigChanged;
            this._memoryCheckTimer.Elapsed += this.OnMemoryCheckTimer_Elapsed;
        }

        private void OnEnable()
        {
            if (this._memoryCheckTimer?.Enabled != true) {
                this._memoryCheckTimer.Enabled = true;
            }
        }

        private void OnDisable()
        {
            this._memoryCheckTimer.Enabled = false;
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
            this.memorySizeCanvas.enabled = PluginConfig.Instance.MemorySize;
            this._memoryCheckTimer.Start();
        }

        private void OnDestroy()
        {
            this._memoryCheckTimer.Elapsed -= this.OnMemoryCheckTimer_Elapsed;
            this._memoryCheckTimer.Dispose();
            PluginConfig.Instance.OnConfigChanged -= this.Instance_OnConfigChanged;
        }
        #endregion

        private void OnMemoryCheckTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            if (!PluginConfig.Instance.MemorySize) {
                return;
            }
            this._memorySize = NativeMethods.GetWorkingSet();
            this.memorySizeText.text = $"WorkingSet : {this._memorySize} byte ({this._memorySize / 1024ul / 1024ul} MB)";
        }
        private void Instance_OnConfigChanged(PluginConfig obj)
        {
            this.memorySizeCanvas.enabled = obj.MemorySize;
            this._memoryCheckTimer.Enabled = obj.MemorySize;
        }
    }
}
