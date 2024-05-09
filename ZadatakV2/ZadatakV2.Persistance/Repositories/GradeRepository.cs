﻿using Microsoft.EntityFrameworkCore;
using ZadatakV2.Persistance.Abstractions;
using ZadatakV2.Persistance.Entities;
using ZadatakV2.WebApi;

namespace ZadatakV2.Persistance.Repositories
{
    public class GradeRepository : IGradeRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public GradeRepository(ApplicationDbContext dbContext)
            => _dbContext = dbContext;

        public async Task AddGradeAsync(Grade grade)
        {
            await _dbContext.Set<Grade>().AddAsync(grade);
            await _dbContext.SaveChangesAsync();            
        }

        public async Task<bool> DoesGradeExist(long studentId, long subjectId)
            => (await _dbContext.Set<Grade>().AnyAsync(grade => grade.StudentId == studentId && grade.SubjectId == subjectId));
    }
}
