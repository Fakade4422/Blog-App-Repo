using Blog.Administrator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Blog.Administrator.Repository
{
    public interface IAdministratorRepository
    {

        Task<IEnumerable<Posts>> GetAllPosts();
        Task<IEnumerable<tblUser>> GetAllUsers();
        Task<IEnumerable<Category>> GetAllCategory();

        /*---- CRUD Operations ------*/
        Task<bool> AddUser (tblUser user);
        Task<bool> UpdateUser (tblUser user);
        Task<bool> DeleteUser (int id);

        Task<bool> AddCategory(Category category);
        Task<bool> UpdateCategory(Category category);
        Task<bool> DeleteCategory(int id);

        Task<bool> AddPost(Category_Posts  category_Posts);
        Task<bool> UpdatePost(Category_Posts category_Posts);
        Task<bool> DeletePost(int id);

        /*---- GET BY ID OPERATIONS -------*/
        Task<tblUser> GetUserById(int id);
        Task<Category> GetCategoryById(int id);
        Task<Posts> GetPostsById(int id);

        /*----Authentication For User -------*/
        Task<bool> ValidateEmail(string emailAddress);
        Task<bool> NewActiveUser(ActiveUser activeUser);
        Task<bool> LogOutActiveUser(ActiveUser activeUser);
    }
}
