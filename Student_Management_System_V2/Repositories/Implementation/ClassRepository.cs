using Microsoft.EntityFrameworkCore;
using Student_Management_System_V2.Data;
using Student_Management_System_V2.DTOs.Class;
using Student_Management_System_V2.Models.Core;
using Student_Management_System_V2.Models.Profiles;
using Student_Management_System_V2.Repositories.Interfaces;

namespace Student_Management_System_V2.Repositories.Implementation
{
    public class ClassRepository : IClassRepository
    {
        private readonly ApplicationDbContext _context;

        public ClassRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Class> CreateNewClassAsync(CreateClassDto dto)
        {
            var _class = new Class
            {
                Name = dto.Name,
            };
            await _context.Classes.AddAsync(_class);
            await _context.SaveChangesAsync();
            return _class ;
        }

        public async Task<bool> DeleteClassAsync(int id)
        {
            var del = await _context.Classes.FirstOrDefaultAsync(x => x.Id == id);
            if (del == null) return false;
            _context.Classes.Remove(del);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<List<Class>> GetAllClassesAsync()
        {
            return await _context.Classes.ToListAsync();
        }

        public async Task<Class> GetClassAsync(int id)
        {
            return await _context.Classes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Class> UpdateClassAsync(int id, Class _class)
        {
            var ClassFromId = await _context.Classes
                                                   .FirstOrDefaultAsync(s => s.Id == id);
            if (ClassFromId == null)
                return null;

            ClassFromId.Name = _class.Name;

            await _context.SaveChangesAsync();
            return ClassFromId;
        }
    }
}
