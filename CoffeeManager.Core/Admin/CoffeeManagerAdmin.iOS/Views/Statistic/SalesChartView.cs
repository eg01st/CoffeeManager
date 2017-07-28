using System;

using UIKit;
using MvvmCross.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core;
using System.Collections.Generic;
using CoffeeManager.Models;
using Alliance.Charts;
using System.Linq;
using Foundation;

namespace CoffeeManagerAdmin.iOS
{
    public partial class SalesChartView : MvxViewController
    {

        private List<Sale> sales;

        public List<Sale> Sales
        {
            get { return sales;}
            set
            {
                sales = value;
                BuildChart();
            }       
        }

        public AllianceChart AllianceChart { get; set;}


        public SalesChartView() : base("SalesChartView", null)
        {
        }

        public override void ViewDidLoad()
        {
            base.ViewDidLoad();
            Title = "График продаж";
            var set = this.CreateBindingSet<SalesChartView, SalesChartViewModel>();
            set.Bind(this).For(t => t.Sales).To(vm => vm.Sales);          
            set.Apply();

        }

        public override void ViewWillTransitionToSize(CoreGraphics.CGSize toSize, IUIViewControllerTransitionCoordinator coordinator)
        {
            base.ViewWillTransitionToSize(toSize, coordinator);
            BuildChart();
        }

        private void BuildChart()
        {
            AllianceChart?.LineChartView.RemoveFromSuperview();
            this.AllianceChart = new AllianceChart(Chart.Line,this.View);
            var minDate = sales.Min(s => s.Time);
            var maxDate = sales.Max(s => s.Time);
            
            List<DateTime> dates = new List<DateTime>();
            List<string> X_labels = new List<string>();
            while(minDate < maxDate)
            {
                X_labels.Add(minDate.ToString("dddd").Substring(0,3));
                dates.Add(minDate);
                minDate = minDate.AddDays(1);
            }

            AllianceChart.LineChartView.XLabels = X_labels;
            AllianceChart.LineChartView.PopOverTextColor = UIColor.White;
            AllianceChart.LineChartView.NumXIntervals = 2;
         
            List<ChartComponent> components = new List<ChartComponent>();

            var random = new Random();
            var groupedByName = sales.GroupBy(g => g.Product1.Name);
            foreach (var item in groupedByName)
            {
                var groupedByDate = item.GroupBy(g => new {g.Time.Date});
                var valueList = new List<float?>();
                foreach (var date in dates)
                {
                    var items = groupedByDate.FirstOrDefault(g => g.Key.Date == date.Date);
                    if(items != null)
                    {
                        valueList.Add(items.Count());
                    }
                    else
                    {
                        valueList.Add(0);
                    }
                }


                ChartComponent ChartComponent = new ChartComponent ();
                ChartComponent.Name = item.Key;
                ChartComponent.axisLabelColor = UIColor.White;
                ChartComponent.valueList = valueList;
                ChartComponent.color = UIColor.FromRGB(random.Next(0, 254),random.Next(0, 254), random.Next(0, 254));
                //ChartComponent.lableColor = UIColor.Black;
                components.Add(ChartComponent);
            }

          
            AllianceChart.LoadChart (components, Chart.Line, this.View);


            View.SetNeedsDisplay();
        }

    }
}

