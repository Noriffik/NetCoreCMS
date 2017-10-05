﻿using System;
using System.Collections.Generic;
using System.Linq;
using NetCoreCMS.Framework.Core.Mvc.Models;
using NetCoreCMS.Framework.Core.Mvc.Services;
using NetCoreCMS.Modules.StudentBoardResult.Repository;
using NetCoreCMS.Modules.StudentBoardResult.Models;
using Microsoft.EntityFrameworkCore;

namespace NetCoreCMS.Modules.StudentBoardResult.Services
{
    public class SbrStudentResultService : IBaseService<SbrStudentResult>
    {
        private readonly SbrStudentResultRepository _entityRepository;

        public SbrStudentResultService(SbrStudentResultRepository entityRepository)
        {
            _entityRepository = entityRepository;
        }

        public SbrStudentResult Get(long entityId, bool isAsNoTracking = false)
        {
            return _entityRepository.Get(entityId);
        }

        public SbrStudentResult Get(long studentId, long examId)
        {
            return _entityRepository.Get(studentId, examId);
        }

        public SbrStudentResult Save(SbrStudentResult entity)
        {
            _entityRepository.Add(entity);
            _entityRepository.SaveChange();
            return entity;
        }

        public SbrStudentResult Update(SbrStudentResult entity)
        {
            var oldEntity = _entityRepository.Query().FirstOrDefault(x => x.Id == entity.Id);
            if (oldEntity != null)
            {
                using (var txn = _entityRepository.BeginTransaction())
                {
                    CopyNewData(oldEntity, entity);
                    _entityRepository.Edit(oldEntity);
                    _entityRepository.SaveChange();
                    txn.Commit();
                }
            }

            return entity;
        }

        public void Remove(long entityId)
        {
            var entity = _entityRepository.Query().FirstOrDefault(x => x.Id == entityId);
            if (entity != null)
            {
                entity.Status = EntityStatus.Deleted;
                _entityRepository.Edit(entity);
                _entityRepository.SaveChange();
            }
        }

        public List<SbrStudentResult> LoadAll(bool isActive = true, int status = -1, string name = "", bool isLikeSearch = false)
        {
            return _entityRepository.LoadAll(isActive, status, name, isLikeSearch);
        }

        public void DeletePermanently(long entityId)
        {
            var entity = _entityRepository.Query().FirstOrDefault(x => x.Id == entityId);
            if (entity != null)
            {
                _entityRepository.Remove(entity);
                _entityRepository.SaveChange();
            }
        }

        private void CopyNewData(SbrStudentResult oldEntity, SbrStudentResult entity)
        {
            oldEntity.ModificationDate = entity.ModificationDate;
            oldEntity.ModifyBy = entity.ModifyBy;
            oldEntity.Name = entity.Name;
            oldEntity.Status = entity.Status;

            oldEntity.RollNo = entity.RollNo;
            oldEntity.RegistrationNo = entity.RegistrationNo;
            oldEntity.Year = entity.Year;
            oldEntity.Gpa = entity.Gpa;
            oldEntity.GpaWithout4th = entity.GpaWithout4th;
            oldEntity.Grade = entity.Grade;

            oldEntity.Exam = entity.Exam;
            oldEntity.Board = entity.Board;
            oldEntity.Group = entity.Group;
            oldEntity.Student = entity.Student;
        }
    }
}