using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace oig_assessment.Application.DTOs.User;
public class UserDto
{
    public Guid Id { get; set; }
    public string LoginName { get; set; } = string.Empty;
    public string Role { get; set; } = string.Empty;

    public DateTime CreatedOn { get; set; }

}
