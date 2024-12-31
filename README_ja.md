# SyncListDictionary.cs

Unity用のスクリプトファイルです。インスペクターでDictionary型の変数を表示したいときに便利です。

## 概要
- **Dictionary** と **List** を同時に作成します。
- インスペクター上では **List** を表示します。
- **Add()** や **Get()** で入出力を行う際は、裏に存在する **Dictionary** を使用します。
- **List** はエディタでのプレイモードのみ機能し、ビルド時には完全に **Dictionary** として動作します。
  - メモリの圧迫や負荷の防止に効果的です。

---

## 使い方
```csharp
// 生成
[SerializeField]
private SyncListDictionary<string, myData> syncListDictionary = new();

// 追加
syncListDictionary.Add("data_1", new myData());

// 取得
syncListDictionary.Get("data_1");

// 既存チェック
syncListDictionary.ContainsKey("data_1");

// クリア
syncListDictionary.Clear();
```

---

## サンプルコード
以下は、カスタムクラスを使用したサンプルコードです。<br>
※これはスクリプト本体ではないのでご注意ください！本体は↑からダウンロードしてください。

```csharp
using System;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField]
    private SyncListDictionary<string, LocalizeData> syncListDictionary = new();

    // 独自クラスを使用するときは[Serializable]を忘れずに
    [Serializable]
    private class LocalizeData
    {
        [SerializeField]
        private string name;

        [SerializeField]
        private string ja;

        [SerializeField]
        private string en;

        public LocalizeData(string name, string ja, string en)
        {
            this.name = name;
            this.ja = ja;
            this.en = en;
        }

        public string Name => name;
        public string Ja => ja;
        public string En => en;
    }

    private void Start()
    {
        // データを追加
        syncListDictionary.Add("txtId_1", new LocalizeData("田中太郎", "こんにちは！", "Hello!"));

        // データを取得
        var data = syncListDictionary.Get("txtId_1");
        Debug.Log("Name: " + data.Name + ", Ja: " + data.Ja + ", En: " + data.En);
    }
}
```
## 実際の表示
![sample1](https://github.com/user-attachments/assets/0fd77a8e-6609-454d-b7a5-abceff04535e)

## 既存の問題
なぜかカスタムクラスをインペクターで表示すると、要素の表示が若干ずれたりすることがあります。<br>
マウスカーソルを合わせると表示が直るし、ずれて要素が全く見えなくなるようなことはないので<br>
とりあえず放置で…。Unity6で問題を確認しています。<br>
誰か修正方法がわかる方いたら教えてください。
