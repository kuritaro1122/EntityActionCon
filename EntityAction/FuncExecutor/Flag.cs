using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Flag {
    private Func<bool> condition = () => false;
    private int flagNum = 1;
    private int _flagNum = 1;
    private bool FlagBroken { get { return _flagNum <= 0; } }
    public Flag(Func<bool> condition, int flagNum = 1) {
        Set(condition, flagNum);
    }
    public void Set(Func<bool> condition, int flagNum = 1) {
        this.condition = condition;
        this._flagNum = this.flagNum = flagNum;
    }

    public void BreakFlag() {
        this._flagNum--;
    }
    public void BreakAllFlag() {
        this._flagNum = 0;
    }
    public void RecreateFlag() {
        this._flagNum = this.flagNum;
    }

    public bool GetFlag() {
        return this.condition() && !this.FlagBroken;
    }
}