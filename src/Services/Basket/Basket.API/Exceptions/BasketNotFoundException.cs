﻿namespace Basket.API.Exceptions
{
    public class BasketNotFoundException : NotFoundException
    {
        public BasketNotFoundException(Guid userId) : base("basket", userId)
        {
        }
    }
}
