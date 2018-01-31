using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerResources
{
	public static int Coins { get; set; }

    public delegate void CoinCountChanged();
    public static event CoinCountChanged OnCoinChange;
    public static event CoinCountChanged OnCoinCollected;

    //public PlayerResources () {; }

    public static void CollectCoin ()
    {
        Coins++;
        if (OnCoinChange != null) OnCoinChange();
        if (OnCoinCollected != null) OnCoinCollected();
    }

    public static void Reset ()
    {
        Coins = 0;
        if (OnCoinChange != null) OnCoinChange();
    }
}
