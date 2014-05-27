using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using MyVanity.Common.Autofac;
using MyVanity.Domain;

namespace MyVanity.Services.Membership
{
    public interface IMembershipService : IPerRequestDependency, IDisposable
    {
        ApplicationUser FindById(string userId);

        ApplicationUser FindByName(string userName);

        Task<ApplicationUser> FindAsync(UserLoginInfo loginInfo);

        Task<ApplicationUser> FindAsync(string userName, string password);
        
        Task<IdentityResult> RemoveLoginAsync(string userId, UserLoginInfo loginInfo);
        
        Task<IdentityResult> ChangePasswordAsync(string userId, string oldPassword, string newPassword);
        
        Task<IdentityResult> AddPasswordAsync(string userId, string newPassword);
        
        Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo loginInfo);
        
        IList<UserLoginInfo> GetLogins(string userId);
        
        Task<ClaimsIdentity> CreateIdentityAsync(ApplicationUser user, string authenticationType);
        
        Task<IdentityResult> CreateAsync<TUser>(TUser user, IEnumerable<ApplicationRole> role, string password) where TUser : User; 
        
        Task<bool> RemoveAsync<TUser>(string userName) where TUser : User;
       
        Task<bool> IsInRole(ApplicationUser user, ApplicationRole role);
    }
}
