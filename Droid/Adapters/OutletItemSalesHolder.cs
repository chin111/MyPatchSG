using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;

using Android.App;
using Android.Views;
using Android.Widget;
using Android.Support.V4.Graphics.Drawable;
using Android.Support.V4.Widget;
using Android.Support.V7.Widget;
using Android.Support.Design.Widget;
using Android.Graphics;

using SkiaSharp;
using Microcharts;
using Microcharts.Droid;

using MyPatchSG.DL.Models;
using System.Globalization;

namespace MyPatchSG.Droid.Adapters
{
    class OutletItemSalesHolder : RecyclerView.ViewHolder
    {
        public TextView Title { get; set; }
        public ChartView TEChartViewBase { get; set; }
        public ChartView TEChartViewWK { get; set; }


        public OutletItemSalesHolder(View itemView) : base(itemView)
        {
            Title = itemView.FindViewById<TextView>(Resource.Id.item_sales_title);
            TEChartViewBase = itemView.FindViewById<ChartView>(Resource.Id.chartviewBase);
            TEChartViewWK = itemView.FindViewById<ChartView>(Resource.Id.chartviewWK);

            Title.SetOnClickListener(null);
            Title.Clickable = false;

            TEChartViewBase.SetOnClickListener(null);
            TEChartViewBase.Clickable = false;

            TEChartViewWK.SetOnClickListener(null);
            TEChartViewWK.Clickable = false;
        }

        public void BindItem(List<vwSalesOutletChart> mVwSalesOutletChart)
        {
            Title.Text = "Sales";

            var Colors = new[]
            {
                SKColor.Parse("#266489"),
                SKColor.Parse("#68B9C0"),
                SKColor.Parse("#90D585"),
            };

            var entriesBase = new List<Entry>();
            var entriesWK = new List<Entry>();

            var MinValue = 0.00f;
            var MaxValue = 0.00f;
            var BaseValue = 0.00f;

            for (int i = 0; i < mVwSalesOutletChart.Count; i++)
            {
                vwSalesOutletChart item = mVwSalesOutletChart[i];
                string wk = item.getWK();
                string wkNum = wk.Substring(wk.Length - 2, 2);
                int wkNumber = Int32.Parse(wkNum);
                float wkPoint = (float)item.getVolWK();
                string wkPointLabel = "";
                if (i == 0 || i == mVwSalesOutletChart.Count - 1)
                {
                    wkPointLabel = wkPoint.ToString();
                }

                string ValueLabel = wkPoint.ToString();

                entriesBase.Add(new Entry((float)item.getVolBase())
                {
                    Label = wkNumber.ToString(),
                    ValueLabel = wkPointLabel,
                    Color = SKColors.Black,
                });

                entriesWK.Add(new Entry((float)item.getVolWK())
                {
                    Label = wkNumber.ToString(),
                    ValueLabel = ValueLabel,
                    Color = Colors[i % 3],
                });

                if (i == 0)
                {
                    MinValue = (float)item.getVolWK();
                    MaxValue = (float)item.getVolWK();
                    BaseValue = (float)item.getVolBase();
                }

                if (MinValue > (float)item.getVolWK())
                {
                    MinValue = (float)item.getVolWK();
                }

                if (MaxValue < (float)item.getVolWK())
                {
                    MaxValue = (float)item.getVolWK();
                }

                if (BaseValue < (float)item.getVolBase())
                {
                    BaseValue = (float)item.getVolBase();
                }
            }

            var DiffMin = BaseValue - MinValue;
            var DiffMax = MaxValue - BaseValue;
            var Diff = DiffMax > DiffMin ? DiffMax : DiffMin;

            MaxValue = BaseValue + Diff;
            MinValue = BaseValue - Diff;

            TEChartViewBase.Chart = new LineChart() { Entries = entriesBase.ToArray(), LineAreaAlpha = 0, BackgroundColor = SKColors.White, MinValue = MinValue, MaxValue = MaxValue, LineSize = 1, PointSize = 1 };
            TEChartViewWK.Chart = new LineChart() { Entries = entriesWK.ToArray(), LineAreaAlpha = 0, BackgroundColor = SKColors.Transparent, MinValue = MinValue, MaxValue = MaxValue, LineMode = LineMode.Straight };

        }
    }
}