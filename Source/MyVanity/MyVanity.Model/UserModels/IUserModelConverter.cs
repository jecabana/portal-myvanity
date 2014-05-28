namespace MyVanity.Model.UserModels
{
    public interface IUserModelConverter : IModelConverter<Domain.User, UserViewModel>
    {
        string GetUserName(Domain.User user);
    }
}
