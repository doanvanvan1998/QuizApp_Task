using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using QuizApp_Task.DTO;
using QuizApp_Task.Entities;
using QuizApp_Task.Model;

namespace QuizApp_Task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly RoleManager<RoleEntity> _roleManager;

        public UserController(UserManager<UserEntity> userManager, RoleManager<RoleEntity> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> GetUsers()
        {
            var users = _userManager.Users.Select(user => new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                Avatar = user.Avatar,
                IsActive = user.IsActive
            });

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserDto>> GetUserById(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }

            var userDto = new UserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                DateOfBirth = user.DateOfBirth,
                Avatar = user.Avatar,
                IsActive = user.IsActive
            };

            return Ok(userDto);
        }

        [HttpPost]
        public async Task<ActionResult> CreateUser([FromBody] CreateUserModel createUserDto)
        {
            var user = new UserEntity
            {
                UserName = createUserDto.Email,
                Email = createUserDto.Email,
                FirstName = createUserDto.FirstName,
                LastName = createUserDto.LastName,
                DateOfBirth = createUserDto.DateOfBirth,
                Avatar = createUserDto.Avatar,
                IsActive = true
            };

            var result = await _userManager.CreateAsync(user, createUserDto.Password);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            foreach (var roleId in createUserDto.RoleIds)
            {
                var role = await _roleManager.FindByIdAsync(roleId.ToString());

                if (role != null)
                {
                    UserEntity userEntity = await _userManager.FindByEmailAsync(createUserDto.Email);
                    if (userEntity != null)
                    {
                        await _userManager.AddToRoleAsync(userEntity, role.Name);
                    }
                    else
                    {
                        throw new Exception("Failed to retrieve the admin user after creation.");
                    }
                }
                else
                {
                    // Xử lý khi không tìm thấy vai trò
                    return NotFound($"Role with ID {roleId} not found");
                }
            }

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserModel updateUserDto)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }

            // Cập nhật thông tin cơ bản của người dùng
            user.FirstName = updateUserDto.FirstName;
            user.LastName = updateUserDto.LastName;
            user.Avatar = updateUserDto.Avatar;
            user.IsActive = updateUserDto.IsActive;

            var updateResult = await _userManager.UpdateAsync(user);
            if (!updateResult.Succeeded)
            {
                return BadRequest(updateResult.Errors);
            }

            // Cập nhật vai trò của người dùng
            // Xóa toàn bộ vai trò hiện tại của người dùng
            var currentRoles = await _userManager.GetRolesAsync(user);
            var removeRolesResult = await _userManager.RemoveFromRolesAsync(user, currentRoles);
            if (!removeRolesResult.Succeeded)
            {
                return BadRequest(removeRolesResult.Errors);
            }

            // Thêm các vai trò mới vào người dùng
            foreach (var roleId in updateUserDto.RoleIds)
            {
                var roleIdAsString = roleId.ToString(); // Chuyển đổi Guid sang string
                var role = await _roleManager.FindByIdAsync(roleIdAsString);
                if (role != null)
                {
                    var addToRoleResult = await _userManager.AddToRoleAsync(user, role.Name);
                    if (!addToRoleResult.Succeeded)
                    {
                        // Xử lý lỗi khi không thể thêm vai trò cho người dùng
                        return BadRequest(addToRoleResult.Errors);
                    }
                }
                else
                {
                    // Xử lý khi không tìm thấy vai trò
                    return NotFound($"Role with ID {roleIdAsString} not found");
                }
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest(result.Errors);
            }

            return NoContent();
        }
    }
}
