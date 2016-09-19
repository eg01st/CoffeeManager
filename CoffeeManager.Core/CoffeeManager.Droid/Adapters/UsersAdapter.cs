using System;
using Android.Support.V7.Widget;
using Android.Views;
using CoffeeManager.Core.ViewModels;


namespace CoffeeManager.Droid.Adapters
{
    public class UsersAdapter : RecyclerView.Adapter
    {
        LoginViewModel viewModel;
        public UsersAdapter(LoginViewModel viewModel)
        {
            this.viewModel = viewModel;
        }

        public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
        {
              
        }

        public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
        {
            // Inflate the CardView for the photo:
            View itemView = LayoutInflater.From(parent.Context).
                        Inflate(Resource.Layout.user_card, parent, false);

            // Create a ViewHolder to hold view references inside the CardView:
            //UserViewHolder vh = new UserViewHolder(itemView);
            return null;
        }

        public override int ItemCount
        {
            get { return viewModel.Users.Length; }
        }
    }
}