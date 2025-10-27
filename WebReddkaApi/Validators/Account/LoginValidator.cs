using FluentValidation;
using Microsoft.AspNetCore.Identity;
using WebAPIDB.Data.Entities.Identity;
using WebReddkaApi.Models.Account;

namespace WebReddkaApi.Validators.Account;

public class LoginValidator : AbstractValidator<LoginModel>
{
    public LoginValidator(UserManager<UserEntity> userManager)
    {
        RuleFor(x => x.Username)
            .NotEmpty().WithMessage("Нік є обов'язковим")
            .MustAsync(async (username, cancellation) =>
            {
                var user = await userManager.FindByNameAsync(username);
                return user != null;
            }).WithMessage("Користувача з таким ніком");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Пароль є обов'язковим")
            .MinimumLength(6).WithMessage("Пароль повинен містити щонайменше 6 символів");

        RuleFor(x => x)
            .MustAsync(async (model, cancellation) =>
            {
                var user = await userManager.FindByNameAsync(model.Username);
                if (user == null) return false;
                return await userManager.CheckPasswordAsync(user, model.Password);
            })
            .WithMessage("Невірний логін або пароль");
    }
}
