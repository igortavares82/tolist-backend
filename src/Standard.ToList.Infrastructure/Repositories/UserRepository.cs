﻿using System.Threading.Tasks;
using MediatR;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Standard.ToList.Model.Aggregates.Users;
using Standard.ToList.Model.Options;

namespace Standard.ToList.Infrastructure.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
		public UserRepository(IOptions<AppSettingOptions> settings, IMediator mediator) : base(settings, mediator)
		{
		}

        public async Task<bool> CanRegisterAsync(string email)
        {
            var builder = Builders<User>.Filter;
            var filter = builder.Eq(it => it.Email, email);
            var count = await Collection.CountDocumentsAsync(filter);

            return count == 0;
        }
    }
}

