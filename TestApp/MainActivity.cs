using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Support.Design.Widget;
using Android.Support.V7.App;
using Android.Views;
using Android.Widget;

namespace TestApp
{
    [Activity(Label = "@string/app_name", Theme = "@style/AppTheme.NoActionBar", MainLauncher = true)]
    public class MainActivity : AppCompatActivity
    {
        public const string broadcastName = "com.barcode.sendBroadcast";
        /// <summary>
        /// 测试环境获取,正式环境不建议使用静态获取
        /// </summary>
        public static MainActivity Instance;
         
        public FlxScanCodeReceiver flxScanCodeReceiver = new FlxScanCodeReceiver();

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            SetContentView(Resource.Layout.activity_main);
            Instance = this;

            Android.Support.V7.Widget.Toolbar toolbar = FindViewById<Android.Support.V7.Widget.Toolbar>(Resource.Id.toolbar);
            SetSupportActionBar(toolbar);

            FloatingActionButton fab = FindViewById<FloatingActionButton>(Resource.Id.fab);
            fab.Click += FabOnClick;
            Com.Scan.Flx.FlxScan.Init(this);
        }

        protected override void OnResume()
        {
            base.OnResume();
            var intentFilter = new IntentFilter();
            intentFilter.AddAction(broadcastName);
            RegisterReceiver(flxScanCodeReceiver, intentFilter);
        }

        protected override void OnPause()
        {
            base.OnPause();
            UnregisterReceiver(flxScanCodeReceiver);
        }

        public override bool OnCreateOptionsMenu(IMenu menu)
        {
            MenuInflater.Inflate(Resource.Menu.menu_main, menu);
            return true;
        }

        public override bool OnOptionsItemSelected(IMenuItem item)
        {
            int id = item.ItemId;
            if (id == Resource.Id.action_settings)
            {
                return true;
            }

            return base.OnOptionsItemSelected(item);
        }

        private void FabOnClick(object sender, EventArgs eventArgs)
        {
            Intent intent = new Intent();
            intent.SetAction("com.barcode.sendBroadcastScan");
            SendBroadcast(intent);
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }

    public class FlxScanCodeReceiver : BroadcastReceiver
    {
        public override void OnReceive(Context context, Intent intent)
        {
            if (intent.Action == MainActivity.broadcastName)
            {
                var str = intent.GetStringExtra("BARCODE");
                if ("" != str)
                {
                    Toast.MakeText(MainActivity.Instance, str, ToastLength.Long).Show();
                }
            }
        }
    }


}

