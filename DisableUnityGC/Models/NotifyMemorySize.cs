using BeatSaberMarkupLanguage;
using DisableUnityGC.Configuration;
using DisableUnityGC.Utilites;
using System;
using System.Collections;
using System.Threading;
using TMPro;
using UnityEngine;

namespace DisableUnityGC.Models
{
    public class NotifyMemorySize : MonoBehaviour
    {
        private Thread _memoryCheckThread;
        private ulong _memorySize;
        private TextMeshProUGUI memorySizeText;
        private Canvas memorySizeCanvas;
        #region Unity message
        private void Awake()
        {
            this.memorySizeCanvas = this.gameObject.AddComponent<Canvas>();
            this.memorySizeCanvas.renderMode = RenderMode.ScreenSpaceOverlay;
            this.memorySizeText = this.memorySizeCanvas.gameObject.AddComponent<TextMeshProUGUI>();
            this._memoryCheckThread = new Thread(new ThreadStart(() =>
            {
                while (true) {
                    try {
                        this.UpdateMemorySizeText();
                    }
                    catch (Exception e) {
                        Plugin.Log.Error(e);
                    }
                    finally {
                        Thread.Sleep(1000);
                    }
                }
            }));
            PluginConfig.Instance.OnConfigChanged += this.Instance_OnConfigChanged;
        }
        private IEnumerator Start()
        {
            yield return new WaitWhile(() => !FontManager.IsInitialized);
            if (FontManager.TryGetTMPFontByFamily("Teko-Medium SDF", out var font)) {
                this.memorySizeText.font = font;
            }
            this.memorySizeText.alignment = TextAlignmentOptions.BottomRight;
            this.memorySizeText.rectTransform.anchoredPosition = new Vector2(1, 0);
            this.memorySizeText.rectTransform.pivot = new Vector2(1, 0);
            this.memorySizeText.transform.position = Vector2.zero;
            this.memorySizeText.fontSize = 40;
            this.memorySizeText.ForceMeshUpdate();
            this.memorySizeCanvas.enabled = PluginConfig.Instance.MemorySize;
            this._memoryCheckThread.Start();
        }

        private void OnDestroy()
        {
            PluginConfig.Instance.OnConfigChanged -= this.Instance_OnConfigChanged;
            if (this._memoryCheckThread != null && this._memoryCheckThread.IsAlive) {
                this._memoryCheckThread.Abort();
            }
        }
        #endregion

        private void UpdateMemorySizeText()
        {
            if (!PluginConfig.Instance.MemorySize) {
                return;
            }
            this._memorySize = NativeMethods.GetWorkingSet();
            HMMainThreadDispatcher.instance?.Enqueue(() =>
            {
                this.memorySizeText.text = $"WorkingSet : {this._memorySize} byte ({this._memorySize / 1024ul / 1024ul} MB)";
            });
        }
        private void Instance_OnConfigChanged(PluginConfig obj)
        {
            this.memorySizeCanvas.enabled = obj.MemorySize;
        }
    }
}
