using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Template.BusinessService.Services.Abstract;
using Template.DAL.Repositories.Core;
using Template.DTO.DTOLibrary;
using Template.Entities.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Template.BusinessService.Services
{
    public class TemplateService : ITemplateService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public TemplateService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this._unitOfWork = unitOfWork;
            this._mapper = mapper;
        }
    }
}
