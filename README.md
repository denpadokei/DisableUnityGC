# DisableUnityGC

[![Build and Release](https://github.com/denpadokei/DisableUnityGC/actions/workflows/build-and-release.yml/badge.svg)](https://github.com/denpadokei/DisableUnityGC/actions/workflows/build-and-release.yml)

GameCore シーン (プレイ中) の間だけ Unity のガベージコレクションを無効化し、GC によるプチフリーズを抑える MOD です。

## 機能

- `GameCore` シーンに入ると `GarbageCollector.GCMode` を `Disabled` に切り替え
- メニューなどほかのシーンへ戻ると自動的に `Enabled` に戻す (シーン遷移終了イベントで切り替え)
- 設定でメモリ表示を ON にすると、画面右下に WorkingSet (byte と MB) を 1 秒間隔で表示
- メモリ使用量の取得は同梱の `Libs/MemoryViewer.dll` (ネイティブ DLL) を使用
- Mod 設定の「DisableGCUnity」から GC 無効化とメモリ表示を個別に切り替え可能

## 動作環境

| 項目 | 内容 |
| --- | --- |
| Beat Saber | 1.13.2 |
| 依存 MOD | BSIPA 4.1.3 / SiraUtil 2.5.1 / BS Utils 1.7.0 / BeatSaberMarkupLanguage 1.4.5 |

## インストール

1. Beat Saber を 1.13.2 に合わせる (BSManager などのバージョン管理ツールを推奨)
2. BSIPA と上記の依存 MOD をインストールする
3. [Releases](https://github.com/denpadokei/DisableUnityGC/releases) から `DisableUnityGC-0.1.3-bs1.13.2.zip` をダウンロードし、`Plugins` フォルダをゲームフォルダへ上書きコピーする

メモリ表示に使う `Libs/MemoryViewer.dll` も zip に含まれるため、`Plugins` と `Libs` の両方をコピーしてください。

## 使い方

ゲームを起動して譜面をプレイするだけで、`GameCore` に入ったタイミングで GC が無効化されます。メニューに戻ると自動で有効に戻るため、手動での操作は不要です。

メモリ使用量を確認したい場合は、Mod 設定の「DisableGCUnity」で **Memory size view** を ON にします。

### 注意

- GC が動かないためメモリが解放されず、ゲーム自体が落ちる可能性があります。16GB 以上のメモリを搭載している環境での使用を推奨します。
- LagKiller が入っている場合は LagKiller が優先され、GC は常に有効になります。
- LagKiller は GC の動作を最適化する MOD で、GC 自体は実行されます。こちらは GC を完全に無効化するためプチフリーズを抑えられますが、そのぶんメモリを消費します。

## 設定

| 項目 | 型 | 既定値 | 説明 |
| --- | --- | --- | --- |
| Enable | bool | true | GameCore 中の GC を無効化するかどうか |
| MemorySize | bool | false | 画面右下にメモリ使用量を表示するかどうか |

設定は `UserData/DisableUnityGC.json` に保存されます。

## ビルド

```powershell
git clone https://github.com/denpadokei/DisableUnityGC.git
cd DisableUnityGC
dotnet build .\DisableUnityGC\DisableUnityGC.csproj -c Release
```

`BeatSaberDir` (または `GameDirectory`) に Beat Saber のインストール先を設定すると、ゲームの参照アセンブリを解決できます。既定では `..\Refs` フォルダが参照先になります。

GitHub Actions の `build-and-release.yml` が push ごとにビルドし、`X.X.X` 形式のタグを push するとリリースを作成します。

## ライセンス

MIT
