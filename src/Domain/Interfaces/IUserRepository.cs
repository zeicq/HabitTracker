﻿using Domain.Entity;

namespace Domain.Interfaces;

public interface IUserRepository
{
    Task UpdateAsync(User user);
}