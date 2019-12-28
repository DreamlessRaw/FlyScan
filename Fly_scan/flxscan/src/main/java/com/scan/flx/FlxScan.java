package com.scan.flx;

import android.annotation.SuppressLint;
import android.app.Activity;
import android.content.Context;
import com.olc.scan.ScanManager;

/**
 * FuLiYe Pda Scan
 * Created by MaZhiLun 2019/12/28
 */
public class FlxScan {
    @SuppressLint("WrongConstant")
    public static void init(Context context) {
        ScanManager scanManager = (ScanManager) context.getSystemService("olc_service_scan");
        if (scanManager != null) {
            scanManager.setScanSwitchLeft(true);
            scanManager.setScanSwitchRight(true);
            scanManager.setBarcodeReceiveModel(2);
        }
    }

    @SuppressLint("WrongConstant")
    public static void init(Activity activity) {
        ScanManager scanManager = (ScanManager) activity.getSystemService("olc_service_scan");
        if (scanManager != null) {
            scanManager.setScanSwitchLeft(true);
            scanManager.setScanSwitchRight(true);
            scanManager.setBarcodeReceiveModel(2);
        }
    }


}
