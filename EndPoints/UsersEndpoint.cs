using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SmartAdmin.WebUI.Data;
using SmartAdmin.WebUI.Extensions;
using SmartAdmin.WebUI.Models;
using SmartAdmin.WebUI.ViewModels;

namespace SmartAdmin.WebUI.EndPoints
{
    [ApiController]
    [Route("api/users")]
    public class UsersEndpoint : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<ApplicationUser> _manager;
        private readonly SmartSettings _settings;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly IConfiguration configuration;
        public UsersEndpoint(ApplicationDbContext context
            , UserManager<ApplicationUser> manager
            , SmartSettings settings
            , SignInManager<ApplicationUser> signInManager
            , IMapper mapper
            , IConfiguration configuration
            )
        {
            _mapper = mapper;
            this.configuration = configuration;
            _context = context;
            _manager = manager;
            _settings = settings;
            _signInManager = signInManager;
        }

        [HttpGet]
        [Route("get-user-by-clientid/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<UserViewModel>>> Get(long? id)
        {
            id=id==0 ? null : id;
            var users = await _context.Users.AsNoTracking().Where(c=>c.ClientId==id).ToListAsync();

            return Ok(new { data =_mapper.Map<List<UserViewModel>>(users) , recordsTotal = users.Count, recordsFiltered = users.Count });
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<UserViewModel> Get([FromRoute] Guid id) => Ok(_mapper.Map<UserViewModel>(_context.Users.Find(id)));

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> Create([FromForm] UserViewModel model)
        {
            try
            {

                model.Id = Guid.NewGuid();
                model.PasswordHash = Encrypt(model.PasswordHash);
                model.UserName = model.Email;
                model.IsActive = true;
                model.CreatedOn = DateTime.Now;
                if (_context.Users.Any(u => u.Email == model.Email))
                {
                   
                    return BadRequest("Email Id already exist");
                }
                var user = _mapper.Map<User>(model);
                _context.Users.Add(user);
                var result  = await _context.SaveChangesAsync();
               

                if (result>0)
                {
                
                        return CreatedAtAction("Get", new { id = model.Id }, model);
                }
                else
                {
                    return BadRequest("Unable to create user");
                }
               
            }
            catch (Exception)
            {

                throw;
            }
           
        }

        
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Update([FromForm] UserViewModel model)
        {
            try
            {
                var user = _mapper.Map<UserViewModel>( _context.Users.Where(m=>m.Id==model.Id).AsNoTracking().FirstOrDefault());
                user.FirstName = model.FirstName;
                user.LastName = model.LastName;
                user.Email = model.Email;
                user.Gender = model.Gender;
                user.IsActive = model.IsActive;
                user.ModifiedOn = DateTime.Now;
                user.PhoneNumber = model.PhoneNumber;
                var mappeduser = _mapper.Map<User>(user);
                _context.Entry(mappeduser).State = EntityState.Modified;
                var result = _context.SaveChanges();
                if (result>0)
                {
                    return NoContent();
                }
            }
            catch (Exception ex)
            {

                throw;
            }


            return BadRequest();
        }

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult Delete([FromForm] UserViewModel model)
        {
            // HACK: The code below is just for demonstration purposes!
            // Please use a different method of preventing the currently logged in user from being removed
            var user = _mapper.Map<User>(model);

             _context.Users.Remove(user);
            var result = _context.SaveChanges();
            if (result>0)
            {
                return NoContent();
            }

            return BadRequest(result);
        }
        public string Encrypt(string plainText, string PublicKey = null)
        {
            if (PublicKey == null)
                PublicKey = Convert.ToString(configuration["AppSettings:PublicKey"]);

            string passPhrase = null;
            string saltValue = null;
            string hashAlgorithm = null;
            int passwordIterations = 0;
            string initVector = null;
            int keySize = 0;
            passPhrase = PublicKey;

            saltValue = "s@1tValue";
            // can be any string
            hashAlgorithm = "MD5";
            // can be "MD5"
            passwordIterations = 2;
            // can be any number
            initVector = "@1B2c3D4e5F6g7H8";
            // must be 16 bytes
            keySize = 256;
            // can be 192 or 128

            // Convert strings into byte arrays.
            // Let us assume that strings only contain ASCII codes.
            byte[] initVectorBytes = null;
            initVectorBytes = Encoding.ASCII.GetBytes(initVector);

            byte[] saltValueBytes = null;
            saltValueBytes = Encoding.ASCII.GetBytes(saltValue);

            // Convert our plaintext into a byte array.
            // Let us assume that plaintext contains UTF8-encoded characters.
            byte[] plainTextBytes = null;
            plainTextBytes = Encoding.UTF8.GetBytes(plainText);

            // First, we must create a password, from which the key will be derived.
            // This password will be generated from the specified passphrase and 
            // salt value. The password will be created using the specified hash 
            // algorithm. Password creation can be done in several iterations.
            PasswordDeriveBytes password = default(PasswordDeriveBytes);
            password = new PasswordDeriveBytes(passPhrase, saltValueBytes, hashAlgorithm, passwordIterations);

            // Use the password to generate pseudo-random bytes for the encryption
            // key. Specify the size of the key in bytes (instead of bits).
            byte[] keyBytes = null;
            keyBytes = password.GetBytes(keySize / 8);

            // Create uninitialized Rijndael encryption object.
            RijndaelManaged symmetricKey = default(RijndaelManaged);
            symmetricKey = new RijndaelManaged();

            symmetricKey.Padding = PaddingMode.ANSIX923;
            // It is reasonable to set encryption mode to Cipher Block Chaining
            // (CBC). Use default options for other symmetric key parameters.
            symmetricKey.Mode = CipherMode.CBC;

            // Generate encryptor from the existing key bytes and initialization 
            // vector. Key size will be defined based on the number of the key 
            // bytes.
            ICryptoTransform encryptor = default(ICryptoTransform);
            encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);

            // Define memory stream which will be used to hold encrypted data.
            MemoryStream memoryStream = default(MemoryStream);
            memoryStream = new MemoryStream();

            // Define cryptographic stream (always use Write mode for encryption).
            CryptoStream cryptoStream = default(CryptoStream);
            cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            // Start encrypting.
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);

            // Finish encrypting.
            cryptoStream.FlushFinalBlock();

            // Convert our encrypted data from a memory stream into a byte array.
            byte[] cipherTextBytes = null;
            cipherTextBytes = memoryStream.ToArray();

            // Close both streams.
            memoryStream.Close();
            cryptoStream.Close();

            // Convert encrypted data into a base64-encoded string.
            string cipherText = null;
            cipherText = Convert.ToBase64String(cipherTextBytes);
            cipherText = cipherText.Replace("/", "0269");
            // Return encrypted string.
            return cipherText;
        }
    }
    
}
