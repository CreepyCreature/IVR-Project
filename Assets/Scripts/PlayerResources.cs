using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerResources
{
	public static int Coins { get; set; }

    public delegate void CoinCollected();
    public static event CoinCollected OnCoinChange;

    //public PlayerResources () {; }

    public static void CollectCoin ()
    {
        Coins++;
        if (OnCoinChange != null) OnCoinChange();
    }

    public static void Reset ()
    {
        Coins = 0;
        if (OnCoinChange != null) OnCoinChange();
    }
}
