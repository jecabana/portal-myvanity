using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using MyVanity.Domain;
using MyVanity.Domain.UoW;

namespace MyVanity.Services.Membership
{
    public class MembershipService: IMembershipService
    {
        private readonly UserManager<ApplicationUser> _usersManager;
        private readonly RoleManager<IdentityRole> _rolesManager; 
        private readonly IMembershipStore _membershipStore;
        private readonly IUnitOfWork _unitOfWork;

        public MembershipService(IMembershipStore membershipStore, IUnitOfWork unitOfWork)
        {
            _membershipStore = membershipStore;
            _unitOfWork = unitOfWork;

            _usersManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(membershipStore.MembershipDbContext));
            _usersManager.UserValidator = new UserValidator<ApplicationUser>(_usersManager) { AllowOnlyAlphanumericUserNames = false };
            _rolesManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(membershipStore.MembershipDbContext));

            foreach (ApplicationRole role in Enum.GetValues(typeof(ApplicationRole)))
                if (!_rolesManager.RoleExists(role.ToString()))
                    _rolesManager.Create(new IdentityRole(role.ToString()));
        }

        public Task<ApplicationUser> FindAsync(string userName, string password)
        {
            return _usersManager.FindAsync(userName, password);
        }

        public ApplicationUser FindByName(string userName)
        {
            return _usersManager.FindByName(userName);
        }

        public Task<ApplicationUser> FindAsync(UserLoginInfo loginInfo)
        {
            return _usersManager.FindAsync(loginInfo);
        }

        public Task<IdentityResult> RemoveLoginAsync(string userId, UserLoginInfo loginInfo)
        { return _usersManager.RemoveLoginAsync(userId, loginInfo); }

        public Task<IdentityResult> ChangePasswordAsync(string userId, string oldPassword, string newPassword)
        {
            return _usersManager.ChangePasswordAsync(userId, oldPassword, newPassword);
        }

        public Task<IdentityResult> AddPasswordAsync(string userId, string newPassword)
        {
            return _usersManager.AddPasswordAsync(userId, newPassword);
        }

        public Task<IdentityResult> AddLoginAsync(string userId, UserLoginInfo loginInfo)
        {
            return _usersManager.AddLoginAsync(userId, loginInfo);
        }

        public IList<UserLoginInfo> GetLogins(string userId)
        {
            return _usersManager.GetLogins(userId);
        }

        public Task<ClaimsIdentity> CreateIdentityAsync(ApplicationUser user, string authenticationType)
        {
            return _usersManager.CreateIdentityAsync(user, authenticationType);
        }

        public ApplicationUser FindById(string userId)
        {
            return _usersManager.FindById(userId);
        }

        public void Dispose()
        {
            _usersManager.Dispose();
            _rolesManager.Dispose();
        }

        public async Task<IdentityResult> CreateAsync<TUser>(TUser user, IEnumerable<ApplicationRole> roles, string password) where TUser : User
        {
           _unitOfWork.GetUserRepository<TUser>().Insert(user);
            var appUser = new ApplicationUser(user.UserName);

            try
            {
                _unitOfWork.SaveChanges();

                appUser.SetOwnerId(user.Id);
                var result = await _usersManager.CreateAsync(appUser, password);

                if (result.Succeeded)
                {
                    foreach (var role in roles)
                        await _usersManager.AddToRoleAsync(appUser.Id, role.ToString());
                }
                else
                {
                    _unitOfWork.GetUserRepository<TUser>().Delete(user);
                    _unitOfWork.SaveChanges();
                }

                return result;
            }
            catch (DbEntityValidationException ex)
            {
                throw ex;
            }
        }

        public async Task<bool> RemoveAsync<TUser>(string userName) where TUser : User
        {
            var user = await _usersManager.FindByNameAsync(userName);
            var repository = _unitOfWork.GetUserRepository<TUser>();

            try
            {
                var dbUser = repository.FindByName(userName);
                repository.Delete(dbUser);
                _unitOfWork.SaveChanges();

                //remove from membership store
                await _membershipStore.DeleteAsync(user);
                return true;
            }
            catch (Exception)
            {
                return false;
            }            
        }

        public async Task<bool> IsInRole(ApplicationUser user, ApplicationRole role)
        {
            var roles = await _usersManager.GetRolesAsync(user.Id);
            return roles.Contains(role.ToString());
        }
    }
}