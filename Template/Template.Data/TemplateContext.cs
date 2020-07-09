using Microsoft.EntityFrameworkCore;

namespace Template.Data
{
    public class TemplateContext : DbContext
    {
        public TemplateContext(DbContextOptions<TemplateContext> options) : base(options)
        {

        }
    }
}
