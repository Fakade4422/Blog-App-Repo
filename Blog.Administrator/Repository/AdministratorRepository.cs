﻿using Blog.Administrator.Models;
using Blog_DatabaseAccess.SqlDataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
/*---Authentication-----*/
using Blog.Auth.Model;
using Blog_DatabaseAccess.SqlDataAccess;
using System.Net.Mail;

namespace Blog.Administrator.Repository
{
    public class AdministratorRepository:IAdministratorRepository
    {
        
        private readonly ISqlDataAccess _db;
        public AdministratorRepository(ISqlDataAccess db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Posts>> GetAllPosts()
        {
            try
            {
                IEnumerable<Posts> allposts = await _db.GetData<Posts, dynamic>("sp_ManagePosts", new { });
                return allposts;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<tblUser>> GetAllUsers()
        {
            try
            {
                IEnumerable<tblUser> allUsers = await _db.GetData<tblUser, dynamic>("sp_ManageUser", new { });
                return allUsers;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public async Task<IEnumerable<Category>> GetAllCategory()
        {
            try
            {
                IEnumerable<Category> allCategory = await _db.GetData<Category, dynamic>("sp_ManageCategory", new { });
                return allCategory;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        /*------- GET BY ID Opps --------*/
        public async Task<tblUser> GetUserById(int id)
        {
            IEnumerable<tblUser> result = await _db.GetData<tblUser, dynamic>("sp_GetUserByID", new { UserID = id });
            return result.FirstOrDefault();
        }

        public async Task<Category> GetCategoryById(int id)
        {
            IEnumerable<Category> result = await _db.GetData<Category, dynamic>("sp_GetCategoryById", new { CategoryID = id });
            return result.FirstOrDefault();
        }

        public async Task<Posts> GetPostsById(int id)
        {
            //IEnumerable<Category_Posts> result = await _db.GetData<Category_Posts, dynamic>("sp_GetPostById", new { PostID = id });
            //return result.FirstOrDefault();
            var result = await _db.GetData<Posts, dynamic>("sp_GetPostById", new { PostID = id });
            return result.FirstOrDefault();
        }



        /*-----------CRUD OPERATIONS--------------------*/

        public async Task<bool> AddUser(tblUser user)
        {
            ///----Adding a new record for a new user  ------/////
            try
            {
                await _db.SaveData("sp_ADMINAddUSER", new
                {
                    user.Name,
                    user.Surname,
                    user.Gender,
                    user.Email,
                    user.Role,
                    user.Username,
                    user.Picture,
                    user.DOB,
                    user.Password
                    
                });
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }

        }


        public async Task<bool> UpdateUser(tblUser user)
        {
            ///----Editing the record of a user  ------/////
            try
            {
                await _db.SaveData("sp_ADMIN_UPDATE_USER", new
                {
                    user.UserID,
                    user.Name,
                    user.Surname,
                    user.Gender,
                    user.Email,
                    user.Role,
                    user.Username,
                    user.Picture,
                    user.DOB,
                    user.Password

                });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteUser(int id)
        {
            try
            {
                await _db.SaveData("sp_DeleteUser", new { UserID = id });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<bool> AddCategory(Category category)
        {
            try
            {
                await _db.SaveData("sp_AddCategory", new
                {
                    category.CategoryTitle,
                    category.CategoryDescription

                });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        public async Task<bool> UpdateCategory(Category category)
        {
            try
            {
                await _db.SaveData("sp_UpdateCategory", new
                {
                    category.CategoryID,
                    category.CategoryDescription,
                    category.CategoryTitle

                });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeleteCategory(int id)
        {
            try
            {
                await _db.SaveData("sp_DeleteCategory", new { CategoryID = id });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }


        }


        public async Task<bool> AddPost(Category_Posts category_Posts)
        {
            try
            {
                await _db.SaveData("sp_AddPOST", new
                {
                    category_Posts.Posts.PostTitle,
                    category_Posts.Posts.PostBody,
                    category_Posts.Posts.Featured,
                    category_Posts.Posts.Thumbnail,
                    category_Posts.Posts.CategoryID
                });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        public async Task<bool> UpdatePost(Category_Posts category_Posts)
        {
            try
            {
                await _db.SaveData("sp_UPDATE_Post", new
                {
                    category_Posts.Posts.PostID,
                    category_Posts.Posts.PostTitle,
                    category_Posts.Posts.PostBody,
                    category_Posts.Posts.Thumbnail,
                    category_Posts.Posts.Featured,
                    category_Posts.Posts.CategoryID

                });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<bool> DeletePost(int id)
        {
            try
            {
                await _db.SaveData("sp_DeletePost", new { PostID = id });
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }

        }

        /*-----Authentication for User -----------*/
        public async Task<bool> ValidateEmail(string emailAddress)
        {
            try
            {
                IEnumerable<User> user = await _db.GetData<User, dynamic>("sp_ValidateEmailAddress", new { Email = emailAddress });
                if (user.Count() > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception ex)
            {

                return false;
            }
        }


        public async Task<bool> NewActiveUser(ActiveUser activeUser)
        {
            try
            {
                await _db.SaveData("sp_NewActiveUser", new
                {
                    activeUser.UserID,
                    activeUser.TimeLoggedIn,
                    activeUser.DayLoggedIn
                });
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }

        public async Task<bool> LogOutActiveUser(ActiveUser activeUser)
        {
            try
            {
                await _db.SaveData("sp_LogOutActiveUser", new
                {
                    activeUser.UserID,
                    activeUser.TimeLoggedOut,

                });
                return true;
            }
            catch (Exception ex)
            {

                return false;
            }
        }


    }
}
