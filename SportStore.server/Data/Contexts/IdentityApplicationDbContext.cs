using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SportStore.server.Data.Models;

namespace SportStore.server.Data.Contexts;

public class IdentityApplicationDbContext(DbContextOptions<IdentityApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options);