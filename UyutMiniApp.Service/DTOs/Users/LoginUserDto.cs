using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UyutMiniApp.Service.DTOs.Users
{
    public class LoginUserDto
    {
        public string PhoneNumber { get; set; }
        public long TelegramUserId { get; set; }
    }
}
