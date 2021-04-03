using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LMSTRYONE.Models;
using LMSTRYONE.ViewModel;
using System.Security.Cryptography;
using System.Web.Security;
using System.Diagnostics;
using System.Data.Entity.Validation;

namespace LMSTRYONE.Services
{
    public class AuthenticationService
    {
        public void Register(RegisterViewModel reg)
        {
            try
            {
                using (LMSEntities db = new LMSEntities())
                {
                    Register register = new Register();
                    //Creating a new Salt value for the the user which can be used to hash the password
                    reg.Salt = CreateSalt();
                    //An Hashed password is created using the previously created salt value
                    //This password is saved in the database;
                    register.Password = CreatePasswordHash(reg.Password, reg.Salt);
                    register.EmployeeId = reg.EmployeeId;
                    register.Salt = reg.Salt;
                    //Debug.WriteLine(reg.Salt);
                    //Debug.WriteLine(reg.Password);
                    //Debug.WriteLine(reg.EmployeeId);
                    db.Registers.Add(register);
                    db.SaveChanges();
                }
            }
            catch(Exception e)
            {
                throw e;
            }
        }
        public bool Login(LoginViewModel user)
        {
            try
            {
                using(LMSEntities db=new LMSEntities())
                {
                    Register reg = db.Registers.Find(user.EmployeeId);
                    // Password entered by the user is hashed with the Salt in the database and then compared
                    if (reg.Password == CreatePasswordHash(user.Password, reg.Salt))
                    {
                        return true;
                    }
                    else return false;
                }

            }catch (Exception ex)
            {
                throw ex;
            }
        }

        //This method is used to check if the users is registerd to the application
        public bool UserCheck(int id)
        {
            try
            {
                using (LMSEntities db = new LMSEntities())
                {
                    Register reg = db.Registers.Find(id);
                    if (reg!=null)
                    {
                        return true;
                    }
                    else return false;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // The Salt is the unique value which is used to Hash the password
        private static string CreateSalt()
        {
            RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();
            byte[] byteArr = new byte[64];
            rng.GetBytes(byteArr);

            return Convert.ToBase64String(byteArr);
        }

        private static string CreatePasswordHash(string password, string salt)
        {
            string passwrodSalt = String.Concat(password, salt);
            string hashedPwd = FormsAuthentication.HashPasswordForStoringInConfigFile(passwrodSalt, "sha1");
            return hashedPwd;
        }
    }
}