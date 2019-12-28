package com.example.fly_scan

import android.annotation.SuppressLint
import android.content.BroadcastReceiver
import android.content.Context
import android.content.Intent
import android.content.IntentFilter
import android.os.Bundle
import android.widget.TextView
import androidx.appcompat.app.AppCompatActivity

class MainActivity : AppCompatActivity() {
    companion object {
        const val m_Broadcastname: String = "com.barcode.sendBroadcast"
        lateinit var tv_code: TextView
    }

    private val receiver = MyCodeReceiver()
    //private lateinit var sm: ScanManager

    //@SuppressLint("WrongConstant")
    override fun onCreate(savedInstanceState: Bundle?) {
        super.onCreate(savedInstanceState)
        setContentView(R.layout.activity_main)
        tv_code = this.findViewById(R.id.tv_showCode)

//        sm= getSystemService("olc_service_scan") as ScanManager
//        sm.setScanSwitchLeft(true)
//        sm.setScanSwitchRight(true)
//        sm.setBarcodeReceiveModel(2)


        tv_code.setOnClickListener {
            val intent = Intent()
            intent.action = "com.barcode.sendBroadcastScan"
            sendBroadcast(intent)
        }

    }

    override fun onResume() {
        super.onResume()
        registerBroadcast()
    }

    private fun registerBroadcast() {
        val intentFilter = IntentFilter()
        intentFilter.addAction(m_Broadcastname)
        registerReceiver(receiver, intentFilter)
    }

    class MyCodeReceiver : BroadcastReceiver() {
        override fun onReceive(context: Context, intent: Intent) {
            if (intent.action == m_Broadcastname) {
                val str = intent.getStringExtra("BARCODE")
                if ("" != str) {
                    tv_code.text = str
                }
            }
        }

        companion object {
            private const val TAG = "MycodeReceiver"
        }
    }

    override fun onPause() {
        super.onPause()
        unregisterReceiver(receiver)
    }

}

