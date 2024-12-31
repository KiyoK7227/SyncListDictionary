using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SyncListDictionary<TKey, TValue>
{
    /////////////////////////////////////////////////////////////////////////////////////
    // キーと値のペアのクラス
    [Serializable]
    public class PairData
    {
        [SerializeField]
        private TKey key;

        [SerializeField]
        private TValue value;

        public PairData(TKey key, TValue value)
        {
            this.key = key;
            this.value = value;
        }

        public TKey Key => key;
        public TValue Value => value;
    }

    /////////////////////////////////////////////////////////////////////////////////////
    // インスペクター用のリストと、実際に使用するディクショナリ
    [SerializeField]
    private List<PairData> list = new();

    private Dictionary<TKey, TValue> dictionary = new();

    public SyncListDictionary() { }

    /////////////////////////////////////////////////////////////////////////////////////
    // 初期設定で、リストの内容をディクショナリに反映
    private bool initialize;

    private void RebuildDictionary()
    {
        if (initialize)
        {
            return;
        }

        dictionary.Clear();
        foreach (var pair in list)
        {
            dictionary.TryAdd(pair.Key, pair.Value);
        }

#if !UNITY_EDITOR // ビルド後ならリストは不要なのでクリア
        list.Clear();
#endif

        initialize = true;
    }

    /////////////////////////////////////////////////////////////////////////////////////
    // データの追加・削除・取得
    public bool Add(TKey key, TValue value)
    {
        if (dictionary.TryAdd(key, value))
        {

#if UNITY_EDITOR // エディタプレイモードでのみリストに追加
            list.Add(new PairData(key, value));
#endif

            return true;
        }
        return false;
    }

    public bool Remove(TKey key)
    {
        var removed = dictionary.Remove(key);

#if UNITY_EDITOR // エディタプレイモードでのみリストから削除
        if (removed)
        {
            list.RemoveAll(pair => EqualityComparer<TKey>.Default.Equals(pair.Key, key));
        }
#endif

        return removed;
    }

    public void Clear()
    {
        list.Clear();
        dictionary.Clear();
    }

    public TValue Get(TKey key)
    {
        RebuildDictionary();
        if (dictionary.TryGetValue(key, out var value))
        {
            return value;
        }
        Debug.LogWarning("Key not found: " + key);
        return default;
    }

    public bool ContainsKey(TKey key)
    {
        RebuildDictionary();
        return dictionary.ContainsKey(key);
    }

    public List<PairData> GetList() => list;
    public Dictionary<TKey, TValue> GetDictionary() => dictionary;
}
