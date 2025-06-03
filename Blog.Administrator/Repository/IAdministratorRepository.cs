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

        Task<bool> AddCategory(Category category);
        Task<bool> UpdateCategory(Category category);

        Task<bool> AddPost(Category_Posts  category_Posts);

        /*---- GET BY ID OPERATIONS -------*/
        Task<tblUser> GetUserById(int id);
        Task<Category> GetCategoryById(int id);
    }
}
