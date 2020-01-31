using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using Worldbuilder.Model.BaseClasses;

namespace Worldbuilder.Helpers
{
    public static class SaveChanges
    {
        public static async Task<bool> SavedSuccessfully<T>(DbContext _context, DbSet<T> itemSet, T itemToSave) where T : IdName
        {
            _context.Attach(itemToSave).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!itemSet.Any(e => e.Id == itemToSave.Id))
                {
                    return false;
                }
                else
                {
                    throw;
                }

            }
            return true;
        }
    }
}
