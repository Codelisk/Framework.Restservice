using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Framework.Restservice.Repositories.Services.Providers
{
    public record DefaultRepositoryProvider(BaseDbContext PrintingContext, UserManager<UserDto> UserManager, IHttpContextAccessor HttpContextAccessor);
}
