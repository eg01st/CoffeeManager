using System;
using System.Collections.Generic;

namespace CoffeeManager.Models
{
    public static class TypesLists
    {
        public static List<Entity> CupTypesList => new List<Entity>()
        {
            new Entity { Name = "Нету", Id = (int)CupTypeEnum.Unknown},
            new Entity { Name = "110", Id = (int)CupTypeEnum.c110},
            new Entity { Name = "170", Id = (int)CupTypeEnum.c170},
            new Entity { Name = "250", Id = (int)CupTypeEnum.c250},
            new Entity { Name = "400", Id = (int)CupTypeEnum.c400},
            new Entity { Name = "Пластик", Id = (int)CupTypeEnum.Plastic},
            new Entity { Name = "500", Id = (int)CupTypeEnum.c500},
        };

    }
}
