using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BookLoan.Application.DTOs;
using BookLoan.Application.Interfaces;
using BookLoan.Domain.Entities;
using BookLoan.Domain.Interfaces;

namespace BookLoan.Application.Services
{
    public class UserService : IUserService
    {

        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<UserDTO>> FindAll()
        {
            IEnumerable<User> users = await _userRepository.FindAll();
            IEnumerable<UserDTO> userDTOs = _mapper.Map<IEnumerable<UserDTO>>(users);
            return userDTOs;

        }

        public async Task<UserDTO> FindById(int id)
        {
            User user = await _userRepository.FindById(id);
            UserDTO userDTO = _mapper.Map<UserDTO>(user);
            return userDTO;
        }

        public async Task<UserDTO> Insert(UserDTO userDTO)
        {

            User user = _mapper.Map<User>(userDTO);

            if (userDTO != null)
            {
                using var hmac = new HMACSHA256();
                byte[] passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(userDTO.Password));
                byte[] passwordSalt = hmac.Key;

                user.ChangePassword(passwordHash, passwordSalt);
            }

            User userInserted = await _userRepository.Insert(user);
            return _mapper.Map<UserDTO>(userInserted);

        }

        public async Task<UserDTO> Update(UserDTO userDTO)
        {
            User user = _mapper.Map<User>(userDTO);
            User userChanged = await _userRepository.Update(user);
            return _mapper.Map<UserDTO>(userChanged);
        }

        public async Task<UserDTO> Delete(int id)
        {
            User userDeleted = await _userRepository.Delete(id);
            return _mapper.Map<UserDTO>(userDeleted);
        }
    }
}
