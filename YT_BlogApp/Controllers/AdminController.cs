﻿using Blog.Administrator.Models;
using Blog.Administrator.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;


namespace YT_BlogApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly IAdministratorRepository _adminRepo;
        public AdminController(IAdministratorRepository adminRepo)
        {
            _adminRepo = adminRepo;
        }

        public IActionResult Landing()
        {
            return View();
        }

        public async Task<IActionResult> ManagePosts()
        {
            IEnumerable<Posts> viewAllPosts = await _adminRepo.GetAllPosts();   ////--- First option to list anything in a table ----////
            return View(viewAllPosts);
        }
        public async Task<ActionResult> ManageUser()
        {
            var viewAllUser = await _adminRepo.GetAllUsers();  ////--- 2nd option to list anything in a table ----////
            return View(viewAllUser);
        }
        public async Task<IActionResult> ManageCategory()
        {
            var viewAllCategory = await _adminRepo.GetAllCategory();
            return View(viewAllCategory);
        }

        /*---- CRUD OPERATIONS ------*/

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddPost(Category_Posts categoryposts)
        {
            try
            {
                // Check if the Posts object exists
                if (categoryposts.Posts == null)
                {
                    // Handle the null case (return error view, etc.)
                    return View("Error");
                }

                byte[] filedata = null;
                if (categoryposts.Posts.File != null && categoryposts.Posts.File.Length != 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        await categoryposts.Posts.File.CopyToAsync(ms);
                        filedata = ms.ToArray();
                    }

                    categoryposts.Posts.Thumbnail = filedata;
                }

                
                ///gets the selected category description from the selected ID and populates it///////
                var selectedCategory = (await _adminRepo.GetAllCategory())
                           .FirstOrDefault(c => c.CategoryID == categoryposts.Posts.CategoryID);
                categoryposts.Posts.CategoryTitle = selectedCategory.CategoryTitle;
                //////////////////////////////////////////////////////////////////////////////////////////////

                if (ModelState.IsValid)
                {
                    bool addPost = await _adminRepo.AddPost(categoryposts);
                    TempData["AddPostSuccessMsg"] = "Add post was successfull";
                    return RedirectToAction(nameof(ManagePosts));
                }

                // Rest of your logic to save the post...
                // Make sure to return something at the end
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["AddPostSuccessMsg"] = "Add post was not successfull";
                // Log the exception
                return View("Error");
            }

        }
        public async Task<ActionResult> AddPost()
        {
            Category_Posts category_Posts = new Category_Posts()
            {
                Posts = new Posts(),
                Categories = await _adminRepo.GetAllCategory()
            };
            return View(category_Posts);
        }

        public async Task<ActionResult> EditPost(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            // Get the record information of the selected post, 
            var postResult = await _adminRepo.GetPostsById(id); 

            if (postResult == null)
            {
                return NotFound();
            }

            // Create and populate the full Category_Posts view model
            ////using this method because in the view we use this type of 2_dimensional model.
            ///Allows us to accomodate every object in it  
            var viewModel = new Category_Posts 
            {
                Posts = postResult, ////This will be the final population of the post object and it's needed information
                Categories = await _adminRepo.GetAllCategory() //This allows us to populate the combo box
            };

            return View(viewModel);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditPost(Category_Posts category_Posts)
        {
            try
            {
                byte[] filedata = null;
                if (category_Posts.Posts.File != null && category_Posts.Posts.File.Length != 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        await category_Posts.Posts.File.CopyToAsync(ms);
                        filedata = ms.ToArray();
                    }

                    category_Posts.Posts.Thumbnail = filedata;
                }

                if (ModelState.IsValid)
                {
                    bool updatePost = await _adminRepo.UpdatePost(category_Posts);
                    TempData["PostUpdatedMessageSuccess"] = "Post Record Successfully Updated";
                    return RedirectToAction(nameof(ManagePosts));

                }

            }
            catch (Exception ex)
            {
                return View(category_Posts);
            }

            return View(category_Posts);

        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddUser(tblUser user)
        {/*-----Adds the User's record's even the picture as well--------*/
            try
            {
                byte[] filedata = null;
                if (user.File != null && user.File.Length != 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        await user.File.CopyToAsync(ms);
                        filedata = ms.ToArray();
                    }

                    user.Picture = filedata;
                }


                if (ModelState.IsValid)
                {
                    bool addUser = await _adminRepo.AddUser(user);
                    TempData["AddUserSuccess"] = "The New User has been added successfully";
                    return RedirectToAction(nameof(ManageUser));
                }

               
            }
            catch (Exception ex)
            {
                TempData["AddUserMsg"] = "Adding User was unsuccessfull";
            }
            return View(user);

        }
        public async Task<ActionResult> AddUser()
        {
            return View();
        }

        
        public async Task<ActionResult> EditUser(int id)/*---Edit the user, get's the user's ID-----*/
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            tblUser user = await _adminRepo.GetUserById(id);
            
            if (user == null)
            {
                return NotFound();
            }


            //---Second Method------
            //var user = await _adminRepo.GetUserById(id);
            //if (user == null)
            //{
            //    return NotFound();
            //}

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditUser(tblUser user)
        {
            try
            {
                byte[] filedata = null;
                if (user.File != null && user.File.Length != 0)
                {
                    using (var ms = new MemoryStream())
                    {
                        await user.File.CopyToAsync(ms);
                        filedata = ms.ToArray();
                    }

                    user.Picture = filedata;
                }

                if (ModelState.IsValid)
                {
                    bool updateUser = await _adminRepo.UpdateUser(user);
                    TempData["UserUpdatedMessageSuccess"] = "User Record Successfully Edited";
                    return RedirectToAction(nameof(ManageUser));

                }

            }
            catch (Exception ex)
            {
                return View(user);
            }

            return View(user);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> AddCategory(Category category)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    bool addCategory = await _adminRepo.AddCategory(category);
                    if (addCategory)
                    {
                        TempData["CategoryAddedSuccess"] = "Category has been added successfully";
                        return RedirectToAction(nameof(ManageCategory));
                    }
                    else
                    {
                        ModelState.AddModelError("", "Failed to add category. Please try again.");
                    }

                }
                catch (Exception ex)
                {
                    TempData["AddCategoryMsg"] = "Adding Category was unsuccessfull";
                }
            }

            return View(category);
        }
        public async Task<ActionResult> AddCategory()
        {
            return View();
        }
        public async Task<ActionResult> EditCategory(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            Category category = await _adminRepo.GetCategoryById(id);

            if (category == null)
            {
                return NotFound();
            }
            //---Second Method------
            //var user = await _adminRepo.GetUserById(id);
            //if (user == null)
            //{
            //    return NotFound();
            //}

            return View(category);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> EditCategory(Category category)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool updateCategory = await _adminRepo.UpdateCategory(category);
                    TempData["CategoryUpdatedMessageSuccess"] = "Category Record Successfully Edited";
                    return RedirectToAction(nameof(ManageCategory));

                }
            }
            catch (Exception ex)
            {
                return View(category);
            }

            return View(category);
        }
    }
}
