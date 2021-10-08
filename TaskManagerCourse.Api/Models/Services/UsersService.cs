using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskManagerCourse.Api.Models.Abstractions;
using TaskManagerCourse.Api.Models.Data;
using TaskManagerCourse.Common.Models;

namespace TaskManagerCourse.Api.Models.Services
{
    public class UsersService : AbstractionService, ICommonService<UserModel>
    {
        private readonly ApplicationContext _db;
        public UsersService(ApplicationContext db)
        {
            _db = db;
        }

        public Tuple<string, string> GetUserLoginPassFromBasicAuth(HttpRequest request)
        {
            string userName = "";
            string userPass = "";
            string authHeader = request.Headers["Authorization"].ToString();
            if(authHeader != null && authHeader.StartsWith("Basic"))
            {
                string encodedUserNamePass = authHeader.Replace("Basic ", "");
                var encoding = Encoding.GetEncoding("iso-8859-1");

                string[] namePassArray = encoding.GetString(Convert.FromBase64String(encodedUserNamePass)).Split(':');
                userName = namePassArray[0];
                userPass = namePassArray[1];
            }
            return new Tuple<string, string>(userName, userPass);
        }

        public User GetUser(string login, string password)
        {
            var user = _db.Users.FirstOrDefault(u => u.Email == login && u.Password == password);
            return user;
        }
        public User GetUser(string login)
        {
            var user = _db.Users.FirstOrDefault(u => u.Email == login);
            return user;
        }
        public ClaimsIdentity GetIdentity(string username, string password)
        {
            User currentUser = GetUser(username, password);
            if (currentUser != null)
            {
                currentUser.LastLoginDate = DateTime.Now;
                _db.Users.Update(currentUser);
                _db.SaveChanges();

                var claims = new List<Claim>
                {
                    new Claim(ClaimsIdentity.DefaultNameClaimType, currentUser.Email),
                    new Claim(ClaimsIdentity.DefaultRoleClaimType, currentUser.Status.ToString())
                };
                ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);
                return claimsIdentity;
            }
            return null;
        }

        public bool Create(UserModel model)
        {
            try
            {
                User newUser = new User(model.FirstName, model.LastName, model.Email,
                        model.Password, model.Status, model.Phone, model.Photo);
                _db.Users.Add(newUser);
                _db.SaveChanges();
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }

        public bool Update(int id, UserModel model)
        {            
            return DoAction(delegate ()
                {
                    User userForUpdate = _db.Users.FirstOrDefault(u => u.Id == id);
                    userForUpdate.FirstName = model.FirstName;
                    userForUpdate.LastName = model.LastName;
                    userForUpdate.Password = model.Password;
                    userForUpdate.Phone = model.Phone;
                    userForUpdate.Photo = model.Photo;
                    userForUpdate.Status = model.Status;
                    userForUpdate.Email = model.Email;

                    _db.Users.Update(userForUpdate);
                    _db.SaveChanges();
                });
        }

        public bool Delete(int id)
        {
            User user = _db.Users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                return DoAction(delegate ()
              {
                  _db.Users.Remove(user);
                  _db.SaveChanges();
              });
            }
            return false;
        }

        public bool CreateMultipleUsers(List<UserModel> userModels)
        {
            
            return DoAction(delegate()
                {
                    var newUsers = userModels.Select(u => new User(u));
                    _db.Users.AddRange(newUsers);
                    _db.SaveChanges();
                });
        }

        public UserModel Get(int id)
        {
            User user = _db.Users.FirstOrDefault(u => u.Id == id);
            return user?.ToDto();
        }

        public IEnumerable<UserModel> GetAllByIds(List<int> usersIds)
        {
            foreach(int id in usersIds)
            {
                var user = _db.Users.FirstOrDefault(u => u.Id == id).ToDto();
                yield return user;
            }
        }
    }
}
