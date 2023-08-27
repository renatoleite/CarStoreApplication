﻿using Domain.ValueObjects;

namespace Domain.Interfaces.Entity
{
    public interface IEntityFactory
    {
        Car NewCar(string brand, string model, int year, User createdBy, User updatedBy);

        User NewLoginUser(int id, string name);
    }
}