using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using VinarishMvc.Areas.Identity.Models;

namespace VinarishMvc.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<VinarishUser> _userManager;
        private readonly SignInManager<VinarishUser> _signInManager;

        public IndexModel(
            UserManager<VinarishUser> userManager,
            SignInManager<VinarishUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public bool IsEmailConfirmed { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Display(Name = "نام کاربری")]
            public string Username { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Phone]
            [Display(Name = "شماره‌ی همراه")]
            public string PhoneNumber { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            VinarishUser user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            string userName = await _userManager.GetUserNameAsync(user);
            string email = await _userManager.GetEmailAsync(user);
            string phoneNumber = await _userManager.GetPhoneNumberAsync(user);

            Input = new InputModel
            {
                Username = userName,
                Email = email,
                PhoneNumber = phoneNumber
            };

            IsEmailConfirmed = await _userManager.IsEmailConfirmedAsync(user);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            VinarishUser user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            string username = await _userManager.GetUserNameAsync(user);
            if (Input.Username != username)
            {
                IdentityResult setUsernameResult = await _userManager.SetUserNameAsync(user, Input.Username);
                if (!setUsernameResult.Succeeded)
                {
                    string userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting Username for user with ID '{userId}'.");
                }
            }

            string email = await _userManager.GetEmailAsync(user);
            if (Input.Email != email)
            {
                IdentityResult setEmailResult = await _userManager.SetEmailAsync(user, Input.Email);
                if (!setEmailResult.Succeeded)
                {
                    string userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting email for user with ID '{userId}'.");
                }
            }

            string phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                IdentityResult setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    string userId = await _userManager.GetUserIdAsync(user);
                    throw new InvalidOperationException($"Unexpected error occurred setting phone number for user with ID '{userId}'.");
                }
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "پرونده به‌روزرسانی شد.";
            return RedirectToPage();
        }
    }
}