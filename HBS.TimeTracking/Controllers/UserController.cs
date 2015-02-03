using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using HBS.Data.Entities.TimeTracking.Models;
using HBS.Data.Entities.TimeTracking.Models;
using HBS.Data.Entities.TimeTracking.ViewModels;

namespace HBS.TimeTracking.Controllers
{
    //[Authorize]
    public class UserController : Controller
    {
        //
        // GET: /User/

        //
        // GET: /Account/Register

        //[Authorize(Roles = "SuperAdmin, Admin")]
        public ActionResult Create()
        {
            var model = new CreateUserRoleViewModel();
            return View(model);
        }

        //
        // POST: /Account/Register

        [HttpPost]
        public ActionResult Create(CreateUserRoleViewModel userRoleModel)
        {
            var model = userRoleModel.UserModel;
            var selectedRoles = userRoleModel.Roles;

            if (ModelState.IsValid)
            {
                // Attempt to register the user
                MembershipCreateStatus createStatus;
                var user = MembershipUserExtended.CreateUser(model.UserName, model.Password, model.Email, model.FirstName, model.LastName, model.Title, model.HourlyRate, model.Address, model.City, model.State, model.Zip, model.Phone, out createStatus);

                if (createStatus == MembershipCreateStatus.Success)
                {
                    //Assign Users to Roles
                    if (selectedRoles.Any())
                        Roles.AddUserToRoles(user.UserName, selectedRoles.ToArray());
                    else
                        Roles.AddUserToRole(user.UserName, Roles.GetAllRoles().FirstOrDefault(c => c.ToLower().Equals("user")));


                    userRoleModel.UserModel = new CreateUserModel(user);
                    userRoleModel.Roles = Roles.GetAllRoles().ToList();
                    //FormsAuthentication.SetAuthCookie(model.UserName, createPersistentCookie: false);
                    //return RedirectToAction("Index", "TimeTrack");
                    ModelState.AddModelError("", string.Format("User {0} created successfully",model.UserName));
                    return View(userRoleModel);
                }
                else
                {
                    userRoleModel.Roles = Roles.GetAllRoles().ToList();
                    ModelState.AddModelError("", ErrorCodeToString(createStatus));
                }
            }
            else
            {
                userRoleModel.Roles = Roles.GetAllRoles().ToList();
            }

            // If we got this far, something failed, redisplay form
            return View(userRoleModel);
        }

        public ActionResult List()
        {
            var membershipUserCollection = MembershipUserExtended.GetExtendedMembershipUserCollection();
            var userModelCollection = new List<UserModel>();

            userModelCollection.AddRange(membershipUserCollection.Select(mu => new UserModel(mu)));


            return View(userModelCollection);
        }

        public ActionResult Detail(string id)
        {
            var model = new UserRoleViewModel(userName: id);

            if (model.UserModel.UserName != null)
                return View(model);

            return RedirectToAction("Create");

        }

        public ActionResult Edit(string id)
        {
            var model = new UserRoleViewModel(userName:id);

            if (model.UserModel.UserName != null)
                return View(model);
            
            return RedirectToAction("Create");
            
        }

        [HttpPost]
        public ActionResult Edit(UserRoleViewModel userRoleModel)
        {
            var model = userRoleModel.UserModel;
            var selectedRoles = userRoleModel.Roles;

            if (ModelState.IsValid)
            {
                try
                {
                    if (selectedRoles.Any())
                    {
                        var mUser = Membership.GetUser(model.UserName);
                        if (mUser != null)
                        {
                            mUser.Email = model.Email;

                            MembershipUserExtended.Update(mUser, model.FirstName, model.LastName,
                                                          model.Title, model.HourlyRate, model.Address, model.City,
                                                          model.State, model.Zip, model.Phone);
                        }
                        else
                        {
                            userRoleModel.Roles = Roles.GetAllRoles().ToList();
                            ModelState.AddModelError("", "Couldn't find user");
                            return View(userRoleModel);
                        }
                        //delete all saved roles before adding new ones
                        var userSavedRoles = Roles.GetRolesForUser(model.UserName);
                        if (userSavedRoles.Any())
                            Roles.RemoveUserFromRoles(model.UserName, userSavedRoles);

                        Roles.AddUserToRoles(model.UserName, selectedRoles.ToArray());

                        var user = MembershipUserExtended.GetUser(model.UserName, false);

                        userRoleModel.UserModel = new UserModel(user);
                        userRoleModel.Roles = Roles.GetAllRoles().ToList();
                        ViewBag.Message = "Changes have been saved successfully";
                    }
                    else
                    {
                        userRoleModel.Roles = Roles.GetAllRoles().ToList();
                        ModelState.AddModelError("", "User must belong to atleast one Role.");
                    }

                }
                catch (Exception ex)
                {
                    ViewBag.Message = string.Empty;
                    ModelState.AddModelError("", ex.Message);
                }
            }
            else
            {
                userRoleModel.Roles = Roles.GetAllRoles().ToList();
            }
            // If we got this far, something failed, redisplay form
            return View(userRoleModel);
        }

        private IEnumerable<string> GetErrorsFromModelState()
        {
            return ModelState.SelectMany(x => x.Value.Errors.Select(error => error.ErrorMessage));
        }

        #region Status Codes

        private static string ErrorCodeToString(MembershipCreateStatus createStatus)
        {
            // See http://go.microsoft.com/fwlink/?LinkID=177550 for
            // a full list of status codes.
            switch (createStatus)
            {
                case MembershipCreateStatus.DuplicateUserName:
                    return "User name already exists. Please enter a different user name.";

                case MembershipCreateStatus.DuplicateEmail:
                    return "A user name for that e-mail address already exists. Please enter a different e-mail address.";

                case MembershipCreateStatus.InvalidPassword:
                    return "The password provided is invalid. Please enter a valid password value.";

                case MembershipCreateStatus.InvalidEmail:
                    return "The e-mail address provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidAnswer:
                    return "The password retrieval answer provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidQuestion:
                    return "The password retrieval question provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.InvalidUserName:
                    return "The user name provided is invalid. Please check the value and try again.";

                case MembershipCreateStatus.ProviderError:
                    return "The authentication provider returned an error. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                case MembershipCreateStatus.UserRejected:
                    return "The user creation request has been canceled. Please verify your entry and try again. If the problem persists, please contact your system administrator.";

                default:
                    return "An unknown error occurred. Please verify your entry and try again. If the problem persists, please contact your system administrator.";
            }
        }

        #endregion
    }
}
