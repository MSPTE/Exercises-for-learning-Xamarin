using System;
using Android.App;
using Android.Content;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;

namespace TipCalculator
{
    [Activity(Label = "TipCalculator", MainLauncher = true, Icon = "@drawable/icon")]
    public class MainActivity : Activity
    {
        EditText ipBill;
        Button btnCalculate;
        TextView opTip;
        TextView opTotal;

        protected override void OnCreate(Bundle bundle)
        {
            base.OnCreate(bundle);
            SetContentView(Resource.Layout.Main);

            ipBill = FindViewById<EditText>(Resource.Id.ip_bill);
            btnCalculate = FindViewById<Button>(Resource.Id.btn_calculate);
            opTip = FindViewById<TextView>(Resource.Id.op_tip);
            opTotal = FindViewById<TextView>(Resource.Id.op_total);

            btnCalculate.Click += OnCalculateClick;
        }

        void OnCalculateClick(object sender, EventArgs e)
        {
            double bill;
            if (!Double.TryParse(ipBill.Text, out bill))
                bill = 0;
            double tip = bill * 0.15;
            double total = bill + tip;

            opTip.Text = tip.ToString();
            opTotal.Text = total.ToString();
        }
    }
}

