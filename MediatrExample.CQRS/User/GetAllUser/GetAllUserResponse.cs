﻿using MediatrExample.Shared.DataModels.User.GetAllUser;
using MediatrExample.Shared.RequestResponse;

namespace MediatrExample.CQRS.User.GetAllUser
{
    public class GetAllUserResponse : IResponse
    {
        public int TotalCount { get; set; }
        public IEnumerable<UserDataModel> UserList { get; set; }
    }
}