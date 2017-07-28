using System;

using Foundation;
using UIKit;
using MvvmCross.Binding.iOS.Views;
using MvvmCross.Binding.BindingContext;
using CoffeeManagerAdmin.Core;
using System.Windows.Input;

namespace CoffeeManagerAdmin.iOS
{
    public partial class SelectSalesTableViewCell : MvxTableViewCell
    {
        public static readonly NSString Key = new NSString("SelectSalesTableViewCell");
        public static readonly UINib Nib;

        static SelectSalesTableViewCell()
        {
            Nib = UINib.FromName("SelectSalesTableViewCell", NSBundle.MainBundle);
        }

        private ICommand _toggleIsSelectedCommand;
        public ICommand ToggleIsSelectedCommand
        {
            get { return _toggleIsSelectedCommand; }
            set
            {
                _toggleIsSelectedCommand = value;

            }
        }

        protected SelectSalesTableViewCell(IntPtr handle) : base(handle)
        {
            // Note: this .ctor should not contain any initialization logic.
        }
        
        public override void AwakeFromNib()
        {
            base.AwakeFromNib();
            this.DelayBind(() => 
            {
                var set = this.CreateBindingSet<SelectSalesTableViewCell, SelectSaleItemViewModel>();
                set.Bind(SaleNameLabel).To(vm => vm.Name);
                set.Bind(IsSelectedSwitch).For(s => s.On).To(vm => vm.IsSelected);
                set.Bind(this).For(t => t.ToggleIsSelectedCommand).To(vm => vm.ToggleIsSelectedCommand);
                set.Apply();    
                IsSelectedSwitch.ValueChanged += (sender, e) => 
                {
                    ToggleIsSelectedCommand.Execute(null);
                };    
            });
        }
    }
}
