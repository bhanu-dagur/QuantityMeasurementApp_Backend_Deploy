using QuantityMeasurementAppModelLayer.DTO;
using QuantityMeasurementAppRepositoryLayer.Interface;
using QuantityMeasurementAppModelLayer.Entity;
using Microsoft.AspNetCore.Identity;
using System.Text.RegularExpressions;
using QuantityMeasurementAppBusinessLayer.Exceptions;
namespace QuantityMeasurementAppBusinessLayer.Interface;
public class AuthService : IAuthService
{
    private readonly IUserRepository  _userRepository;
    private readonly IJwtService _jwtService;
    private readonly PasswordHasher<User> _passwordHasser;


    public AuthService(IJwtService jwtService, IUserRepository _userRepository)
    {
        _jwtService = jwtService;
        this._userRepository = _userRepository;
        _passwordHasser = new PasswordHasher<User>();

    }

    private bool ValidateInfo(string email, string password, string fullName,string phone)
    {
        if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(password) ||
            string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(phone) ||
            phone.Length != 10)
        {
            return false;
        }

        string emialRegex=@"^[a-zA-Z0-9_.+-]+@[a-zA-Z0-9-]+\.[a-zA-Z0-9-.]+$";
        if (!Regex.IsMatch(email, emialRegex))
        {
            return false;
        }

        return true;
    }

    public Task<bool> Register(RegisterDTO user)
    {
        var fullName = user.FullName;
        var email = user.Email;
        var password = user.Password;
        var phone = user.Phone;

        // if(!ValidateInfo(email, password, fullName, phone))
        // {
        //     return Task.FromResult(false);
        // }

        User userobj = new User();
        userobj.FullName = fullName;
        userobj.Email = email;
        userobj.Phone = user.Phone;

        //Hash the password
        string hashPassword= _passwordHasser.HashPassword(userobj,password);

        userobj.Password = hashPassword;  
        
        Console.WriteLine("Save User Mehtod business layer");
        Task<bool> result = _userRepository.SaveUser(userobj);
        return result;
    }

    public async Task<string> Login(LoginDTO user)
    {

        User? loggedUser = await _userRepository.VerifyUser(user.Email);

        if (loggedUser != null && 
            _passwordHasser.VerifyHashedPassword(loggedUser, loggedUser.Password, user.Password) == PasswordVerificationResult.Success)
        {
            return  _jwtService.GenerateToken(loggedUser);
        }
    
        throw new PasswordMismatchException("Password not matched");
    }
}
