using System.Collections.Generic;

namespace CoffeeManager.Api.AuthModels
{
    // Models returned by AccountController actions.

    public class UserInfoViewModel
    {
        public string Email { get; set; }
        
        public bool IsAdmin { get; set; }

        public string ApiUrl { get; set; }
    }
}
