using CoffeeManager.Models.Data.DTO.StaffMotivation;
using MobileCore.ViewModels;

namespace CoffeeManagerAdmin.Core.ViewModels.Motivation
{
    public class MotivationItemViewModel : FeedItemElementViewModel
    {
        private readonly UserMotivationDTO dto;

        public MotivationItemViewModel(UserMotivationDTO dto)
        {
            this.dto = dto;
        }

        public string UserName => dto.UserName;
        public decimal ShiftScore => dto.ShiftScore;
        public decimal MoneyScore => dto.MoneyScore;
        public decimal OtherScore => dto.OtherScore;
        public decimal EntireScore => dto.EntireScore;
    }
}