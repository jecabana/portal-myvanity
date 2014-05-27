using MyVanity.Domain;

namespace MyVanity.Model.ProfileModels.Contact.Impl
{
    public class ContactModelConverter : IContactModelConverter
    {
        public ContactModel ConvertToModel(Domain.Contact entity)
        {
            return new ContactModel
                   {
                       Address1 = entity.Address,
                       Address2 = entity.Address1,
                       Phone = new PhoneModel { Home = entity.Phone.Home, Mobile = entity.Phone.Mobile, Work = entity.Phone.Work },
                       ZipCode = entity.ZipCode,
                       Social = new SocialModel { Facebook = entity.Social.Facebook, Twitter = entity.Social.Twitter }
                   };
        }

        public Domain.Contact ConvertToSource(ContactModel model)
        {
            return new Domain.Contact
                   {
                       Address = model.Address1,
                       Address1 = model.Address2,
                       Phone = new Phone {Home = model.Phone.Home, Mobile = model.Phone.Mobile, Work = model.Phone.Work},
                       ZipCode = model.ZipCode,
                       Social =
                           new Social
                           {
                               Facebook = model.Social.Facebook,
                               Twitter = model.Social.Twitter
                           }
                   };
        }
    }
}
